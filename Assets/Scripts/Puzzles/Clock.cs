using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] Transform hourHand, minuteHand, secondHand;
    public string keypadCode;
    int hour, minute, second;
    AudioSource clockSound;
    [SerializeField] AudioClip[] ticks;
    [SerializeField] TextMeshPro digitalClock;

    private void Start()
    {
        clockSound = GetComponent<AudioSource>();

        //Generate a random code
        hour = Random.Range(1, 12);
        string h = hour.ToString();
        minute = Random.Range(0, 12) * 5; //Makes minute be a multiple of 5 so easy to read on clock
        string m = minute.ToString();

        //Starting position for second hand
        second = Random.Range(5, 55);

        //Formatting for keypad
        if (hour < 10)
        {
           h = "0" + h;
        }
        if (minute < 10)
        {
            m = "0" + m;
        }

        keypadCode = h + m;
        digitalClock.text = h + ":" + m + " am";

        //Finding positions on clock
        float hHand = (hour / 12f) * 360f;
        //Debug.Log(hHand);
        hourHand.rotation = Quaternion.Euler(0, 0, hHand);

        float mHand = (minute / 60f) * 360f;
        minuteHand.eulerAngles = new Vector3(0, 0, mHand);

        float sHand = (second / 60f) * 360f;
        secondHand.eulerAngles = new Vector3(0, 0, sHand);

        //Starts first animation for seconds hand
        StartCoroutine(SecondAnimation());
    }

    //Smoothly shifts second hand one space and then
    int step = 10;
    bool breakTick;
    IEnumerator SecondAnimation()
    {
        yield return new WaitForSeconds(0.85f);

        //Get a random tick sound and play it
        if (!breakTick)
        {
            clockSound.clip = ticks[0];
            clockSound.volume -= 0.2f;
            breakTick = true;
        }
        else
        {
            int tick = Random.Range(0, ticks.Length-1);
            clockSound.clip = ticks[tick+1];
            clockSound.volume += 0.2f;
            breakTick = false;
        }
        clockSound.Play();

        yield return new WaitForSeconds(0.05f);

        //Smooth sift of the hand of 2degrees over 0.05seconds foreward one second
        for (int i = 0; i < step; i++) 
        {
            secondHand.eulerAngles += new Vector3(0, 0, 2f / step);
            yield return new WaitForSeconds(0.05f / step);
        }
        //Smooth sift of the hand of 2degrees over 0.05seconds back to normal position
        for (int i = 0; i < step; i++)
        {
            secondHand.eulerAngles -= new Vector3(0, 0, 2f / step);
            yield return new WaitForSeconds(0.05f / step);
        }

        //Restarts the animation
        StartCoroutine(SecondAnimation());
    }
}
