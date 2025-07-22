using System;
using Atomic.Entities;
using BehaviorDesigner.Runtime;

namespace _BehaviourTreePractice
{
    [Serializable]
    public class SharedSceneEntity : SharedVariable<SceneEntity>
    {
        public static implicit operator SharedSceneEntity(SceneEntity value)
        {
            return new SharedSceneEntity { Value = value };
        }
    }
}