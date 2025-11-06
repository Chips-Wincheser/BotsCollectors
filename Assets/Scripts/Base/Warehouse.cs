using System;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    [SerializeField] private SpawnerResources _spawnerResources;

    private List<Resource> _resources;

    public event Action<int> ResourceLoaded;

    private void Awake()
    {
        _resources = new List<Resource>();
    }

    public int GetObjectCount()
    {
        return _resources.Count;
    }

    public void ReceiveItem(Resource resource)
    {
        if (resource != null)
        {
            _resources.Add(resource);
            _spawnerResources.PutInPool(resource);
            ResourceLoaded?.Invoke(_resources.Count);
        }
    }

    public void ConsumeResources(int amount)
    {
        if (_resources.Count > amount)
            _resources.RemoveRange(0, amount);
    }

    public void RemoveRange(int count)
    {
        _resources.RemoveRange(0,count);
    }
}
