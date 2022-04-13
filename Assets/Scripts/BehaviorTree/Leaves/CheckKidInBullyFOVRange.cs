using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class CheckKidInBullyFOVRange : Node
    {
        private static int _kidLayerMask = 1 << 7;
        private Transform _transform;

        public CheckKidInBullyFOVRange(Transform transform)
        {
            _transform = transform;
        }

        public override float Simulate(WorldState idealWorldState, Dictionary<WorldStateVariables, float> weights)
        {
            float cost = 0f;

            Tree._currentWorldState.SetWorldState(WorldStateVariables.KIDSEENBYBULLY, WorldStateVarValues.TRUE);

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
            object t = GetData("student");
            if (t == null)
            {
                Collider[] colliders = Physics.OverlapSphere(
                    _transform.position,
                    BullyBT.fovRange,
                    _kidLayerMask);

                if (colliders.Length > 0)
                {
                    parent.SetData("student", colliders[0].transform);
                    Tree._currentWorldState.SetWorldState(WorldStateVariables.KIDSEENBYBULLY, WorldStateVarValues.TRUE);

                    state = NodeState.SUCCESS;
                    return state;
                }

                parent.SetData("student", null);
                Tree._currentWorldState.SetWorldState(WorldStateVariables.KIDSEENBYBULLY, WorldStateVarValues.FALSE);
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}