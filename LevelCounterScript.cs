using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelCounterScript : MonoBehaviour
{
    public int level;
    [SerializeField]
    private TextMeshProUGUI coinCounterLabel;
    [SerializeField]
    private GameObject gameManager;

    private void Start()
    {
        coinCounterLabel = GetComponent<TextMeshProUGUI>();

        
    }

    private void LateUpdate()
    {
        level = gameManager.GetComponent<GameManager>().gameLevel;
        coinCounterLabel.text = level.ToString();
    }
}
