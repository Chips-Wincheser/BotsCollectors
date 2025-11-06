using System.Collections;
using UnityEngine;

public class BaseBuilding : MonoBehaviour
{
    [SerializeField] private UnitHub PrefabBase;

    [SerializeField] private SpawnerUnit _unitSpawner;
    [SerializeField] private UnitHub _unitHub;
    [SerializeField] private Warehouse _warehouse;
    [SerializeField] private FlagSetter _flagSetter;

    private int _countResourcesToCreateBase = 5;

    private bool _isFlagUp;
    private Flag _flag;

    private WaitForSeconds _waitForSeconds;
    private int delay = 6;

    private void OnEnable()
    {
        _warehouse.ResourceLoaded+=GatheringResources;
        _flagSetter.FlagSeted+=ToggleFlag;
    }

    private void OnDisable()
    {
        _warehouse.ResourceLoaded-=GatheringResources;
        _flagSetter.FlagSeted-=ToggleFlag;
    }

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(delay);
    }

    private void GatheringResources(int count)
    {
        if (count>=_countResourcesToCreateBase && _isFlagUp==true)
        {
            _warehouse.ConsumeResources(_countResourcesToCreateBase);

            Unit unit = _unitHub.GetFreeObject();

            if(unit != null)
            {
                unit.AssignToNewBase(_flag.transform.position);
                StartCoroutine(WaitToUnitArrive(unit));
            }
        }
    }

    private void ToggleFlag(bool isFlagUp,Flag flag)
    {
        _isFlagUp= !isFlagUp;
        _flag=flag;
    }

    private void CreateNewBase(Unit builder)
    {
        if (_unitHub.UnitCount>1 && _unitSpawner!=null && _flag!=null)
        {
            Vector3 flagPosition = _flag.transform.position;
            UnitHub newBase = Instantiate(PrefabBase, flagPosition, Quaternion.identity);
            newBase.AddExistingUnit(builder);
            newBase.SetSpawner(_unitSpawner);

            _isFlagUp = false;

            Destroy(_flag.gameObject);
            _flag = null;
        }
    }

    private IEnumerator WaitToUnitArrive(Unit unit)
    {
        yield return _waitForSeconds;
        CreateNewBase(unit);
    }
}
