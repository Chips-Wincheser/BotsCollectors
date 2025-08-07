using TMPro;
using UnityEngine;

public class ViewAmountResources : MonoBehaviour
{
    [SerializeField] private Warehouse _warehouse;
    [SerializeField] private TextMeshProUGUI _counterText;

    private int _count=0;

    private void OnEnable()
    {
        _warehouse.ResourceLoaded+=UpdateCounterDisplay;
    }

    private void OnDisable()
    {
        _warehouse.ResourceLoaded-=UpdateCounterDisplay;
    }

    private void UpdateCounterDisplay()
    {
        _count++;
        _counterText.text =_count.ToString();
    }
}
