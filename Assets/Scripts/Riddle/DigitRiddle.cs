using System;
using Manager;
using TMPro;
using UnityEngine;

namespace Riddle
{
    public class DigitRiddle : MonoBehaviour
    {
        [SerializeField] private TMP_Text _firstDigitDisplay;
        [SerializeField] private TMP_Text _secondDigitDisplay;
        [SerializeField] private TMP_Text _thirdDigitDisplay;
        [SerializeField] private string _riddleID;
        
        private int _firstDigit;
        private int _secondDigit;
        private int _thirdDigit;

        private void Awake()
        {
            SetDisplayDigits();
        }
        private void SetDisplayDigits()
        {
            _firstDigitDisplay.text = _firstDigit.ToString();
            _secondDigitDisplay.text = _secondDigit.ToString();
            _thirdDigitDisplay.text = _thirdDigit.ToString();
        }
        private bool CodeChecker()
        {
            return _firstDigit == 5 && _secondDigit == 1 && _thirdDigit == 2;
        }
        private int IncrementDigit(int digit)
        {
            if (digit < 9)
                return digit += 1;
            else
            {
                return 0;
            }
        }
        
        public void IncrementFirstDigit()
        {
            _firstDigit = IncrementDigit(_firstDigit);
            SetDisplayDigits();
            if (CodeChecker())
            {
                GameEventManager.OnRiddleComplete.Invoke(_riddleID);
            }
            else
            {
                GameEventManager.OnRiddleReset.Invoke(_riddleID);
            }
        }
        public void IncrementSecondDigit()
        {
            _secondDigit = IncrementDigit(_secondDigit);
            SetDisplayDigits();
            if (CodeChecker())
            {
                GameEventManager.OnRiddleComplete.Invoke(_riddleID);
            }
            else
            {
                GameEventManager.OnRiddleReset.Invoke(_riddleID);
            }
        }
        public void IncrementThirdDigit()
        {
            _thirdDigit = IncrementDigit(_thirdDigit);
            SetDisplayDigits();
            if (CodeChecker())
            {
                GameEventManager.OnRiddleComplete.Invoke(_riddleID);
            }
            else
            {
                GameEventManager.OnRiddleReset.Invoke(_riddleID);
            }
        }
    }
}
