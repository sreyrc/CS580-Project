using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class StudentBT : Tree
    {
        public Transform[] waypoints;
        public static float walkSpeed = 2f;
        public static float runSpeed = 5.35f;
        //public static float fovRange = 6f;

        public static WorldState _idealWorldState;

        public static Dictionary<WorldStateVariables, float> _worldStateVariableWeights;

        protected override Node SetupTree()
        {
            _idealWorldState = new WorldState();

            // the world state this agent wants to achieve
            _idealWorldState.SetWorldState(WorldStateVariables.KIDATCAFE, WorldStateVarValues.TRUE);
            _idealWorldState.SetWorldState(WorldStateVariables.KIDATCLASSROOM, WorldStateVarValues.FALSE);
            _idealWorldState.SetWorldState(WorldStateVariables.KIDBEATENUP, WorldStateVarValues.FALSE);

            // weights or importance of each world state variable for this agent
            _worldStateVariableWeights = new Dictionary<WorldStateVariables, float>();
            _worldStateVariableWeights.Add(WorldStateVariables.KIDATCAFE, 2.0f);
            _worldStateVariableWeights.Add(WorldStateVariables.KIDATCLASSROOM, 1.0f);
            _worldStateVariableWeights.Add(WorldStateVariables.KIDBEATENUP, 3.0f);


            // Setup Student BT
            Node root = new Sequencer(new List<Node>
            {
                new Selector(new List<Node>
                {
                    new KidRunAwayFromBully(transform)
                }),
                //new RunToRandomPos(transform),
            });

            return root;
        }
    }
}