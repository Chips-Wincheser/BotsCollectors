using UnityEngine;

public class ResourceCollectorManager : MonoBehaviour
{
    [SerializeField] private ButtonEvent _scanButton;
    [SerializeField] private SpawnerResources _spawnerResources;
    [SerializeField] private SpawnerUnit _spawnerUnit;

    private void OnEnable()
    {
        _scanButton.Clicked += ScanArea;
    }

    private void OnDisable()
    {
        _scanButton.Clicked -= ScanArea;
    }

    private void ScanArea()
    {
        int activeResource = _spawnerResources.Pool.CountActive;
     
        if (activeResource > 0)
        {
            foreach (var unit in _spawnerUnit.ActiveObjects)
            {
                if (unit.IsBusy!=true)
                {
                    foreach (var apple in _spawnerResources.ActiveObjects)
                    {
                        if (apple.IsBusy==false)
                        {
                            apple.IsBusy = true;
                            unit.MoveToTarget(apple.transform.position);
                            return;
                        }
                    }
                }
            }
        }
    }

}
