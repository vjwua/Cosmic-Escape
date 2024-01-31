using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidBody;
    [SerializeField] float mainThrust = 100.0f;
    [SerializeField] float rotationThrust = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
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
            rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(Vector3.back);
        }
    }

    void ApplyRotation(Vector3 keycodeVector)
    {
        rigidBody.freezeRotation = true;  // freezing rotation so we can manually rotate
        transform.Rotate(keycodeVector * rotationThrust * Time.deltaTime);
        rigidBody.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
