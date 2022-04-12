using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorldStateVariables
{
    BULLYATKIDPOS,
    KIDBEATENUP,
    BULLYSEENBYMONITOR,
    MONITORATBULLYPOS,
    KIDATCAFE,
    COUNT
}

public enum WorldStateVarValues
{
    FALSE = 0,
    TRUE,
    DONTCARE,
    COUNT
}

public class WorldState 
{
    private Dictionary<WorldStateVariables, WorldStateVarValues> _worldState = new Dictionary<WorldStateVariables, WorldStateVarValues>();
    public WorldState()
    {
        _worldState[WorldStateVariables.BULLYATKIDPOS] = WorldStateVarValues.DONTCARE;
        _worldState[WorldStateVariables.KIDBEATENUP] = WorldStateVarValues.DONTCARE;
        _worldState[WorldStateVariables.BULLYSEENBYMONITOR] = WorldStateVarValues.DONTCARE;
        _worldState[WorldStateVariables.MONITORATBULLYPOS] = WorldStateVarValues.DONTCARE;
        _worldState[WorldStateVariables.KIDATCAFE] = WorldStateVarValues.DONTCARE;
    }

    public WorldStateVarValues GetWorldState(WorldStateVariables key) {
        return _worldState[key];
    }

    public void SetWorldState(WorldStateVariables key, WorldStateVarValues value) {
        _worldState[key] = value;
    }

    public Dictionary<WorldStateVariables, WorldStateVarValues> GetWorldStateDS() {
        return _worldState;
    }
}
