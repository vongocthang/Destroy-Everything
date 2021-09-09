using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public Player player;
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateControl();
    }

    void RotateControl()
    {
        Vector3 relativePos = player.targetNearest.transform.position - parent.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        Quaternion current = parent.transform.localRotation;
        parent.transform.localRotation = Quaternion.Slerp(current, rotation, 100 * Time.deltaTime);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag=="Tilemap not blocked")
        {
            if (player.jumpDown == true)
            {
                player.cd.isTrigger = false;
            }
        }
    }
}
