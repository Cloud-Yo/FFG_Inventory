using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Item : MonoBehaviour, ISelectable
{

    [SerializeField] private ItemSO _itemScriptableObject = null;
    public ItemSO ItemScriptableObject { get { return _itemScriptableObject; } set { _itemScriptableObject = value; } }
    private Image _myImage = null;
    public string Name { get { return _itemScriptableObject.ItemName; } }
    

    void Start()
    {
        _myImage = GetComponent<Image>();
        _myImage.sprite = _itemScriptableObject.Icon;
        _myImage.raycastTarget = false;
        _myImage.SetNativeSize();
    }

    public void SetNewPosition(Vector2 pos)
    {
        transform.localPosition = pos;
    }

    public void PingPosition(Vector2 position)
    {
        if (position == (Vector2)this.transform.localPosition)
        {
            SelectionManager.Instance.CurrentItem = this;
        }
    }


}
