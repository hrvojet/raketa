using UnityEngine;
using UnityEngine.SceneManagement;

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

    enum State { Alive, Dying, Transcending};
    State state = State.Alive;

    bool CollisionsDisabled = false;

    int target = 60;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        rocketSound = GetComponent<AudioSource>();
        rocketSound.loop = true;
        rocketSound.Play();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.targetFrameRate != target)
        {
            Application.targetFrameRate = target;
        }

        if (state == State.Alive)
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
        if(state != State.Alive || CollisionsDisabled) { return; }

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
        state = State.Transcending;        
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
        Invoke("LoadNextScene", LoadLevelTime);
    }
    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        deathParticles.Play();
        Invoke("LoadFirstLevel", LoadLevelTime);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = ++currentSceneIndex;
        if (SceneManager.sceneCountInBuildSettings == nextSceneIndex)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex); // todo allow more than two levels
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
        rocketSound.volume = 1f;
        if (!audioSource.isPlaying)
        {
            //audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }
    private void StopApplyingThrust()
    {
        rocketSound.volume = 0f;
        //audioSource.Stop();
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
            LoadNextScene();
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
