using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventoryManager _inventoryManager = null;
    [SerializeField] private MovableIcon _itemSelectedIcon = null;
    [SerializeField] private ParticleSystem _cursorDeletePS = null;
    [SerializeField] private UIManager _uiManager = null;
    private AudioManager _audioManager = null;

    private static Vector2 _cursorPosition;
    private Item _currentItem = null;
    private Item _selectedItem = null;
    private bool _selectionActive = false;
    private void Start()
    {
        _audioManager = GetComponent<AudioManager>();
    }
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
                _uiManager.SetItemName($"Removed Item");
                _inventoryManager.DeleteItem(_selectedItem);
                _selectedItem = null;
                _selectionActive = false;
                _itemSelectedIcon.DisableImage();
                _uiManager.SetSelectedSlot(false, Vector2.zero);
                _audioManager.PlaySFX("delete");
                _cursorDeletePS.Emit(1);

            }
        }
        else
        {
            _inventoryManager.SpawnRandomItems();
            _audioManager.PlaySFX("randomItems");
            _uiManager.SetItemName("A Fresh Start");
        }
        //OnCursorMoved();
    }
    private void OnCursorMoved()
    {
        
        
        var selected = from item in _inventoryManager.Items
                        where item.ItemPosition == _cursorPosition
                        select item;
        
        _currentItem = selected.FirstOrDefault();
        
        if (!_selectionActive)
        {
            _uiManager.SetItemName("");
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
                _selectedItem.gameObject.SetActive(false);
                _uiManager.SetSelectedSlot(true, _selectedItem.ItemPosition);
                _uiManager.SetItemName(_currentItem.Name);
                _itemSelectedIcon.ChangeImage(SelectedItemSprite());
                _audioManager.PlaySFX("selectedItem");
            }
            else
            {
                _inventoryManager.SwapItemPosition(_selectedItem, _currentItem, _cursorPosition);
                _selectedItem.gameObject.SetActive(true);
                _itemSelectedIcon.DisableImage();
                _uiManager.SetSelectedSlot(false, Vector2.zero);
                _uiManager.SetItemName("");
                _selectionActive = false;
                _audioManager.PlaySFX("swapItems");
            }
            OnCursorMoved();
        }
        else
        {
            if(_selectionActive)
            {
                _inventoryManager.SwapItemPosition(_selectedItem, _currentItem, _cursorPosition);
                _selectedItem.gameObject.SetActive(true);
                _itemSelectedIcon.DisableImage();
                _uiManager.SetSelectedSlot(false, Vector2.zero);
                _uiManager.SetItemName("");
                _selectionActive = false;
                _audioManager.PlaySFX("swapItems");
                OnCursorMoved();
            }
            else
            {
                _uiManager.SetItemName("Empty Slot");
                _audioManager.PlaySFX("noSelection");
            }

        }
       
    }

    private Sprite SelectedItemSprite()
    {
        if(_selectionActive)
        {
            return _selectedItem.ItemScriptableObject.Icon;
        }
        return null;
    }
}
