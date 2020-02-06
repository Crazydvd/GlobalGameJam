using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = -10f;

   public void Attract (Transform body)
    {
        //direction we want to face (center of the object/planet)
        Vector3 targetDir = (body.position - transform.position).normalized;

        //the body's current direction
        Vector3 bodyUp = body.up;

        body.GetComponent<Rigidbody>().AddForce(targetDir * gravity);
        body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
    }
}
