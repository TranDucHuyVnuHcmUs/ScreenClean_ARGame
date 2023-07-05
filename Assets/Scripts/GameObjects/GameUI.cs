using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text scoreText;

    internal void UpdateScore(float score)
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
