using UnityEngine;

namespace Test
{
    public class MouseClick : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                if (hit && hit.transform.root.TryGetComponent<Enemy>(out var enemy))
                {
                    Player.Instance.Fire();
                }
            }
        }
    }
}

