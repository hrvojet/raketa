﻿using UnityEngine;
using GameEnum;
using RocketGameLevelManager;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    AudioSource rocketSound;
    [SerializeField] float mainThrust = 35f;
    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float LoadLevelTime = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip deathSound;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;
    
    RocketState state = GameEnum.RocketState.Alive;

    bool CollisionsDisabled = false;

    int fpsTarget = 60;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //rocketSound = GetComponent<AudioSource>();
        //rocketSound.loop = true;
        //rocketSound.Play();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = fpsTarget;

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.targetFrameRate != fpsTarget)
        {
            Application.targetFrameRate = fpsTarget;
        }

        if (state == RocketState.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
        if(Debug.isDebugBuild) // checking if debug methods will be available in final build
        {
            RespondToDebugKeys();
        }
    }

    private void RespondToDebugKeys()
    {
        RespondToNextLevelInput();
        ToggleColisionDetection();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(state != RocketState.Alive || CollisionsDisabled) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:  // kill player
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        state = RocketState.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
        Invoke("LoadNextLevel", LoadLevelTime);
    }
    private void StartDeathSequence()
    {
        state = RocketState.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        deathParticles.Play();
        Invoke("ResetGame", LoadLevelTime);
    }

    private void ResetGame()
    {
        GameLoader.LoadFirstLevel();
    }

    private void LoadNextLevel()
    {
        GameLoader.LoadNextScene();
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            StopApplyingThrust();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        //rocketSound.volume = 1f;
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }
    private void StopApplyingThrust()
    {
        //rocketSound.volume = 0f;
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void RespondToRotateInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateManually(rcsThrust * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateManually(-rcsThrust * Time.deltaTime);
        }
    }

    private void RotateManually(float rotationThisFrame)
    {
        rigidBody.freezeRotation = true; // take manual control of rotation
        transform.Rotate(Vector3.forward * rotationThisFrame);
        rigidBody.freezeRotation = false; // resume physics control rotation
    }

    private void RespondToNextLevelInput()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    private void ToggleColisionDetection()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            CollisionsDisabled = !CollisionsDisabled;
        }
    }
}
