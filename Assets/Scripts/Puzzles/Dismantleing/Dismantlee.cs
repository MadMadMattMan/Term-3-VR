using Meta.XR.BuildingBlocks;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dismantlee : MonoBehaviour
{
    [SerializeField] List<Tool> workTool;
    [SerializeField] string type;
    public bool dismantleing = false;
    GameObject GrabComponent;
    float step = 0.055f;
    float time = 2;

    private void Start()
    {
        GrabComponent = GetComponentInChildren<BuildingBlock>().gameObject;
        GrabComponent.SetActive(false);
    }

    public void Dismantle(Tool tool)
    {
        if (workTool.Contains(tool))
        {
            Debug.Log("Dismantle Succesful");
            dismantleing = true;
        }
        else
        {
            Debug.Log("Dismantle Failed - Wrong tool");
        }
    }

    void Drop()
    {
        //Makes the Dismantlee act using gravity
        GetComponent<Rigidbody>().isKinematic = false;
        //Gets the hand grab child of Dismantlee and enables it so it can be grabbed
        GrabComponent.SetActive(true);
    }

    private void Update()
    {
        //If the Dismantlee marked for dismantleing and is being held, start dismatleing based on type
        if (dismantleing && type == "Screw" && GetComponent<Rigidbody>().isKinematic)
        {
            //Move and rotate the screw
            transform.Translate(0, 0, step * Time.deltaTime);
            transform.Rotate(0, 0, -10);
            time -= Time.deltaTime;
            //If dismantle time is over, end dismantleing
            if (time <= 0)
            {
                type = "Dismantled";
                Drop();
            }
        }
    }

}