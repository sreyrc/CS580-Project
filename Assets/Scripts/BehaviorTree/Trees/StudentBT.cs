using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class StudentBT : Tree
    {
        public Transform[] waypoints;
        public static float walkSpeed = 1f;
        public static float runSpeed = 2.35f;
        //public static float fovRange = 6f;

        protected override Node SetupTree()
        {
            // the world state this agent wants to achieve
            _idealWorldState.SetWorldState(WorldStateVariables.KIDATCAFE, WorldStateVarValues.TRUE);
            _idealWorldState.SetWorldState(WorldStateVariables.KIDATCLASSROOM, WorldStateVarValues.FALSE);
            _idealWorldState.SetWorldState(WorldStateVariables.KIDBEATENUP, WorldStateVarValues.FALSE);
            _idealWorldState.SetWorldState(WorldStateVariables.BULLYATKIDPOS, WorldStateVarValues.FALSE);

            // weights or importance of each world state variable for this agent
            _worldStateVariableWeights.Add(WorldStateVariables.KIDATCAFE, 2.0f);
            _worldStateVariableWeights.Add(WorldStateVariables.KIDATCLASSROOM, 1.0f);
            _worldStateVariableWeights.Add(WorldStateVariables.KIDBEATENUP, 3.0f);
            _worldStateVariableWeights.Add(WorldStateVariables.BULLYATKIDPOS, 3.0f);


            // Setup Student BT
            Node root = new Selector(new List<Node>
            {
                new SmartSelector(new List<Node>
                {
                    new KidRunToCafe(transform),
                    new KidRunToClassroom(transform),
                    new KidRunAwayFromBully(transform),
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