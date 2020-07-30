using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource rocketSound;
    [SerializeField]float mainThrust = 50f;
    [SerializeField]float rcsThrust = 200f;

    enum State { Alive, Dying, Transcending};
    State state = State.Alive;

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
        if(state != State.Dying)
        {
            Thrust();
            Rotate();
        }
        else
        {
            rocketSound.volume = 0f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Friendly");
                break;
            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextScene", 1.5f);
                break;
            default:  // kill player
                state = State.Dying;
                print("Dead");
                Invoke("LoadFirstLevel", 2f);
                break;
        }
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1); // todo allow more than two levels
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
