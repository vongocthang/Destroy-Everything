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
            if (player.getDown == true || player.jumpDown == true)
            {
                action = false;
                player.getDown = false;
                player.jumpDown = false;
                Debug.Log("không hành động");
            }
        }
        if (other.tag == "Tilemap not blocked")
        {
            if (player.getDown == true || player.jumpDown == true)
            {
                action = false;
                player.getDown = false;
                player.jumpDown = false;
                Debug.Log("không hành động");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Tilemap blocked")
        {
            action = true;
            if (player.jumpUp == false)
            {
                player.jumpDown = true;
                Debug.Log("nhảy xuống");
            }
        }
        if (other.tag == "Tilemap not blocked")
        {
            action = true;
            if (player.jumpUp == false)
            {
                player.jumpDown = true;
                Debug.Log("nhảy xuống");
            }
        }
    }
}
