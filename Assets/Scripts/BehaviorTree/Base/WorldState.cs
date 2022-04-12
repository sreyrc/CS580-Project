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
    private Dictionary<WorldStateVariables, WorldStateVarValues> _worldState;
    public WorldState()
    {
        _worldState = new Dictionary<WorldStateVariables, WorldStateVarValues>();
        _worldState[WorldStateVariables.BULLYATKIDPOS] = WorldStateVarValues.DONTCARE;
        _worldState[WorldStateVariables.KIDBEATENUP] = WorldStateVarValues.DONTCARE;
        _worldState[WorldStateVariables.BULLYSEENBYMONITOR] = WorldStateVarValues.DONTCARE;
        _worldState[WorldStateVariables.MONITORATBULLYPOS] = WorldStateVarValues.DONTCARE;
        _worldState[WorldStateVariables.KIDATCAFE] = WorldStateVarValues.DONTCARE;
    }

    public WorldState(WorldState ws)
    {
        _worldState = new Dictionary<WorldStateVariables, WorldStateVarValues>(ws._worldState);
    }

    public void DeepCopy(WorldState ws)
    {
        foreach (KeyValuePair<WorldStateVariables, WorldStateVarValues> entry in ws._worldState)
        {
            _worldState[entry.Key] = entry.Value;
        }
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
