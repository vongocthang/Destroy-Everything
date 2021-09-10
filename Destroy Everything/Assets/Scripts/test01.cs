using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class test01 : MonoBehaviour
{
    public bool check01 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(0) && check01 == false)
        {
            check01 = true;
        }
        if (Input.GetMouseButtonDown(0) && check01 == true)
        {
            check01 = false;
        }
    }
}
