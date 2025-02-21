using System;
using UnityEngine;

namespace Manager
{
    public class GameEventManager : MonoBehaviour
    {
        public static Action<string> OnRiddleComplete;
        public static Action<string> OnColorRiddleComplete;
        public static Action<string> OnRiddleReset;
        public static Action<string> OnColorRiddleReset;
    }
}
