using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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


    //Nhảy lên
    public void JumpUp()
    {
        if (playerSC.jumpUp == false && playerSC.jumpDown == false && playerSC.getDown == false)
        {
            Debug.Log("nhảy lên");
            playerSC.jumpUp = true;
            playerSC.cd.isTrigger = true;
            playerRB.velocity = Vector2.up * playerSC.jumpSpeed;
        }
    }

    //Nhảy xuống
    public void JumpDown()
    {
        if (playerSC.jumpDown == false)
        {
            if (playerSC.onBlocked == false)
            {
                Debug.Log("nhảy xuống");
                playerSC.cd.isTrigger = true;
                playerSC.jumpDown = true;
            }
        }
    }
}
