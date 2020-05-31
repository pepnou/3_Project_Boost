using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody rigidBody;
    private AudioSource audioSource;

    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float mainThrust = 150f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
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
        /*foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white, 1f);
        }*/
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("ok");
                break;
            default:
                print("dead");
                break;
        }
    }
}
