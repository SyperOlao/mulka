using JetBrains.Annotations;
using Player.Attack;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Interfaces
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private string weaponName;
        [SerializeField] private Collider weaponCollider;
        [SerializeField] private int damage;
        [SerializeField] private Sprite icon;
        [SerializeField] private float attackSpeed;
        [SerializeField] private Animator animator;
        [SerializeField] protected Transform weaponPosition;
        [SerializeField] [CanBeNull] protected GameObject weaponObject;
        protected PlayerAttackStateMachine StateMachine;

        public void Initialize(PlayerAttackStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        
        protected void EquipWeapon(GameObject newWeapon)
        {
            if (newWeapon == null || weaponObject == null) return;
            weaponObject = newWeapon;
            weaponObject.transform.SetParent(weaponPosition);
            weaponObject.transform.localPosition = Vector3.zero;
            weaponObject.transform.localRotation = Quaternion.identity;
        }
        
        public string WeaponName { get; set; }
        protected Animator Animator => animator;
        protected Collider WeaponCollider => weaponCollider;

        public int Damage => damage;

        public Sprite Icon => icon;

        public float AttackSpeed => attackSpeed;

        public abstract void OnAttack(InputAction.CallbackContext context);
        public abstract void EndAttack();
    }
}