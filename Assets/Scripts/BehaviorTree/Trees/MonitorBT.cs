using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class MonitorBT : Tree
    {
        public Transform[] waypoints;
        public static float walkSpeed = 1f;
        public static float runSpeed = 2.35f;
        public static float fovRange = 6f;

        protected override Node SetupTree()
        {
            // the world state this agent wants to achieve
            _idealWorldState.SetWorldState(WorldStateVariables.BULLYSEENBYMONITOR, WorldStateVarValues.TRUE);
            _idealWorldState.SetWorldState(WorldStateVariables.MONITORATBULLYPOS, WorldStateVarValues.TRUE);
            _idealWorldState.SetWorldState(WorldStateVariables.KIDSEENBYMONITOR, WorldStateVarValues.TRUE);
            _idealWorldState.SetWorldState(WorldStateVariables.MONITORATKIDPOS, WorldStateVarValues.TRUE);
            _idealWorldState.SetWorldState(WorldStateVariables.BULLYPUNISHED, WorldStateVarValues.TRUE);

            // weights or importance of each world state variable for this agent
            _worldStateVariableWeights.Add(WorldStateVariables.BULLYSEENBYMONITOR, 5.0f);
            _worldStateVariableWeights.Add(WorldStateVariables.MONITORATBULLYPOS, 4.0f);
            _worldStateVariableWeights.Add(WorldStateVariables.KIDSEENBYMONITOR, 2.0f);
            _worldStateVariableWeights.Add(WorldStateVariables.MONITORATKIDPOS, 1.0f);
            _worldStateVariableWeights.Add(WorldStateVariables.BULLYPUNISHED, 3.0f);

            // Setup Monitor BT
            Node root = new Selector(new List<Node>
            {
                new SmartSelector(new List<Node>
                {
                    new CheckBullyInMonitorFOVRange(transform),
                    new MonitorRunToBully(transform),
                    new CheckKidInMonitorFOVRange(transform),
                    new MonitorRunToKid(transform),
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