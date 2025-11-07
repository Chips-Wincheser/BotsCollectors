using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private readonly int _isRun = Animator.StringToHash("IsRun");
    private readonly int _isPickUp = Animator.StringToHash("IsPickUp");

    public void SetRunning(bool isRunning)
    {
        _animator.SetBool(_isRun, isRunning);
    }

    public void SetPickUp(bool isPickUp)
    {
        _animator.SetBool(_isPickUp, isPickUp);
    }
}
