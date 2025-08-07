using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsBusy;
    public bool IsInUse { get; private set; }

    private void Awake()
    {
        IsBusy = false;
    }

    public bool TryTake()
    {
        if (IsInUse)
            return false;

        IsInUse = true;
        return true;
    }

    public void Release()
    {
        IsInUse = false;
    }
}
