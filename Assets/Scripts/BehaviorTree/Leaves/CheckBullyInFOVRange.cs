using System.Collections;
using UnityEngine;

namespace BehaviorTree
{
    public class CheckBullyInFOVRange : Node
    {
        private static int _bullyLayerMask = 1 << 6;
        private Transform _transform;

        public CheckBullyInFOVRange(Transform transform)
        {
            _transform = transform;
        }

        public override NodeState Evaluate()
        {
            object t = GetData("bully");
            if (t == null)
            {
                Collider[] colliders = Physics.OverlapSphere(
                    _transform.position,
                    MonitorBT.fovRange,
                    _bullyLayerMask);

                if (colliders.Length > 0)
                {
                    parent.parent.SetData("bully", colliders[0].transform);

                    state = NodeState.SUCCESS;
                    return state;
                }

                state = NodeState.FAILURE;
                return state;
            }

            state = NodeState.SUCCESS;
            return state;
        }
    }
}