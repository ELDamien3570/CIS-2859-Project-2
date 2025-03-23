using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Objectecs")]
    [SerializeField]
    private GameObject riserPlatform;
    [SerializeField]
    private GameObject levelOnePlatforms;
    [SerializeField]
    private GameObject levelTwoPlatforms;
    [SerializeField]
    private GameObject levelThreePlatforms;
    [SerializeField]
    private GameObject[] EnemyAI;
    [SerializeField]
    private GameObject BossAI;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private UIManager uiManager;

    [Header("Manager Variables")]
    public int gameLevel = 1;
    private int i;
    public int aiCount;
    public bool bossAlive = true;

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        aiCount = EnemyAI.Length;
    }
    private void Update()
    {
        LevelTwoCompletion();
        if (LevelTwoCompletion())
        {
            gameLevel = 3;
            LevelThree();
        }
        if (playerController.death)
            StartCoroutine(LevelRestart());
    }  
    public void ShowLevelTwoRiser()
    {
        riserPlatform.SetActive(true);
        gameLevel = 2;
    }
    public void LevelTwo()
    {
        levelTwoPlatforms.SetActive(true);
        for (i = 0; i < EnemyAI.Length; i++)
        {
            EnemyAI[i].SetActive(true);
        }
    }
    private bool LevelTwoCompletion()
    {
        if (aiCount <= 0)
            return true;
        else
            return false;
    }

    public void EnemyKilled()
    {
        aiCount--;
    }

    public void BossKilled()
    {
        bossAlive = false;
        uiManager.ShowWinScreen();
    }
    private void LevelThree()
    {
        levelThreePlatforms.SetActive(true);    
        BossAI.SetActive(true);
    }

    IEnumerator LevelRestart()
    {
        uiManager.ShowLoseScreen();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
