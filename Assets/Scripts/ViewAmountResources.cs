using TMPro;
using UnityEngine;

public class ViewAmountResources : MonoBehaviour
{
    [SerializeField] private Warehouse _warehouse;
    [SerializeField] private TextMeshProUGUI _counterText;

    private void OnEnable()
    {
        _warehouse.ResourceLoaded+=UpdateCounterDisplay;
    }

    private void OnDisable()
    {
        _warehouse.ResourceLoaded-=UpdateCounterDisplay;
    }

    private void UpdateCounterDisplay(int resourcesCount)
    {

        _counterText.text =resourcesCount.ToString();
    }
}
