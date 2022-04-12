using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BullyBT : Tree
    {
        public Transform[] waypoints;
        public static float walkSpeed = 2f;
        public static float runSpeed = 5.35f;
        public static float fovRange = 6f;

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Patrol(transform, waypoints),
            });

            return root;
        }
    }
}