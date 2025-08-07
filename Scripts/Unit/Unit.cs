using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitAnimated _animated;

    private float _speed = 5f;
    private Transform _transform;
    private Vector3 _targetPosition;
    private Vector3 _spawnPosition;
    private Tween _moveTween;

    private WaitForSeconds _waitForSeconds;
    private int delay=5;

    public bool IsBusy {  get; private set; }

    private void Awake()
    {
        _transform = transform;
        _spawnPosition= transform.position;
        IsBusy = false;
        _waitForSeconds = new WaitForSeconds(delay);
    }

    public void MoveToTarget(Vector3 target)
    {
        _targetPosition = target;
        IsBusy = true;

        if (_moveTween != null && _moveTween.IsActive())
            _moveTween.Kill();

        _animated.SetPickUp(false);
        _animated.SetRunning(true);

        _transform.DOLookAt(_targetPosition, 0.3f);
        _moveTween = _transform.DOMove(_targetPosition, _speed).OnComplete(OnArrived);
    }

    private void OnArrived()
    {
        _animated.SetRunning(false);
        _animated.SetPickUp(true);
        IsBusy = false;

        if (_targetPosition != _spawnPosition)
        {
            StartCoroutine(ReturnToBaseDelay());
        }
    }

    private IEnumerator ReturnToBaseDelay()
    {
        yield return _waitForSeconds;
        MoveToTarget(_spawnPosition);
    }
}
