using System;
using UnityEngine.AI;
using Zenject;

namespace Client
{
    [Serializable]
    public struct NavMeshAgentComponent
    {
        public NavMeshAgent agent;
    }
}