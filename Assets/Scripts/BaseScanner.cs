using System;
using UnityEngine;

public class BaseScanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;

    [SerializeField] private ButtonEvent _scanButton;

    public event Action<Resource> OnResourceFound;
    public event Action OnScanCompleted;

    private void OnEnable()
    {
        _scanButton.Clicked += ScanArea;
    }

    private void OnDisable()
    {
        _scanButton.Clicked -= ScanArea;
    }

    private void Start()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<Resource>(out Resource resource))
            {
                OnResourceFound?.Invoke(resource);
            }
        }
    }

    public void ScanArea()
    {
        OnScanCompleted?.Invoke();
    }
}
