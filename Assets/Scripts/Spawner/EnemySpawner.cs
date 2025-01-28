using System.Collections;
using Enemy.Health;
using Enemy.Health.FaceCamera;
using Enemy.Health.FollowTarget;
using UI.Battle;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;


namespace Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private int poolMaxSize = 10;
        [SerializeField] private int poolCapacity = 2;
        [SerializeField] private Canvas healthBarCanvas;
        [SerializeField] private GameObject healthBar;


        private NavMeshTriangulation _triangulation;
        private ObjectPool<GameObject> _pool;
        private ObjectPool<GameObject> _poolHealthBar;

        private void Awake()
        {
            _triangulation = NavMesh.CalculateTriangulation();
            _pool = new ObjectPool<GameObject>(
                createFunc: () => OnCreate(healthBar),
                actionOnGet: ActionOnGet,
                actionOnRelease: ActionOnRelease,
                actionOnDestroy: Destroy,
                collectionCheck: true,
                defaultCapacity: poolCapacity,
                maxSize: poolMaxSize
            );
            
            
            _poolHealthBar = new ObjectPool<GameObject>(
                createFunc: () => OnCreate(healthBar),
                actionOnGet: ActionOnGet,
                actionOnRelease: ActionOnRelease,
                actionOnDestroy: Destroy,
                collectionCheck: true,
                defaultCapacity: poolCapacity,
                maxSize: poolMaxSize
            );
        }

        private GameObject OnCreate(GameObject prefab)
        {
            var obj = Instantiate(prefab, ChooseRandomPointOnNavMesh(_triangulation), Quaternion.identity);

            var healthBarObject = Instantiate(healthBar, transform);

            var navMeshAgent = obj.GetComponent<NavMeshAgent>();
            var enemy = obj.GetComponent<Enemy.Enemy>();
            var followTarget = healthBarObject.GetComponent<FollowTarget>();
            followTarget.Target = navMeshAgent.transform;
            followTarget.Offset = new Vector3(0, 1.5f, 0);
            var healthProgressBar = healthBarObject.GetComponent<HealthProgressBar>();
            enemy.Health.HealthProgressBar = healthProgressBar;
            SetupHealthBar(healthBarCanvas, playerCamera, healthBarObject);

            navMeshAgent.enabled = true;
            return obj;
        }


        private void SetupHealthBar(Canvas canvas, Camera camera1, GameObject healthBarInstance)
        {
            healthBarInstance.transform.SetParent(canvas.transform);
            if (healthBarInstance.TryGetComponent(out FaceCamera faceCamera))
            {
                faceCamera.Camera = camera1;
            }
        }

        private static Vector3 ChooseRandomPointOnNavMesh(NavMeshTriangulation triangulation)
        {
            var firstVertex = Random.Range(0, triangulation.vertices.Length);
            var secondVertex = Random.Range(0, triangulation.vertices.Length);

            return Vector3.Lerp(triangulation.vertices[firstVertex], triangulation.vertices[secondVertex],
                Random.value);
        }

        private IEnumerator SpawnAgents()
        {
            var wait = new WaitForSeconds(1f / poolCapacity);
            for (var i = 0; i < poolMaxSize; i++)
            {
                GetObject();
                yield return wait;
            }
        }

        private static void ActionOnGet(GameObject obj)
        {
            obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obj.SetActive(true);
        }

        public void ActionOnRelease(GameObject obj)
        {
            obj.SetActive(false);
        }

        private void Start()
        {
            StartCoroutine(SpawnAgents());
        }

        private void GetObject()
        {
            if (Random.value > 0.5f)
            {
                _pool.Get();
            }
            else
            {
                _poolHealthBar.Get();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // Выбираем пул, из которого был объект
            if (other.gameObject.name.Contains(enemyPrefab.name))
            {
                _pool.Release(other.gameObject);
            }
            else if (other.gameObject.name.Contains(healthBar.name))
            {
                _poolHealthBar.Release(other.gameObject);
            }
        }
    }
}