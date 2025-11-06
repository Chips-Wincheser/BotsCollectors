using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private ResourceDetector _resourceDetector;
    [SerializeField] private WarehouseDetector _warehouseDetector;

    private Resource _resource;

    private void OnEnable()
    {
        _resourceDetector.OnResourceDetected += HandleResourceDetected;
        _warehouseDetector.OnWarehouseDetected += HandleWarehouseDetected;
    }

    private void OnDisable()
    {
        _resourceDetector.OnResourceDetected -= HandleResourceDetected;
        _warehouseDetector.OnWarehouseDetected -= HandleWarehouseDetected;
    }

    private void HandleWarehouseDetected(Warehouse warehouse, ResourceStorage resourceStorage)
    {
        if (_resource != null)
        {
            _resource.transform.SetParent(null);
            warehouse.ReceiveItem(_resource);
            resourceStorage.DeleteResource(_resource);
            _resource = null;
        }
    }

    private void HandleResourceDetected(Resource apple)
    {
        if (_resource == null && apple.transform.parent == null)
        {
            apple.transform.SetParent(_hand);
            apple.transform.localPosition = Vector3.zero;
            _resource=apple;
        }
    }
}
