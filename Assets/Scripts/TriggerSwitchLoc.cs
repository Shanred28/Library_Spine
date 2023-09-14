using UnityEngine;
using UnityEngine.Events;

namespace Test
{
    // Invoke UnityEvent on enter player trigger.
    [RequireComponent(typeof(BoxCollider2D))]
    public class TriggerSwitchLoc : MonoBehaviour
    {

        public static UnityEvent triggerPlayerEnter = new UnityEvent();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.TryGetComponent<Player>(out var player))
            {
                triggerPlayerEnter?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.root.TryGetComponent<Player>(out var player))
            { 
                enabled = false;
            }
        }
    }
}

