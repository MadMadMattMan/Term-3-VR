using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace NavKeypad
{
    //Grabbed from Unity Asset Store - adjusted
    public class Keypad : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private UnityEvent onAccessGranted;
        [SerializeField] private UnityEvent onAccessDenied;
        [Header("Combination Code (9 Numbers Max)")]
        public string keypadCombo = "12345";

        public UnityEvent OnAccessGranted => onAccessGranted;
        public UnityEvent OnAccessDenied => onAccessDenied;

        [Header("Settings")]
        [SerializeField] private string accessGrantedText = "Granted";
        [SerializeField] private string accessDeniedText = "Denied";

        [SerializeField] KeypadVRCode myCode;

        [Header("Visuals")]
        [SerializeField] private float displayResultTime = 1f;
        [Range(0, 5)]
        [SerializeField] private float screenIntensity = 2.5f;
        [Header("Colors")]
        [SerializeField] private Color screenNormalColor = new Color(0.98f, 0.50f, 0.032f, 1f);
        [SerializeField] private Color screenDeniedColor = new Color(1f, 0f, 0f, 1f);
        [SerializeField] private Color screenGrantedColor = new Color(0f, 0.62f, 0.07f); 
        [Header("SoundFx")]
        [SerializeField] private AudioClip buttonClickedSfx;
        [SerializeField] private AudioClip accessDeniedSfx;
        [SerializeField] private AudioClip accessGrantedSfx;
        [Header("Component References")]
        [SerializeField] private Renderer panelMesh;
        [SerializeField] private TMP_Text keypadDisplayText;
        [SerializeField] private AudioSource audioSource;


        private string currentInput;
        private bool displayingResult = false;
        private bool accessWasGranted = false;

        void Start()
        {
            ClearInput();
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);
        }


        //Edited the input function - it gets value from pressedbutton
        public void AddInput(string input)
        {
            //Play the sound effect
            audioSource.PlayOneShot(buttonClickedSfx);
            //Check if already unlocked or displaying a result, if so stop the function
            if (displayingResult || accessWasGranted) return;

            //get the input and check it agaist some cases
            switch (input)
            {
                //if input is enter, check the current combo
                case "enter":
                    CheckCombo();
                    break;

                //if the case is anything else
                default:
                    //If the input is a over 4 inputs long and not null check the current combo and break function
                    if (currentInput != null && currentInput.Length == 4) // 4 max passcode size 
                    {
                        CheckCombo();
                        return;
                    }
                    //if not add the input to the current input and display it
                    currentInput += input;
                    keypadDisplayText.text = currentInput;
                    break;
            }

        }

        public void CheckCombo()
        {
            bool granted = false;
            if (currentInput == keypadCombo)
            {
                myCode.Win();
                granted = true;
            }

            if (!displayingResult)
            {
                StartCoroutine(DisplayResultRoutine(granted));            }
            else
            {
                Debug.LogWarning("Couldn't process input.");
            }

        }

        private IEnumerator DisplayResultRoutine(bool granted)
        {
            displayingResult = true;

            if (granted) AccessGranted();
            else AccessDenied();

            yield return new WaitForSeconds(displayResultTime);
            displayingResult = false;
            if (granted) yield break;
            ClearInput();
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);

        }

        private void AccessDenied()
        {
            keypadDisplayText.text = accessDeniedText;
            onAccessDenied?.Invoke();
            panelMesh.material.SetVector("_EmissionColor", screenDeniedColor * screenIntensity);
            audioSource.PlayOneShot(accessDeniedSfx);
        }

        private void ClearInput()
        {
            currentInput = "";
            keypadDisplayText.text = currentInput;
        }

        private void AccessGranted()
        {
            accessWasGranted = true;
            keypadDisplayText.text = accessGrantedText;
            onAccessGranted?.Invoke();
            panelMesh.material.SetVector("_EmissionColor", screenGrantedColor * screenIntensity);
            audioSource.PlayOneShot(accessGrantedSfx);
        }

    }
}