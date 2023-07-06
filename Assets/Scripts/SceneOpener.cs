using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOpener : MonoBehaviour
{
    public const string MAIN_MENU_SCENE_NAME = "MenuScene";
    public const string GAME_SCENE = "GameScene";
    public const string RECORD_SCENE_NAME = "HandGestureRecorderScene";
    public const string BINDING_SCENE_NAME = "HandGestureBindingScene";

    public void OpenMainMenuScene()
    { SceneManager.LoadScene(MAIN_MENU_SCENE_NAME); }

    public void OpenGameScene()
    {
        SceneManager.LoadScene(GAME_SCENE);
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
