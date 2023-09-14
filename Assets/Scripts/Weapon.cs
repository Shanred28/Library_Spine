using UnityEngine;

namespace Test
{ 
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private float _riteFire = 2f;
        [SerializeField] protected float _distanceFire;

        protected RaycastHit2D hit;

        protected AudioSource _audioSource;
        private float _timerFire;
        protected bool _isNotRedy;

        private void Start()
        {
            _audioSource = transform.root.GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_timerFire > 0)
            {
                _timerFire -= Time.deltaTime;
                _isNotRedy = true;
            }
            else
                _isNotRedy = false;
        }

        protected virtual  void GunFire()
         {
            _timerFire = _riteFire;

            Ray2D ray = new Ray2D(transform.position, transform.right);
            hit = Physics2D.Raycast(transform.position, transform.right, _distanceFire);
        }
    }
}
