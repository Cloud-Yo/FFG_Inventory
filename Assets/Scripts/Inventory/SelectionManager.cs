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
    [SerializeField] private TMP_Text _itemNameTxt = null;
    [SerializeField] private MovableIcon _itemSelectedIcon = null;
    [SerializeField] private ParticleSystem _cursorDeletePS = null;
    [Header("Audio")]
    private AudioSource _audioSource = null;
    [SerializeField] private AudioClip _selectedClip = null;
    [SerializeField] private AudioClip _noSelectionClip = null;
    [SerializeField] private AudioClip _deletedClip = null;
    [SerializeField] private AudioClip _swapClip = null; 
    [SerializeField] private AudioClip _randomItemsClip = null;   

    private static Vector2 _cursorPosition;
    private Item _currentItem = null;
    private Item _selectedItem = null;
    private bool _selectionActive = false;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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
                _inventoryManager.DeleteItem(_selectedItem);
                _selectedItem = null;
                _selectionActive = false;
                _itemSelectedIcon.DisableImage();
                PlayClip(_deletedClip);
                _cursorDeletePS.Emit(1);

            }
        }
        else
        {
            _inventoryManager.SpawnRandomItems();
            PlayClip(_randomItemsClip);
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
                _selectedItem.gameObject.SetActive(false);
                _itemSelectedIcon.ChangeImage(SelectedItemSprite());
                PlayClip(_selectedClip);
            }
            else
            {
                _inventoryManager.SwapItemPosition(_selectedItem, _currentItem, _cursorPosition);
                _selectedItem.gameObject.SetActive(true);
                _itemSelectedIcon.DisableImage();
                _selectionActive = false;
                PlayClip(_swapClip);
            }

        }
        else
        {
            if(_selectionActive)
            {
                _inventoryManager.SwapItemPosition(_selectedItem, _currentItem, _cursorPosition);
                _selectedItem.gameObject.SetActive(true);
                _itemSelectedIcon.DisableImage();
                _selectionActive = false;
                PlayClip(_swapClip);
            }
            else
            {
                Debug.Log("No Item to Select.");
                PlayClip(_noSelectionClip);
            }

        }
        OnCursorMoved();
    }

    private Sprite SelectedItemSprite()
    {
        if(_selectionActive)
        {
            return _selectedItem.ItemScriptableObject.Icon;
        }
        return null;
    }
    
    private void PlayClip(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip); 
    }
}
