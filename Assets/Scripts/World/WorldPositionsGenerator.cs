﻿using System.Collections.Generic;
using UnityEngine;

public class WorldPositionsGenerator : MonoBehaviour
{
    public static WorldPositionsGenerator Instance { get; private set; }
    [SerializeField] private List<Vector3> _randomPositions;
    public List<Vector3> RandomPositions
    {
        get => _randomPositions;
    }

    public Vector3 RandomPosition
    {
        get { return _randomPositions[Random.Range(0, _randomPositions.Count)]; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        GenerateRandomPositions();
    }

    private void GenerateRandomPositions()
    {
        Vector2 fieldSize = new Vector2Int(-5, 5);
        for (int x = (int) fieldSize.x; x < fieldSize.y + 1; x++)
        {
            for (int z = (int) fieldSize.x; z < fieldSize.y + 1; z++)
            {
                _randomPositions.Add(new Vector3(x, 0, z)); 
            }
        }
    }
}
