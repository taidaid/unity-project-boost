using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource m_MyAudioSource;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 5f; 

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                // TODO recharge fuel
                break;
            default:
                // TODO kill player
                break;
        }
    }

    private void Rotate()
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

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!m_MyAudioSource.isPlaying)
            {
                m_MyAudioSource.Play();
            }
            rigidbody.AddRelativeForce(Vector3.up * mainThrust);
        }
        else
        {
            m_MyAudioSource.Stop();
        }
    }
}
