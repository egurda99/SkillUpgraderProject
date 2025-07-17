using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace RealTimePractice
{
    public sealed class NetworkAsyncTimeProvider : IAsyncTimeProvider
    {
        private const string TimeApiUrl = "http://worldtimeapi.org/api/ip";

        [Serializable]
        private class TimeApiResponse
        {
            public string datetime; // ISO 8601 format
        }

        public async UniTask<DateTime> GetCurrentTimeAsync()
        {
            using var request = UnityWebRequest.Get(TimeApiUrl);
            await request.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
            if (request.result != UnityWebRequest.Result.Success)
#else
        if (request.isNetworkError || request.isHttpError)
#endif
            {
                throw new Exception($"Network error while fetching time: {request.error}");
            }

            var json = request.downloadHandler.text;
            var response = JsonUtility.FromJson<TimeApiResponse>(json);

            if (DateTime.TryParse(response.datetime, out var result))
            {
                return result.ToLocalTime(); // или оставить в UTC — зависит от требований
            }

            throw new Exception("Failed to parse datetime from API response.");
        }
    }
}
