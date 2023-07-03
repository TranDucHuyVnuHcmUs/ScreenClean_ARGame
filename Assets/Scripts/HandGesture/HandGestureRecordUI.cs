using UnityEngine;
using UnityEngine.UIElements;

public class HandGestureRecordUI : MonoBehaviour
{
    public HandGestureRecorder register;

    public TextField nameTextField;
    public Button recordButton;

    public bool isRecording = false;

    public string GetNameFromUI()
    {
        return nameTextField.text;
    }

    public void Toggle()
    {
        isRecording = !isRecording;
        if (isRecording)
        {
            register.StartRecording();
            recordButton.text = "Stop";
        }
        else recordButton.text = "Start";
    }


}