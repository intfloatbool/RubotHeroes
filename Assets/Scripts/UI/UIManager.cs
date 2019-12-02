using System;
using System.Collections.Generic;
using UI.Enum;
using UnityEngine;

public class UIManager : SingletonDoL<UIManager>
{
    [SerializeField] private bool isLoadFromPlayerPrefs;
    [SerializeField] private List<UIType> _enabledUI;
    protected override UIManager GetLink() => this;

    private void Start()
    {
        if (isLoadFromPlayerPrefs)
            LoadDefaultsEnabledUIFromPlayerPrefs();
       
        GameScenes.OnSceneLoaded += DisableAllUnusingUIonScene;
    }

    private void LoadDefaultsEnabledUIFromPlayerPrefs()
    {
        List<UIType> keys = new List<UIType>();
        int uiCount = Enum.GetNames(typeof(UIType)).Length;
        for (int i = 0; i < uiCount; i++)
        {
            UIType uiType = (UIType) i;
            int existingKey = PlayerPrefs.GetInt(uiType.ToString(), -1);
            if (existingKey == 1)
            {
                keys.Add(uiType);
            }
        }

        _enabledUI = keys;
    }

    private void DisableAllUnusingUIonScene(Scenes scene)
    {
        CustomizableUI[] userInterfaces = FindObjectsOfType<CustomizableUI>();
        foreach (CustomizableUI ui in userInterfaces)
        {
            if(_enabledUI.Contains(ui.UiType))
                continue;
            if (!ui.gameObject.activeInHierarchy)
                continue;
            ui.SetActive(false);
            Debug.Log($"Ui by type {ui.UiType} disabling!");
        }
    }
}
