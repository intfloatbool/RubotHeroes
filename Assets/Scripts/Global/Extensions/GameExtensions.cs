﻿using System.Collections.Generic;
using UnityEngine;

namespace Global.Extensions
{
    public static class GameExtensions 
    {
        public static void Shuffle<T>(this IList<T> list)  
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;
                int k = Random.Range(0, n);
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }
    }
}
