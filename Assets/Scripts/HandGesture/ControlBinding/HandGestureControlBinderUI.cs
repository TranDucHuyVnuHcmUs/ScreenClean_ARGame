using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HandGestureControlBinderUI : MonoBehaviour
{
    public Dropdown pickKeyGestureDropdown;

    public void Initialize(List<HandGestureSample> samples)
    {
        var options = new List<Dropdown.OptionData>();
        foreach (var sample in samples)
        {
            options.Add(new Dropdown.OptionData(sample.gestureName));
        }

        pickKeyGestureDropdown.AddOptions(options);
    }
}