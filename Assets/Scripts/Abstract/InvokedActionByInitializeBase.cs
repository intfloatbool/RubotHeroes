using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InvokedActionByInitializeBase : MonoBehaviour, IActionByInitialize
{
    public abstract void OnInitialized(Player player);
}
