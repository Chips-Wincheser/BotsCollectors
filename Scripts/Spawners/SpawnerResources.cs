using UnityEngine;

public class SpawnerResources : SpawnerBase<Resource>
{
    private BoxCollider _platformCollider;

    protected override void Awake()
    {
        base.Awake();
        _platformCollider = GetComponent<BoxCollider>();
    }

    protected override void Create(Resource obj)
    {
        Vector3 platformSize = _platformCollider.size;
        Vector3 platformCenter = _platformCollider.center;

        float randomX = Random.Range(-platformSize.x / 2f, platformSize.x / 2f);
        float randomZ = Random.Range(-platformSize.z / 2f, platformSize.z / 2f);
        float positionY = 0f;

        Vector3 localOffset = new Vector3(randomX, positionY, randomZ) + platformCenter;

        Vector3 worldPosition = transform.TransformPoint(localOffset);

        obj.transform.position = worldPosition;
        obj.gameObject.SetActive(true);
        ActiveObjects.Add(obj);
    }
}
