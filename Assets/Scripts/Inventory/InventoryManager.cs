using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GridManager _gridManager = null;

    [Header("Items")]
    [SerializeField] private List<ItemSO> _inventoryItems = null;
    [SerializeField] private int _amountOfItems = 5;
    private List<GameObject> _items = new List<GameObject>();

    private void Start()
    {
        SpawnRandomItems();
    }

    public void SpawnRandomItems()
    {
        if (_items.Count > 0)
        {
            foreach (var item in _items)
            {
                Destroy(item);
            }
            _items.Clear();
        }
        List<ItemSO> randomItems = new List<ItemSO>((List<ItemSO>)Utilities.RandomItems(_inventoryItems, _amountOfItems, false));
        List<Vector2> slots = new List<Vector2>((List<Vector2>)Utilities.RandomItems(_gridManager.Slots, _amountOfItems));
       
        for (int i = 0; i < _amountOfItems; i++)
        {
            GameObject newItem = new GameObject(randomItems[i].ItemName);
            newItem.transform.SetParent(this.transform, false);
            newItem.transform.localPosition = slots[i];
            Item item = newItem.AddComponent<Item>();
            item.ItemScriptableObject = randomItems[i];
            _items.Add(newItem);
        }

        /*
        for (int i = 0; i < _amountOfItems; i++)
        {
            GameObject newItem = Instantiate(new GameObject(randomItems[i].ItemName));
            newItem.transform.SetParent(this.transform, false);
            newItem.transform.localPosition = slots[i];
            Item item = newItem.AddComponent<Item>();
            item.ItemScriptableObject = randomItems[i];
            _items.Add(newItem);
        }
         */
    }
}
