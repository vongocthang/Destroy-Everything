using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    GameObject player;//
    Rigidbody2D playerRB;
    public float moveSpeed;//Tốc đọ di chuyển
    public float jumpSpeed;//Lực nhảy
    Animator playerAnim;

    public FloatingJoystick floJoy;
    public float addForce;
    public float bootsMultiplier;

    //Biến tạm
    public float aa;
    float bb;
    float mass;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
        player = a[0];
        playerRB = player.GetComponent<Rigidbody2D>();
        playerAnim = player.GetComponentInChildren<Animator>();

        //Biến tạm
        bb = player.GetComponent<Rigidbody2D>().mass;

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


    //Điều khiển nhảy
    public void JumpUp()
    {
        //player.transform.Translate(Vector2.up * jumpSpeed * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("nhay");
            playerRB.AddForce(new Vector2(0, jumpSpeed) * Time.deltaTime, ForceMode2D.Impulse);

            jumpSpeed-= aa;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            jumpSpeed = bb;
        }
        if (jumpSpeed < aa)
        {
            jumpSpeed = 0;
        }
    }
}
