using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoSingleton<SelectionManager>
{
    [SerializeField] private SelectedIcon _selectedItemImage = null;
    [SerializeField] private ParticleSystem _cursorDeletePS = null;
    private Vector2 _selectorPosition;
    public Vector2 SelectorPosition { get{return _selectorPosition;} set{_selectorPosition = value;}}
    private Item _selectedItem;
    private Item _currentItem;
    public Item CurrentItem { get { return _currentItem; } set { _currentItem = value; } }
    private bool _selectionActive = false;

    public void OnXPressed()
    {
        _currentItem = null;
        InventoryManager.Instance.PingItemLocation(_selectorPosition);
        if (_currentItem != null)
        {
            if (!_selectionActive)
            {
                _selectedItem = _currentItem;
                _selectionActive = true;
                _selectedItem.gameObject.SetActive(false);
                UIManager.Instance.SetSelectedSlot(true, _selectedItem.transform.localPosition);
                UIManager.Instance.SetItemName(_currentItem.Name);
                _selectedItemImage.ChangeImage(_selectedItem.ItemScriptableObject.Icon);
                AudioManager.Instance.PlaySFX("selectedItem");
            }
            else
            {
                InventoryManager.Instance.SwapItemPosition(_selectedItem, _currentItem, _selectorPosition);
                _selectedItem.gameObject.SetActive(true);
                _selectedItemImage.DisableImage();
                UIManager.Instance.SetSelectedSlot(false, Vector2.zero);
                UIManager.Instance.SetItemName("");
                _selectionActive = false;
                AudioManager.Instance.PlaySFX("swapItems");
            }
            SelectorPosition = _selectorPosition;
        }
        else
        {
            if (_selectionActive)
            {
                InventoryManager.Instance.SwapItemPosition(_selectedItem, _currentItem, _selectorPosition);
                _selectedItem.gameObject.SetActive(true);
                _selectedItemImage.DisableImage();
                UIManager.Instance.SetSelectedSlot(false, Vector2.zero);
                UIManager.Instance.SetItemName("");
                _selectionActive = false;
                AudioManager.Instance.PlaySFX("swapItems");
                SelectorPosition = _selectorPosition;
            }
            else
            {
                UIManager.Instance.SetItemName("Empty Slot");
                AudioManager.Instance.PlaySFX("noSelection");
            }

        }
    }

    public void OnYPressed()
    {
        if (_selectionActive)
        {
            if (_selectedItem != null)
            {
                UIManager.Instance.SetItemName($"Removed Item");
                InventoryManager.Instance.DeleteItem(_selectedItem);
                _selectedItem = null;
                _selectionActive = false;
                _selectedItemImage.DisableImage();
                UIManager.Instance.SetSelectedSlot(false, Vector2.zero);
                AudioManager.Instance.PlaySFX("delete");
                _cursorDeletePS.Emit(1);

            }
        }
        else
        {
            InventoryManager.Instance.SpawnRandomItems();
            AudioManager.Instance.PlaySFX("randomItems");
            UIManager.Instance.SetItemName("A Fresh Start");
            SelectorPosition = _selectorPosition;
        }
    }
}
