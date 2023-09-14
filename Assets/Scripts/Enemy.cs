using Spine.Unity;
using UnityEngine;

namespace Test
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(AudioSource))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _speedMove = 1;

        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioClip _audioClipAngry;

        private Rigidbody2D _rigidbody2D;
        private bool _isRun = false;
        private AudioSource _audioSource;

        // Arrow string name sceleton skin.
        private string[] _sckins = { "blue", "orange", "teal" };

        // Animations sceleton.
        private string _idleAnimation = "idle";
        private string _runAnimation = "run";
        private string _angryAnimation = "angry";
        private string _winAnimation = "win";


        private SkeletonAnimation _skeletonAnimation;
        private Spine.AnimationState _animationState;

        #region Unity Event
        private void Start()
        {
            _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            _skeletonAnimation.skeleton.SetSkin(_sckins[Random.Range(0,3)]);
            _animationState = _skeletonAnimation.AnimationState;
            _rigidbody2D = GetComponentInChildren<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
            _animationState.SetAnimation(0, _idleAnimation, true);
        }

        //Reaction to the player by trigger.
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.TryGetComponent<Player>(out var player))
            {
                AngryAnimation();
                _audioSource.PlayOneShot(_audioClipAngry);
                RunAnimation();              
                _isRun = true;
            }
        }

        private void FixedUpdate()
        {
            if (_isRun)
                MoveRun();
        }
        #endregion

        #region Metods
        private void RunAnimation()
        {
            _animationState.SetAnimation(0, _runAnimation, true);
        }

        private void AngryAnimation()
        {
            _animationState.SetAnimation(1, _angryAnimation, false);
        }

        private void WinAnimation()
        {
            _animationState.SetAnimation(0,_winAnimation , true);
        }

        private void MoveRun()
        {
            Vector3 currentVelocity = _rigidbody2D.velocity;
            Vector3 rightVelocity = -transform.right  * _speedMove;
            _rigidbody2D.velocity = Vector3.Lerp(currentVelocity, rightVelocity, Time.fixedDeltaTime);
        }
        #endregion

        #region public API
        public void OnDead()
        {
            var offset = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
            Instantiate(_particleSystem, offset, Quaternion.identity);
            Destroy(gameObject);
        }

        public void StopZombiWin()
        {
            WinAnimation();
            _isRun = false;
        }
        #endregion
    }
}

