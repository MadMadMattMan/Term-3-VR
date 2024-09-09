using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TabletUI : MonoBehaviour
{
    public enum currentActionStates
    {
        ResetRoom,
        QuitGame,
    }
    currentActionStates currentAction;

    public GameObject ResetButton, QuitButton, YesNoButton, AdvancedButton, SoundSlider, MainMenuText;

    private void Start()
    {
        ResetTablet();
    }

    public void AreYouSure(currentActionStates action)
    {
        currentAction = action;
        //Change tablet to are you sure text
    }


    public void AYSYes()
    {
        if (currentAction == currentActionStates.ResetRoom)
        {
            ResetRoom();
        }
        else if (currentAction == currentActionStates.QuitGame)
        {
            Application.Quit();
        }
    }

    public void AYSNo()
    {
        if (currentAction == currentActionStates.ResetRoom)
        {
            ResetTablet();
        }
        else if (currentAction == currentActionStates.QuitGame)
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
        //AdvancedButton.SetActive(true);

        YesNoButton.SetActive(false);
        //SoundSlider.SetActive(false);
    }
}
