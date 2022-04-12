using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class MonitorBT : Tree
    {
        public Transform[] waypoints;
        public static float walkSpeed = 0.5f;
        public static float runSpeed = 2.35f;
        public static float fovRange = 6f;

        public static WorldState _idealWorldState;

        public static Dictionary<WorldStateVariables, float> _worldStateVariableWeights;

        protected override Node SetupTree()
        {
            _idealWorldState = new WorldState();

            // the world state this agent wants to achieve
            _idealWorldState.SetWorldState(WorldStateVariables.BULLYSEENBYMONITOR, WorldStateVarValues.TRUE);
            _idealWorldState.SetWorldState(WorldStateVariables.MONITORATBULLYPOS, WorldStateVarValues.TRUE);
            _idealWorldState.SetWorldState(WorldStateVariables.KIDATCAFE, WorldStateVarValues.FALSE);

            // weights or importance of each world state variable for this agent
            _worldStateVariableWeights = new Dictionary<WorldStateVariables, float>();
            _worldStateVariableWeights.Add(WorldStateVariables.BULLYSEENBYMONITOR, 2.0f);
            _worldStateVariableWeights.Add(WorldStateVariables.MONITORATBULLYPOS, 1.0f);
            _worldStateVariableWeights.Add(WorldStateVariables.KIDATCAFE, 3.0f);

            // Setup Monitor BT
            //Node root = new Selector(new List<Node>
            //{
            //    new Sequencer(new List<Node>
            //    {
            //        new CheckBullyInFOVRange(transform),
            //        new RunToBully(transform),
            //    }),
            //    new Patrol(transform, waypoints),
            //});
            Node root = new Selector(new List<Node>
            {
                new CheckBullyInFOVRange(transform),
                new RunToBully(transform),
            });

            return root;
        }
    }
}