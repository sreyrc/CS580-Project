using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root = null;

        // stores global world variables and states - shared by all agents/trees
        public static WorldState _currentWorldState;

        public WorldState _idealWorldState;

        public Dictionary<WorldStateVariables, float> _worldStateVariableWeights;

        protected void Start()
        {
            _currentWorldState  = new WorldState();
            _idealWorldState = new WorldState();
            _worldStateVariableWeights = new Dictionary<WorldStateVariables, float>();
            _root = SetupTree();

            // Setup Initial world state
            _currentWorldState.SetWorldState(WorldStateVariables.BULLYATKIDPOS, WorldStateVarValues.FALSE);
            _currentWorldState.SetWorldState(WorldStateVariables.BULLYSEENBYMONITOR, WorldStateVarValues.FALSE);
            _currentWorldState.SetWorldState(WorldStateVariables.BULLYPUNISHED, WorldStateVarValues.FALSE);
            _currentWorldState.SetWorldState(WorldStateVariables.MONITORATBULLYPOS, WorldStateVarValues.FALSE);
            _currentWorldState.SetWorldState(WorldStateVariables.MONITORATKIDPOS, WorldStateVarValues.FALSE);
            _currentWorldState.SetWorldState(WorldStateVariables.KIDATCAFE, WorldStateVarValues.FALSE);
            _currentWorldState.SetWorldState(WorldStateVariables.KIDBEATENUP, WorldStateVarValues.FALSE);
            _currentWorldState.SetWorldState(WorldStateVariables.KIDATCLASSROOM, WorldStateVarValues.TRUE);
            _currentWorldState.SetWorldState(WorldStateVariables.KIDSEENBYMONITOR, WorldStateVarValues.FALSE);
            _currentWorldState.SetWorldState(WorldStateVariables.KIDSEENBYBULLY, WorldStateVarValues.FALSE);
        }

        private void Update()
        {
            if (_root != null)
            {
                //if (_root.GetState() != NodeState.RUNNING)
                //{
                //}
                _root.Simulate(_idealWorldState, _worldStateVariableWeights);
                _root.Evaluate();
            }
        }

        protected abstract Node SetupTree();
    }
}
