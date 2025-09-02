using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

abstract public class SpawnerBase<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T _prefab;
    [SerializeField] protected int PoolCapacity = 5;
    [SerializeField] protected int PoolMaxSize = 5;

    public List<T> ActiveObjects { get; private set; }
    public ObjectPool<T> Pool { get; private set; }

    protected virtual void Awake()
    {
        Pool= new ObjectPool<T>(
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
        while (Pool.CountActive<PoolMaxSize)
        {
            Pool.Get();
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
        Pool.Release(obj);
    }
}
