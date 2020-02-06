using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlouxGravity : MonoBehaviour
{
    public GravityAttractor attractor;
 
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        attractor.Attract(transform);
    }
}
