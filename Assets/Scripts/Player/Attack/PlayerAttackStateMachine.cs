using UnityEngine;

namespace Player.Attack
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAttackStateMachine: Common.StateMachine.StateMachine
    {
        [SerializeField, Min(0)] private int damage;
        [SerializeField] private float attackSpeed;
        
        public int Damage => damage;
        
        
        
    }
}