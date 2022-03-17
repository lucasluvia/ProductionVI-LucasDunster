using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScanDirection
{
    X_POS,
    X_NEG,
    Y_POS,
    Y_NEG,
    Z_POS,
    Z_NEG
}


public class ScanningDetector : MonoBehaviour
{
    public ScanDirection scanDirection;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("ScanDir", (int)scanDirection);
    }
}
