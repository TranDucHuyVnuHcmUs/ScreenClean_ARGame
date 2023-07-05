using Mediapipe.Unity;
using UnityEngine;

public abstract class EventAdapter<TRegister, TListener> : MonoBehaviour
{
    public TRegister register;
    public TListener listener;

    private void Start()
    {
        Init();
    }

    protected void Init()
    {
        Listen();
    }

    public abstract void Listen();
}