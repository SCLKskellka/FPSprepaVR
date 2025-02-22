using System;
using UnityEngine;
using Manager;

namespace Props
{
    public class RiddleUnlock : MonoBehaviour
    {
        [SerializeField] private string _riddleID;
        [SerializeField] private int _colorCaptorNeeds;

        private Renderer _meshRenderer;
        private Collider _collider;
        private int _colorCaptorComplete;

        private void Awake()
        {
            _meshRenderer = transform.GetComponent<MeshRenderer>();
            _collider = transform.GetComponent<Collider>();
        }
        private void OnEnable()
        {
            GameEventManager.OnRiddleComplete += Unlock;
            GameEventManager.OnRiddleReset += Reset;
            GameEventManager.OnColorRiddleComplete += ColorUnlock;
            GameEventManager.OnColorRiddleReset += ColorReset;
        }
        private void OnDisable()
        {
            GameEventManager.OnRiddleComplete -= Unlock;
            GameEventManager.OnRiddleReset -= Reset;
            GameEventManager.OnColorRiddleComplete -= ColorUnlock;
            GameEventManager.OnColorRiddleReset -= ColorReset;
        }
        private void Unlock(string riddleID)
        {
            if(riddleID == _riddleID)
            {
                _meshRenderer.enabled = false;
                _collider.enabled = false;
            }
        }
        private void ColorUnlock(string riddleID)
        {
            if (riddleID == _riddleID)
            {
                _colorCaptorComplete += 1;
                if (_colorCaptorComplete == _colorCaptorNeeds)
                {
                    _meshRenderer.enabled = false;
                    _collider.enabled = false;
                }
            }
        }
        private void Reset(string riddleID)
        {
            if(riddleID == _riddleID)
            {
                _meshRenderer.enabled = true;
                _collider.enabled = true;
            }
        }
        private void ColorReset(string riddleID, bool isCompleted)
        {
            if(riddleID == _riddleID)
            {
                if(isCompleted)
                    _colorCaptorComplete -= 1;
                if (_colorCaptorComplete != _colorCaptorNeeds)
                {
                    _meshRenderer.enabled = true;
                    _collider.enabled = true; 
                }
            }
        }
    }
}
