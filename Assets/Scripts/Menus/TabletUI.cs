using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TabletUI : MonoBehaviour
{

    public GameObject ResetButton, QuitButton, YesNoButton, AdvancedButton, SoundSlider, MainMenuText;
    string currentAction;

    private void Start()
    {
        ResetTablet();
    }

    public void AreYouSure(string action)
    {
        currentAction = action;
        //Change tablet to are you sure text
        MainMenuText.SetActive(false);
        ResetButton.SetActive(false);
        QuitButton.SetActive(false);

        YesNoButton.SetActive(true);

    }

    public void AYSYes()
    {
        if (currentAction == "reset")
        {
            ResetRoom();
        }
        else if (currentAction == "quit")
        {
            Application.Quit();
        }
    }

    public void AYSNo()
    {
        if (currentAction == "reset")
        {
            ResetTablet();
        }
        else if (currentAction == "quit")
        {
            Application.Quit();
        }
    }

    public void ResetRoom()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void ResetTablet()
    {
        MainMenuText.SetActive(true);
        ResetButton.SetActive(true);
        QuitButton.SetActive(true);

        YesNoButton.SetActive(false);
    }
}
