using System;
using Manager;
using UnityEngine;

namespace Riddle
{
    public class StayContactCaptor : MonoBehaviour
    {
        [SerializeField] private string _riddleID;
        [SerializeField] private Material _complete;
        [SerializeField] private Material _uncomplete;
        [SerializeField] private float _timerValue;
        [SerializeField] private bool _isRiversed;
        

        private Renderer _myRenderer;
        private float _currentTime;
        private bool _isTimerStarted;

        private void Awake()
        {
            _myRenderer = transform.GetComponent<Renderer>();
        }
        private void Start()
        {
            _isTimerStarted = false;
            _currentTime = _timerValue;
        }
        private void Update()
        {
            if (_isTimerStarted && _currentTime > 0)
                _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
            {
                _isTimerStarted = false;
                RiddleComplete();
            }
        }
        private void OnCollisionEnter(Collision other)
        {
            if(other.transform.CompareTag("Puzzle") && _isRiversed == false)
            {
                ResetRiddle();
            }
            if(other.transform.CompareTag("Puzzle") && _isRiversed)
            {
                _isTimerStarted = true;
            }
        }
        private void OnCollisionExit(Collision other)
        {
            if(other.transform.CompareTag("Puzzle") && _isRiversed == false)
            {
                _isTimerStarted = true;
            }
            if(other.transform.CompareTag("Puzzle") && _isRiversed)
            {
                ResetRiddle();
            }
        }
        private void RiddleComplete()
        {
            GameEventManager.OnRiddleComplete.Invoke(_riddleID);
            _myRenderer.material = _complete;
        }
        private void ResetRiddle()
        {
            GameEventManager.OnRiddleReset.Invoke(_riddleID);
            _myRenderer.material = _uncomplete;
            _isTimerStarted = false;
            _currentTime = _timerValue;
        }
    }
}
