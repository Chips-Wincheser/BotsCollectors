using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private UnitHub _unitHub;
    [SerializeField] private BaseScanner _baseScanner;
    [SerializeField] private ResourceStorage _storage;
    [SerializeField] private Warehouse _warehouse;
    [SerializeField] private FlagSetter _flagSetter;

    private int _countResourcesToCreateUnit = 3;

    private bool _isSpawnUnit=true;

    private void OnEnable()
    {
        _baseScanner.ResourceFounded+=HandleResourceFound;
        _baseScanner.TargetsAssignmentRequested+=AssignUnitToResource;
        _warehouse.ResourceLoaded+=SpawnUnit;
        _flagSetter.UnitSpawnToggled+=ToggleFlag;
    }

    private void OnDisable()
    {
        _baseScanner.ResourceFounded-=HandleResourceFound;
        _baseScanner.TargetsAssignmentRequested-=AssignUnitToResource;
        _warehouse.ResourceLoaded-=SpawnUnit;
        _flagSetter.UnitSpawnToggled-=ToggleFlag;
    }

    private void SpawnUnit(int count)
    {
        if(_isSpawnUnit == true)
        {
            if (count >= _countResourcesToCreateUnit)
            {
                _unitHub.CreateNewUnit(transform.position);
                _warehouse.ConsumeResources(_countResourcesToCreateUnit);
                _warehouse.RemoveRange(_countResourcesToCreateUnit);
            }
        }
    }

    private void ToggleFlag(bool isSpawnUnit)
    {
        _isSpawnUnit= isSpawnUnit;
    }

    private void HandleResourceFound(Resource resource)
    {
        _storage.AddResource(resource);
    }

    private void AssignUnitToResource()
    {
        Unit unit = _unitHub.GetFreeObject();

        if(unit != null)
        {
            Resource targetResource = _storage.TakeResource();

            if (targetResource != null)
                unit.MoveToTarget(targetResource.transform.position, true);
        }
    }
}
