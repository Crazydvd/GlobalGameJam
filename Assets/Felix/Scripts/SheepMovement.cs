using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SheepMovement : MonoBehaviour
{

    [FormerlySerializedAs("maxAacceleration")] [SerializeField]
    private float maxAcceleration = 13.0f;
    [SerializeField]
    private float maxVelocity = 5.0f;
    [SerializeField]
    private float rotateSpeed;

    [SerializeField] 
    private float wanderRate = 0.4f;

    [SerializeField] 
    private float wanderOffset = 1.3f;
    [SerializeField] 
    private float wanderRadius = 2.0f;
    
    
    

    private Rigidbody rb;

    private float wanderOrientation = 0; 
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        Vector3 sheepAcceleration = GetSteerOrientation();
        
        Steer(sheepAcceleration);
        LookWhereYouGoing();
    }

    private Vector3 GetSteerOrientation()
    {
        float sheepOrientation = OrientationInRadians(); //rotation of the sheep in radians

        wanderOrientation += GetRandomNumber() * wanderRate;

        float targetOrientation = wanderOrientation + sheepOrientation; //get new orientation to rotate to 

        Vector3 targetPosition = transform.position + (GetOrientationVector(sheepOrientation) * wanderOffset);

        targetPosition = targetPosition + (GetOrientationVector(targetOrientation) * wanderRadius);

        return Seek(targetPosition);
    }


    private void LookWhereYouGoing()
    {
        Vector3 direction = rb.velocity;
        
        direction.Normalize();

        if (direction.magnitude > 0.001)
        {
            float targetRotation = -1 * (Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg);
            float newRotation = Mathf.LerpAngle(rb.rotation.eulerAngles.y, targetRotation, Time.deltaTime * rotateSpeed);
            rb.rotation = Quaternion.Euler(0,newRotation,0);
            
        }
        
    }
    
    private Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 acceleration = targetPosition - transform.position;
        acceleration.y = 0;
        acceleration.Normalize();
    
        acceleration *= maxAcceleration;
        
        return acceleration;
    }

    private void Steer(Vector3 linearAcceleration)
    {
        rb.velocity += linearAcceleration * Time.deltaTime;

        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    private Vector3 GetOrientationVector(float orientation)
    {
        return new Vector3(Mathf.Cos(-orientation), 0, Mathf.Sin(-orientation));
    }

    private float OrientationInRadians()
    {
        return rb.rotation.eulerAngles.y * Mathf.Deg2Rad;
    }


    private float GetRandomNumber()
    {
        return Random.value - Random.value;
    }
    
}
