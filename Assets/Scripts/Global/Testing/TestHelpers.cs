using System.Collections.Generic;
using UnityEngine;

public static class TestHelpers 
{
    public static void CheckLinks(this MonoBehaviour monoBehaviour, IEnumerable<Object> links)
    {
        foreach (Object link in links)
        {
            if(link == null)
                Debug.LogError($"Missing reference! {link.name}");
        }
    }
}
