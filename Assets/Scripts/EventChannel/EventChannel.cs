using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventChannel", menuName = "EventChannel/EventChannel")]
public class EventChannel<T> : ScriptableObject
{
    protected Action<T> _onEventRaised;

    public void RegisterListener(Action<T> listener)
    {
        _onEventRaised += listener;
    }

    public void UnregisterListener(Action<T> listener)
    {
        _onEventRaised -= listener;
    }

    public void RaiseEvent(T param)
    {
        _onEventRaised?.Invoke(param);
    }
}
