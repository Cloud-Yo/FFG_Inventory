using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    [Header("Items")]
    [SerializeField] private List<ItemSO> _inventoryItems = null;
    [SerializeField] private int _amountOfItems = 5;
    private List<Item> _items = new List<Item>();
    public List<Item> Items { get { return _items; } }

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
                Destroy(item.gameObject);
            }
            _items.Clear();
        }
        List<ItemSO> randomItems = new List<ItemSO>((List<ItemSO>)Utilities.RandomItems(_inventoryItems, _amountOfItems, false));
        List<Vector2> slots = new List<Vector2>((List<Vector2>)Utilities.RandomItems(GridManager.Instance.Slots, _amountOfItems));

        for (int i = 0; i < _amountOfItems; i++)
        {
            GameObject newItem = new GameObject(randomItems[i].ItemName);
            newItem.transform.SetParent(this.transform, false);
            newItem.transform.localPosition = slots[i];
            Item item = newItem.AddComponent<Item>();
            item.ItemScriptableObject = randomItems[i];
            _items.Add(item);
        }

    }

    public void SwapItemPosition(Item selected, Item current, Vector2 pos)
    {
        Vector2 NewPosition = new Vector2();
        NewPosition = pos;
        Vector2 OldPosition = new Vector2();
        OldPosition = selected.transform.localPosition;
        int selectedIdx = _items.IndexOf(selected);
        if (current != null)
        {
            NewPosition = current.transform.localPosition;
            int currentIdx = _items.IndexOf(current);
            _items[currentIdx].SetNewPosition(OldPosition);
        }

        _items[selectedIdx].SetNewPosition(NewPosition);
    }

    public void DeleteItem(Item item)
    {
        if (_items.Contains(item))
        {
            Destroy(item.gameObject);
            _items.Remove(item);
        }
    }

    public void PingItemLocation(Vector2 pos)
    {
        foreach (var item in _items)
        {
            item.PingPosition(pos);
        }
    }

}
