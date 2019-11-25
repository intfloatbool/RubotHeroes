using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleEnvironmentRandomizer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spaceRenderer;
    [SerializeField] private Transform _environmentParent;
    
    [System.Serializable]
    private class EnvironmentPreset
    {
        public GameObject EnvironmentPrefab;
        public Sprite SpaceSprite;

        public void Create(SpriteRenderer rend, Transform parent)
        {
            if (rend != null)
            {
                rend.sprite = SpaceSprite;
            }

            if (parent != null)
            {
                Instantiate(EnvironmentPrefab, parent);
            }
        }
    }
    
    [SerializeField] private List<EnvironmentPreset> _presets = new List<EnvironmentPreset>();

    private void Start()
    {
        GenerateRandomEnvironment();
    }

    private void GenerateRandomEnvironment()
    {
        if (!_presets.Any())
            return;
        EnvironmentPreset randPreset = _presets[Random.Range(0, _presets.Count)];
        randPreset?.Create(_spaceRenderer, _environmentParent);
    }
}
