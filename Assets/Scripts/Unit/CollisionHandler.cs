using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private Transform _hand;

    private Resource _resource;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Warehouse>(out Warehouse warehouse))
        {
            warehouse.ReceiveItem(_resource);
        }

        if (other.gameObject.TryGetComponent<Resource>(out Resource apple))
        {
            apple.transform.SetParent(_hand);
            apple.transform.localPosition = Vector3.zero;
            _resource=apple;
        }
    }
}
