// using Firebase;
// using Firebase.Analytics;
// using UnityEngine;
//
// namespace Code.Infrastructure.Services.Analytics._Firebase
// {
//   public class AnalyticService : IAnalyticService
//   {
//     private bool _canUseAnalytics;
//
//     public AnalyticService()
//     {
//       FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
//       {
//         var dependencyStatus = task.Result;
//         if (dependencyStatus == global::Firebase.DependencyStatus.Available)
//         {
//           // Create and hold a reference to your FirebaseApp,
//           // where app is a Firebase.FirebaseApp property of your application class.
//           _canUseAnalytics = true;
//           //  var app = FirebaseApp.DefaultInstance;
//           // Set a flag here to indicate whether Firebase is ready to use by your app.
//         }
//         else
//         {
//           Debug.LogError(System.String.Format(
//             "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
//           // Firebase Unity SDK is not safe to use here.
//         }
//       });
//     }
//
//     public void LogMessage(string eventName)
//     {
//       if (!_canUseAnalytics)
//         return;
//       FirebaseAnalytics.LogEvent(eventName);
//     }
//
//     public void InAppPurchaseEvent()
//     {
//       if (!_canUseAnalytics)
//         return;
//       FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventPurchase);
//     }
//
//     public void InterstitialAd()
//     {
//       if (!_canUseAnalytics)
//         return;
//
//       FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAdImpression, new Parameter("Ad_Type", "Interstitial_Ad"));
//     }
//
//     public void RewardedAd()
//     {
//       if (!_canUseAnalytics)
//         return;
//       FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAdImpression, new Parameter("Ad_Type", "Rewarded_Ad"));
//     }
//
//     public void BannerAd()
//     {
//       if (!_canUseAnalytics)
//         return;
//       FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAdImpression, new Parameter("Ad_Type", "Banner_Ad"));
//     }
//
//
//
//     public void LevelUp(int eventName)
//     {
//       if (!_canUseAnalytics)
//         return;
//       FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelUp,
//         new Parameter(FirebaseAnalytics.ParameterLevel, eventName));
//     }
//   }
// }
