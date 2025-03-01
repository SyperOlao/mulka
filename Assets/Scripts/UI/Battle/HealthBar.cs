using System;
using Enemy.Health;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject container;
        [SerializeField] private Slider mainSlider;
        [SerializeField] private Slider easeSlider;
        [SerializeField] private Health health;
        private const float LerpSpeed = 0.05f;
        private bool _isActive;
        private void Start()
        {
            var max = health.MaxHealth;
            mainSlider.maxValue = max;
            easeSlider.maxValue = max;
            mainSlider.value = max;
            easeSlider.value = max;
        }

        private void OnEnable()
        {
            health.TakeDamage += TakeDamage;
            _isActive = true;
        }

        private void OnDisable()
        {
            health.TakeDamage -= TakeDamage;
            _isActive = false;
        }

        public void Update()
        {
            if (_isActive)
            {
                easeSlider.value = Mathf.Lerp(easeSlider.value, health.CurrentHealth, LerpSpeed);
            }
        }


        private void TakeDamage()
        {
            mainSlider.value = health.CurrentHealth;
            if (health.CurrentHealth <= 0)
            {
                container.SetActive(false);
            }
        }
    }
}