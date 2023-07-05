using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HandGestureActionBinderUI : MonoBehaviour
{
    public HandGestureActionBinder binder;
    public List<HandGestureActionBindingUI> bindingUIs;


    public void InitUI()
    {
        Initialize(binder.GetActions(), binder.GetHandGestures(true), binder.GetHandGestures(false),
            binder.GetBindingData());
    }

    public void ChangeBinding(HandGestureAction action, int index, bool isLeft)
    {
        binder.ChangeBinding(action, index-1, isLeft);
    }

    public void ChangeBinding(HandGestureAction action, int leftIndex, int rightIndex)
    {
        binder.ChangeBinding(action, leftIndex - 1, true);
        binder.ChangeBinding(action, rightIndex - 1, false);
    }

    public void Initialize(List<HandGestureAction> actions, List<HandGesture> leftSamples, List<HandGesture> rightSamples,
        HandGestureInputScheme bindingData)
    {

        //foreach (var bindingUI in bindingUIs)
        for (int i = 0; i < bindingUIs.Count; i++)
        {
            bindingUIs[i].Initialize(this, actions[i], leftSamples, rightSamples, bindingData.bindings[i]);
        }
    }

    public void SaveData()
    {
        binder.SaveBinding();
    }

}