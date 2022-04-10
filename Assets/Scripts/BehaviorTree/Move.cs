using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Move : Node
    {
        private Vector3 _targetPosition;
        private Vector3 _targetDirection;
        private CharacterController _characterController;
        private Transform _transform;
        private float _speed;
        private float _targetSpeed;
        private float SpeedChangeRate = 10.0f;

        // animation
        private Animator _animator;
        private bool _hasAnimator;
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;
        private float _animationBlend;

        public Move(Vector3 targetPosition, CharacterController characterController, Transform transform, float speed, bool hasAnimator, Animator animator = null)
        {
            _targetPosition = targetPosition;
            _characterController = characterController;
            _transform = transform;
            _targetSpeed = speed;
            _hasAnimator = hasAnimator;
            _animator = animator;

            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        public override NodeState Evaluate()
        {
            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_characterController.velocity.x, 0.0f, _characterController.velocity.z).magnitude;
            float speedOffset = 0.1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < _targetSpeed - speedOffset || currentHorizontalSpeed > _targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, _targetSpeed, Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = _targetSpeed;
            }
            _animationBlend = Mathf.Lerp(_animationBlend, _targetSpeed, Time.deltaTime * SpeedChangeRate);

            _targetDirection = _targetPosition - _transform.position;
            _characterController.Move(_targetDirection.normalized * (_speed * Time.deltaTime));

            //if (Vector3.Equals(_transform.position, _targetPosition))
            if ((_transform.position.x > _targetPosition.x - 0.1f && _transform.position.x < _targetPosition.x + 0.1f) &&
                (_transform.position.z > _targetPosition.z - 0.1f && _transform.position.z < _targetPosition.z + 0.1f))
            {
                _characterController.Move(new Vector3(0, 0, 0));

                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetFloat(_animIDSpeed, 0f);
                    _animator.SetFloat(_animIDMotionSpeed, 0f);
                }

                return NodeState.SUCCESS;
            }
            else
            {
                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetFloat(_animIDSpeed, _animationBlend);
                    _animator.SetFloat(_animIDMotionSpeed, 1f);
                }

                return NodeState.RUNNING;
            }
        }
    }
}