using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    public Player player;
    public bool action = false;//Thực hiện hành động khác di chuyển sang trái-phải

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
            player.onBlocked = true;

            if (player.getDown == true || player.jumpDown == true)
            {
                action = false;
                player.getDown = false;
                player.jumpDown = false;
            }
        }
        if (other.tag == "Tilemap not blocked")
        {
            player.onBlocked = false;

            if (player.getDown == true || player.jumpDown == true)
            {
                action = false;
                player.getDown = false;
                player.jumpDown = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Tilemap blocked")
        {
            action = true;
            if (player.jumpUp == false && player.jumpDown == false)
            {
                player.jumpDown = true;
            }
        }
        if (other.tag == "Tilemap not blocked")
        {
            action = true;
            if (player.jumpUp == false && player.jumpDown == false)
            {
                player.jumpDown = true;
            }
        }
    }
}
