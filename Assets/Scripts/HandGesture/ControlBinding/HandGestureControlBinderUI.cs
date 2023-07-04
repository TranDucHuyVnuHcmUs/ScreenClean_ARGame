using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HandGestureControlBinderUI : MonoBehaviour
{
    public HandGestureControlBinder binder;
    public List<HandGestureBindingUI> bindingUIs;


    public void InitUI()
    {
        Initialize(binder.GetControls(), binder.GetHandGestures(true), binder.GetHandGestures(false),
            binder.GetBindingData());
    }

    public void ChangeBinding(string control, int index, bool isLeft)
    {
        binder.ChangeBinding(control, index-1, isLeft);
    }

    public void ChangeBinding(string control, int leftIndex, int rightIndex)
    {
        binder.ChangeBinding(control, leftIndex - 1, true);
        binder.ChangeBinding(control, rightIndex - 1, false);
    }

    public void Initialize(List<string> controls, List<HandGestureSample> leftSamples, List<HandGestureSample> rightSamples,
        HandGestureControlBindingData bindingData)
    {

        //foreach (var bindingUI in bindingUIs)
        for (int i = 0; i < bindingUIs.Count; i++)
        {
            bindingUIs[i].Initialize(this, controls[i], leftSamples, rightSamples, bindingData.bindings[i]);
        }
    }

    public void SaveData()
    {
        binder.SaveBinding();
    }

}