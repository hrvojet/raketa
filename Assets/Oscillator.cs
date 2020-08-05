using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] // allows only one script per game object
public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    Vector3 offsetVector = new Vector3(0f, 0f, 0f);
    [SerializeField] float period = 2f;
    [SerializeField] float timeDelay = 0f;

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
        //Invoke("UpdateMovement", timeDelay);
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (period <= Mathf.Epsilon) { period = 1; } // protect from devide by zero
        float cycles = Time.time / period; // grows constantly from 0
        print(cycles);

        const float tau = (float)Math.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to +1

        //print("rawSinWave: " + rawSinWave + " cycles: " + cycles);

        movementFactor = rawSinWave / 2f + 0.5f;
        offsetVector = movementFactor * movementVector;
        transform.position = offsetVector + startingPos;
    }


    /*Yea, I suppose there needs to be some kind of delay for every moving obstacle, but I can't find any changes in this 
     * lecture in **Oscilator.cs** script. I believe delay needs to be implemented as *SerializeField* so it can be 
     * manipulated in Unity... */
}
