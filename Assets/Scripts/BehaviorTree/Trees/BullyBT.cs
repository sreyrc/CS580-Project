using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BullyBT : Tree
    {
        public static float walkSpeed = 1f;
        public static float runSpeed = 2.35f;
        public static float fovRange = 10f;

        protected override Node SetupTree()
        {
            // the world state this agent wants to achieve

            // weights or importance of each world state variable for this agent

            // Setup BullyBT
            Node root = new Sequencer(new List<Node>
            {
                new Idle(transform, 5.0f),
            });

            return root;
        }
    }
}