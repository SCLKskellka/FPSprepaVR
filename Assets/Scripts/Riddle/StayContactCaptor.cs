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
    public class StayContactCaptor : MonoBehaviour
    {
        [SerializeField] private string _riddleID;
        [SerializeField] private Material _complete;
        [SerializeField] private Material _uncomplete;
        [SerializeField] private float _timerValue;
        [SerializeField] private bool _isRiversed;
        [SerializeField] private bool _isColorCaptor;
        [SerializeField] private ColorCaptor _colorCaptor;
        

        private Renderer _myRenderer;
        private float _currentTime;
        private bool _isTimerStarted;

        private void Awake()
        {
            _myRenderer = transform.GetComponent<Renderer>();
        }
        private void Start()
        {
            if(_isColorCaptor == false)
            {
                _myRenderer.material = _uncomplete;
            }
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
                if (_isColorCaptor)
                {
                    RiddleColorComplete();
                }
                else
                {
                    RiddleComplete();
                }
            }
        }
        private void OnCollisionEnter(Collision other)
        {
            if (_isColorCaptor)
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
            else
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
        }
        private void OnCollisionExit(Collision other)
        {
            if (_isColorCaptor)
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
            else
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
           
        }
        private void RiddleComplete()
        {
            GameEventManager.OnRiddleComplete.Invoke(_riddleID);
            _myRenderer.material = _complete;
        }
        private void RiddleColorComplete()
        {
            GameEventManager.OnColorRiddleComplete.Invoke(_riddleID);
        }
        private void ResetRiddle()
        {
            GameEventManager.OnRiddleReset.Invoke(_riddleID);
            _myRenderer.material = _uncomplete;
            _isTimerStarted = false;
            _currentTime = _timerValue;
        }
        private void ResetColorRiddle()
        {
            GameEventManager.OnColorRiddleReset.Invoke(_riddleID);
            _isTimerStarted = false;
            _currentTime = _timerValue;
        }
    }
}
