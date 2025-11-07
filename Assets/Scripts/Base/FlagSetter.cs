using System;
using UnityEngine;

public class FlagSetter : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private PlayerInput _playerInput;

    private Flag _currentFlag;
    private bool _isPlacingFlag = false;

    public event Action<bool,Flag> FlagPlaced;
    public event Action<bool> UnitSpawnToggled;

    private void OnEnable()
    {
        _playerInput.GroundClicked += HandleGroundClick;
    }

    private void OnDisable()
    {
        _playerInput.GroundClicked -= HandleGroundClick;
    }

    private void OnMouseDown()
    {
        _isPlacingFlag = true;
    }

    private void HandleGroundClick(Vector3 position)
    {
        if (_currentFlag != null)
        {
            _currentFlag.transform.position = position;
        }
        else
        {
            _currentFlag = Instantiate(_flagPrefab, position, Quaternion.identity);
        }

        _isPlacingFlag = false;

        FlagPlaced?.Invoke(_isPlacingFlag, _currentFlag);
        UnitSpawnToggled?.Invoke(_isPlacingFlag);
    }
}
