using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamePlayMaker : MonoBehaviour
{
    public PlaygroundData playgroundData;

    [SerializeField] private GameTimerMaker gameTimerMaker;
    [SerializeField] private DirtMaker dirtMaker;
    [SerializeField] private DirtRandomizer dirtRandomizer;
    [SerializeField] private TowelMaker towelMaker;
    [SerializeField] private WaterBucketMaker waterBucketMaker;

    private Dictionary<Type, GameAgentMaker> makerDict;
    private bool isInitialized = false;

    [SerializeField] private List<GameAgent> createdAgents; 

    private void Awake()
    {
        makerDict = new Dictionary<Type, GameAgentMaker>();
    }

    private void AddMaker<T>(GameAgentMaker<T> maker)
        where T : GameAgentData
    {
        makerDict.Add(typeof(T), maker);
    }

    internal void CleanGame()
    {
        foreach (var maker in  makerDict.Values)
        {
            maker.CleanObjects();
        }
        this.createdAgents.Clear();
    }

    internal void MakeGame(GamePlayState state)
    {
        if (!isInitialized) Initialize(state.PlaygroundData);
        var types = makerDict.Keys.ToList();
        for (int i = 0; i < state.agentsData.Count; i++)
        {
            MakeAgent(state.agentsData[i]);
        }
    }

    private void Initialize(PlaygroundData playgroundData)
    {
        this.playgroundData = playgroundData;

        AddConcreteMaker(this.dirtRandomizer, playgroundData);
        AddConcreteMaker(this.dirtMaker, playgroundData);
        AddConcreteMaker(this.gameTimerMaker, playgroundData);
        AddConcreteMaker(this.towelMaker, playgroundData);
        AddConcreteMaker(this.waterBucketMaker, playgroundData);

        isInitialized = true;
    }

    private void AddConcreteMaker<T>(GameConcreteMaker<T> maker, PlaygroundData playgroundData)
        where T: GameConcreteData
    {
        maker.playgroundData = playgroundData;
        AddMaker(maker);
    }

    private void MakeAgent(GameAgentData gameAgentData)
    {
        var types = makerDict.Keys.ToList();
        for (int i = 0; i < makerDict.Count; i++)
        {
            try
            {
                var cstate = Convert.ChangeType(gameAgentData, types[i]);
                this.createdAgents.AddRange(makerDict[types[i]].MakeObject(gameAgentData));
            }
            catch { continue; }
        }
    }

    internal List<GameAgent> GetAllGameAgents()
    {
        return this.createdAgents;
    }
}
