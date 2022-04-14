using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class MonitorBT : Tree
    {
        public static float walkSpeed = 1f;
        public static float runSpeed = 2.35f;
        public static float fovRange = 10f;

        protected override Node SetupTree()
        {
            // the world state this agent wants to achieve
            _idealWorldState.SetWorldState(WorldStateVariables.BULLYSEENBYMONITOR, WorldStateVarValues.TRUE);
            _idealWorldState.SetWorldState(WorldStateVariables.MONITORATBULLYPOS, WorldStateVarValues.TRUE);
            _idealWorldState.SetWorldState(WorldStateVariables.KIDSEENBYMONITOR, WorldStateVarValues.TRUE);
            _idealWorldState.SetWorldState(WorldStateVariables.MONITORATKIDPOS, WorldStateVarValues.TRUE);

            // weights or importance of each world state variable for this agent
            _worldStateVariableWeights.SetWorldStateWeights(WorldStateVariables.BULLYSEENBYMONITOR, 5.0f);
            _worldStateVariableWeights.SetWorldStateWeights(WorldStateVariables.MONITORATBULLYPOS, 4.0f);
            _worldStateVariableWeights.SetWorldStateWeights(WorldStateVariables.KIDSEENBYMONITOR, 3.0f);
            _worldStateVariableWeights.SetWorldStateWeights(WorldStateVariables.MONITORATKIDPOS, 2.0f);

            //_worldStateVariableWeights.SetWorldStateWeights(WorldStateVariables.BULLYSEENBYMONITOR, 3.0f);
            //_worldStateVariableWeights.SetWorldStateWeights(WorldStateVariables.MONITORATBULLYPOS, 2.0f);
            //_worldStateVariableWeights.SetWorldStateWeights(WorldStateVariables.KIDSEENBYMONITOR, 5.0f);
            //_worldStateVariableWeights.SetWorldStateWeights(WorldStateVariables.MONITORATKIDPOS, 4.0f);

            // Setup Monitor BT
            Node root = new Selector(new List<Node>
            {
                new Inverter(new List<Node>
                {
                    new Idle(transform, 2.0f),
                }),
                new SmartSelector(new List<Node>
                {
                    new CheckBullyInMonitorFOVRange(transform),
                    new MonitorRunToBully(transform),
                    new CheckKidInMonitorFOVRange(transform),
                    new MonitorRunToKid(transform),
                }),
            });

            return root;
        }
    }
}