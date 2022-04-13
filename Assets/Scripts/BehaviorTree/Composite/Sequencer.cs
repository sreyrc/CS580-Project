using System.Collections.Generic;

namespace BehaviorTree
{
    public class Sequencer : Node
    {
        //int _runningIndex = 0;

        public Sequencer() : base() { }
        public Sequencer(List<Node> children) : base(children) { }

        public override float Simulate(WorldState idealWorldState, Dictionary<WorldStateVariables, float> weights)
        {
            WorldState worldStateCopy = new WorldState(Tree._currentWorldState);

            float cost = 0f;

            foreach (Node child in children)
            {
                cost += child.Simulate(idealWorldState, weights);
            }

            Tree._currentWorldState.DeepCopy(worldStateCopy);

            return cost;
        }

        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            //for (int i = _runningIndex; i < children.Count; ++i)
            for (int i = 0; i < children.Count; ++i)
            {
                switch (children[i].Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        //_runningIndex = 0;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    //_runningIndex = i;
                    //state = NodeState.RUNNING;
                    //return state;
                    default:
                        state = NodeState.SUCCESS;
                        //_runningIndex = 0;
                        return state;
                }
            }

            //_runningIndex = 0;
            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }

    }

}