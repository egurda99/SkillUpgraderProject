//using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Code.AppRunner
{
    public abstract class LoadingStepScriptableObject : ScriptableObject
    {
        // public abstract UniTask Do();
        public abstract string Title { get; }
    }
}
