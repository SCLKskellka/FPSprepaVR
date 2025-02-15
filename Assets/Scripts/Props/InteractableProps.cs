using System;
using UnityEngine;

namespace Props
{
    [RequireComponent(typeof(Rigidbody))]
    public class InteractableProps : MonoBehaviour, IInteractable
    {
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = transform.GetComponent<Rigidbody>();
        }

        public void Take(Transform newParent)
        {
            transform.parent = newParent;
            _rb.useGravity = false;
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            transform.position = newParent.position;
            transform.rotation = newParent.rotation;
        }

        public void Drop()
        {
            transform.parent = null;
            _rb.useGravity = true;
            _rb.constraints = RigidbodyConstraints.None;
        }

        public void Throw(float power)
        {
            transform.parent = null;
            _rb.useGravity = true;
            _rb.constraints = RigidbodyConstraints.None;
            _rb.AddForce(Vector3.forward * power, ForceMode.Impulse);
        }
    }
}
