using System;
using UI.Battle;
using UnityEngine;

namespace Enemy.Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField, Min(1)] private float maxHealth = 100f;
        private HealthProgressBar _healthProgressBar ;
        private bool _isHealthProgressBarAssigned;
        public float MaxHealth => maxHealth;
        public float CurrentHealth { get; private set; }
        public event Action<float> Changed;
        public HealthProgressBar HealthProgressBar
        {
            set => _healthProgressBar = value;
        }
     

        private void Start()
        {
            CurrentHealth = maxHealth;
            if (_healthProgressBar != null)
            {
                _isHealthProgressBarAssigned = true;
            }
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, maxHealth);
            if (!_isHealthProgressBarAssigned) return;
            if (_healthProgressBar.isActiveAndEnabled)
            {
                _healthProgressBar.SetProgress(CurrentHealth / maxHealth);         
            }
          
            Changed?.Invoke(damage);
        }
    }
}