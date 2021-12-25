using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    
    private UiInputActions _playerInput;

    private InputAction _yButton;
    private InputAction _moveUpDown;
    private InputAction _moveLeftRight;
    private InputAction _xButton;
    private InputAction _BumperButtons;
    
    public UnityEvent<float> OnMoveUpDown = new UnityEvent<float>();
    public UnityEvent<float> OnMoveleftRight = new UnityEvent<float>();
    public UnityEvent OnPressY = new UnityEvent();
    public UnityEvent OnPressX = new UnityEvent();
    public UnityEvent<int> OnPressBumper = new UnityEvent<int>();

    private void Awake()
    {
        _playerInput = new UiInputActions();

    }

    private void OnEnable()
    {
        _yButton = _playerInput.Player.YButton;
        _yButton.Enable();
        _playerInput.Player.YButton.performed += OnYButtonPress;
        _playerInput.Player.YButton.Enable();

        _xButton = _playerInput.Player.XButton;
        _xButton.Enable();
        _playerInput.Player.XButton.performed += OnXButtonPress;
        _playerInput.Player.XButton.Enable();

        _moveUpDown = _playerInput.Player.MoveUpDown;
        _moveUpDown.Enable();
        _playerInput.Player.MoveUpDown.performed += SelectorUpDown;
        _playerInput.Player.MoveUpDown.Enable();        
        
        _moveLeftRight = _playerInput.Player.MoveLeftRight;
        _moveLeftRight.Enable();
        _playerInput.Player.MoveLeftRight.performed += SelectorLeftRight;
        _playerInput.Player.MoveLeftRight.Enable();


        _BumperButtons = _playerInput.Player.Bumpers;
        _BumperButtons.Enable();
        _playerInput.Player.Bumpers.performed += OnBumpersPressed;
        _playerInput.Player.Bumpers.Enable();
    }


    private void OnDisable()
    {
        _yButton.Disable();
        _playerInput.Player.YButton.Disable();

        _xButton.Disable();
        _playerInput.Player.XButton.Disable();

        _moveUpDown.Disable();
        _playerInput.Player.MoveUpDown.performed -= SelectorUpDown;
        
        _moveLeftRight.Disable();
        _playerInput.Player.MoveLeftRight.performed -= SelectorLeftRight;

        _BumperButtons.Disable();
        _playerInput.Player.Bumpers.performed -= OnBumpersPressed;
    }

    private void OnXButtonPress(InputAction.CallbackContext obj)
    {
        OnPressX?.Invoke();
    }
    private void OnYButtonPress(InputAction.CallbackContext obj)
    {
        OnPressY?.Invoke();
    }

    private void SelectorLeftRight(InputAction.CallbackContext obj)
    {
        OnMoveleftRight?.Invoke(Mathf.Round(_moveLeftRight.ReadValue<float>()));
    }

    private void SelectorUpDown(InputAction.CallbackContext obj)
    {
        OnMoveUpDown?.Invoke(Mathf.Round(_moveUpDown.ReadValue<float>()));
    }


    private void OnBumpersPressed(InputAction.CallbackContext obj)
    {
        OnPressBumper?.Invoke((int)Mathf.Round(_BumperButtons.ReadValue<float>()));
    }

}
