using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BullyBT : Tree
    {
        public Transform[] waypoints;
        public static float walkSpeed = 1f;
        public static float runSpeed = 2.35f;
        public static float fovRange = 6f;

        protected override Node SetupTree()
        {
            // the world state this agent wants to achieve
            _idealWorldState.SetWorldState(WorldStateVariables.BULLYATKIDPOS, WorldStateVarValues.TRUE);
            _idealWorldState.SetWorldState(WorldStateVariables.KIDBEATENUP, WorldStateVarValues.TRUE);
            //_idealWorldState.SetWorldState(WorldStateVariables.BULLYPUNISHED, WorldStateVarValues.FALSE);
            _idealWorldState.SetWorldState(WorldStateVariables.KIDSEENBYBULLY, WorldStateVarValues.TRUE);

            // weights or importance of each world state variable for this agent
            _worldStateVariableWeights.Add(WorldStateVariables.BULLYATKIDPOS, 2.0f);
            _worldStateVariableWeights.Add(WorldStateVariables.KIDBEATENUP, 1.0f);
            //_worldStateVariableWeights.Add(WorldStateVariables.BULLYPUNISHED, 1.0f);
            _worldStateVariableWeights.Add(WorldStateVariables.KIDSEENBYBULLY, 3.0f);

            Node root = new Selector(new List<Node>
            {
                new SmartSelector(new List<Node>
                {
                    new CheckKidInBullyFOVRange(transform),
                    new BullyRunToKid(transform),
                    new BullyAttackKid(transform),
                }),
                new Wait(transform, 5.0f, new List<Node>
                {
                    new RunToRandomPos(transform),
                }),
            });

            return root;
        }
    }
}