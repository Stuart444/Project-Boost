using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    Rigidbody rigidBody;
    AudioSource audioSource;

    bool isTransitioning = false;
    [SerializeField] Lives lives;

    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isTransitioning)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    private void OnCollisionEnter (Collision collision)
    {
        if (isTransitioning || DevTools.disableCol ) // Devtools = debug code
        {
            return; // if not alive, leave this Method
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        lives.SuccessLevel();
    }

    private void StartDeathSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        mainEngineParticles.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        lives.LivesSystem();
    }

    #region Movement
    private void RespondToThrustInput ()
    {
        if (Input.GetKey(KeyCode.Space)) // Can Thrust while rotating
        {
            ApplyThrust();
        }
        else
        {
            StopApplyingThrust();
        }
    }

    private void StopApplyingThrust()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void ApplyThrust ()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying) // Only play if the Audio isn't playing
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    private void RespondToRotateInput ()
    {
        rigidBody.angularVelocity = Vector3.zero; // Remove Rotation due to physics

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
    }
    #endregion
}
