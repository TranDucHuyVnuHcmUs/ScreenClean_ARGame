using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BindingUI : MonoBehaviour
{
    public string control;
    public Dropdown leftHandGestureDropdown, rightHandGestureDropdown;


    public void Initialize(List<HandGestureSample> samples, Dropdown dropdown)
    {
        var options = new List<Dropdown.OptionData>();
        foreach (var sample in samples)
        {
            options.Add(new Dropdown.OptionData(sample.gestureName));
        }
        dropdown.AddOptions(options);
    }


    public void Initialize(List<HandGestureSample> leftSamples, List<HandGestureSample> rightSamples)
    {
        Initialize(leftSamples, leftHandGestureDropdown);
        Initialize(rightSamples, rightHandGestureDropdown);
    }
}
