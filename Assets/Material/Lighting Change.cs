using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingChange : MonoBehaviour
{
    [SerializeField] Color Default = new Color(255, 237, 186);
    [SerializeField] Color UV = new Color(100, 83, 148);
    Light mainLight;

    private void Start()
    {
        mainLight = GetComponent<Light>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeLightColor();
        }
    }


    public void ChangeLightColor()
    {
        if (mainLight.color == Default)
            mainLight.color = UV;
        else
            mainLight.color = Default;

    }
}
