using UnityEngine;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    [ SerializeField ] TextMeshProUGUI timerText;
    [ SerializeField ] private float remainingTime;
    [ SerializeField ] TextMeshProUGUI timerBoostText;

    public PlayerController playerController;

    void Update()
    {
        if ( remainingTime > 0 )
        {
            remainingTime -= Time.deltaTime;
        }
        else if ( remainingTime < 0 )
        {
            remainingTime = 0;
        }

        int minutes = Mathf.FloorToInt( remainingTime / 60 );
        int seconds = Mathf.FloorToInt( remainingTime % 60 );

        timerText.text = string.Format("{0:00}:{1:00}" , minutes , seconds );

        if ( playerController.isTimeBoosted == true )
        {
            remainingTime += 5;
            playerController.isTimeBoosted = false;
            StartCoroutine(ShowTimerMessage());
            timerBoostText.gameObject.SetActive(true);
        }
    }

    private IEnumerator ShowTimerMessage()
    {
        timerBoostText.text = "+5 seconds";
        yield return new WaitForSeconds(3f);
        timerBoostText.text = "";
    }
}
