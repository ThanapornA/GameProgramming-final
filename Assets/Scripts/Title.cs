using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [ SerializeField ] GameObject TitleScreenButtons;
    [ SerializeField ] GameObject HowToScreen;
    [ SerializeField ] GameObject GuideScreen;

    void Start()
    {
        HowToScreen.SetActive(false);
        GuideScreen.SetActive(false);
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void HowToPlayButton()
    {
        TitleScreenButtons.SetActive(false);
        HowToScreen.SetActive(true);
    }

    public void NextButton()
    {
        HowToScreen.SetActive(false);
        GuideScreen.SetActive(true);
    }

    public void CloseButton()
    {
        GuideScreen.SetActive(false);
        TitleScreenButtons.SetActive(true);
    }
}
