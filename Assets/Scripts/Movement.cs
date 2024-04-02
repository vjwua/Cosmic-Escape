using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100.0f;
    [SerializeField] float rotationThrust = 100.0f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem rocketEngineParticles;

    [SerializeField] ParticleSystem leftSideThrusterParticles;

    [SerializeField] ParticleSystem rightSideThrusterParticles;

    Rigidbody rigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {   
            StartThrusting();
        }
        else {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RightRotating();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            LeftRotating();
        }
        else {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!rocketEngineParticles.isPlaying) {
            rocketEngineParticles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        rocketEngineParticles.Stop();
    }

    void LeftRotating() {
        ApplyRotation(Vector3.back);
        if (!leftSideThrusterParticles.isPlaying) {
            leftSideThrusterParticles.Play();
        }
    }

    void RightRotating() {
        ApplyRotation(Vector3.forward);
        if (!rightSideThrusterParticles.isPlaying) {
            rightSideThrusterParticles.Play();
        }
    }

    void StopRotating() {
        leftSideThrusterParticles.Stop();
        rightSideThrusterParticles.Stop();
    }

    void ApplyRotation(Vector3 keycodeVector)
    {
        rigidBody.freezeRotation = true;  // freezing rotation so we can manually rotate
        transform.Rotate(keycodeVector * rotationThrust * Time.deltaTime);
        rigidBody.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
