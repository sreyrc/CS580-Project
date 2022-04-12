using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BullyBT : Tree
    {
        public Transform[] waypoints;
        public static float walkSpeed = 2f;
        public static float runSpeed = 5.35f;
        public static float fovRange = 6f;

        WorldState _idealWorldState;
        
        private Dictionary<WorldStateVariables, float> _worldStateVariableWeights;

        protected override Node SetupTree()
        {
            _idealWorldState = new WorldState();
            
            // the world state this agent wants to achieve
            _idealWorldState.SetWorldState(WorldStateVariables.BULLYATKIDPOS, WorldStateVarValues.TRUE);
            _idealWorldState.SetWorldState(WorldStateVariables.KIDBEATENUP, WorldStateVarValues.TRUE);
            _idealWorldState.SetWorldState(WorldStateVariables.BULLYSEENBYMONITOR, WorldStateVarValues.FALSE);

            // weights or importance of each world state variable for this agent
            _worldStateVariableWeights = new Dictionary<WorldStateVariables, float>();
            _worldStateVariableWeights.Add(WorldStateVariables.BULLYATKIDPOS, 2.0f);
            _worldStateVariableWeights.Add(WorldStateVariables.KIDBEATENUP, 1.0f);
            _worldStateVariableWeights.Add(WorldStateVariables.BULLYSEENBYMONITOR, 3.0f);

            Node root = new Selector(new List<Node>
            {
                new Patrol(transform, waypoints),
            });

            return root;
        }
    }
}