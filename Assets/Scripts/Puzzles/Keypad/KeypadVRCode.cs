using NavKeypad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadVRCode : MonoBehaviour
{
    public void PressButton(KeypadButton button)
    {
        button.PressButton();
    }
    public void ReleaseButton(KeypadButton button)
    {
        button.ReleaseButton();
    }

    [SerializeField] LightingChange lightChange;
    [SerializeField] GameObject winButtons;

    public void Win()
    {
        lightChange.WinLight();
        GetComponent<AudioSource>().Play();
        winButtons.SetActive(true);
    }
}
