using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private Transform _hand;

    public event Action<Resource> OnPicked;
    public event Action<Warehouse> OnWarehouseReached;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Resource>(out Resource apple))
        {

            apple.transform.SetParent(_hand);
            apple.transform.localPosition = Vector3.zero;
            OnPicked?.Invoke(apple);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Warehouse>(out Warehouse warehouse))
        {
            OnWarehouseReached?.Invoke(warehouse);
        }

        if (other.gameObject.TryGetComponent<Resource>(out Resource apple))
        {

            apple.transform.SetParent(_hand);
            apple.transform.localPosition = Vector3.zero;
            OnPicked?.Invoke(apple);
        }
    }
}
