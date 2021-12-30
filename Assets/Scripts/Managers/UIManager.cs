using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private TMP_Text _itemNameText = null;
    [SerializeField] private Image _selectedSlotImage = null;

    private void Start()
    {
        _selectedSlotImage.enabled = false;
    }

    public void SetItemName(string name)
    {
        _itemNameText.SetText(name);
    }

    public void SetSelectedSlot(bool enabled, Vector2 pos)
    {
        if (enabled)
        {
            _selectedSlotImage.transform.localPosition = pos;
            _selectedSlotImage.enabled = true;
        }
        else
        {
            _selectedSlotImage.enabled = false; 
        }
    }
}
