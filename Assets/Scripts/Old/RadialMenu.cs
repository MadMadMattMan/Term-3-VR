using Oculus.Platform.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] [Range(2, 10)]  int numberOfOptions;
    int gapAngle = 10;
    [SerializeField] GameObject radialPartUnselected, radialPartHighlighted;
    [SerializeField] Transform handTransform, radialCanvas;

    [Header("Feature Variables")]
    List<GameObject> spawnedParts = new List<GameObject>();

    //int tempOptionCount = -1;

    private void Start()
    {
        ResetRadialParts();
    }

    public void Selecting()
    {
        int selectedOption = GetSelectedPart();

        for (int i = 0; i < spawnedParts.Count; i++)
        {
            //If selected part, make graphics like selected prefab
            if (i == selectedOption)
            {
                spawnedParts[i].GetComponent<Image>().color = radialPartHighlighted.GetComponent<Image>().color;
                spawnedParts[i].GetComponent<Image>().material = radialPartHighlighted.GetComponent<Image>().material;
                spawnedParts[i].transform.localScale = radialPartHighlighted.transform.localScale;
            }
            //If not, make graphics like unselected prefab
            else
            {
                spawnedParts[i].GetComponent<Image>().color = radialPartUnselected.GetComponent<Image>().color;
                spawnedParts[i].GetComponent<Image>().material = radialPartUnselected.GetComponent<Image>().material;
                spawnedParts[i].transform.localScale = radialPartUnselected.transform.localScale;
            }
        }
    }

    int GetSelectedPart()
    {
        Vector3 centreToHand = handTransform.position - radialCanvas.position;
        Vector3 centreToHandProjection = Vector3.ProjectOnPlane(centreToHand, radialCanvas.forward);

        float angle = Vector3.SignedAngle(radialCanvas.up, centreToHandProjection, -radialCanvas.forward);

        if (angle < 0)
            angle += 360;

        return (int)(angle * numberOfOptions / 360);
    }

    public void ResetRadialParts()
    {
        foreach (var part in spawnedParts)
        {
            Destroy(part);
        }

        spawnedParts.Clear();

        for (int i = 0; i < numberOfOptions; i++)
        {
            float angle = -i * 360 / numberOfOptions - gapAngle / 2;
            Vector3 radialPartAngle = new Vector3(0, 0, angle);

            GameObject spawnedPart = Instantiate(radialPartUnselected, radialCanvas);
            spawnedPart.transform.localEulerAngles = radialPartAngle;

            spawnedPart.GetComponent<Image>().fillAmount = (1f / (float)numberOfOptions) - ((float)gapAngle / 360f);

            spawnedParts.Add(spawnedPart);
        }
    }
}
