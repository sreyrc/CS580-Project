using System.Collections.Generic;

namespace BehaviorTree
{
    public class Sequencer : Node
    {
        public Sequencer() : base() { }
        public Sequencer(List<Node> children) : base(children) { }

        public override float Simulate()
        {
            WorldState worldStateCopy = new WorldState(Tree._currentWorldState);

            float cost = 0f;

            foreach (Node child in children)
            {
                cost += child.Simulate();
            }

            Tree._currentWorldState.DeepCopy(worldStateCopy);

            return cost;
        }

        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }

    }

}