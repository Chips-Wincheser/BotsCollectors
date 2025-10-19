using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    [SerializeField] private Button _button;

    public event Action Clicked;

    protected void OnEnable()
    {
        _button.onClick.AddListener(HandleButtonClick);
    }

    protected void OnDisable()
    {
        _button.onClick.RemoveListener(HandleButtonClick);
    }

    private void HandleButtonClick()
    {
        Clicked?.Invoke();
    }
}
