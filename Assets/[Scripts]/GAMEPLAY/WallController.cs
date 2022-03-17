using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public GravityDirection direction;
    public bool isFlashing;
    [SerializeField] private Color baseColour;
    [SerializeField] private Color flashColour;

    private bool hasValues;
    private Renderer renderer;
    private Transform playerReference;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        playerReference = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if(isFlashing)
        {
            renderer.material.SetColor("_Color", flashColour);
            isFlashing = false;
        }
        else
            renderer.material.SetColor("_Color", baseColour);

    }


}
