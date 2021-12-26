using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Size")]
    [SerializeField] private int _rows;
    public int Rows { get { return _rows; } }

    [SerializeField] private int _columns;
    public int Columns { get { return _columns;} }

    [SerializeField] private float _scale;
    public float Scale { get { return _scale; } }

    public float Height { get { return (_scale * _rows) - _scale; } }
    public float Width { get { return (_scale * _columns) - _scale; } }

    [Header("Positions")]
    [SerializeField] private List<Vector2> _slots = new List<Vector2>();
    public List<Vector2> Slots { get { return _slots;} }




    private void Awake()
    {
        InitializeGrid();
    }


    private void InitializeGrid()
    {
        //vector2.zero
        Vector2 pos = new Vector3(-(Width * 0.5f), -(Height * 0.5f));
        for (int i = 0; i < _columns; i++)
        {
            for (int j = 0; j < _rows; j++)
            {
                GridSlot slot = new GridSlot(pos);
                _slots.Add(slot.Position);
                pos.y += _scale;
            }

            pos.x += _scale;
            pos.y = -(Height * 0.5f);
        }
    }

}
