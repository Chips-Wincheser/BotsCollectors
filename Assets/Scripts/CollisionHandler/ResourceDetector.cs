using System;
using UnityEngine;

public class ResourceDetector : MonoBehaviour
{
    public event Action<Resource> OnResourceDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Resource>(out Resource resource))
        {
            OnResourceDetected?.Invoke(resource);
        }
    }
}
