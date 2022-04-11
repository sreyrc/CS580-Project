using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class MonitorBT : Tree
    {
        public Transform[] waypoints;
        public static float speed = 2f;
        public static float fovRange = 6f;

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequencer(new List<Node>
                {
                    new CheckBullyInFOVRange(transform),
                    new GoToBully(transform),
                }),
                new Patrol(transform, waypoints),
            });

            return root;
        }
    }
}