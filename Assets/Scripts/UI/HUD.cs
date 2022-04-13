using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BehaviorTree;

public class HUD : MonoBehaviour
{
    public Text currentWorldStateText;
    public Text agentIdealWorldStateText;

    public GameObject canvas;

    public Dropdown agentDropdown;

    public Slider slider;

    int prevDropdownValue = -1;

    // stores student, monitor and bully Game Objects
    public GameObject[] agents;

    private BehaviorTree.Tree[] agentTrees;

    private Slider[] sliderInstances;

    private void Start()
    {
        //agents = new GameObject [3];
        agentTrees = new BehaviorTree.Tree[3];

        for (int i = 0; i < agents.Length; i++)
        {
            agentTrees[i] = agents[i].GetComponent<BehaviorTree.Tree>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentWorldStateText.text = "";
        Dictionary<WorldStateVariables, WorldStateVarValues> worldState =
            BehaviorTree.Tree._currentWorldState.GetWorldStateDS();

        foreach (KeyValuePair<WorldStateVariables, WorldStateVarValues> entry in worldState)
        {
            currentWorldStateText.text += entry.Key.ToString() + "\t\t" + entry.Value + "\n";
        }

        if (prevDropdownValue != agentDropdown.value)
        {
            agentIdealWorldStateText.text = "";
            prevDropdownValue = agentDropdown.value;

            if (sliderInstances.Length > 0)
            {
                for (int i = 0; i < sliderInstances.Length; i++)
                    Destroy(sliderInstances[i].gameObject);
            }

            Dictionary<WorldStateVariables, WorldStateVarValues> agentIdealState =
                agentTrees[agentDropdown.value]._idealWorldState.GetWorldStateDS();

            int relevantStatesCount = 0;

            foreach (KeyValuePair<WorldStateVariables, WorldStateVarValues> entry in agentIdealState)
            {
                if (entry.Value != WorldStateVarValues.DONTCARE)
                {
                    agentIdealWorldStateText.text += entry.Key.ToString() + "\t\t" + entry.Value + "\n";
                    relevantStatesCount++;
                }
            }

            sliderInstances = new Slider[relevantStatesCount];

            int offset = 0;

            for (int i = 0; i < relevantStatesCount; i++)
            {
                Slider sliderInstance = Instantiate(slider);
                sliderInstance.GetComponent<Transform>().SetParent(canvas.GetComponent<Transform>());
                RectTransform sliderRectTransform = sliderInstance.GetComponent<RectTransform>();
                sliderRectTransform.anchoredPosition = new Vector2(100, -offset);
                sliderInstances[i] = sliderInstance;
                offset += 10;
            }
        }
    }
}
