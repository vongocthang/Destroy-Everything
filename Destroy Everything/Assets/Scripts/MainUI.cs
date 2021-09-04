using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    Player player;//
    Rigidbody2D playerRB;

    Animator playerAnim;

    public FloatingJoystick floJoy;
    //Biến tạm


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
        player = a[0].GetComponent<Player>();
        playerRB = a[0].GetComponent<Rigidbody2D>();
        playerAnim = a[0].GetComponentInChildren<Animator>();

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
            player.transform.Translate(Vector2.right * direction.x * player.moveSpeed
                * Time.deltaTime);
            playerAnim.Play("walk");
        }
        else
        {
            player.transform.Translate(Vector2.right * direction.x * player.moveSpeed
                * Time.deltaTime);
        }

        if (direction.x == 0)
        {
            playerAnim.Play("ide");
        }
    }
}
