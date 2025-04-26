using System;
using System.Collections.Generic;
using Common.Enums;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

namespace Interfaces
{
    [RequireComponent(typeof(MeshCollider))]
    public abstract class EnemyWeapon: MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float hitInterval = 1f;  
        [SerializeField] private Animator animator;

        private Collider _weaponCollider;
        private readonly HashSet<IPlayer> _alreadyHit = new();
        private readonly Collider[] _hitsBuffer = new Collider[10];
        public event Action AttackAnimationEnded;
        public int Damage => damage;
        private Coroutine _attackRoutine;
        private string _tag;
        private void Awake()
        {
            _weaponCollider = GetComponent<Collider>();
            if (_weaponCollider == null)
                Debug.LogError("Weapon collider missing!");
        }
        
        protected void StartAttackPhase(int attackTrigger, string tag)
        {
            _tag = tag;
            animator.ResetTrigger(attackTrigger);
            animator.SetTrigger(attackTrigger);
            _attackRoutine = StartCoroutine(WaitForAnimationEnd());
        }

     
        protected void EndAttackPhase(int attackTrigger)
        {
            
            Debug.Log("EndAttackPhase");
            AttackAnimationEnded?.Invoke();
            animator.ResetTrigger(attackTrigger);       
            _alreadyHit.Clear();
        }
        
        private IEnumerator WaitForAnimationEnd()
        {
            yield return new WaitForSeconds(1.3f);
           
            EndAnimation();
        }
        public void CheckForHits()
        {
            var bounds = _weaponCollider.bounds;
            var center = bounds.center;
            var halfSize = bounds.extents;

            var count = Physics.OverlapBoxNonAlloc(center, halfSize, _hitsBuffer, Quaternion.identity);
            for (var i = 0; i < count; i++)
            {
                if (!_hitsBuffer[i].TryGetComponent<IPlayer>(out var player)) continue;
                
                if (_alreadyHit.Add(player))
                {
                    player.OnTakeDamage(damage);
                }
            }
        }

        public abstract void OnAttack();
        public abstract void EndAnimation();
    }
}