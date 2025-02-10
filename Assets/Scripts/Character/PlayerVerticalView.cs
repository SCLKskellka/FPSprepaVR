using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerVerticalView : MonoBehaviour
{
    [Range(-10f,10f)]public float VerticalSensitivity;
    
    [SerializeField] private GameObject _head;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _minimalVerticalRotLimit;
    [SerializeField] private float _maximalVerticalRotLimit;
    
    private Vector2 _rotationInputValue;
    private Quaternion _stockedVerticalRotation;
    private void Start() {
        _stockedVerticalRotation = _head.transform.localRotation;
    }
    private void FixedUpdate() {
        PerformVerticalRotation();
        //Debug.Log("vew: "+ _stockedVerticalRotation);
    }
    private void OnLook(InputValue value)
    {
        _rotationInputValue = value.Get<Vector2>();
    }
    private void PerformVerticalRotation() {
        if(VerticalSensitivity >= 0)_stockedVerticalRotation.x += _rotationInputValue.y * (_rotationSpeed * VerticalSensitivity)* (-1);// ; //reverse value
        else _stockedVerticalRotation.x += _rotationInputValue.y * (_rotationSpeed / (VerticalSensitivity * -1)) * (-1);// ; //reverse value
        _stockedVerticalRotation.x = Mathf.Clamp(_stockedVerticalRotation.x, _minimalVerticalRotLimit, _maximalVerticalRotLimit); //limitation de la rotation
        _head.transform.localRotation = Quaternion.Euler(_stockedVerticalRotation.x, _stockedVerticalRotation.y, _stockedVerticalRotation.z);
    }
}
