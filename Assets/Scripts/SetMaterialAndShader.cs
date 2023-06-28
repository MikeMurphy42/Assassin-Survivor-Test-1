using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterialAndShader : MonoBehaviour
{
    public Material yourMaterial;
    public Shader yourShader;

    private void Start()
    {
        if (yourMaterial != null && yourShader != null)
        {
            // Create a copy of the material
            Material newMaterial = new Material(yourMaterial);
            // Set the shader of the copied material
            newMaterial.shader = yourShader;

            // Set the material of the particle system
            GetComponent<ParticleSystemRenderer>().material = newMaterial;
        }
        else
        {
            Debug.LogError("Please assign a Material and Shader in the Inspector.");
        }
    }
}
