using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BullyRunToKid : Node
    {
        private Transform _transform;

        // Animation
        private Animator _animator;
        private int _animIDSpeed;
        private int _animIDMotionSpeed;
        private float _animationBlend;
        private float SpeedChangeRate = 10.0f;

        public BullyRunToKid(Transform transform)
        {
            _transform = transform;

            _animator = transform.GetComponent<Animator>();
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        public override float Simulate()
        {
            float cost = 0f;

            Tree._currentWorldState.SetWorldState(WorldStateVariables.BULLYATKIDPOS, WorldStateVarValues.TRUE);

            foreach (KeyValuePair<WorldStateVariables, WorldStateVarValues> entry in BullyBT._idealWorldState.GetWorldStateDS())
            {
                if (entry.Value != WorldStateVarValues.DONTCARE)
                {
                    // Diff(currentWorldState[key], idealWorldState[key]) * wt[key] + ..... 
                    cost += Mathf.Abs(entry.Value - Tree._currentWorldState.GetWorldState(entry.Key)) * BullyBT._worldStateVariableWeights[entry.Key];
                }
            }

            return cost;
        }

        public override NodeState Evaluate()
        {
            Transform target = (Transform)GetData("student");

            _animationBlend = Mathf.Lerp(_animationBlend, BullyBT.runSpeed, Time.deltaTime * SpeedChangeRate);

            if (target != null)
            {
                if (Vector3.Distance(_transform.position, target.position) > 0.5f)
                {
                    _transform.position = Vector3.MoveTowards(_transform.position, target.position, BullyBT.runSpeed * Time.deltaTime);
                    _transform.LookAt(target.position);

                    _animator.SetFloat(_animIDSpeed, _animationBlend);
                    _animator.SetFloat(_animIDMotionSpeed, 1f);
                }
                else
                {
                    _animator.SetFloat(_animIDSpeed, 0f);
                    _animator.SetFloat(_animIDMotionSpeed, 0f);

                    Tree._currentWorldState.SetWorldState(WorldStateVariables.BULLYATKIDPOS, WorldStateVarValues.TRUE);

                    state = NodeState.SUCCESS;
                    return state;
                }
            }

            state = NodeState.RUNNING;
            return state;
        }
    }
}