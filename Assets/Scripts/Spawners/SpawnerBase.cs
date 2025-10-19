using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

abstract public class SpawnerBase<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T _prefab;
    [SerializeField] protected int PoolCapacity = 5;
    [SerializeField] protected int PoolMaxSize = 5;

    private ObjectPool<T> _pool;

    protected List<T> ActiveObjects;

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

    protected virtual void Start()
    {
        while (_pool.CountActive<PoolMaxSize)
        {
            SpawnFromPool();
        }
    }

    protected abstract void Create(T obj);

    protected void TurningOff(T obj)
    {
        obj.gameObject.SetActive(false);
        ActiveObjects.Remove(obj);
    }

    protected void SpawnFromPool()
    {
        _pool.Get();
    }

    public void PutInPool(T obj)
    {
        _pool.Release(obj);
    }

    public int GetActiveObject()
    {
        return ActiveObjects.Count;
    }

    public T GetObjectByIndex(int index)
    {
        return ActiveObjects.ElementAt(index);
    }
}
