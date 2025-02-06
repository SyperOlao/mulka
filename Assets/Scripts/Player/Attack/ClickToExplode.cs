using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Attack
{
    public class ClickToExplode: MonoBehaviour
    {
        [SerializeField] private Camera Camera;
        [SerializeField] private ParticleSystem ParticleSystemPrefab;
        public int MaxHits = 25;
        public float Radius = 10f;
        public LayerMask HitLayer;
        public LayerMask BlockExplosionLayer;
        public int MaxDamage = 50;
        public int MinDamage = 10;
        public float ExplosiveForce;

        private Collider[] Hits;

        private void Awake()
        {
            Hits = new Collider[MaxHits];
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Ray ray = Camera.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (!Physics.Raycast(ray, out var hit)) return;
                
                
                Instantiate(ParticleSystemPrefab, hit.point, Quaternion.identity);
                var hits = Physics.OverlapSphereNonAlloc(hit.point, Radius, Hits, HitLayer);

                for (var i = 0; i < hits; i++)
                {
                    var distance = Vector3.Distance(hit.point, Hits[i].transform.position);
                    var damage = Mathf.FloorToInt(Mathf.Lerp(MaxDamage, MinDamage, distance / Radius));
                    if (Hits[i].TryGetComponent(out Rigidbody rigidbody) && !Physics.Raycast(hit.point, 
                        (Hits[i].transform.position - hit.point).normalized, distance, BlockExplosionLayer.value))
                    {
                        rigidbody.AddExplosionForce(ExplosiveForce, hit.point, Radius);
                    }
                    if (Hits[i].TryGetComponent(out IDamageable damageable))
                    {
                        damageable.OnTakeDamage(damage);
                    }
                }
            }
        }
    }
}