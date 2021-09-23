using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        InvokeRepeating("Delay", 5, 1);
    }

    void Delay()
    {
        Debug.Log("OK");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
