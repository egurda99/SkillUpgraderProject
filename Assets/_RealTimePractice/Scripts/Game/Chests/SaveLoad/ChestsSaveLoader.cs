using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RealTimePractice
{
    [Serializable]
    public class ChestsSaveLoader : SaveLoader<ChestsManager, ChestsData>
    {
        private ITimeProvider _timeProvider;

        public ChestsSaveLoader(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        protected override ChestsData ConvertToData(ChestsManager service)
        {
            var chestsDataList = new List<ChestData>();

            foreach (var chest in service.Chests)
            {
                chestsDataList.Add(new ChestData(chest.ChestType, chest.ID,
                    GetCurrentTimeUniversalAsync().ToString(), chest.Duration, chest.RemainingTime));
            }

            return new ChestsData(chestsDataList);
        }

        protected override void SetupData(ChestsManager service, ChestsData data)
        {
            var chestList = new List<Chest>();

            DateTime currentTime;
            if (_timeProvider is IAsyncTimeProvider asyncProvider)
            {
                currentTime =
                    asyncProvider.GetCurrentTimeAsync().GetAwaiter()
                        .GetResult(); // блокировка допустима, если понимаешь риски
            }
            else if (_timeProvider is ISyncTimeProvider syncProvider)
            {
                currentTime = syncProvider.GetCurrentTime();
            }
            else
            {
                throw new Exception("Unsupported time provider type.");
            }

            foreach (var chestData in data.ChestsDataList)
            {
                var chest = new Chest(chestData.ChestType, chestData.Id, chestData.OpenDuration);

                var saveTime = DateTime.Parse(chestData.SaveTime);
                var elapsedSinceSave = currentTime - saveTime;


                var remainingTime = TimeSpan.FromSeconds(chestData.RemainingTime) - elapsedSinceSave;
                if (remainingTime < TimeSpan.Zero)
                    remainingTime = TimeSpan.Zero;

                chest.Synchronize((float)remainingTime.TotalSeconds);
                chestList.Add(chest);
            }

            service.SetupChests(chestList);

            Debug.Log($"<color=yellow>SetupChests chests = {data.ChestsDataList}</color>");
            Debug.Log($"<color=yellow>Amount chests = {data.ChestsDataList.Count}</color>");
        }

        protected override void SetupDefaultData(ChestsManager service)
        {
            base.SetupDefaultData(service);
            Debug.Log("[ChestsDataStorage] No chests file found, returning empty list.");
        }

        public async UniTask<DateTime> GetCurrentTimeUniversalAsync()
        {
            if (_timeProvider is IAsyncTimeProvider asyncProvider)
            {
                return await asyncProvider.GetCurrentTimeAsync();
            }

            if (_timeProvider is ISyncTimeProvider syncProvider)
            {
                return syncProvider.GetCurrentTime();
            }

            throw new InvalidOperationException("Unsupported ITimeProvider implementation.");
        }
    }
}
