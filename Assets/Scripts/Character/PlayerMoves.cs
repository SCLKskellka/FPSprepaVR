using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Character
{
    public class PlayerMoves : MonoBehaviour
    {
        [FormerlySerializedAs("player")]
        [Header("Movement")]
        [SerializeField] private GameObject _player;
        [SerializeField] private int _defaultMoveSpeed;
        [SerializeField] private int _jumpForce;
        [SerializeField] private float _groundDrag;

        [Header("Ground Check")] 
        public bool Grounded;
        [SerializeField] private float _playerHeight;
        [SerializeField] private LayerMask _groundLayer;
    
        private Vector2 _moveInputValue;
        private Rigidbody _rb;
        private Transform _orientation;
        private bool _isMoving;
        private bool _isRuning;
        private int _currentMoveSpeed;

        private void Start() {
            _rb = _player.transform.GetComponent<Rigidbody>();
            _orientation = _player.transform;
            _currentMoveSpeed = _defaultMoveSpeed;
        }
        private void FixedUpdate() {
            PerformMove();
        }
        private void Update() {
            //ground check
            Grounded = Physics.Raycast(_orientation.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
            SpeedControl();
            //handle drag
            if (Grounded) {
                _rb.linearDamping = _groundDrag;
            }
            else {
                _rb.linearDamping = 0;
                /*if (_rb.velocity.y < 0) {
                Vector3 tmpVel = _rb.velocity;
                tmpVel.y *= 1.01f;
                _rb.velocity = tmpVel;
            }*/
            }
        }
        private void OnMove(InputValue value)
        {
            _moveInputValue = value.Get<Vector2>();
        }
        public void OnRun() {
            if (_isMoving && _isRuning)
            {
                _currentMoveSpeed = _defaultMoveSpeed;
                _isRuning = false;
            }
            else if (_isMoving && _isRuning == false)
            {
                _currentMoveSpeed *= 2;
                _isRuning = true;
            }
            //_currentMoveSpeed = _isMoving ? _currentMoveSpeed * 2 : defaultMoveSpeed;
        }
        private void PerformMove() {
            if (_moveInputValue != Vector2.zero) {
                _isMoving = true;
                Vector3 moveDirection = _orientation.forward * _moveInputValue.y + _orientation.right * _moveInputValue.x;
                _rb.AddForce(moveDirection.normalized * _currentMoveSpeed * 10f, ForceMode.Force);
            }
            else {
                _isMoving = false;
                _currentMoveSpeed = _defaultMoveSpeed;
            }
        }
        private void SpeedControl() {
            Vector3 flatVel = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
            //limit velocity if needed
            if (flatVel.magnitude > _currentMoveSpeed)
            {
                Vector3 limiteVel = flatVel.normalized * _currentMoveSpeed;
                _rb.linearVelocity = new Vector3(limiteVel.x, _rb.linearVelocity.y, limiteVel.z);
            }
        }
        // private void OnJump() {
        //     if(Grounded)transform.GetComponent <Rigidbody>().AddForce(transform.up * _jumpForce * 10f);
        // }
        
    }
}
