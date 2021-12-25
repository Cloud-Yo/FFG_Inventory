using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Utilities 
{
    /// <summary>
    /// Returns a List of randomly selected objects.  
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection">Collection of objects to select from</param>
    /// <param name="amount">Number of objects to select</param>
    /// <param name="unique">Wheteher or not each item returned should be unique (default = true)</param>
    /// <returns></returns>
    public static IEnumerable RandomItems<T>(IEnumerable<T> collection, int amount, bool unique = true)
    {
        List<T> temp = new List<T>(collection);
        List<T> result = new List<T>();
        for (int i = 0; i < amount; i++)
        {
            int index = Random.Range(0, temp.Count);
            result.Add(temp[index]);
            if(unique)
            {
                temp.RemoveAt(index);
            }
        }
        return result;
    }
}
