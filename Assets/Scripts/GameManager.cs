using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    ///score + ending///
    [ SerializeField ] GameObject GameResult;
    [ SerializeField ] TextMeshProUGUI GameResultTxt;
    public bool isGameEnd = false;
    [ SerializeField ] AudioClip gameEndSFX;

    public int currentTotalScore = 0;
    [ SerializeField ] TextMeshProUGUI CurrentTotalScoreUITxt;
    
    ///ui + sfx///
    [ SerializeField ] OrderManager[] orderManager;
    [ SerializeField ] TextMeshProUGUI penaltyLosingTimeText;
    [ SerializeField ] AudioSource GameManagerAudioSource;
    [ SerializeField ] AudioClip wrongOrderSFX;
    [ SerializeField ] AudioClip correctOrderSFX;
    [ SerializeField ] Timer timer;
    [ SerializeField ] AudioClip timeBoostedSFX;

    ///other///
    public bool isLosingTime = false;
    [ SerializeField ] GameObject Employee;
    private bool hasPlayedGameEndSound = false;

    void Start()
    {
        GameResult.gameObject.SetActive(false);

        foreach (OrderManager om in orderManager)
        {
            om.onCorrectItemDelivered.AddListener(PlayCorrectSFX);
            om.onWrongItemDelivered.AddListener(PlayWrongSFX);
            om.onOrderExpire.AddListener(PlayWrongSFX);
        }

        timer.onTimeBoosted.AddListener(PlayBoostedSFX);
    }

    void Update()
    {
        CurrentTotalScoreUITxt.text = currentTotalScore.ToString();

        if ( isGameEnd == true )
        {
            Employee.SetActive(false);

            if (!hasPlayedGameEndSound)
            {
                GameManagerAudioSource.PlayOneShot(gameEndSFX);
                hasPlayedGameEndSound = true; // prevent replaying
            }

            GameResultTxt.text = currentTotalScore.ToString();
            GameEndCondition(currentTotalScore);
        }
    }

    public void GameEndCondition( int totalScore )
    {
        GameResult.gameObject.SetActive(true);
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void PlayBoostedSFX()
    {
       GameManagerAudioSource.PlayOneShot(timeBoostedSFX);
    }
    
    public void PlayCorrectSFX()
    {
       GameManagerAudioSource.PlayOneShot(correctOrderSFX);
       currentTotalScore += 10;
    }

    public void PlayWrongSFX()
    {
        isLosingTime = true;
        penaltyLosingTimeText.gameObject.SetActive(true);
        GameManagerAudioSource.PlayOneShot(wrongOrderSFX);
        StartCoroutine(ShowPenaltyMessage());
    }

    private IEnumerator ShowPenaltyMessage()
    {
        penaltyLosingTimeText.color = Color.red;
        penaltyLosingTimeText.text = "-3 seconds";
        yield return new WaitForSeconds(3f);
        penaltyLosingTimeText.text = "";
    }
}
