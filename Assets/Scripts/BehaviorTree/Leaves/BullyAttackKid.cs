using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BullyAttackKid : Node
    {
        private Transform _transform;
        private float _time;

        // Animation
        private Animator _animator;
        private int _animIDSpeed;
        private int _animIDMotionSpeed;
        private int _animIDJump;

        public BullyAttackKid(Transform transform)
        {
            _transform = transform;
            _time = 2f;

            _animator = transform.GetComponent<Animator>();
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _animIDJump = Animator.StringToHash("Jump");
        }

        public override float Simulate(WorldState idealWorldState, WorldStateWeights weights)
        {
            float cost = 0f;

            Tree._currentWorldState.SetWorldState(WorldStateVariables.KIDBEATENUP, WorldStateVarValues.TRUE);

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
            if (_time <= 0f)
            {
                _time = 2f;

                Tree._currentWorldState.SetWorldState(WorldStateVariables.KIDBEATENUP, WorldStateVarValues.TRUE);

                //_animator.SetBool(_animIDJump, false);
                //_animator.SetFloat(_animIDSpeed, 0f);
                //_animator.SetFloat(_animIDMotionSpeed, 0f);

                state = NodeState.SUCCESS;
                return state;
            }

            //_animator.SetBool(_animIDJump, true);
            _animator.SetFloat(_animIDSpeed, 0f);
            _animator.SetFloat(_animIDMotionSpeed, 0f);

            _time -= Time.deltaTime;

            state = NodeState.RUNNING;
            return state;
        }
    }
}