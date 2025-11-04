using UnityEngine;

public class SpawnerUnit : SpawnerBase<Unit>
{
    private Vector3 _offset=Vector3.zero;

    private int _step=5;

    protected override void Create(Unit obj)
    {
        _offset+=new Vector3(_step,0,0);
        obj.transform.position = transform.position+_offset;
        obj.gameObject.SetActive(true);
        ActiveObjects.Add(obj);
    }

    public bool TrySpawnNewUnit()
    {
        if(GetActiveObject() < PoolMaxSize)
        {
            Spawn();
            return true;
        }

        return false;
    }

    public void AddExistingUnit(Unit unit)
    {
        ActiveObjects.Add(unit);
    }

    public Unit GetFreeObject()
    {
        foreach (var item in ActiveObjects)
        {
            if (item.IsBusy==false)
            {
                return item;
            }
        }

        return null;
    }
}
