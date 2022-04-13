using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Idle : Node
    {
        private float _delay;
        private float _time;

        // Animation
        private Animator _animator;
        private int _animIDSpeed;
        private int _animIDMotionSpeed;

        public Idle(Transform transform, float delay): base()
        {
            _delay = delay;
            _time = _delay;

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
            if (_time <= 0f)
            {
                _time = _delay;

                state = NodeState.SUCCESS;
                return state;
            }
            
            _time -= Time.deltaTime;

            _animator.SetFloat(_animIDSpeed, 0f);
            _animator.SetFloat(_animIDMotionSpeed, 0f);

            state = NodeState.RUNNING;
            return state;
        }
    }
}