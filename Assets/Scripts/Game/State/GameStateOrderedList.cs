
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// This object contains many other states, to group them up for easier configuration.
/// </summary>
[CreateAssetMenu(fileName = "GameStateOrderedList", menuName = "Game/States/Ordered list")]
public class GameStateOrderedList : GameState
{
    public List<GameState> children;
}