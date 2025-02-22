using System;
using Manager;
using UnityEngine;

namespace Riddle
{
    public enum ColorCaptor
    {
        Red,
        Green,
        Blue
    }
    public class StayContactColoredCaptor : MonoBehaviour
    {
        [SerializeField] private string _riddleID;
        [SerializeField] private float _timerValue;
        [SerializeField] private bool _isRiversed;
        [SerializeField] private ColorCaptor _colorCaptor;
        

        private Renderer _myRenderer;
        private float _currentTime;
        private bool _isTimerStarted;
        private bool _isCompleted;

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
            if (_currentTime <= 0 && _isCompleted == false)
            {
                RiddleColorComplete();
                _isCompleted = true;
            }
        }
        private void OnCollisionEnter(Collision other)
        {
            switch (_colorCaptor)
            {
                case ColorCaptor.Red:
                    if(other.transform.CompareTag("RedPuzzle") && _isRiversed == false)
                    {
                        ResetColorRiddle();
                    }
                    if(other.transform.CompareTag("RedPuzzle") && _isRiversed)
                    {
                        _isTimerStarted = true;
                    }
                    break;
                case ColorCaptor.Green:
                    if(other.transform.CompareTag("GreenPuzzle") && _isRiversed == false)
                    {
                        ResetColorRiddle();
                    }
                    if(other.transform.CompareTag("GreenPuzzle") && _isRiversed)
                    {
                        _isTimerStarted = true;
                    }
                    break;
                case ColorCaptor.Blue:
                    if(other.transform.CompareTag("BluePuzzle") && _isRiversed == false)
                    {
                        ResetColorRiddle();
                    }
                    if(other.transform.CompareTag("BluePuzzle") && _isRiversed)
                    {
                        _isTimerStarted = true;
                    }
                    break;
            }
        }
        private void OnCollisionExit(Collision other)
        {
            switch (_colorCaptor)
            {
                case ColorCaptor.Red:
                    if(other.transform.CompareTag("RedPuzzle") && _isRiversed == false)
                    {
                        _isTimerStarted = true;
                    }
                    if(other.transform.CompareTag("RedPuzzle") && _isRiversed)
                    {
                        ResetColorRiddle();
                    }
                    break;
                case ColorCaptor.Green:
                    if(other.transform.CompareTag("GreenPuzzle") && _isRiversed == false)
                    {
                        _isTimerStarted = true;
                    }
                    if(other.transform.CompareTag("GreenPuzzle") && _isRiversed)
                    {
                        ResetColorRiddle();
                    }
                    break;
                case ColorCaptor.Blue:
                    if(other.transform.CompareTag("BluePuzzle") && _isRiversed == false)
                    {
                        _isTimerStarted = true;
                    }
                    if(other.transform.CompareTag("BluePuzzle") && _isRiversed)
                    {
                        ResetColorRiddle();
                    }
                    break;
            }
        }
        private void RiddleColorComplete()
        {
            GameEventManager.OnColorRiddleComplete.Invoke(_riddleID);
        }
        private void ResetColorRiddle()
        {
            //Debug.Log("riddle reset");
            GameEventManager.OnColorRiddleReset.Invoke(_riddleID,_isCompleted);
            _isTimerStarted = false;
            _currentTime = _timerValue;
            _isCompleted = false;
        }
    }
}
