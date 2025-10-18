using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

abstract public class SpawnerBase<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T _prefab;
    [SerializeField] protected int PoolCapacity = 5;
    [SerializeField] protected int PoolMaxSize = 5;

    private ObjectPool<T> _pool;

    public List<T> ActiveObjects { get; private set; }

    protected virtual void Awake()
    {
        _pool= new ObjectPool<T>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => Create(obj),
            actionOnRelease: (obj) => TurningOff(obj),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: PoolCapacity,
            maxSize: PoolMaxSize
            );

        ActiveObjects = new List<T>();
    }

    private void Start()
    {
        while (_pool.CountActive<PoolMaxSize)
        {
            _pool.Get();
        }
    }

    protected abstract void Create(T obj);

    protected void TurningOff(T obj)
    {
        obj.gameObject.SetActive(false);
        ActiveObjects.Remove(obj);
    }

    public void PutInPool(T obj)
    {
        _pool.Release(obj);
    }
}
