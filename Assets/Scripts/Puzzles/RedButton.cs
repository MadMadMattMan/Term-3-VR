using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : MonoBehaviour
{
    PokeInteractable buttonInteractor;
    LightingChange UVLight;
    BreakableObject glass;

    private void Start()
    {
        buttonInteractor = GetComponentInChildren<PokeInteractable>();
        buttonInteractor.MaxInteractors = 0;
    }

    public void EnableButton()
    {
        buttonInteractor.MaxInteractors = -1;
    }
}
