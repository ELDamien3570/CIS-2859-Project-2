using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    GameObject winScreen;
    [SerializeField]
    GameObject loseScreen;

    [Header("TS Components")]
    [SerializeField]
    GameObject JumpIndicator;
    

    public void ShowWinScreen()
    {
        winScreen.SetActive(true); 
    }

    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
    }

    public void ShowJumpIndicator()
    {
        return;
        //JumpIndicator.SetActive(true);
        //StartCoroutine(ResetActive(JumpIndicator));
    }

    IEnumerator ResetActive(GameObject resetObject)
    {
        yield return new WaitForSeconds(1f);
        resetObject.SetActive(false);
    }

}
