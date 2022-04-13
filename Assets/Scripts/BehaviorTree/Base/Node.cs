using System.Collections;
using System.Collections.Generic;

namespace BehaviorTree
{
    // Possible states of Nodes
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE,
    }
    public class Node
    {
        // Protected so that derived classes can access
        protected NodeState state;

        // Stores parent of this node 
        public Node parent;

        // List of all the children of this node
        protected List<Node> children = new List<Node>();

        // Shared data stored in this node. This data can be accessed by other nodes in the branch of the tree
        private Dictionary<string, object> _nodeDictionary = new Dictionary<string, object>();

        public Node()
        {
            state = NodeState.SUCCESS;
            parent = null;
        }

        public Node(List<Node> children)
        {
            state = NodeState.SUCCESS;
            // Assign parent of these children to this current node
            // and add the children to the list of children
            foreach (Node child in children)
                _Attach(child);
        }

        private void _Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public virtual float Simulate(WorldState idealWorldState, Dictionary<WorldStateVariables, float> weights) => 0.0f;

        public NodeState GetState()
        {
            return state;
        }

        public void SetData(string key, object value)
        {
            _nodeDictionary[key] = value;
        }

        public object GetData(string key)
        {
            // If this node has the data for this key, return it
            if (_nodeDictionary.ContainsKey(key)) {
                return _nodeDictionary[key];
            }
            Node node = parent;
            while (node != null)
            {
                /* Work your way up the tree recursively until you get the desired value. 
                 * And if found, keep passing that result back to this node
                 */
                object value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (_nodeDictionary.ContainsKey(key))
            {
                _nodeDictionary.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }

}
