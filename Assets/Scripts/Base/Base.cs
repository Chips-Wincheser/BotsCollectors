using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private SpawnerUnit _unitSpawner;
    [SerializeField] private BaseScanner _baseScanner;
    [SerializeField] private ResourceStorage _storage;
    [SerializeField] private Warehouse _warehouse;
    [SerializeField] private FlagSetter _flagSetter;

    private int _countResourcesToCreateUnit = 3;
    private int _broughtResources;

    private bool _isSpawnUnit=true;

    private void OnEnable()
    {
        //Time.timeScale=3f;
        _baseScanner.OnResourceFound+=HandleResourceFound;
        _baseScanner.TargetsAssignmentRequested+=AssignUnitToResource;
        _warehouse.ResourceLoaded+=SpawnUnit;
        _flagSetter.FlagSeted+=ToggleFlag;
    }

    private void OnDisable()
    {
        _baseScanner.OnResourceFound-=HandleResourceFound;
        _baseScanner.TargetsAssignmentRequested-=AssignUnitToResource;
        _warehouse.ResourceLoaded-=SpawnUnit;
        _flagSetter.FlagSeted-=ToggleFlag;
    }

    private void SpawnUnit(int count)
    {
        if(_isSpawnUnit == true)
        {
            _broughtResources++;
        
            if (_broughtResources >= _countResourcesToCreateUnit)
            {
                if (_unitSpawner.TrySpawnNewUnit())
                {
                    _warehouse.ConsumeResources(_countResourcesToCreateUnit);
                    _broughtResources=0;
                }
            }
        }
    }

    private void ToggleFlag(bool isSpawnUnit,Flag flag)
    {
        _isSpawnUnit= isSpawnUnit;
    }

    private void HandleResourceFound(Resource resource)
    {
        _storage.AddResource(resource);
    }

    private void AssignUnitToResource()
    {
        /*for (int i = 0; i < _unitSpawner.GetActiveObject(); i++)
        {
            Unit unit = _unitSpawner.GetObjectByIndex(i);

            if (unit.IsBusy == false)
            {
                Resource targetResource = _storage.TakeResource();

                if (targetResource == null)
                    break;

                unit.MoveToTarget(targetResource.transform.position,true);
            }
        }*/

        Unit unit = _unitSpawner.GetFreeObject();

        if(unit != null)
        {
            Resource targetResource = _storage.TakeResource();

            if (targetResource != null)
                unit.MoveToTarget(targetResource.transform.position, true);
        }
    }

    public void AddExistingUnit(Unit unit)
    {
        unit.SetNewHome(transform.position);
        _unitSpawner.AddExistingUnit(unit);
        _isSpawnUnit= true;
    }
}
