using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public const string RECORD_SCENE_NAME = "HandGestureRecorderScene";
    public const string BINDING_SCENE_NAME = "HandGestureBindingScene";

    public void OpenGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenRecordScene()
    {
        SceneManager.LoadScene(RECORD_SCENE_NAME);
    }

    public void OpenControlBindingScene()
    {
        SceneManager.LoadScene(BINDING_SCENE_NAME);
    }
}
