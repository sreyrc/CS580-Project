using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
public class GuyBT : Tree
{
    public float speed = 2f;
    public UnityEngine.Vector3 _targetPosition;

    protected override Node SetupTree()
    {
        Node root = new Move(_targetPosition, gameObject.GetComponent<UnityEngine.CharacterController>()
            ,gameObject.GetComponent<UnityEngine.Transform>(), speed);
        return root;
    }
}
