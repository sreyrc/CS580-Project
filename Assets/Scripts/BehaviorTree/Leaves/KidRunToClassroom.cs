using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class KidRunToClassroom : Node
    {
        private Transform _transform;
        private Vector3 _target;
        WorldState _idealWorldState;
        WorldStateWeights _weights;

        // Animation
        private Animator _animator;
        private int _animIDSpeed;
        private int _animIDMotionSpeed;
        private float _animationBlend;
        private float SpeedChangeRate = 10.0f;

        public KidRunToClassroom(Transform transform)
        {
            _transform = transform;
            _target = new Vector3(15f, 0f, 4.5f);

            _animator = transform.GetComponent<Animator>();
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        public override float Simulate(WorldState idealWorldState, WorldStateWeights weights)
        {
            _idealWorldState = idealWorldState;
            _weights = weights;

            float cost = 0f;

            Tree._currentWorldState.SetWorldState(WorldStateVariables.KIDATCLASSROOM, WorldStateVarValues.TRUE);

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
            _animationBlend = Mathf.Lerp(_animationBlend, StudentBT.runSpeed, Time.deltaTime * SpeedChangeRate);

            if (Vector3.Distance(_transform.position, _target) > 0.5f)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, _target, StudentBT.runSpeed * Time.deltaTime);
                _transform.LookAt(_target);

                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, 1f);

                state = NodeState.FAILURE;
                return state;
            }
            else
            {
                _animator.SetFloat(_animIDSpeed, 0f);
                _animator.SetFloat(_animIDMotionSpeed, 0f);

                Tree._currentWorldState.SetWorldState(WorldStateVariables.KIDATCAFE, WorldStateVarValues.TRUE);

                _idealWorldState.SetWorldState(WorldStateVariables.KIDATCAFE, WorldStateVarValues.TRUE);
                _idealWorldState.SetWorldState(WorldStateVariables.KIDATCLASSROOM, WorldStateVarValues.FALSE);

                _weights.SetWorldStateWeights(WorldStateVariables.KIDATCAFE, 2f);
                _weights.SetWorldStateWeights(WorldStateVariables.KIDATCLASSROOM, 1f);

                state = NodeState.SUCCESS;
                return state;
            }
        }
    }
}