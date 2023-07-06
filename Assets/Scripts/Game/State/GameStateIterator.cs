
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStateIterator
{
    private GameStateOrderedList _list;
    private List<GameState> _unpackedList;          //without list state inside
    private int index = 0;

    public GameStateIterator(GameStateOrderedList list)
    {
        this._list = list;
        this._unpackedList = UnpackList(list);
    }

    public GameState Next()
    {
        if (index < _unpackedList.Count)
        {
            Debug.Log("Next : " + _unpackedList[index + 1].name);
            return _unpackedList[index++];
        }
        else return null;
    }

    public bool EndOfList() { return (index >= _unpackedList.Count); }

    private List<GameState> UnpackList(GameStateOrderedList list)
    {
        List<GameState> res = new List<GameState>();
        for (int i = 0; i < list.children.Count; i++)
        {
            try {
                GameStateOrderedList l = (GameStateOrderedList)list.children[i];
                res.AddRange(UnpackList(l));
            }
            catch {
                res.Add(list.children[i]);
            }
        }
        return res;
    }
}