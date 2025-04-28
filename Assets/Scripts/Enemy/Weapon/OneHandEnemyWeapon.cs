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
        private readonly string _attackTagHash = EnemyAnimatorEnum.Attack;

        public override void OnAttack()
        {
            StartAttackPhase(_attackHash, _attackTagHash);
        }

  
        
        public override void EndAnimation()
        {
            EndAttackPhase(_attackHash);
        }
        
        
    }
}