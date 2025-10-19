using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpawnerResources : SpawnerBase<Resource>
{
    private float _spawnInterval=6;
    private BoxCollider _platformCollider;
    private WaitForSeconds _WaitForSeconds;

    protected override void Awake()
    {
        base.Awake();
        _WaitForSeconds = new WaitForSeconds(_spawnInterval);
        _platformCollider = GetComponent<BoxCollider>();
    }

    protected override void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (true)
        {
            if (ActiveObjects.Count < PoolMaxSize)
                SpawnFromPool();

            yield return _WaitForSeconds;
        }
    }

    protected override void Create(Resource obj)
    {
        Vector3 platformSize = _platformCollider.size;
        Vector3 platformCenter = _platformCollider.center;

        float randomX = UnityEngine.Random.Range(-platformSize.x / 2f, platformSize.x / 2f);
        float randomZ = UnityEngine.Random.Range(-platformSize.z / 2f, platformSize.z / 2f);
        float positionY = 0f;

        Vector3 localOffset = new Vector3(randomX, positionY, randomZ) + platformCenter;

        Vector3 worldPosition = transform.TransformPoint(localOffset);

        obj.transform.position = worldPosition;
        obj.gameObject.SetActive(true);
        ActiveObjects.Add(obj);
    }
}
