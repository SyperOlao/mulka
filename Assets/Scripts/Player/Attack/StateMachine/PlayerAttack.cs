using System.Collections;
using System.Collections.Generic;
using Common.Enums;
using Common.Utils;
using Interfaces;
using Player.Movement.StateMachine;
using UnityEngine;

namespace Player.Attack.StateMachine
{
    public class PlayerAttack : PlayerBaseAttackState
    {
        private readonly int _attackHash = Animator.StringToHash(PlayerAnimatorEnum.IsAttack);
        private float _timer = 1f;
        private readonly HashSet<IDamageable> _enemiesInRange = new();
        private readonly Collider[] _hitColliders = new Collider[10];
        private const float CollisionDelay = 0.2f;

        public PlayerAttack(PlayerAttackStateMachine state) : base(state)
        {
        }


        public override void Enter()
        {
            StateMachine.Animator.SetInteger(_attackHash, 1);
        }

        public override void Tick()
        {
            _timer -= Time.deltaTime;
            CheckForDamageableInBox();
            if (_timer <= 0)
            {
                StateMachine.SwitchState(new PlayerIdleAttackState(StateMachine));
            }
        }


        private void CheckForDamageableInBox()
        {
            var fistLeftCollider = StateMachine.FistLeftCollider;
            var boxCenter = fistLeftCollider.transform.position;
            var boxSize = fistLeftCollider.bounds.size;

            var colliderCount = Physics.OverlapBoxNonAlloc(boxCenter, boxSize / 2, _hitColliders, Quaternion.identity);

            for (var i = 0; i < colliderCount; i++)
            {
                if (!_hitColliders[i].TryGetComponent(out IDamageable damageable)) continue;
                if (_enemiesInRange.Add(damageable))
                {
                    Damage(damageable);
                }
            }
        }

        private void Damage(IDamageable enemy)
        {
            enemy.OnTakeDamage(StateMachine.Damage);
        }
        


        public override void Exit()
        {
            StateMachine.Animator.SetInteger(_attackHash, 4);
            EndDamage();
        }

        private void EndDamage()
        {
            foreach (var enemy in _enemiesInRange)
            {
                enemy.EndDamage();
            }
        }
    }
}