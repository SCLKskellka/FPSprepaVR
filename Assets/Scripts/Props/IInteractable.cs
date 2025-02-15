using UnityEngine;

namespace Props
{
    public interface IInteractable
    {
        public void Take(Transform newParent);
        public void Drop();
        public void Throw(float power);
    }
}
