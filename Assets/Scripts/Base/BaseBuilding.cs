using System.Collections;
using UnityEngine;

public class BaseBuilding : MonoBehaviour
{
    [SerializeField] private Base PrefabBase;

    [SerializeField] private SpawnerUnit _unitSpawner;
    [SerializeField] private Warehouse _warehouse;
    [SerializeField] private FlagSetter _flagSetter;

    private int _countResourcesToCreateBase = 5;
    private int _broughtResources=0;

    private bool _isFlagUp;
    private Flag _flag;

    private WaitForSeconds _waitForSeconds;
    private int delay = 5;

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
        _broughtResources++;

        if (_broughtResources>=_countResourcesToCreateBase && _isFlagUp==true)
        {
            _warehouse.ConsumeResources(_countResourcesToCreateBase);

            for (int i = 0; i < _unitSpawner.GetActiveObject(); i++)
            {
                Unit unit = _unitSpawner.GetObjectByIndex(i);

                if (unit.IsBusy == false)
                {
                    unit.AssignToNewBase(_flag.transform.position);
                    StartCoroutine(WaitToUnitArrive(unit));
                    break;
                }
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
        Base newBase = Instantiate(PrefabBase, _flag.transform.position, Quaternion.identity);
        newBase.AddExistingUnit(builder);
        _isFlagUp= false;
        Destroy(_flag.gameObject);
    }

    private IEnumerator WaitToUnitArrive(Unit unit)
    {
        yield return _waitForSeconds;
        CreateNewBase(unit);
    }
}
