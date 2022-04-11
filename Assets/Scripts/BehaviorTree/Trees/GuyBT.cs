using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class GuyBT : Tree
    {
        public float speed = 2f;
        public Vector3 _targetPosition;

        // agent
        private CharacterController _characterController;

        // animation
        private Animator _animator;
        private bool _hasAnimator;

        protected override Node SetupTree()
        {
            _hasAnimator = gameObject.TryGetComponent(out _animator);
            _characterController = gameObject.GetComponent<CharacterController>();

            Node root;
            if (_hasAnimator)
            {
                _animator = gameObject.GetComponent<Animator>();
                root = new Move(_targetPosition, _characterController, gameObject.GetComponent<Transform>(), speed, _hasAnimator, _animator);
            }
            else
            {
                root = new Move(_targetPosition, _characterController, gameObject.GetComponent<Transform>(), speed, _hasAnimator);
            }
            return root;
        }
    }
}