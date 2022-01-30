using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    int index;
    MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        index = 0;
        meshRenderer.material.color = Color.red;
    }

    public void ChangeColor()
    {
        index++;
        if (index > 5) index = 0;
        switch(index)
        {
            case 0:
            meshRenderer.material.color = Color.red;
                break;
            case 1:
                meshRenderer.material.color = Color.Lerp(Color.red, Color.yellow, 0.5f);
                break;
            case 2:
                meshRenderer.material.color = Color.yellow;
                break;
            case 3:
                meshRenderer.material.color = Color.green;
                break;
            case 4:
                meshRenderer.material.color = Color.blue;
                break;
            case 5:
                meshRenderer.material.color = Color.Lerp(Color.blue, Color.red, 0.5f);
                break;
        }
    }
}
