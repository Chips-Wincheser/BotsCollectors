using System;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    [SerializeField] private SpawnerResources _spawnerResources;

    private List<Resource> _resources;

    public event Action ResourceLoaded;

    private void Awake()
    {
        _resources = new List<Resource>();
    }

    public void ReceiveItem(Resource resource)
    {
        _resources.Add(resource);
        _spawnerResources.PutInPool(resource);
        ResourceLoaded?.Invoke();
    }
}
