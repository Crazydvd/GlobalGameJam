using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandom : MonoBehaviour
{
    public List<GameObject> listGoSpawn = new List<GameObject>();

    private void Start()
    {
        GameObject goToSpawn = listGoSpawn[Random.Range(0, listGoSpawn.Count)];
        Instantiate(goToSpawn, transform.position, Quaternion.identity);
        Destroy(gameObject);
       
    }
}
