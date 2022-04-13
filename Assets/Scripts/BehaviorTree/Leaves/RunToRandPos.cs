using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class RunToRandomPos : Node
    {
        private Transform _transform;
        private Vector3 _targetPosition;

        // Animation
        private Animator _animator;
        private int _animIDSpeed;
        private int _animIDMotionSpeed;
        private float _animationBlend;
        private float SpeedChangeRate = 10.0f;

        public RunToRandomPos(Transform transform)
        {
            _transform = transform;
            _targetPosition = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));

            _animator = transform.GetComponent<Animator>();
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        public override float Simulate(WorldState idealWorldState, WorldStateWeights weights)
        {
            float cost = 0f;

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
            _animationBlend = Mathf.Lerp(_animationBlend, 2.35f, Time.deltaTime * SpeedChangeRate);

            if (Vector3.Distance(_transform.position, _targetPosition) > 0.01f)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, _targetPosition, 2.35f * Time.deltaTime);
                _transform.LookAt(_targetPosition);

                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, 1f);
            }
            else
            {
                _targetPosition = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));

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