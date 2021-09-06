using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    GameObject player;
    Player playerSC;//
    Rigidbody2D playerRB;
    Animator playerAnim;

    public FloatingJoystick floJoy;
    //Biến tạm


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
        player = a[0];
        playerSC = player.GetComponent<Player>();
        playerRB = player.GetComponent<Rigidbody2D>();
        playerAnim = player.GetComponentInChildren<Animator>();

        //Biến tạm
        
    }

    // Update is called once per frame
    void Update()
    {
        JoystickControll();
    }

    //Điều khiển Joystick di chuyển trái phải
    void JoystickControll()
    {
        Vector2 direction = Vector2.right * floJoy.Horizontal;

        if(direction.x > 0)
        {
            player.transform.Translate(Vector2.right * direction.x * playerSC.moveSpeed
                * Time.deltaTime);
            playerAnim.Play("walk");
        }
        else
        {
            player.transform.Translate(Vector2.right * direction.x * playerSC.moveSpeed
                * Time.deltaTime);
        }

        if (direction.x == 0)
        {
            playerAnim.Play("ide");
        }
    }

    public void JumpDown()
    {
        player.GetComponent<Collider2D>().isTrigger = true;
        playerSC.jumpDown = true;
    }
}
