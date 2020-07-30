using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource rocketSound;
    [SerializeField]float mainThrust = 50f;
    [SerializeField]float rcsThrust = 200f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rocketSound = GetComponent<AudioSource>();

        rocketSound.loop = true;
        rocketSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Friendly");
                break;
            case "Fuel":
                print("Fuel");
                break;
            default:
                print("Dead");
                // kill player
                break;
        }
    }

    private void Thrust()
    {        
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);
            rocketSound.volume = 1f;
            if (!rocketSound.isPlaying)
            {
                //rocketSound.Play();
            }
        }
        else
        {
            rocketSound.volume = 0f;
            //rocketSound.Stop();
        }
    }
    private void Rotate()
    {
        rigidBody.freezeRotation = true; // take manual control of rotation
        
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; // resume physics control rotation
    }
}
