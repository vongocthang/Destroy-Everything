using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    public Player player;
    public bool action = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Tilemap blocked")
        {
            if (player.jumpUp == false && player.getDown == false && player.jumpDown == false)
            {
                action = false;
            }
        }
        if (other.tag == "Tilemap not blocked")
        {
            if (player.jumpUp == false && player.getDown == false && player.jumpDown == false)
            {
                action = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Tilemap blocked")
        {
            action = true;
        }
        if (other.tag == "Tilemap not blocked")
        {
            action = true;
        }
    }
}
