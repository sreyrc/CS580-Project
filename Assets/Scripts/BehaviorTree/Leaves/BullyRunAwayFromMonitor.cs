using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BullyRunAwayFromMonitor : Node
    {
        private Transform _transform;
        private float _time;
        private static int _bullyLayerMask = 1 << 6;

        // Animation
        private Animator _animator;
        private int _animIDSpeed;
        private int _animIDMotionSpeed;
        private float _animationBlend;
        private float SpeedChangeRate = 10.0f;

        public BullyRunAwayFromMonitor(Transform transform)
        {
            _transform = transform;
            _time = 2f;

            _animator = transform.GetComponent<Animator>();
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        public override float Simulate(WorldState idealWorldState, WorldStateWeights weights)
        {
            float cost = 0f;

            Tree._currentWorldState.SetWorldState(WorldStateVariables.MONITORATBULLYPOS, WorldStateVarValues.FALSE);

            foreach (KeyValuePair<WorldStateVariables, WorldStateVarValues> entry in idealWorldState.GetWorldStateDS())
            {
                if (entry.Value != WorldStateVarValues.DONTCARE)
                {
                    // Diff(currentWorldState[key], idealWorldState[key]) * wt[key] + ..... 
                    cost += Mathf.Abs(entry.Value - Tree._currentWorldState.GetWorldState(entry.Key)) * weights.GetWorldStateWeight(entry.Key);
                }
            }

            return cost;
        }

        public override NodeState Evaluate()
        {
            Collider[] colliders = Physics.OverlapSphere(
                _transform.position,
                6f,
                _bullyLayerMask);

            if (colliders.Length > 0)
            {
                Transform monitorTransform = colliders[0].transform;

                if (_time <= 0f)
                {
                    _animationBlend = Mathf.Lerp(_animationBlend, StudentBT.runSpeed, Time.deltaTime * SpeedChangeRate);

                    if (monitorTransform != null)
                    {
                        Vector3 runDirection = (_transform.position - monitorTransform.position).normalized;
                        Vector3 runTargetPos = _transform.position + 50f * runDirection;

                        // Kid is far enough away from bully
                        if (Vector3.Distance(_transform.position, monitorTransform.position) > 8f)
                        {
                            _time = 2f;

                            _animator.SetFloat(_animIDSpeed, 0f);
                            _animator.SetFloat(_animIDMotionSpeed, 0f);

                            Tree._currentWorldState.SetWorldState(WorldStateVariables.MONITORATBULLYPOS, WorldStateVarValues.FALSE);

                            state = NodeState.SUCCESS;
                            return state;
                        }
                        else
                        {
                            _transform.position = Vector3.MoveTowards(_transform.position, runTargetPos, StudentBT.runSpeed * Time.deltaTime);
                            _transform.LookAt(runTargetPos);

                            _animator.SetFloat(_animIDSpeed, _animationBlend);
                            _animator.SetFloat(_animIDMotionSpeed, 1f);

                            state = NodeState.RUNNING;
                            return state;
                        }
                    }
                    else
                    {
                        state = NodeState.FAILURE;
                        return state;
                    }
                }

                _time -= Time.deltaTime;

                state = NodeState.RUNNING;
                return state;
            }

            _time = 2f;
            state = NodeState.SUCCESS;
            return state;
        }
    }
}