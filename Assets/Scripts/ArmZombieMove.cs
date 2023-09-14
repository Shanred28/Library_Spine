using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    //Enable move arm zombi on loc night.
    public class ArmZombieMove : MonoBehaviour
    {
        private List< Transform> _armsZombi = new List<Transform>();
        [SerializeField] private float _speed = 0.1f;
        [SerializeField] private float _timeChangeDirectionsMoveArm = 3f;

        private bool _isActive = false;
        private void Start()
        {
            TriggerSwitchLoc.triggerPlayerEnter.AddListener(OnMove);
            foreach (Transform child in transform)
            {
                _armsZombi.Add (child);
            }
        }

        private void Update () 
        {
            if (_isActive)
            {               
                MoveArm();
            }   
        }

        private void MoveArm()
        {
            foreach (Transform child in _armsZombi)
            {
                child.position += new Vector3(0, _speed * Time.deltaTime, 0);
            }               
        }

        private void ChangeDirectionSpeed()
        {
            _speed *= -1;
        }

        private void OnMove()
        {
            _isActive = true;
            InvokeRepeating("ChangeDirectionSpeed", _timeChangeDirectionsMoveArm, _timeChangeDirectionsMoveArm);
            TriggerSwitchLoc.triggerPlayerEnter.RemoveListener(OnMove);
        }
    }
}

