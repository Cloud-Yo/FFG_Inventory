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

    /// <summary>
    /// Return a valid index while cycling through a collection
    /// </summary>
    /// <param name="size">The length of the collection</param>
    /// <param name="currentIndex">The current index of the collection</param>
    /// <param name="direction">the direction in which to cycle to the next item</param>
    /// <returns></returns>
    public static int CycleThroughCollection(int size, int currentIndex, int direction)
    {
        int index = 0;
        Debug.Log($"index received {currentIndex}");
        switch(direction)
        {
            case 1:
                {
                    if (currentIndex < (size - 1))
                    {
                          index = currentIndex + direction;
                    }
                    else if(currentIndex >= size -1)
                    {
                        return 0;
                    }
                }
                break;
                case -1:
                {
                    if(currentIndex > 0)
                    {
                        index = currentIndex + direction;
                    }
                    else if(currentIndex <= 0)
                    {
                        return size - 1;
                    }
                }
                break;
                default:
                break;
                
        }

        return index;
      
    }
}
