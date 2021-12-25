using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorMovement : MonoBehaviour
{
    [SerializeField] private const float _moveAmount = 19f;
    [SerializeField] private GridManager _inventoryGridManager;
    private RectTransform _myRT = null;
    public Vector2 SelectorPosition {get { return (Vector2)this.transform.localPosition;} }
    private Vector2 _gridLimits;


    void Start()
    {
        _myRT = GetComponent<RectTransform>();
        _gridLimits = new Vector2(_inventoryGridManager.Width * 0.5f, _inventoryGridManager.Height * 0.5f);
        _myRT.localPosition = new Vector2(-(_gridLimits.x), _gridLimits.y);
    }

    private void OnMovedSelector()
    {
        //
    }

    public void MoveLeftRight(float x)
    {
        float amount = _moveAmount * x;
        _myRT.transform.localPosition += new Vector3(amount, 0, 0);
        if (_myRT.transform.localPosition.x < -_gridLimits.x)
        {
            _myRT.transform.localPosition = new Vector2(_gridLimits.x, _myRT.transform.localPosition.y);
        }
        else if(_myRT.transform.localPosition.x > _gridLimits.x)
        {
            _myRT.transform.localPosition = new Vector2(-_gridLimits.x, _myRT.transform.localPosition.y);
        }
    }    
    
    public void MoveUpDown(float y)
    {
        float amount = _moveAmount * y;
        _myRT.transform.localPosition += new Vector3(0, amount, 0);
        if (_myRT.transform.localPosition.y > _gridLimits.y)
        {
            _myRT.transform.localPosition = new Vector2(_myRT.transform.localPosition.x,-_gridLimits.y);
        }
        else if (_myRT.transform.localPosition.y < -_gridLimits.y)
        {
            _myRT.transform.localPosition = new Vector2(_myRT.transform.localPosition.x,_gridLimits.y);
        }
    }

}
