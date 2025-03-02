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
        [SerializeField] [CanBeNull] protected GameObject weaponObject = null;
        [SerializeField] [CanBeNull] protected Transform weaponSpawner = null;
        protected PlayerAttackStateMachine StateMachine;
        public void Initialize(PlayerAttackStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        
        public virtual void EquipWeapon()
        {
   
        }

        protected void EquipWeapon(GameObject newWeapon, Vector3 localPosition, Vector3 localRotation)
        {
            if (newWeapon == null || weaponObject == null) return;
            weaponObject.SetActive(true);
            weaponObject = newWeapon;
            weaponObject.transform.SetParent(weaponPosition);
            weaponObject.transform.localPosition = localPosition;
            weaponObject.transform.localRotation = Quaternion.Euler(localRotation);
        }

        public void UnEquipWeapon()
        {
            if (weaponObject == null) return;
            if (weaponSpawner == null) return;
            weaponObject.transform.SetParent(weaponSpawner);

            weaponObject.transform.position = weaponSpawner.position;
            weaponObject.transform.rotation = weaponSpawner.rotation;

            weaponObject.SetActive(false);
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