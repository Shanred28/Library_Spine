using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Test
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : SingletonBase<Player>
    {
        [Header("Movement")]
        [SerializeField] private float _speedMove;

        [SerializeField] private LightningGun _turretGun;

        private Rigidbody2D _rigidbody2D;
        private bool _isRun = true;

        private SkeletonAnimation _skeletonAnimation;
        private Spine.AnimationState _animationState;

        private string _idleAnimation = "idle";
        private string _runAnimation = "run";
        private string _shootAnimation = "shoot";
        private string _looseAnimation = "loose";

        TrackEntry _trackShoot;
        TrackEntry _trackRun;

        public UnityEvent onDeadEvent = new UnityEvent();

        #region Unity Event
        private new void Awake()
        {
            base.Awake();
            _skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
            _animationState = _skeletonAnimation.AnimationState;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _trackRun = _animationState.SetAnimation(0, _runAnimation, true);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.root.TryGetComponent<Enemy>(out var enemy))
            {
                OnDead();
            }
        }

        private void FixedUpdate()
        {
            if (_isRun)
                Movement();
        }
        #endregion

        #region Motods
        private void Movement()
        {
            Vector3 currentVelocity = _rigidbody2D.velocity;
            Vector3 rightVelocity = transform.right * _speedMove;
            _rigidbody2D.velocity = Vector3.Lerp(currentVelocity, rightVelocity, Time.fixedDeltaTime);
        }

        private void RunAnimation()
        {
            _animationState.SetAnimation(0, _runAnimation, true);
        }

        private void OnDead()
        {
            onDeadEvent?.Invoke();
            _isRun = false;
            _skeletonAnimation.state.SetAnimation(0, _looseAnimation, false);
        }

        #endregion

        #region public API
        public void FinishLevelWin()
        {
            _isRun = false;
            _skeletonAnimation.state.SetAnimation(0, _idleAnimation, false);
        }

        public void Fire()
        {
            if (_turretGun.Fire())
            {
                _isRun = false;
                _trackShoot = _skeletonAnimation.state.SetAnimation(0, _shootAnimation, false);

                StartCoroutine(WaitForSpineAnimationEnd(_trackShoot));
            }
        }
        #endregion

        IEnumerator WaitForSpineAnimationEnd(TrackEntry trackEntry)
        {
            yield return new WaitForSpineAnimationComplete(trackEntry);
            _isRun = true;
            RunAnimation();
        }
    }
}

