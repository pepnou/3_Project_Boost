using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    private Rigidbody rigidBody;
    private AudioSource audioSource;

    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float mainThrust = 150f;

    enum State { ALIVE, DYING, TRANSCANDING};
    private State state = State.ALIVE;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.ALIVE)
        {
            ProcessInput();
        }
    }

    private void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        bool left = Input.GetKey(KeyCode.Q);
        bool right = Input.GetKey(KeyCode.D);
        if (left ^ right)
        {
            rigidBody.angularVelocity = Vector3.zero;
            float rotationThisTurn = Time.deltaTime * rcsThrust;

            if (left)
            {
                transform.Rotate(Vector3.forward * rotationThisTurn);
            }

            if (right)
            {
                transform.Rotate(-Vector3.forward * rotationThisTurn);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(state != State.ALIVE) { return;}

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.TRANSCANDING;
                Invoke("LoadNextScene", 1f);
                break;
            default:
                state = State.DYING;
                Invoke("LoadFirstScene", 1f);
                break;
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }
}
