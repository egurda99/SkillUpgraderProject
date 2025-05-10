using TMPro;
using UnityEngine;

public sealed class BulletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _maxValueText;
    [SerializeField] private TextMeshProUGUI _currentValueText;

    public void SetMaxValue(string value)
    {
        _maxValueText.text = value;
    }

    public void SetCurrentValue(string value)
    {
        _currentValueText.text = value;
    }
}