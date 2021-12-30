using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridSlot
{
    private Vector2 _position;
    public Vector2 Position { get { return _position; }}

    public GameObject InventoryItem { get; set; } = null;

    public GridSlot(Vector2 pos)
    {
        _position = pos;

    }

}
