using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HandGestureControlBinderUI : MonoBehaviour
{
    public HandGestureControlBinder binder;
    public List<BindingUI> bindingUIs;


    private void Start()
    {
        binder.dataInitilizedEvent.AddListener(InitUI);   
    }


    private void InitUI()
    {
        Initialize(binder.leftHandGestureSamples, binder.rightHandGestureSamples);
    }

    public void Initialize(List<HandGestureSample> leftSamples, List<HandGestureSample> rightSamples)
    {
        foreach (var bindingUI in  bindingUIs)
        {
            bindingUI.Initialize(leftSamples, rightSamples);
        }
    }

}