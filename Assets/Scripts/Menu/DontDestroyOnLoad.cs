using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
   protected virtual void Awake()
   {
      DontDestroyOnLoad(this.gameObject);
   }
}
