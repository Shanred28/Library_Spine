using UnityEngine;
using UnityEngine.Events;

namespace Test
{
    public class TriggerFinish : MonoBehaviour
    {
        public static UnityEvent FinishTriggerEvent = new UnityEvent();
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.TryGetComponent<Player>(out var player))
                FinishTriggerEvent?.Invoke();
        }
    }
}

