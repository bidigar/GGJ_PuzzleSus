using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBoxManager : MonoBehaviour
{
    [SerializeField] GameObject key;
    [SerializeField] GameObject holeOne;
    [SerializeField] GameObject holeTwo;
    [SerializeField] GameObject holeThree;
    MeshRenderer holeOne_MeshRenderer;
    MeshRenderer holeTwo_MeshRenderer;
    MeshRenderer holeThree_MeshRenderer;

    private void Start()
    {
        holeOne_MeshRenderer = holeOne.GetComponent<MeshRenderer>();
        holeTwo_MeshRenderer = holeTwo.GetComponent<MeshRenderer>();
        holeThree_MeshRenderer = holeThree.GetComponent<MeshRenderer>();
    }

    public void GhostTouch()
    {
        holeOne.SetActive(true);
        holeTwo.SetActive(true);
        holeThree.SetActive(true);
    }

    private void Update()
    {
        if (holeOne_MeshRenderer.material.color == Color.Lerp(Color.red, Color.yellow, 0.5f) && holeTwo_MeshRenderer.material.color == Color.green && holeThree_MeshRenderer.material.color == Color.blue)
        {
            key.SetActive(true);
            Destroy(gameObject);
        }
    }
}
