using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScanner : MonoBehaviour
{
    [SerializeField] private ButtonEvent _scanButton;
    [SerializeField] private float _scanRadius;
    [SerializeField] private float _scanTime=5f;

    private WaitForSeconds _WaitForSeconds;

    public event Action<Resource> OnResourceFound;
    public event Action TargetsAssignmentRequested;

    private void OnEnable()
    {
        _scanButton.Clicked += AssignTargets;
    }

    private void OnDisable()
    {
        _scanButton.Clicked -= AssignTargets;
    }

    private void Start()
    {
        _WaitForSeconds = new WaitForSeconds(_scanTime);
        StartCoroutine(ScanArea());
    }

    public void AssignTargets()
    {
        TargetsAssignmentRequested?.Invoke();
    }

    private IEnumerator ScanArea()
    {
        while (true)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius);

            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<Resource>(out Resource resource))
                {
                    OnResourceFound?.Invoke(resource);
                }
            }

            yield return _WaitForSeconds;
        }
    }
}
