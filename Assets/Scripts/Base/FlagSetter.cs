using System;
using UnityEngine;

public class FlagSetter : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;

    private Camera _mainCamera;
    private Flag _currentFlag;
    private bool _isPlacingFlag = false;
    private string _compareTag = "Ground";

    public event Action<bool,Flag> FlagSeted;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        _isPlacingFlag = true;
    }

    private void Update()
    {
        if (_isPlacingFlag && Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag(_compareTag))
                {
                    if (_currentFlag != null)
                    {
                        _currentFlag.transform.position = hit.point;
                        Debug.Log("Флаг перемещён.");
                    }
                    else
                    {
                        _currentFlag = Instantiate(_flagPrefab, hit.point, Quaternion.identity);
                        Debug.Log("Флаг установлен.");
                    }

                    _isPlacingFlag = false;
                    FlagSeted?.Invoke(_isPlacingFlag, _currentFlag);
                }
            }
        }
    }
}
