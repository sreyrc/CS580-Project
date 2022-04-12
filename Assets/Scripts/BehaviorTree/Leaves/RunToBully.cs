using System.Collections;
using UnityEngine;

namespace BehaviorTree
{
    public class RunToBully : Node
    {
        private Transform _transform;

        // Animation
        private Animator _animator;
        private int _animIDSpeed;
        private int _animIDMotionSpeed;
        private float _animationBlend;
        private float SpeedChangeRate = 10.0f;

        public RunToBully(Transform transform)
        {
            _transform = transform;

            _animator = transform.GetComponent<Animator>();
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        public override NodeState Evaluate()
        {
            Transform target = (Transform)GetData("bully");

            _animationBlend = Mathf.Lerp(_animationBlend, MonitorBT.runSpeed, Time.deltaTime * SpeedChangeRate);

            if (Vector3.Distance(_transform.position, target.position) > 0.01f)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, target.position, MonitorBT.runSpeed * Time.deltaTime);
                _transform.LookAt(target.position);

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