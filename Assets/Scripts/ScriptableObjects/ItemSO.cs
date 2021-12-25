using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items")]
public class ItemSO : ScriptableObject 
{
    [SerializeField] private string _itemName;
    public string ItemName{ get { return _itemName; } }
    [SerializeField] private Sprite _icon;
    public Sprite Icon { get { return _icon;} }

}
