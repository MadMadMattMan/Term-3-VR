using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    [SerializeField] AudioClip[] musicClips;
    [SerializeField] AudioSource player;
    int clipNum = 1;
    float time = 0f, fadeTime = 5f;
    bool fadeMusic = false;
    float maxVolume = 0.015f;

    private void Update()
    {
        if (time > 1)
        {
            if (!player.isPlaying)
            {
                player.volume = 0;
                player.PlayOneShot(musicClips[clipNum]);
                fadeMusic = true;
                clipNum++;
            }
        }

        if (fadeMusic == true)
        {
            player.volume += maxVolume / fadeTime * Time.deltaTime;
            if (player.volume >= maxVolume)
                fadeMusic = false;
        }

        time += Time.deltaTime;
    }
}
