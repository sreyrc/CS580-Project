using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class Move : Node
{
    private Vector3 _targetPosition;
    private Vector3 _targetDirection;
    private CharacterController _characterController;
    private Transform _transform;
    private float _speed;

    public Move(Vector3 targetPosition, CharacterController characterController, 
        Transform transform, float speed)
    {
        _targetPosition = targetPosition;
        _characterController = characterController;        
        _transform = transform;
        _speed = speed;
    }

    public override NodeState Evaluate()
    {
        _targetDirection = _targetPosition - _transform.position;
        _characterController.Move(_targetDirection.normalized * (_speed * Time.deltaTime));

        if(Vector3.Equals(_transform.position,_targetPosition))
        {
            _characterController.Move(new Vector3(0, 0, 0));
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.RUNNING;
        }
    }
}