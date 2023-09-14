using DigitalRuby.LightningBolt;
using System.Collections;
using UnityEngine;

namespace Test
{
    public class LightningGun : Weapon
    {
        [SerializeField] private float _timeLifeProjectile;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioClip _audioClipShoot;

        protected override void GunFire()
        {
            base.GunFire();           
            
            if (hit  && hit.transform.root.TryGetComponent<Enemy>(out var enemy))
            {
                Instantiate(_particleSystem, transform.position, Quaternion.identity);
                var obj = Instantiate (Resources.Load("LightningBoltAnimatedPrefab", typeof(GameObject))) as GameObject;
                var projectile = obj.GetComponent<LightningBoltScript>();
                projectile.StartPosition = transform.position;
                projectile.EndPosition = hit.point ;
                _audioSource.PlayOneShot(_audioClipShoot);
                StartCoroutine(WaitForSeconds(obj, enemy));
            }                    
        }

        public bool Fire()
        {
            if (_isNotRedy)
                return false;
            else
            {              
                GunFire();
                return true;
            }           
        }

        IEnumerator WaitForSeconds(GameObject gameObject, Enemy enemy)
        {
            yield return new WaitForSeconds(_timeLifeProjectile);
            enemy.OnDead();
            Destroy(gameObject);          
        }
    }
}

