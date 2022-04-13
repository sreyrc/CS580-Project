using System.Collections.Generic;
using UnityEngine;

public class WorldStateWeights
{
    private Dictionary<WorldStateVariables, float> _worldStateWeights;
    public WorldStateWeights()
    {
        _worldStateWeights = new Dictionary<WorldStateVariables, float>();
        _worldStateWeights[WorldStateVariables.BULLYATKIDPOS] = 5f;
        _worldStateWeights[WorldStateVariables.BULLYSEENBYMONITOR] = 5f;
        _worldStateWeights[WorldStateVariables.BULLYPUNISHED] = 5f;
        _worldStateWeights[WorldStateVariables.MONITORATBULLYPOS] = 5f;
        _worldStateWeights[WorldStateVariables.MONITORATKIDPOS] = 5f;
        _worldStateWeights[WorldStateVariables.KIDBEATENUP] = 5f;
        _worldStateWeights[WorldStateVariables.KIDSEENBYBULLY] = 5f;
        _worldStateWeights[WorldStateVariables.KIDSEENBYMONITOR] = 5f;
        _worldStateWeights[WorldStateVariables.KIDATCAFE] = 5f;
        _worldStateWeights[WorldStateVariables.KIDATCLASSROOM] = 5f;
    }

    public WorldStateWeights(WorldStateWeights wsw)
    {
        _worldStateWeights = new Dictionary<WorldStateVariables, float>(wsw._worldStateWeights);
    }

    public void DeepCopy(WorldStateWeights wsw)
    {
        foreach (KeyValuePair<WorldStateVariables, float> entry in wsw._worldStateWeights)
        {
            _worldStateWeights[entry.Key] = entry.Value;
        }
    }

    public float GetWorldStateWeight(WorldStateVariables key)
    {
        return _worldStateWeights[key];
    }

    public void SetWorldStateWeights(WorldStateVariables key, float value)
    {
        _worldStateWeights[key] = value;
    }

    public Dictionary<WorldStateVariables, float> GetWorldStateWeightsDS()
    {
        return _worldStateWeights;
    }
}
