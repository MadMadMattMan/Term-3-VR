using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHit : MonoBehaviour
{
    [SerializeField] GameObject hammerParent;
    ParticleSystem ParticleSystem;
    AudioSource audioSource;
    [SerializeField] AudioClip bang;
    [SerializeField] AudioClip smash;

    bool soundable = true;

    private void Start()
    {
        ParticleSystem = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }


    private void FixedUpdate()
    {
        RaycastHit hitData;
        Debug.DrawRay(transform.position, -transform.right * 0.1f, Color.red, 1f);

        if (Physics.Raycast(transform.position, -transform.right, out hitData, 0.06f))
        {
            Debug.Log("Casted Ray hit");
            //Try finding the breakable object data and if not just play smash particle effect
            try
            {
                Debug.Log("Smashed Object");
                BreakableObject hitObject = hitData.transform.gameObject.GetComponent<BreakableObject>();

                if (soundable)
                    audioSource.PlayOneShot(smash);

                hitObject.SmashCheck();
            }
            catch
            {
                if (soundable)
                    audioSource.PlayOneShot(bang);

                //Particle effect
            }
        }
    }

    IEnumerator HitDelay()
    {
        soundable = false;
        yield return new WaitForSeconds(0.2f);
        soundable = true;
    }
}
