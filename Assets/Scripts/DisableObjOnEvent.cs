using UnityEngine;

namespace Test
{
    // Disable on event player on trigger enter. 
    public class DisableObjOnEvent : MonoBehaviour
    {
        private void Start()
        {
            TriggerSwitchLoc.triggerPlayerEnter.AddListener(Off);
        }

        private void Off()
        { 
            enabled = false;
            TriggerSwitchLoc.triggerPlayerEnter.RemoveListener(Off);
        }
    }
}

