using System;
using UnityEngine;

namespace Client.Components
{
    [Serializable]
    public struct AnimatorView
    {
        public Animator Value;
    }
    
    [Serializable]
    public struct DeathRequest
    {
    }
    
    [Serializable]
    public struct DeathEvent
    {
    }
    
    [Serializable]
    public struct Health
    {
        public int Value;
    }
    
    [Serializable]
    public struct Inactive
    {
    }

    #region 2

    [Serializable]
    public struct CollisionEnterRequest
    {
    }
    
    [Serializable]
    public struct BulletTag
    {
    }
    
    [Serializable]
    public struct SourceEntity
    {
        public int Value;
    }
    
    [Serializable]
    public struct TargetEntity
    {
        public int Value;
    }

    #endregion

    #region 3

    [Serializable]
    public struct Damage
    {
        public int Value;
    }

    [Serializable]
    public struct TakeDamageRequest
    {
    }
    
    [Serializable]
    public struct TakeDamageEvent
    {
    }
    
    [Serializable]
    public struct DamageableTag
    {
    }
    
    [Serializable]
    public struct OneFrame
    {
    }

    #endregion
}
