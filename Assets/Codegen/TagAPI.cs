/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;

namespace Atomic.Entities
{
    public static class TagAPI
    {
        ///Keys
        public const int Player = 1;


        ///Extensions
        public static bool HasPlayerTag(this IEntity obj) => obj.HasTag(Player);
        public static bool NotPlayerTag(this IEntity obj) => !obj.HasTag(Player);
        public static bool AddPlayerTag(this IEntity obj) => obj.AddTag(Player);
        public static bool DelPlayerTag(this IEntity obj) => obj.DelTag(Player);
    }
}
