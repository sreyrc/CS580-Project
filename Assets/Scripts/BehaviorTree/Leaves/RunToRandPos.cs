using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class RunToRandomPos : Node
    {
        private Transform _transform;

        // Animation
        private Animator _animator;
        private int _animIDSpeed;
        private int _animIDMotionSpeed;
        private float _animationBlend;
        private float SpeedChangeRate = 10.0f;

        public RunToRandomPos(Transform transform)
        {
            _transform = transform;

            _animator = transform.GetComponent<Animator>();
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        public override float Simulate()
        {
            float cost = 0f;

            //foreach (KeyValuePair<WorldStateVariables, WorldStateVarValues> entry in MonitorBT._idealWorldState.GetWorldStateDS())
            //{
            //    if (entry.Value != WorldStateVarValues.DONTCARE)
            //    {
            //        // Diff(currentWorldState[key], idealWorldState[key]) * wt[key] + ..... 
            //        cost += Mathf.Abs(entry.Value - Tree._currentWorldState.GetWorldState(entry.Key)) * MonitorBT._worldStateVariableWeights[entry.Key];
            //    }
            //}

            return cost;
        }

        public override NodeState Evaluate()
        {
            // Find a random pos to head to 
            Vector3 targetPosition = new Vector3(Random.Range(-9, 9), 0, Random.Range(-9, 9));

            _animationBlend = Mathf.Lerp(_animationBlend, 5.35f, Time.deltaTime * SpeedChangeRate);

            if (Vector3.Distance(_transform.position, targetPosition) > 0.01f)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, 5.35f * Time.deltaTime);
                _transform.LookAt(targetPosition);

                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, 1f);
            }
            else
            {
                _animator.SetFloat(_animIDSpeed, 0f);
                _animator.SetFloat(_animIDMotionSpeed, 0f);

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.RUNNING;
            return state;
        }
    }
}