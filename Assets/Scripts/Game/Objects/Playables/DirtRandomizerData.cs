using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DirtData", menuName = "Game/Objects/Data/Dirt randomizer")]
public class DirtRandomizerData : GameConcreteData
{
    public int number;
    public int totalCapacity = 10;
    public int maxCapacity = 3;
    public Vector3 minRange = new Vector3(-0.5f, -0.5f, 0);
    public Vector3 maxRange = new Vector3(0.5f, 0.5f, 0);          // from [0,1] only! 
}
