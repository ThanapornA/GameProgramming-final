using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [ SerializeField ] TextMeshProUGUI timerText;
    [ SerializeField ] private float remainingTime;
    [ SerializeField ] TextMeshProUGUI timerEventText;

    public PlayerController playerController;
    public GameManager gameManager;

    ///countdown before player can play
    [ SerializeField ] TextMeshProUGUI CountDownBeforeStartGameTxt;
    [ SerializeField ] GameObject Employee;
    float currentTime = 0f;
    float startingTime = 4f;

    public UnityEvent onTimeBoosted;

    void Awake()
    {
        Employee.SetActive(false);
    }

    void Start()
    {
        currentTime = startingTime;
        StartCoroutine(CountDownBeforeStartGame());
    }

    void Update()
    {
        if ( remainingTime > 0 )
        {
            remainingTime -= Time.deltaTime;
        }
        else if ( remainingTime < 0 )
        {
            remainingTime = 0;
            gameManager.isGameEnd = true;
        }

        int minutes = Mathf.FloorToInt( remainingTime / 60 );
        int seconds = Mathf.FloorToInt( remainingTime % 60 );

        timerText.text = string.Format("{0:00}:{1:00}" , minutes , seconds );

        if ( playerController.isTimeBoosted == true )
        {
            onTimeBoosted.Invoke();
            remainingTime += 5;
            playerController.isTimeBoosted = false;
            StartCoroutine(ShowTimerMessage());
            timerEventText.gameObject.SetActive(true);
        }

        if ( gameManager.isLosingTime == true )
        {
            Debug.Log("Losing time triggered");
            remainingTime -= 3;
            gameManager.isLosingTime = false;
        }
    }

    private IEnumerator ShowTimerMessage()
    {
        timerEventText.color = Color.green;
        timerEventText.text = "+5 seconds";
        yield return new WaitForSeconds(3f);
        timerEventText.text = "";
    }

    private IEnumerator CountDownBeforeStartGame()
    {
        Time.timeScale = 0f;
        while ( currentTime > 0f )
        {
            yield return new WaitForSecondsRealtime(1f);
            currentTime -= 1;
            CountDownBeforeStartGameTxt.text = currentTime.ToString("0");
        }

        yield return new WaitForSecondsRealtime(1f);
        CountDownBeforeStartGameTxt.text = "GO";
        Employee.SetActive(true);
        Time.timeScale = 1f;

        CountDownBeforeStartGameTxt.gameObject.SetActive(false);
    }
}
