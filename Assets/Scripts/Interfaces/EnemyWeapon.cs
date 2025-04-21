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
        [SerializeField] private string weaponName;
        [SerializeField] private int damage;
        [SerializeField] private float attackSpeed;
        [SerializeField] private Animator animator;
        private GameObject _weaponObject;
        
        private HashSet<IPlayer> _enemiesInRange = new();
        private readonly Collider[] _hitColliders = new Collider[10];
        private readonly Collider[] _playerWeaponColliders;
        
        private Collider _weaponCollider;
        private AnimatorStateInfo _stateInfo;
        public int Damage => damage;
        protected Animator Animator => animator;
        protected float AttackSpeed => attackSpeed;
        
        private void Awake()
        {
            _weaponCollider = GetComponent<MeshCollider>();
            if (_weaponCollider == null)
            {
                Debug.LogError("Missing MeshCollider on weapon!");
            }
            _weaponObject = gameObject;
            _weaponObject.SetActive(true);
            
        }
        
        private IEnumerator WaitAndEndAttack()
        {
            yield return null;
            
            var clips = animator.GetCurrentAnimatorClipInfo(1);
            if (clips.Length == 0)
            {
                yield break;
            }

            var clip = clips[0].clip;
            var duration = clip.length / animator.speed;
            yield return new WaitForSeconds(duration);
            
            EndAttack();
        }

        public float GetClipTime()
        {
            var clips = animator.GetCurrentAnimatorClipInfo(1);
            if (clips.Length == 0)
            {
                return 0;
            }

            var clip = clips[0].clip;
            return clip.length / animator.speed;
        }

        
        private void CheckForDamageableInBox()
        {
          
            var boxCenter = _weaponCollider.transform.position;
            var boxSize = _weaponCollider.bounds.size;

            var colliderCount = Physics.OverlapBoxNonAlloc(boxCenter, boxSize / 2, _hitColliders, Quaternion.identity);

            for (var i = 0; i < colliderCount; i++)
            {
                if (!_hitColliders[i].TryGetComponent(out IPlayer damageable)) continue;
                if (_enemiesInRange.Add(damageable))
                {
                    TakeDamage(damageable);
                }
            }
        }
        

        
        private void TakeDamage(IPlayer player)
        {
            player.OnTakeDamage(damage);
        }
        
        public abstract void OnAttack();

        protected virtual void AttackLogic()
        {
            CheckForDamageableInBox();

        }

        public abstract void StartAnimation();
        public abstract void EndAnimation();
        public void EndAttack()
        {
            Debug.Log("EndAttack");
            _enemiesInRange = new HashSet<IPlayer>();
        }
    }
}