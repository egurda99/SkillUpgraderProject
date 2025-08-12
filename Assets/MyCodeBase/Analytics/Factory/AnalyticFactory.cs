// using Code.Gameplay.Common.Time;
// using Code.Infrastructure.Services.LevelData;
// using Code.Infrastructure.Services.StaticData;
// using UnityEngine;
//
// namespace Code.Infrastructure.Services.Analytics.Factory
// {
//   public class AnalyticFactory : IAnalyticFactory
//   {
//     private readonly IAnalyticService _analytics;
//     private IStaticDataService _staticData;
//     private readonly ILevelDataService _levelService;
//     private readonly ITimeService _timeService;
//
//     public AnalyticFactory(IAnalyticService analytics,
//       IStaticDataService staticData,
//       ILevelDataService levelService,
//       ITimeService timeService)
//     {
//       _staticData = staticData;
//       _levelService = levelService;
//       _timeService = timeService;
//       _analytics = analytics;
//     }
//
//     public void LogMessageWithTime(string message)
//     {
//     }
//
//     public void BootLog(string logMessage) =>
//       _analytics.LogMessage(logMessage);
//
//     public void WithMetaLevelLog(string logMessage) =>
//       _analytics.LogMessage("Meta" + "_" + logMessage);
//
//     public void WithPreviousLevelLog(string logMessage)
//     {
//       if (_levelService.PreviousLevelData == null)
//         return;
//       _analytics.LogMessage(_levelService.PreviousLevelData.LevelKey + "_" + logMessage);
//     }
//
//     public void WithCurrentLevelLog(string logMessage) =>
//       _analytics.LogMessage(_levelService.CurrentLevelData.LevelKey + "_" + logMessage);
//
//     public void WithNextLevelLog(string logMessage)
//     {
//       if (_levelService.NextLevelData == null)
//         return;
//       _analytics.LogMessage(_levelService.NextLevelData.LevelKey + "_" + logMessage);
//     }
//
//
//     public void MetaStartLevel()
//     {
//       string message = "MetaStartLevel"
//                        + "_" + GetPreviousLevelLog()
//                        + "_" + _levelService.NextLevelData.LevelKey;
//
//       Debug.Log(message);
//       _analytics.LogMessage(message);
//     }
//
//     public void MetaStartSurvival()
//     {
//       string message = "MetaStartSurvival"
//                        + "_" + GetPreviousLevelLog()
//                        + "_" + _levelService.NextLevelData.LevelKey;
//
//       Debug.Log(message);
//       _analytics.LogMessage(message);
//     }
//
//     private string GetPreviousLevelLog()
//     {
//       if (_levelService.PreviousLevelData == null)
//         return "Initial";
//       else
//         return _levelService.PreviousLevelData.LevelKey;
//     }
//   }
// }
