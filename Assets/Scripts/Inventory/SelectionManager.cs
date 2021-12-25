using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventoryManager = null;
    [SerializeField] private TMP_Text _itemNameTxt = null;
    private static Vector2 _cursorPosition;
    private Item _currentItem = null;
    private Item _selectedItem = null;
    private bool _selectionActive = false;

    public Vector2 CursorPosition
    {
        get { return _cursorPosition; }
        set { _cursorPosition = value; OnCursorMoved(); }
    }

    public void OnYPressed()
    {
        if(_selectionActive)
        {
            if(_selectedItem != null)
            {
                _inventoryManager.DeleteItem(_selectedItem);
                _selectedItem = null;
                _selectionActive = false;

            }
        }
        else
        {
            _inventoryManager.SpawnRandomItems();   
        }
        OnCursorMoved();
    }
    private void OnCursorMoved()
    {
        var selected = from item in _inventoryManager.Items
                        where item.ItemPosition == _cursorPosition
                        select item;
        _currentItem = selected.FirstOrDefault();   
        if (_currentItem != null)
        {
            _itemNameTxt.SetText(_currentItem.Name);
        }
        else
        {
            _itemNameTxt.SetText("Empty");
            _currentItem = null;
        }
    }

    public void OnXPressed()
    {
        OnItemSelected();
    }

    private void OnItemSelected()
    {
        if(_currentItem != null)
        {
            if(!_selectionActive)
            {
                _selectedItem = _currentItem;
                _selectionActive = true;
            }
            else
            {
                _inventoryManager.SwapItemPosition(_selectedItem, _currentItem, _cursorPosition);
                _selectionActive = false;
            }

        }
        else
        {
            if(_selectionActive)
            {
                _inventoryManager.SwapItemPosition(_selectedItem, _currentItem, _cursorPosition);
                _selectionActive = false;
            }
            else
            {
                Debug.Log("No Item to Select.");
            }

        }
        OnCursorMoved();
    }
    
}
