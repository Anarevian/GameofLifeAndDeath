using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
    [SerializeField] private Material sky;

    private void Start()
    {
        RenderSettings.skybox = sky;
    }
}
