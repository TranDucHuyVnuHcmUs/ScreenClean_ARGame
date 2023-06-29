using Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ColliderUnityEvent: UnityEvent<Collider> { }

public class TriggerCollisionEventHandler : MonoBehaviour
{
    public UnityEvent triggerEnterVoidEvent, triggerExitVoidEvent;
    public ColliderUnityEvent triggerEnterEvent, triggerExitEvent;

    private void Awake()
    {
        triggerEnterEvent = new ColliderUnityEvent();
        triggerExitEvent = new ColliderUnityEvent();
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerEnterEvent.Invoke(other);
        triggerEnterVoidEvent.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        triggerExitEvent.Invoke(other);
        triggerExitVoidEvent.Invoke();
    }
}