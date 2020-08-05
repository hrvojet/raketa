using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveOscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(0f, 10f, 0f);
    Vector3 offsetVector = new Vector3(0f, 0f, 0f);
    [SerializeField] float period = 2f;
    [SerializeField] float timeDelay = 0f; // delay too make wave effect

    [Range(1,20)][SerializeField] float movementFactor; //0 not moved, 1 fully moved

    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (period <= Mathf.Epsilon) { period = 1; } // protect from devide by zero
        float cycles = Time.time + timeDelay / period; // grows constantly from 0

        const float tau = (float)Math.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to +1

        //print("rawSinWave: " + rawSinWave + " cycles: " + cycles);

        movementFactor = rawSinWave / 2f + 0.5f;
        offsetVector = movementFactor * movementVector;
        transform.position = offsetVector + startingPos;
    }
}
