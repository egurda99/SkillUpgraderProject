using System;
using System.Collections.Generic;

namespace Game.Hints
{
    [Serializable]
    public sealed class HintsSaveLoader : SaveLoader<HintManager, HintsData>
    {
        protected override HintsData ConvertToData(HintManager service)
        {
            return new HintsData
            {
                CompletedHints = service.GetCompletedHints()
            };
        }

        protected override void SetupData(HintManager service, HintsData data)
        {
            var hints = new Dictionary<HintType, bool>();

            foreach (HintType hintType in Enum.GetValues(typeof(HintType)))
            {
                hints[hintType] = data.CompletedHints.Contains(hintType);
            }

            service.Initialize(hints);
        }
    }
}
