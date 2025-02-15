using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Character {
    public class PlayerHorizontalRotation : MonoBehaviour {
        [Range(-10f,10f)]public float HorizontalSensitivity;
        [HideInInspector]public Vector2 RotationInputValue;
        
        [SerializeField] private GameObject _player;
        [SerializeField] private float _rotationSpeed;
        
        private Quaternion _stockedHorizontalRotation;
        private Rigidbody _rb;

        private void Start()
        {
            _rb = _player.GetComponent<Rigidbody>();
            _stockedHorizontalRotation = _player.transform.localRotation;
        }

        private void FixedUpdate() {
            PerformHorizontalRotation();
        }

        private void OnLook(InputValue value)
        {
            RotationInputValue = value.Get<Vector2>();
        }
        
        private void PerformHorizontalRotation() {
            if(HorizontalSensitivity >= 0)_stockedHorizontalRotation.y += RotationInputValue.x * (_rotationSpeed * HorizontalSensitivity);
            else _stockedHorizontalRotation.y += RotationInputValue.x * (_rotationSpeed / (HorizontalSensitivity * -1)) ;
            Quaternion rotation = new Quaternion(0, _rb.rotation.y * _stockedHorizontalRotation.y, 0, 0);
            _player.transform.localRotation = Quaternion.Euler(_stockedHorizontalRotation.x,
                _stockedHorizontalRotation.y, _stockedHorizontalRotation.z).normalized;
        }
    }
}
