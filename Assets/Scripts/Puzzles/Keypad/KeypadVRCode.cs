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
    public int RandomCode()
    {
        return Random.RandomRange(1000, 9999);
    }
    
}
