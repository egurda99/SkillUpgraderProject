using System;
using UnityEngine;

public sealed class AnimationEventDispatcher : MonoBehaviour
{
    public event Action<string> OnEventReceived;

    public void ReceiveEvent(string key)
    {
        this.OnEventReceived?.Invoke(key);
    }
}
