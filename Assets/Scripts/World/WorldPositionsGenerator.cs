using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public Vector3 GetRandomPosExcept(IEnumerable<Vector3> exceptions)
    {
        Vector3[] exceptionPositions = _randomPositions.Where(p => exceptions.All(e => e != p)).ToArray();
        return exceptionPositions[Random.Range(0, exceptionPositions.Length)];
    }

    private void GenerateRandomPositions()
    {
        Vector2 fieldSize = new Vector2Int(-6, 6);
        for (int x = (int) fieldSize.x; x < fieldSize.y + 1; x++)
        {
            for (int z = (int) fieldSize.x; z < fieldSize.y + 1; z++)
            {
                _randomPositions.Add(new Vector3(x, 0, z)); 
            }
        }
    }

    public Vector3 GetLongestPosition(Vector3 target)
    {
        Vector3 groundedTarget = new Vector3(
            target.x, 0, target.z);
        Vector3 longestPosition = Vector3.zero;
        float maxDistance = 0;
        _randomPositions.ForEach(p =>
        {
            float distance = Vector3.Distance(p, groundedTarget);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                longestPosition = p;
            }
        });

        return longestPosition;
    }
}
