// using Io.AppMetrica;
// using UnityEngine;
//
// namespace Code.Infrastructure.Services.Analytics._Firebase
// {
//   public static class AnalyticStatic
//   {
//     private static string appKey = "8cdff46e-5a9e-49e3-9119-a849b4391cab";
//
//     public static void Initialize() =>
//       Io.AppMetrica.AppMetrica.Activate(new AppMetricaConfig(appKey));
//
//     public static void LogMessage(string message)
//     {
//       Debug.Log(message);
// #if !UNITY_EDITOR
//         AppMetrica.ReportEvent(message);
// #endif
//     }
//
//     public static void LogMessage(string key, string value)
//     {
//       Debug.Log(key);
// #if !UNITY_EDITOR
//         AppMetrica.ReportEvent(key, value);
// #endif
//     }
//   }
// }
