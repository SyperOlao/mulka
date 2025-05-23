﻿using System.Collections;
using System.Collections.Generic;
using Common.Enums;
using Common.Utils;
using Enemy.Movement.StateMachine;
using Interfaces;
using Player.Movement.StateMachine;
using UnityEngine;

namespace Player.Attack.StateMachine
{
    public class PlayerMeleeAttack : PlayerBaseAttackState
    {
        private readonly HashSet<IDamageable> _enemiesInRange = new();
        private readonly Collider[] _hitColliders = new Collider[10];
        private readonly Collider[] _playerWeaponColliders;
        public PlayerMeleeAttack(PlayerAttackStateMachine state, Collider[] collider) : base(state)
        {
            _playerWeaponColliders = collider;
        }


        public override void Enter()
        {
        }

        public override void Tick()
        {
            foreach (var playerWeaponCollider in _playerWeaponColliders)
            {
                CheckForDamageableInBox(playerWeaponCollider);      
            }
          
        }


        private void CheckForDamageableInBox(Collider playerWeaponCollider)
        {
          
            var boxCenter = playerWeaponCollider.transform.position;
            var boxSize = playerWeaponCollider.bounds.size;

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
            enemy.OnTakeDamage(StateMachine.Weapon.Damage);
        }
        


        public override void Exit()
        {
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