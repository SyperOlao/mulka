using System;
using Common.Enums;
using UnityEngine;

namespace Player
{
    public class PlayerCondition:MonoBehaviour
    {
        private bool _isAttack;
        private Animator _animator;
        private readonly int _attackHash = Animator.StringToHash(PlayerAnimatorEnum.IsAttack);

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public bool IsAttack
        {
            get => _isAttack;
            set
            {
                _animator.SetBool(_attackHash, value);
                _isAttack = value;
            }
        }
    }
}