using System.Collections.Generic;

namespace BehaviorTree
{
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override float Simulate(WorldState idealWorldState, Dictionary<WorldStateVariables, float> weights)
        {
            WorldState worldStateCopy = new WorldState(Tree._currentWorldState);

            float min = float.MaxValue;

            for (int i = 0; i < children.Count; ++i)
            {
                Node node = children[i];
                float cost = node.Simulate(idealWorldState, weights);
                if (cost < min)
                {
                    min = cost;
                }
                Tree._currentWorldState.DeepCopy(worldStateCopy);
            }

            return min;
        }

        public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }
            state = NodeState.FAILURE;
            return state;
        }
    }
}