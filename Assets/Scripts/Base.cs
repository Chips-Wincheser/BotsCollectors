using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private SpawnerUnit _unitSpawner;
    [SerializeField] private BaseScanner _baseScanner;
    [SerializeField] private ResourceStorage _storage;

    private void OnEnable()
    {
        _baseScanner.OnResourceFound+=HandleResourceFound;
        _baseScanner.OnScanCompleted+=AssignUnitToResource;
    }

    private void OnDisable()
    {
        _baseScanner.OnResourceFound-=HandleResourceFound;
        _baseScanner.OnScanCompleted-=AssignUnitToResource;
    }

    private void HandleResourceFound(Resource resource)
    {
        _storage.AddResource(resource);
    }

    private void AssignUnitToResource()
    {
        Resource targetResource=_storage.TakeOneResource();

        if (targetResource != null)
        {
            foreach (var unit in _unitSpawner.ActiveObjects)
            {
                if (unit.IsBusy==false)
                {
                    unit.MoveToTarget(targetResource.transform.position);
                    break;
                }
            }
        }
    }
}
