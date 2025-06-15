using System;
using UnityEngine;

public sealed class TriggerEventDispatcher : MonoBehaviour
{
    public event Action<Collider> OnTriggerEntered;
    public event Action<Collider> OnTriggerStayed;
    public event Action<Collider> OnTriggerExited;


    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEntered?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExited?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerStayed?.Invoke(other);
    }
}