using TMPro;
using UnityEngine;

public sealed class ValueView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueText;

    public void SetupValue(string value)
    {
        _valueText.text = value;
    }
}
