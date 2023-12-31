﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class DirtRandomizer : GameConcreteMaker<DirtRandomizerData>
{
    private int totalCapacity;
    private int maxCapacity = 3;
    private int count = 1;
    private List<DirtCube> dirts;

    protected override void Awake()
    {
        base.Awake();
        dirts = new List<DirtCube>();
    }

    protected override List<GameAgent> MakeObjectFromData(DirtRandomizerData agentData)
    {
        totalCapacity = agentData.totalCapacity;
        maxCapacity = agentData.maxCapacity;
        count = agentData.number;
        var agents = new List<GameAgent>();
        
        for (int i = 0; i < agentData.number; i++)
        {
            var newObj = Instantiate(prefab);
            newObj.transform.parent = parentTransform;
            this.createdObjects.Add(newObj);
            InitObject(newObj, agentData);
            var dirtCubeComp = newObj.GetComponentInChildren<DirtCube>();
            if (dirtCubeComp)
            { 
                dirts.Add(dirtCubeComp);
                agents.Add(dirtCubeComp);
            }

        }
        return agents;
    }

    private Vector3 RandomizeVector(Vector3 minRange, Vector3 maxRange)
    {
        var x = UnityEngine.Random.Range(minRange.x, maxRange.x);
        var y = UnityEngine.Random.Range(minRange.y, maxRange.y);
        var z = UnityEngine.Random.Range(minRange.z, maxRange.z);
        return new Vector3(x, y, z);
    }

    internal override void InitObject(GameObject newObj, DirtRandomizerData agentData)
    {
        do
        {
            newObj.transform.localPosition = RandomizeVector(agentData.minRange, agentData.maxRange);
            newObj.transform.localPosition = PositionAccordingToPlayRect(newObj.transform.localPosition, playgroundData.screenRectSize);
        }
        while (PositionIsCloseToAnyAgent(newObj.transform.localPosition,
            agentData.minDistanceThreshold));

        newObj.transform.localRotation = Quaternion.Euler(agentData.initLocalRotation);
        newObj.transform.localScale = agentData.initLocalScale;

        var dirtComp = newObj.GetComponentInChildren<DirtCube>();
        if (!dirtComp) return;
        dirtComp.capacity = RandomizerCapacity();
    }

    private bool PositionIsCloseToAnyAgent(Vector3 localPosition, float threshold)
    {
        List<GameAgent> agents = GamePlay.GetAllGameAgents();
        foreach (var agent in agents) {
            if ( Vector3.Distance(agent.transform.parent.localPosition, localPosition) <= threshold) return true;
        }
        foreach (var dirt in dirts)
            if (Vector3.Distance(dirt.transform.parent.localPosition, localPosition) <= threshold) return true;
        return false;
    }

    private int RandomizerCapacity()
    {
        int cap = 0;
        if (count == 1)
        {
            cap = this.totalCapacity;
            --count;
            this.totalCapacity = 0;
            maxCapacity = 0;
            return cap;
        }
        else if ( ((float)maxCapacity) == (totalCapacity * 1.0f / count))
        {
            cap = maxCapacity;
        }
        else cap = UnityEngine.Random.Range(1, maxCapacity + 1);
        
        this.totalCapacity -= cap;
        --count;
        maxCapacity = Math.Min(maxCapacity, this.totalCapacity / this.count);

        return cap;
    }

    private Vector3 PositionAccordingToPlayRect(Vector3 localPosition, Vector2 screenRectSize)
    {
        return new Vector3(
            localPosition.x * screenRectSize.x,
            localPosition.y * screenRectSize.y,
            localPosition.z);
    }

    public override void CleanObjects()
    {
        base.CleanObjects();
        this.dirts.Clear();
    }
}