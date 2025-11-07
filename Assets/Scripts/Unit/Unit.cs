using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Unit : MonoBehaviour
{
    [SerializeField] private CharacterAnimator _animated;

    private float _speed = 5f;
    private Transform _transform;
    private Vector3 _targetPosition;
    private Vector3 _spawnPosition;
    private Tween _moveTween;

    private WaitForSeconds _waitForSeconds;
    private int delay=5;
    private bool _doNeedBack;

    public bool IsBusy {  get; private set; }

    private void Awake()
    {
        _transform = transform;
        _spawnPosition= _transform.position;
        IsBusy = false;
        _waitForSeconds = new WaitForSeconds(delay);
    }

    public void MoveToTarget(Vector3 target,bool doNeedBack)
    {
        _doNeedBack= doNeedBack;
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
        
        if (_doNeedBack)
        {
            _animated.SetPickUp(true);

            if (_targetPosition != _spawnPosition)
            {
                StartCoroutine(ReturnToBaseDelay());
            }
        }
        else
        {
            IsBusy = false;
        }
    }

    private IEnumerator ReturnToBaseDelay()
    {
        yield return _waitForSeconds;
        MoveToTarget(_spawnPosition,true);
        yield return _waitForSeconds;
        IsBusy = false;
    }

    public void AssignToNewBase(Vector3 target)
    {
        MoveToTarget(target,false);
    }

    public void SetNewHome(Vector3 newBasePosition)
    {
        _spawnPosition = newBasePosition;
    }
}
