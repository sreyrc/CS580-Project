using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Wait : Node
    {
        private Transform _transform;
        private float _delay;
        private float _time;
        bool _childIsRunning;

        // Animation
        private Animator _animator;
        private int _animIDSpeed;
        private int _animIDMotionSpeed;

        public Wait(Transform transform, float delay, List<Node> children) : base(children)
        {
            _transform = transform;
            _delay = delay;
            _time = _delay;
            _childIsRunning = false;

            _animator = transform.GetComponent<Animator>();
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        public override float Simulate(WorldState idealWorldState, WorldStateWeights weights)
        {
            WorldState worldStateCopy = new WorldState(Tree._currentWorldState);

            float cost = 0f;

            cost += children[0].Simulate(idealWorldState, weights);

            Tree._currentWorldState.DeepCopy(worldStateCopy);

            return cost;
        }

        public override NodeState Evaluate()
        {
            if (_childIsRunning)
            {
                state = children[0].Evaluate();
            }
            else
            {
                if (_time <= 0f)
                {
                    _time = _delay;

                    state = children[0].Evaluate();
                }
                else
                {
                    _time -= Time.deltaTime;

                    _animator.SetFloat(_animIDSpeed, 0f);
                    _animator.SetFloat(_animIDMotionSpeed, 0f);

                    state = NodeState.RUNNING;
                    return state;
                }
            }

            if (state == NodeState.RUNNING)
            {
                _childIsRunning = true;
            }
            else
            {
                _childIsRunning = false;
            }

            return state;
        }
    }
}