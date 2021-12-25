using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Item : MonoBehaviour
{
    [SerializeField] private ItemSO _itemScriptableObject = null;
    public ItemSO ItemScriptableObject { get { return _itemScriptableObject; } set { _itemScriptableObject = value; } }
    private Image _myImage = null;
    public string Name { get { return this.transform.name; } }
    public Vector2 ItemPosition { get { return (Vector2)this.transform.localPosition; } }
    
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

}
