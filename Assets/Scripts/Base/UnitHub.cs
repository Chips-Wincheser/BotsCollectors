using System.Collections.Generic;
using UnityEngine;

public class UnitHub : MonoBehaviour
{
    [SerializeField] private SpawnerUnit _spawnerUnit;

    private List<Unit> _unitList;
    private int _startCountUnits=3;

    public int UnitCount => _unitList.Count;

    private void Awake()
    {
        _unitList = new List<Unit>();

        if(_spawnerUnit != null)
        {
            for (int i = 0; i < _startCountUnits; i++)
            {
                Unit unit = _spawnerUnit.Create(transform.position);
                _unitList.Add(unit);
            }
        }
    }

    public void SetSpawner(SpawnerUnit spawnerUnit)
    {
        _spawnerUnit = spawnerUnit;
    }

    public void AddExistingUnit(Unit unit)
    {
        _unitList.Add(unit);
    }

    public void CreateNewUnit(Vector3 position)
    {
        Unit unit = _spawnerUnit.Create(position);
        _unitList.Add(unit);
    }

    public Unit GetFreeObject()
    {
        foreach (var unit in _unitList)
        {
            if (unit.IsBusy==false)
            {
                return unit;
            }
        }

        return null;
    }
}
