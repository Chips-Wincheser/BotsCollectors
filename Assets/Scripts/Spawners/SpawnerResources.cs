using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


[RequireComponent(typeof(BoxCollider))]
public class SpawnerResources: MonoBehaviour
{
    [SerializeField] private Resource _prefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<Resource> _pool;

    private float _spawnInterval = 3f;
    private float _halfSizeFactor = 2f;
    private BoxCollider _platformCollider;
    private WaitForSeconds _WaitForSeconds;

    private List<Resource> _activeObjects;

    private void Awake()
    {
        _pool= new ObjectPool<Resource>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => Create(obj),
            actionOnRelease: (obj) => TurnOff(obj),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );

        _activeObjects = new List<Resource>();
        _WaitForSeconds = new WaitForSeconds(_spawnInterval);
        _platformCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private void Create(Resource obj)
    {
        Vector3 platformSize = _platformCollider.size;
        Vector3 platformCenter = _platformCollider.center;

        float randomX = Random.Range(-platformSize.x / _halfSizeFactor, platformSize.x / _halfSizeFactor);
        float randomZ = Random.Range(-platformSize.z / _halfSizeFactor, platformSize.z / _halfSizeFactor);
        float positionY = 0f;

        Vector3 localOffset = new Vector3(randomX, positionY, randomZ) + platformCenter;

        Vector3 worldPosition = transform.TransformPoint(localOffset);

        obj.transform.position = worldPosition;
        obj.gameObject.SetActive(true);
        _activeObjects.Add(obj);
    }

    private void TurnOff(Resource obj)
    {
        obj.gameObject.SetActive(false);
        _activeObjects.Remove(obj);
    }

    private void Spawn()
    {
        _pool.Get();
    }

    public void PutInPool(Resource obj)
    {
        _pool.Release(obj);
    }

    private IEnumerator Spawner()
    {
        while (enabled)
        {
            if (_activeObjects.Count < _poolMaxSize)
                Spawn();

            yield return _WaitForSeconds;
        }
    }
}
