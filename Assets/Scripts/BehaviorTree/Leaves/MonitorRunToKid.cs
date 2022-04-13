using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class MonitorRunToKid : Node
    {
        private Transform _transform;

        // Animation
        private Animator _animator;
        private int _animIDSpeed;
        private int _animIDMotionSpeed;
        private float _animationBlend;
        private float SpeedChangeRate = 10.0f;

        public MonitorRunToKid(Transform transform)
        {
            _transform = transform;

            _animator = transform.GetComponent<Animator>();
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        public override float Simulate(WorldState idealWorldState, Dictionary<WorldStateVariables, float> weights)
        {
            float cost = 0f;

            Tree._currentWorldState.SetWorldState(WorldStateVariables.MONITORATKIDPOS, WorldStateVarValues.TRUE);

            foreach (KeyValuePair<WorldStateVariables, WorldStateVarValues> entry in idealWorldState.GetWorldStateDS())
            {
                if (entry.Value != WorldStateVarValues.DONTCARE)
                {
                    // Diff(currentWorldState[key], idealWorldState[key]) * wt[key] + ..... 
                    cost += Mathf.Abs(entry.Value - Tree._currentWorldState.GetWorldState(entry.Key)) * weights[entry.Key];
                }
            }

            return cost;
        }

        public override NodeState Evaluate()
        {
            Transform target = (Transform)GetData("student");

            _animationBlend = Mathf.Lerp(_animationBlend, MonitorBT.runSpeed, Time.deltaTime * SpeedChangeRate);

            if (target != null)
            {
                if (Vector3.Distance(_transform.position, target.position) > 0.5f)
                {
                    _transform.position = Vector3.MoveTowards(_transform.position, target.position, MonitorBT.runSpeed * Time.deltaTime);
                    _transform.LookAt(target.position);

                    _animator.SetFloat(_animIDSpeed, _animationBlend);
                    _animator.SetFloat(_animIDMotionSpeed, 1f);

                    state = NodeState.FAILURE;
                    return state;
                }
                else
                {
                    _animator.SetFloat(_animIDSpeed, 0f);
                    _animator.SetFloat(_animIDMotionSpeed, 0f);

                    Tree._currentWorldState.SetWorldState(WorldStateVariables.MONITORATKIDPOS, WorldStateVarValues.TRUE);

                    state = NodeState.SUCCESS;
                    return state;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}