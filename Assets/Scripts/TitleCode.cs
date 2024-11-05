using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCode : MonoBehaviour
{
    //Defines what states the title scene can be
    public enum State
    {
        Title,
        Tutorial,
        Final,
        Enter
    }

    //Local state
    State state;

    //References
    [SerializeField] GameObject titleSlide, tutorialSlide, finalSlide;
    [SerializeField] TextMeshPro buttonText;

    //On start make sure current state is title and set text to be right via statecheck
    private void Start()
    {
        state = State.Title;
        StateCheck();
    }

    //When next button is pressed, move to next state and do state check
    public void NextState()
    {
        state++;
        StateCheck();
    }

    //Checks the set stage of the enum and does whats needed
    void StateCheck()
    {
        if (state == State.Title)
        {
            titleSlide.SetActive(true);
            tutorialSlide.SetActive(false);
            finalSlide.SetActive(false);
            buttonText.text = "Next";
        }
        else if (state == State.Tutorial)
        {
            titleSlide.SetActive(false);
            tutorialSlide.SetActive(true);
            finalSlide.SetActive(false);
            buttonText.text = "Next";
        }
        else if (state == State.Final)
        {
            titleSlide.SetActive(false);
            tutorialSlide.SetActive(false);
            finalSlide.SetActive(true);
            buttonText.text = "Start";
        }
        else if (state == State.Enter)
        {
            SceneManager.LoadScene(1);
        }
    }
}
