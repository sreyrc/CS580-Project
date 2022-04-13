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

        protected void Start()
        {
            _root = SetupTree();
            _currentWorldState  = new WorldState();

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
                if (_root.GetState() != NodeState.RUNNING)
                {
                    _root.Simulate();
                }
                _root.Evaluate();
            }
        }

        protected abstract Node SetupTree();
    }
}
