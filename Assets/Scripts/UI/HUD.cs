using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BehaviorTree;

public class HUD : MonoBehaviour
{
    public Text currentWorldStateText;
    public Text agentIdealWorldStateText;
    public Dropdown agentDropdown;
    // stores sliders for ideal world states
    //public Slider[] sliders;
    // stores student, monitor and bully Game Objects
    public GameObject[] agents;

    private BehaviorTree.Tree[] agentTrees;
    //private int prevDropdownValue = -1;
    //private float[] prevSliderValue;

    private void Start()
    {
        agentTrees = new BehaviorTree.Tree[3];
        for (int i = 0; i < agents.Length; ++i)
        {
            agentTrees[i] = agents[i].GetComponent<BehaviorTree.Tree>();
        }

        //prevSliderValue = new float[sliders.Length];
        //for (int i = 0; i < sliders.Length; ++i)
        //{
        //    sliders[i].gameObject.SetActive(false);
        //    prevSliderValue[i] = -1f;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        currentWorldStateText.text = "Current World State\n";
        Dictionary<WorldStateVariables, WorldStateVarValues> worldState =
            BehaviorTree.Tree._currentWorldState.GetWorldStateDS();

        foreach (KeyValuePair<WorldStateVariables, WorldStateVarValues> entry in worldState)
        {
            currentWorldStateText.text += entry.Key.ToString() + "\t\t" + entry.Value + "\n";
        }

        //if (prevDropdownValue != agentDropdown.value)
        //{
        agentIdealWorldStateText.text = "Ideal World State\n";
        //prevDropdownValue = agentDropdown.value;

        Dictionary<WorldStateVariables, WorldStateVarValues> agentIdealState =
            agentTrees[agentDropdown.value]._idealWorldState.GetWorldStateDS();

        WorldStateWeights agentWeights =
            agentTrees[agentDropdown.value]._worldStateVariableWeights;

        //int count = 0;
        foreach (KeyValuePair<WorldStateVariables, WorldStateVarValues> entry in agentIdealState)
        {
            if (entry.Value != WorldStateVarValues.DONTCARE)
            {
                agentIdealWorldStateText.text += entry.Key.ToString() + "\t\t" + entry.Value + "\t\t" + "Weight: " + agentWeights.GetWorldStateWeight(entry.Key).ToString("n2") + "\n";
                //sliders[count].gameObject.SetActive(true);
                //sliders[count].value = agentWeights.GetWorldStateWeight(entry.Key);
                //++count;
            }
        }

        //for (int i = count; i < sliders.Length; ++i)
        //{
        //    sliders[i].gameObject.SetActive(false);
        //}

        //count = 0;
        //foreach (KeyValuePair<WorldStateVariables, WorldStateVarValues> entry in agentIdealState)
        //{
        //    //weights[entry.Key] = slider[count].value;
        //    if (entry.Value != WorldStateVarValues.DONTCARE)
        //    {
        //        if (prevSliderValue[count] != sliders[count].value)
        //        {
        //            agentTrees[agentDropdown.value]._worldStateVariableWeights.SetWorldStateWeights(entry.Key, sliders[count].value);
        //        }
        //        ++count;
        //    }
        //}
        //}
    }

    //public void SetSliders()
    //{
    //    Dictionary<WorldStateVariables, WorldStateVarValues> agentIdealState =
    //        agentTrees[agentDropdown.value]._idealWorldState.GetWorldStateDS();

    //    WorldStateWeights agentWeights =
    //        agentTrees[agentDropdown.value]._worldStateVariableWeights;

    //    int count = 0;
    //    foreach (KeyValuePair<WorldStateVariables, WorldStateVarValues> entry in agentIdealState)
    //    {
    //        if (entry.Value != WorldStateVarValues.DONTCARE)
    //        {
    //            sliders[count].value = agentTrees[agentDropdown.value]._worldStateVariableWeights.GetWorldStateWeight(entry.Key);
    //            ++count;
    //        }
    //    }
    //}
}
