using System;
using System.Collections;
using UnityEngine;

public class BaseScanner : MonoBehaviour
{
    [SerializeField] private SpawnerResources _spawnerResources;

    [SerializeField] private float _scanRadius;
    [SerializeField] private float _scanTime=5f;

    private WaitForSeconds _WaitForSeconds;

    public event Action<Resource> OnResourceFound;
    public event Action TargetsAssignmentRequested;

    private void Start()
    {
        _WaitForSeconds = new WaitForSeconds(_scanTime);
        StartCoroutine(ScanArea());
    }

    private IEnumerator ScanArea()
    {
        while (true)
        {
            for (int i = 0; i < _spawnerResources.GetActiveObject(); i++)
            {
                Resource resource = _spawnerResources.GetObjectByIndex(i);
                OnResourceFound?.Invoke(resource);
                TargetsAssignmentRequested?.Invoke();
            }

            yield return _WaitForSeconds;
        }
    }
}
