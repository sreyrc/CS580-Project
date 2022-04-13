using System.Collections.Generic;

namespace BehaviorTree
{
    public class Selector : Node
    {
        private int _runningChildIndex = 0;
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override float Simulate(WorldState idealWorldState, WorldStateWeights weights)
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
            if (_runningChildIndex != 0)
            {
                state = children[_runningChildIndex].Evaluate();
                return state;
            }

            int count = -1;
            foreach (Node node in children)
            {
                ++count;
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        _runningChildIndex = count;
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            _runningChildIndex = 0;
            state = NodeState.FAILURE;
            return state;
        }
    }
}