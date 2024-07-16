using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float MainThrust = 1000f;
    [SerializeField] float rotationThrust = 200f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    Rigidbody rb;
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(rotationThrust);
            rightBooster.Play();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(-rotationThrust);
            leftBooster.Play();
        }
        else
        {
            //rightBooster.Stop();
            //leftBooster.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.back * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false;
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * MainThrust * Time.deltaTime);
            mainBooster.Play();
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop();
            mainBooster.Stop();
        }
    }
}
