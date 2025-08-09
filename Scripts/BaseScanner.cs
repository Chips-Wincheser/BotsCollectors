using UnityEngine;

public class BaseScanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;
    [SerializeField] private ResourceStorage _storage;
    [SerializeField] private SpawnerUnit _unitSpawner;
    [SerializeField] private ButtonEvent _scanButton;

    private void OnEnable()
    {
        _scanButton.Clicked += ScanArea;
    }

    private void OnDisable()
    {
        _scanButton.Clicked -= ScanArea;
    }

    public void ScanArea()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<Resource>(out Resource resource))
            {
                _storage.AddResource(resource);
            }
        }

        AssignUnitToResource(_storage.ContainsResource());
    }

    private void AssignUnitToResource(Resource targetResource)
    {
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
