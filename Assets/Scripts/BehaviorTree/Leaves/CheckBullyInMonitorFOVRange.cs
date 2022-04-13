using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class CheckBullyInMonitorFOVRange : Node
    {
        private static int _bullyLayerMask = 1 << 6;
        private Transform _transform;

        public CheckBullyInMonitorFOVRange(Transform transform)
        {
            _transform = transform;
        }

        public override float Simulate(WorldState idealWorldState, Dictionary<WorldStateVariables, float> weights)
        {
            float cost = 0f;

            Tree._currentWorldState.SetWorldState(WorldStateVariables.BULLYSEENBYMONITOR, WorldStateVarValues.TRUE);

            foreach (KeyValuePair<WorldStateVariables, WorldStateVarValues> entry in idealWorldState.GetWorldStateDS())
            {
                if (entry.Value != WorldStateVarValues.DONTCARE)
                {
                    // Diff(currentWorldState[key], idealWorldState[key]) * wt[key] + ..... 
                    cost += Mathf.Abs(entry.Value - Tree._currentWorldState.GetWorldState(entry.Key)) * weights[entry.Key];
                }
            }
            
            return cost;
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
                    parent.SetData("bully", colliders[0].transform);
                    Tree._currentWorldState.SetWorldState(WorldStateVariables.BULLYSEENBYMONITOR, WorldStateVarValues.TRUE);

                    state = NodeState.SUCCESS;
                    return state;
                }

                parent.SetData("bully", null);
                Tree._currentWorldState.SetWorldState(WorldStateVariables.BULLYSEENBYMONITOR, WorldStateVarValues.FALSE);
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}