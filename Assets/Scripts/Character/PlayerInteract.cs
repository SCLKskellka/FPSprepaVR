using Props;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private Transform _playerCam;
        [SerializeField] private Transform _anchorItem;

        private bool _isCarrying;

        private void OnInteract()
        {
            //Debug.Log("Interact");
            Interaction();
        }
        private void Interaction()
        {
            if (_isCarrying)
            {
                _anchorItem.GetChild(0).GetComponent<IInteractable>().Drop();
                _isCarrying = false;
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(_playerCam.position, _playerCam.TransformDirection(Vector3.forward), out hit, 2, Physics.AllLayers,
                        QueryTriggerInteraction.Ignore))
                {
                    if(hit.transform.TryGetComponent(out IInteractable interactable))
                    {
                        //Debug.Log("TakeObject");
                        hit.transform.GetComponent<IInteractable>().Take(_anchorItem);
                        _isCarrying = true;
                    }
                    else
                    {
                        Debug.Log(hit.transform.gameObject.name);
                    }
                }
                else
                {
                    //Debug.Log("No object in range");
                }
            }
        }
    }
}
