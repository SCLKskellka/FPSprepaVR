using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Camera.Character {
    public class PlayerHorizontalRotation : MonoBehaviour {
        [Range(-10f,10f)]public float HorizontalSensitivity;
        [HideInInspector]public Vector2 RotationInputValue;
        
        [SerializeField] private GameObject player;
        [SerializeField] private float rotationSpeed;
        
        private Quaternion _stockedHorizontalRotation;
        private Rigidbody _rb;

        private void Start()
        {
            _rb = player.GetComponent<Rigidbody>();
            _stockedHorizontalRotation = player.transform.localRotation;
        }

        private void FixedUpdate() {
            PerformHorizontalRotation();
        }

        private void OnLook(InputValue value)
        {
            RotationInputValue = value.Get<Vector2>();
        }
        
        private void PerformHorizontalRotation() {
            if(HorizontalSensitivity >= 0)_stockedHorizontalRotation.y += RotationInputValue.x * (rotationSpeed * HorizontalSensitivity)/** (-1)*/;
            else _stockedHorizontalRotation.y += RotationInputValue.x * (rotationSpeed / (HorizontalSensitivity * -1)) /** (-1)*/;
            //Debug.Log("_stockedHorizontalRotation.y: "+_stockedHorizontalRotation.y );
            Quaternion rotation = new Quaternion(0, _rb.rotation.y * _stockedHorizontalRotation.y, 0, 0);
            //Debug.Log("rotation input value: "+_rotationInputValue + " rotation: " + rotation.normalized);
            //_rb.MoveRotation(Quaternion.Euler(_stockedHorizontalRotation.x, _stockedHorizontalRotation.y, _stockedHorizontalRotation.z).normalized);
            player.transform.localRotation = Quaternion.Euler(_stockedHorizontalRotation.x,
                _stockedHorizontalRotation.y, _stockedHorizontalRotation.z).normalized;
        }
    }
}
