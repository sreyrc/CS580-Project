using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Inverter : Node
    {
        public Inverter(List<Node> children) : base(children)
        {
        }

        public override float Simulate(WorldState idealWorldState, WorldStateWeights weights)
        {
            WorldState worldStateCopy = new WorldState(Tree._currentWorldState);

            float cost = 0f;

            cost += children[0].Simulate(idealWorldState, weights);

            Tree._currentWorldState.DeepCopy(worldStateCopy);

            return cost;
        }

        public override NodeState Evaluate()
        {
            switch (children[0].Evaluate())
            {
                case NodeState.FAILURE:
                    state = NodeState.SUCCESS;
                    return state;
                case NodeState.RUNNING:
                    state = NodeState.RUNNING;
                    return state;
                case NodeState.SUCCESS:
                    state = NodeState.FAILURE;
                    return state;
                default:
                    state = NodeState.RUNNING;
                    return state;
            }
        }
    }
}