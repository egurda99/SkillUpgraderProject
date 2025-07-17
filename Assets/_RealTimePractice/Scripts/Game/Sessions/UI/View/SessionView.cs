using TMPro;
using UnityEngine;

namespace RealTimePractice.UI
{
    public sealed class SessionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _sessionNumberText;
        [SerializeField] private TextMeshProUGUI _startTimeText;
        [SerializeField] private TextMeshProUGUI _endTimeText;
        [SerializeField] private TextMeshProUGUI _durationTimeText;

        public void SetSessionNumberText(string sessionNumber)
        {
            _sessionNumberText.text = sessionNumber;
        }

        public void SetStartTimeText(string startTime)
        {
            _startTimeText.text = startTime;
        }

        public void SetEndTimeText(string endTime)
        {
            _endTimeText.text = endTime;
        }

        public void SetDurationTimeText(string durationTime)
        {
            _durationTimeText.text = durationTime;
        }
    }
}
