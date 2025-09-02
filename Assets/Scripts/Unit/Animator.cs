using UnityEngine;

public class Animator : MonoBehaviour
{
    [SerializeField] private UnityEngine.Animator _animator;

    private readonly int _isRun = UnityEngine.Animator.StringToHash("IsRun");
    private readonly int _isPickUp = UnityEngine.Animator.StringToHash("IsPickUp");

    public void SetRunning(bool isRunning)
    {
        _animator.SetBool(_isRun, isRunning);
    }

    public void SetPickUp(bool isPickUp)
    {
        _animator.SetBool(_isPickUp, isPickUp);
    }
}
