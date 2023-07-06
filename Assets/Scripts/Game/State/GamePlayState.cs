
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GamePlayState", menuName = "Game/States/Play")]
public class GamePlayState : GameState
{
    public int numberOfTowel;
    public int numberOfDirt;
    public int numberOfHardDirt;
    public int numberOfWaterDrop;
    
    public bool useTimer;
    public float time;
}