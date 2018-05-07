using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Thrust();
        Rotate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing
                print("ok"); //TODO: Remove this line
                break;
            default:
                print("die"); //TODO: Remove this line
                break;
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) // Can Thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust);

            if (!audioSource.isPlaying) // Only play if the Audio isn't playing
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
        rigidBody.freezeRotation = true; // Take manual control of rotation

        float rotationSpeed = rcsThrust * Time.deltaTime;


        // Press A or D to rotate Left or Right
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        rigidBody.freezeRotation = false; // Resume physics control of rotation
    }
}
