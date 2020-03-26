using System;
using AI;
using UnityEngine;
using Random = UnityEngine.Random;

[Obsolete("Use AI.WanderBehaviour instead", true)]
public class WanderBehaviourOBSOLETE : MonoBehaviour
{
    private SheepBehaviourManager sheepBehaviourManager;
    private Rigidbody rb;

    private float wanderOrientation = 0; 
    bool smooting = true;

    // Start is called before the first frame update
    void Start()
    {
        sheepBehaviourManager = GetComponent<SheepBehaviourManager>();
        rb = GetComponent<Rigidbody>();
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

        wanderOrientation += GetRandomNumber() * sheepBehaviourManager.sheepSettings.WanderRate;

        float targetOrientation = wanderOrientation + sheepOrientation; //get new orientation to rotate to 

        Vector3 targetPosition = transform.position + (GetOrientationVector(sheepOrientation) * sheepBehaviourManager.sheepSettings.WanderOffset);

        targetPosition = targetPosition + (GetOrientationVector(targetOrientation) * sheepBehaviourManager.sheepSettings.WanderRadius);
        
        return Seek(targetPosition);
    }

    private void LookWhereYouGoing()
    {
        Vector3 direction = rb.velocity;

        direction.Normalize();

        if (direction.sqrMagnitude > 0.001f)
        {
            float targetRotation = -1 * (Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg);
            float newRotation = Mathf.LerpAngle(rb.rotation.eulerAngles.y, targetRotation, Time.deltaTime * sheepBehaviourManager.sheepSettings.RotateSpeed);
            rb.rotation = Quaternion.Euler(0,newRotation,0);
        }
    }
    
    private Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 acceleration = targetPosition - transform.position;
        acceleration.y = 0;
        acceleration.Normalize();
    
        acceleration *= sheepBehaviourManager.sheepSettings.MaxAcceleration;
        
        return acceleration;
    }

    private void Steer(Vector3 linearAcceleration)
    {
        //Debug.Log(linearAcceleration);
       // Vector3 testVec = new Vector3(0.3f,0,0.0f);
        rb.velocity += linearAcceleration * Time.deltaTime;

        if (rb.velocity.magnitude > sheepBehaviourManager.sheepSettings.MaxVelocity)
        {
            rb.velocity = rb.velocity.normalized * sheepBehaviourManager.sheepSettings.MaxVelocity;
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
