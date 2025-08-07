using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private CollisionHandler _collisionHandler;

    private List<Resource> _resources;

    private void Awake()
    {
        _resources = new List<Resource>();
    }

    private void OnEnable()
    {
        _collisionHandler.OnPicked+=AddItem;
        _collisionHandler.OnWarehouseReached+=UnloadItem;
    }

    private void OnDisable()
    {
        _collisionHandler.OnPicked-=AddItem;
        _collisionHandler.OnWarehouseReached-=UnloadItem;
    }

    private void AddItem(Resource resource)
    {
        if (resource.TryTake())
            _resources.Add(resource);
    }

    private void UnloadItem(Warehouse warehouse)
    {
        if (_resources.Count > 0)
        {
            Resource resourceToUnload = _resources[0];
            _resources.RemoveAt(0);
            warehouse.ReceiveItem(resourceToUnload);
        }
    }
}
