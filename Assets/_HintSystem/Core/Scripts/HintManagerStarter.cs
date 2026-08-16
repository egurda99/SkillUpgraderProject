using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Hints
{
    public sealed class HintManagerStarter : MonoBehaviour
    {
        [SerializeField] private List<HintType> _completedHints = new();

        [ShowInInspector] [ReadOnly] private HintManager _hintManager;


        [Inject]
        public void Construct(HintManager hintManager)
        {
            _hintManager = hintManager;

            var hints = new Dictionary<HintType, bool>();

            foreach (HintType hintType in Enum.GetValues(typeof(HintType)))
            {
                hints[hintType] = _completedHints.Contains(hintType);
            }

            _hintManager.Initialize(hints);
        }
    }
}
