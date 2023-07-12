using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public string label;
    public Text scoreText;

    internal void UpdateScore(float score)
    {
        scoreText.text = label + ": " + score.ToString();
    }
}
