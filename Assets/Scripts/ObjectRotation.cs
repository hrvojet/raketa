using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CheckpointStorage;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;

    private void Start()
    {
        if (Token.IsTokenCollected)
        {
            this.GetComponent<Renderer>().enabled = false;
        }
    }

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
