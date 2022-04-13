using System.Collections.Generic;

namespace BehaviorTree
{
    public class SmartSelector : Node
    {
        float _minCost;
        int _minCostChildIndex;

        public SmartSelector() : base() { }
        public SmartSelector(List<Node> children) : base(children) { }

        public override float Simulate(WorldState idealWorldState, Dictionary<WorldStateVariables, float> weights)
        {
            WorldState worldStateCopy = new WorldState(Tree._currentWorldState);

            float min = float.MaxValue;
            int minIndex = -1;

            for (int i = 0; i < children.Count; ++i)
            {
                Node node = children[i];
                float cost = node.Simulate(idealWorldState, weights);
                if (cost < min)
                {
                    min = cost;
                    minIndex = i;
                }
                Tree._currentWorldState.DeepCopy(worldStateCopy);
            }

            _minCost = min;
            _minCostChildIndex = minIndex;

            return _minCost;
        }

        public override NodeState Evaluate()
        {
            switch (children[_minCostChildIndex].Evaluate())
            {
                case NodeState.FAILURE:
                    break;
                case NodeState.SUCCESS:
                    state = NodeState.SUCCESS;
                    return state;
                case NodeState.RUNNING:
                    state = NodeState.RUNNING;
                    return state;
                default:
                    break;
            }

            state = NodeState.FAILURE;
            return state;
        }

    }

}