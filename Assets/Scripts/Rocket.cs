﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    [SerializeField] static int lives = 3;
    [SerializeField] Text livesUI;

    Rigidbody rigidBody;
    AudioSource audioSource;

    bool isTransitioning = false;

    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        livesUI.text = lives.ToString();
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
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        mainEngineParticles.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        lives--;
        livesUI.text = lives.ToString();
        print(lives);
        if (lives < 0)
        {
            Invoke("RestartGame", levelLoadDelay);
        }
        else
        {
            Invoke("RestartLevel", levelLoadDelay);
        }
    }

    private void LoadNextLevel ()
    {
        int currentlevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextlevelIndex = currentlevelIndex + 1;

        if (nextlevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextlevelIndex = 0;
        }

        SceneManager.LoadScene(nextlevelIndex);
    }

    private void RestartLevel()
    {
        /*TODO: Make a difficulty selection later on
        Easy: Restart from same level when dead
        Hard: Restart from level 1 when dead
        Maybe put level stuff into a level manager?*/
        //SceneManager.LoadScene(0);
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
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
