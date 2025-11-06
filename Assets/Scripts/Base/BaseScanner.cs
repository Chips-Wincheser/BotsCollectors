using System;
using System.Collections;
using UnityEngine;

public class BaseScanner : MonoBehaviour
{
    [SerializeField] private SpawnerResources _spawnerResources;

    [SerializeField] private float _scanRadius;
    [SerializeField] private float _scanTime=2f;

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
            Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius);

            bool anyResourceFound = false;

            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<Resource>(out Resource resource))
                {
                    OnResourceFound?.Invoke(resource);
                    anyResourceFound = true;
                }
            }

            if (anyResourceFound)
                TargetsAssignmentRequested?.Invoke();

            yield return _WaitForSeconds;
        }
    }
}

