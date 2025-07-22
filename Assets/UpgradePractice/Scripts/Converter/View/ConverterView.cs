using System;
using UnityEngine;

namespace _UpgradePractice.Scripts
{
    [Serializable]
    public sealed class ConverterView
    {
        [SerializeField] private ConverterData _converterData;
        [SerializeField] private ZoneVisual _inputZoneVisual;
        [SerializeField] private ZoneVisual _outputZoneVisual;
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private Animator _workerAnimator;

        public Animator WorkerAnimator => _workerAnimator;
        public ProgressBar ProgressBar => _progressBar;

        public ConverterData Data => _converterData;

        public ZoneVisual InputZoneVisual => _inputZoneVisual;

        public ZoneVisual OutputZoneVisual => _outputZoneVisual;
    }
}
