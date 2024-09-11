using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessActiveLink : MonoBehaviour
{
    Camera cam;
    PostProcessLayer layer;

    private void Start()
    {
        cam = GetComponent<Camera>();
        layer = GetComponent<PostProcessLayer>();

        layer.enabled = cam.isActiveAndEnabled;
    }
}
