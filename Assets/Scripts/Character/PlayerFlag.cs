using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlag : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    public void InitializeFlag(Color color)
    {
        this._meshRenderer.materials[0].SetColor("_Color", color);
    }
}
