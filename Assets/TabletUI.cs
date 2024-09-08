using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TabletUI : MonoBehaviour
{
    enum currentActionStates
    {
        ResetRoom,
    }

    currentActionStates action;


    public void AreYouSure(currentActionStates action)
    {
        action = action;
        //Change tablet to are you sure text
    }


    public void AYSYes()
    {
        if (currentActionStates == ResetRoom())
        {

        }
    }
    public void AYSNo()
    {

    }


    public void ResetRoom()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
