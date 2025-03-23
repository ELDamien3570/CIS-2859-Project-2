using TMPro;
using UnityEngine;

public class CoinCounterScript : MonoBehaviour
{
    public int coinCount = 0;
    [SerializeField]
    private TextMeshProUGUI coinCounterLabel;

    private void Start()
    {
        coinCounterLabel = GetComponent<TextMeshProUGUI>(); 
    }

    private void LateUpdate()
    {
        coinCounterLabel.text = coinCount.ToString();
    }

}
