using System;
using BehaviorDesigner.Runtime;
using BehaviourTreePractice;

namespace _BehaviourTreePractice
{
    [Serializable]
    public class SharedTree : SharedVariable<Tree>
    {
        public static implicit operator SharedTree(Tree value)
        {
            return new SharedTree { Value = value };
        }
    }
}