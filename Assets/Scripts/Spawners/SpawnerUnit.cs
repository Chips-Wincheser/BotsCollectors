using UnityEngine;

public class SpawnerUnit : MonoBehaviour
{
    [SerializeField] private Unit _prefab;

    public Unit Create(Vector3 Position)
    {
        Unit unit = Instantiate(_prefab, Position, Quaternion.identity);
        
        return unit;
    }
}
