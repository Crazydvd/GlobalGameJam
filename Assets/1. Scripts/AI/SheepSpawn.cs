using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawn : MonoBehaviour
{
    public GameObject sheep; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(sheep, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
