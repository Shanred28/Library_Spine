using UnityEngine;

namespace Test
{
    public class MoonTracking : MonoBehaviour
    {
        private Vector3 offset = new Vector3(1, 0, 0);
        private bool _isActive = false;

        private void Start () 
        {
            TriggerSwitchLoc.triggerPlayerEnter.AddListener(OnTracking);
        }

        private void Update()
        {   
            if(_isActive)
               transform.position += offset * Time.deltaTime;
        }

        private void OnTracking()
        {
            _isActive = true;
            TriggerSwitchLoc.triggerPlayerEnter.RemoveListener(OnTracking);
        }
    }
}

