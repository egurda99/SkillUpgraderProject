using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.AppRunner
{
    public abstract class LoadingStep : MonoBehaviour
    {
        public abstract string Title { get; }
        public abstract UniTask Do();
    }
}
