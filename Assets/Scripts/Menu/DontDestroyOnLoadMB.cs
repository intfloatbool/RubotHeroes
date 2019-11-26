using UnityEngine;

public class DontDestroyOnLoadMB : MonoBehaviour
{
   protected virtual void Awake()
   {
      GameObject.DontDestroyOnLoad(gameObject);
   }
}
