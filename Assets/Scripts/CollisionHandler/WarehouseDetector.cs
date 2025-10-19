using System;
using UnityEngine;

public class WarehouseDetector : MonoBehaviour
{
    public event Action<Warehouse, ResourceStorage> OnWarehouseDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Warehouse>(out Warehouse warehouse) && other.TryGetComponent<ResourceStorage>(out ResourceStorage resourceStorage))
        {
            OnWarehouseDetected?.Invoke(warehouse, resourceStorage);
        }
    }
}
