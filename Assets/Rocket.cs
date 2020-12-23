﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    new Rigidbody rigidbody;
    AudioSource audioSource;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 5f;
    [SerializeField] AudioClip mainEngineAudio;
    [SerializeField] AudioClip deathAudio;
    [SerializeField] AudioClip levelFinishAudio;

    enum State { Alive, Dying, Transcending};
    State state = State.Alive;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive)
        {
            RespondToThrustInput();
        }
        
        RespondToRotateInput();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive)
        {
            return;
        }
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartLevelFinishSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathAudio);
        Invoke("LoadFirstLevel", 1f);
    }

    private void StartLevelFinishSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(levelFinishAudio);
        Invoke("LoadNextScene", 1f);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }

    private void RespondToRotateInput()
    {
        rigidbody.freezeRotation = true; //Take manual control of rotation
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidbody.freezeRotation = false; // resume physics control of rotation
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrustWithSound();
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ApplyThrustWithSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineAudio);
        }
        rigidbody.AddRelativeForce(Vector3.up * mainThrust);
    }
}
