using System.Collections;
using Enemy.Health;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Battle
{
    public class HealthProgressBar : MonoBehaviour
    {
        [SerializeField] private Image progressImage;
        [SerializeField] private float defaultSpeed = 3f;
        [SerializeField] private Gradient colorGradient;
        [SerializeField] private UnityEvent<float> onProgress;
        [SerializeField] private UnityEvent onCompleted;


        private Coroutine _animationCoroutine;

        private void Start()
        {
            if (progressImage.type == Image.Type.Filled) return;
            enabled = false;
#if UNITY_EDITOR
            EditorGUIUtility.PingObject(gameObject);
#endif
        }

        // (health.CurrentHealth / health.MaxHealth
        public void SetProgress(float damage)
        {
            SetProgress(damage, defaultSpeed);
        }

        private void SetProgress(float progress, float speed)
        {
            if (progress is < 0 or > 1)
            {
                progress = Mathf.Clamp01(progress);
            }

            if (!(progress - progressImage.fillAmount < 0.001)) return;
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine = StartCoroutine(AnimateProgress(progress, speed));
        }

        private IEnumerator AnimateProgress(float progress, float speed)
        {
            var time = 0f;
            var initialProgress = progressImage.fillAmount;
            while (time < 1)
            {
                progressImage.fillAmount = Mathf.Lerp(initialProgress, progress, time);
                time += Time.deltaTime * speed;

                progressImage.color = colorGradient.Evaluate(1 - progressImage.fillAmount);
                onProgress?.Invoke(progressImage.fillAmount);
                yield return null;
            }

            progressImage.fillAmount = progress;
            progressImage.color = colorGradient.Evaluate(1 - progressImage.fillAmount);

            onProgress?.Invoke(progress);
            onCompleted?.Invoke();
        }
    }
}