using System.Collections;
using Common.Enums;
using Interfaces;
using UnityEngine.InputSystem;
using UnityEngine;


namespace Enemy.Weapon
{
    public class OneHandEnemyWeapon: EnemyWeapon
    {
        
        private readonly int _attackHash = Animator.StringToHash(EnemyAnimatorEnum.EnemyAttack);

        public override void OnAttack()
        {
            AttackLogic();
        }

        private IEnumerator ResetAnimation()
        {
            yield return new WaitForSeconds(GetClipTime());
            
        }

        public override void StartAnimation()
        {
            Debug.Log("StartAnimation");
            Animator.SetBool(_attackHash, true);
        }
        
        public override void EndAnimation()
        {
            Debug.Log("EndAnimation");
            Animator.SetBool(_attackHash, false);
        }
        
        
    }
}