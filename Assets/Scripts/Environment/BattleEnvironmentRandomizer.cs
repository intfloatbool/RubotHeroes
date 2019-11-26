using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleEnvironmentRandomizer : MonoBehaviour
{
    [SerializeField] private Transform _environmentParent;
    
    [System.Serializable]
    private class EnvironmentPreset
    {
        public GameObject EnvironmentPrefab;
        public Material SkyBoxMat;

        public void Create(Transform parent)
        {
            if (SkyBoxMat != null)
            {
                RenderSettings.skybox = SkyBoxMat;
            }

            if (parent != null && EnvironmentPrefab != null)
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
        randPreset?.Create(_environmentParent);
    }
}
