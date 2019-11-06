using System.Collections.Generic;
using UnityEngine;

public class RobotColorInitializer : InvokedActionByInitializeBase
{
    [SerializeField] private List<MeshRenderer> _meshRenderes;

    public void InitializeColor(Color color)
    {
        foreach (MeshRenderer rend in _meshRenderes)
        {
            Material mat = rend.materials[0];
            mat.SetColor("_Color", color);
        }
    }

    public override void OnInitialized(Player player)
    {
        InitializeColor(player.Color);
    }
}
