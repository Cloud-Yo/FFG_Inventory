using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    
    private UiInputActions _lootInput;
    private UiInputActions _movementInput;
    private InputAction _generateRandomLoot;
    private InputAction _moveUpDown;
    private InputAction _moveLeftRight;
    public UnityEvent<float> OnMoveUpDown = new UnityEvent<float>();
    public UnityEvent<float> OnMoveleftRight = new UnityEvent<float>();
    [SerializeField] private InventoryManager _inventoryManager = null;

    private void Awake()
    {
        _lootInput = new UiInputActions();
        _movementInput = new UiInputActions();

    }

    private void OnEnable()
    {
        _generateRandomLoot = _lootInput.Player.RegenerateRandomLoot;
        _generateRandomLoot.Enable();
        _lootInput.Player.RegenerateRandomLoot.performed += ResetLoot;
        _lootInput.Player.RegenerateRandomLoot.Enable();

        _moveUpDown = _movementInput.Player.MoveUpDown;
        _moveUpDown.Enable();
        _movementInput.Player.MoveUpDown.performed += SelectorUpDown;
        _movementInput.Player.MoveUpDown.Enable();        
        
        _moveLeftRight = _movementInput.Player.MoveLeftRight;
        _moveLeftRight.Enable();
        _movementInput.Player.MoveLeftRight.performed += SelectorLeftRight;
        _movementInput.Player.MoveLeftRight.Enable();
    }

    private void OnDisable()
    {
        _generateRandomLoot.Disable();
        _lootInput.Player.RegenerateRandomLoot.Disable();

        _moveUpDown.Disable();
        _movementInput.Player.MoveUpDown.performed -= SelectorUpDown;
        
        _moveLeftRight.Disable();
        _movementInput.Player.MoveLeftRight.performed -= SelectorLeftRight;
    }

    private void ResetLoot(InputAction.CallbackContext obj)
    {
        _inventoryManager.SpawnRandomItems();
    }

    private void SelectorLeftRight(InputAction.CallbackContext obj)
    {
        OnMoveleftRight?.Invoke(Mathf.Round(_moveLeftRight.ReadValue<float>()));
    }

    private void SelectorUpDown(InputAction.CallbackContext obj)
    {
        OnMoveUpDown?.Invoke(Mathf.Round(_moveUpDown.ReadValue<float>()));
    }

}
