using TMPro;
using UnityEngine;

public abstract class StatViewBase : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI ValueText;

    public abstract void SetValue(string value);
}
