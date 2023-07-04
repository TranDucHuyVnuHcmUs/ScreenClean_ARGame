using UnityEngine;
using UnityEngine.UI;

public class HandGestureRecordUI : MonoBehaviour
{
    public HandGestureRecorder recorder;

    public InputField nameField;
    public Text recordButtonText;

    public bool isRecording = false;

    public string GetNameFromUI()
    {
        return nameField.text;
    }

    public void Toggle()
    {
        isRecording = !isRecording;
        if (isRecording)
        {
            recorder.StartRecording(nameField.text);
            recordButtonText.text = "Stop";
        }
        else
        {
            recorder.StopRecording();
            recordButtonText.text = "Start";
        }
    }


}