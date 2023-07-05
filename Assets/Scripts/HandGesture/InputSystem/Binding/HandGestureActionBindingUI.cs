using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HandGestureActionBindingUI : MonoBehaviour
{
    public HandGestureActionBinderUI binderUI;
    public string control;
    public Text controlLabel;
    public Dropdown leftHandGestureDropdown, rightHandGestureDropdown;


    public void Initialize(List<HandGesture> samples, Dropdown dropdown, UnityAction<int> onValueChangedCallback)
    {
        var options = new List<Dropdown.OptionData>();
        foreach (var sample in samples)
        {
            options.Add(new Dropdown.OptionData(sample.gestureName));
        }
        dropdown.AddOptions(options);
        dropdown.onValueChanged.AddListener(onValueChangedCallback);
    }


    public void Initialize(HandGestureActionBinderUI binderUI,
        string controlName, List<HandGesture> leftSamples, List<HandGesture> rightSamples, HandGestureActionBinding handGestureControlBind)
    {
        this.binderUI = binderUI;
        this.control = controlName;
        this.controlLabel.text = controlName;
        Initialize(leftSamples, leftHandGestureDropdown, ChangeLeftBinding);
        Initialize(rightSamples, rightHandGestureDropdown, ChangeRightBinding);

        RestorePreviousChoices(leftHandGestureDropdown, handGestureControlBind.leftGesture, leftSamples);
        RestorePreviousChoices(rightHandGestureDropdown, handGestureControlBind.rightGesture, rightSamples);
    }

    private void RestorePreviousChoices(Dropdown dropdown, HandGesture gesture, List<HandGesture> leftSamples)
    {
        if (gesture == null) dropdown.value = 0;
        else
        {
            for (int i = 0; i < leftSamples.Count; i++)
                if ( (gesture.gestureName == leftSamples[i].gestureName))
                {
                    dropdown.value = i + 1;
                    break;
                }
        }
    }

    /// <summary>
    /// This function is to register the dropdown value changed event.
    /// </summary>
    /// <param name="index"></param>
    public void ChangeLeftBinding(int index)
    {
        binderUI.ChangeBinding(this.control, index, true);
    }

    public void ChangeRightBinding(int index)
    {
        binderUI.ChangeBinding(this.control, index, false);
    }
}
