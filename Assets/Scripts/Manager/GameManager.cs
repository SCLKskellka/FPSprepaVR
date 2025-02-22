using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
