using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BehaviorTree;

public class CurrentWorldStateDisplay : MonoBehaviour
{
    public Text currentWorldStateText;
    public Text agentIdealWorldStateText;

    // stores student, monitor and bully Game Objects
    public GameObject[] agents;
    
    public Dropdown agent;

    int agentSelected;

    private object[] agentTrees;

    public Text leafExecutingBully;
    public Text leafExecutingStudent;
    public Text leafExecutingMonitor;

    private void Start()
    {
        agents = new GameObject [3];
        agentTrees = new BehaviorTree.Tree[agents.Length];

        for (int i = 0; i < agents.Length; i++)
        {
            switch(i)
            {
                case 0: agentTrees[i] = agents[i].GetComponent<StudentBT>(); break;
                case 1: agentTrees[i] = agents[i].GetComponent<MonitorBT>(); break;
                case 2: agentTrees[i] = agents[i].GetComponent<BullyBT>(); break;
            }
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

        //Dictionary<WorldStateVariables, WorldStateVarValues> agentIdealState = 

        foreach (KeyValuePair<WorldStateVariables, WorldStateVarValues> entry in worldState)
        {
            currentWorldStateText.text += entry.Key.ToString() + "\t\t" + entry.Value + "\n";
        }


    }
}
