using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private SpawnerUnit _unitSpawner;
    [SerializeField] private BaseScanner _baseScanner;
    [SerializeField] private ResourceStorage _storage;

    private void OnEnable()
    {
        _baseScanner.OnResourceFound+=HandleResourceFound;
        _baseScanner.TargetsAssignmentRequested+=AssignUnitToResource;
    }

    private void OnDisable()
    {
        _baseScanner.OnResourceFound-=HandleResourceFound;
        _baseScanner.TargetsAssignmentRequested-=AssignUnitToResource;
    }

    private void HandleResourceFound(Resource resource)
    {
        _storage.AddResource(resource);
    }

    private void AssignUnitToResource()
    {
        Resource targetResource=_storage.TakeResource();

        if (targetResource != null)
        {
            for (int i=0; i < _unitSpawner.GetActiveObject() ; i++)
            {
                Unit unit = _unitSpawner.GetObjectByIndex(i);

                if (unit.IsBusy==false)
                {
                    unit.MoveToTarget(targetResource.transform.position);
                    break;
                }
            }
        }
    }
}
