using System;

namespace Game.Tutorial
{
    [Serializable]
    public sealed class TutorialSaveLoader : SaveLoader<TutorialManager, TutorialData>
    {
        protected override TutorialData ConvertToData(TutorialManager service)
        {
            return new TutorialData
            {
                IsCompleted = service.IsCompleted,
                CurrentIndex = service.CurrentIndex
            };
        }

        protected override void SetupData(TutorialManager service, TutorialData data)
        {
            service.Initialize(data.IsCompleted, data.CurrentIndex);
        }
    }
}
