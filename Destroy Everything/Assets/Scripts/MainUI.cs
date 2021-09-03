using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    GameObject player;//
    Rigidbody2D playerRB;
    public float moveSpeed;//Tốc đọ di chuyển
    public float jumpSpeed;//Lực nhảy
    public bool up = false;//Đang nhảy lên
    public bool down = false;//Đang hạ xuống
    Animator playerAnim;

    public FloatingJoystick floJoy;
    //Biến tạm
    public float height;//Độ cao tối đa có thể nhảy đến;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
        player = a[0];
        playerRB = player.GetComponent<Rigidbody2D>();
        playerAnim = player.GetComponentInChildren<Animator>();

        //Biến tạm
        
    }

    // Update is called once per frame
    void Update()
    {
        JoystickControll();
        JumpUp();
    }

    //Điều khiển Joystick di chuyển trái phải
    void JoystickControll()
    {
        Vector2 direction = Vector2.right * floJoy.Horizontal;

        if(direction.x > 0)
        {
            player.transform.Translate(Vector2.right * direction.x * moveSpeed * Time.deltaTime);
            playerAnim.Play("walk");
        }
        else
        {
            player.transform.Translate(Vector2.right * direction.x * moveSpeed * Time.deltaTime);
        }

        if (direction.x == 0)
        {
            playerAnim.Play("ide");
        }
    }

    float y;
    //Điều khiển nhảy
    public void JumpUp()
    {
        if (up == false)
        {
            y = player.transform.position.y;
        }
        if (Input.GetKey(KeyCode.UpArrow) && down == false)
        {
            up = true;
            playerRB.gravityScale = 0;
            player.transform.Translate(Vector2.up * jumpSpeed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || player.transform.position.y > y + height)
        {
            Debug.Log("ha xuong");
            down = true;
            up = false;
            playerRB.gravityScale = 1;
        }
        //if (jumpSpeed < aa)
        //{
        //    jumpSpeed = 0;
        //}
    }

    //Hạ xuống sau khi nhảy lên
    public void GetDown()
    {

    }

}
