using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D cd;
    public GameObject gun;
    Animator gunAnim;
    public GameObject bullet;
    public GameObject leg;
    Animator legAnim;
    Leg legScript;
    public GameObject head;
    public GameObject headPosition;
    public GameObject[] target;//Tập các mục tiêu cần tiêu diệt
    float dis;//Khoảng cách từ Player đến mục tiêu gần nhất
    int location;//Vị trí của mục tiêu gần nhất trong mảng target
    public bool flipX;

    public float moveSpeed;//Tốc đọ di chuyển trái phải
    public float jumpSpeed;//Lực nhảy lên
    public bool jumpUp = false;//Đang nhảy lên - ấn nút mũi tên lên
    public bool getDown = false;//Đang hạ xuống sau khi nhảy lên
    public bool jumpDown = false;//Đang chủ động nhảy xuống - ấn nút mũi tên xuống

    public MainUI mainUI;
    

    //Biến tạm thời
    float timeLine;
    public float height;//Độ cao tối đa có thể nhảy đến;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        target = GameObject.FindGameObjectsWithTag("Target");
        dis = Vector3.Distance(transform.position, target[0].transform.position);
        gunAnim = gun.GetComponent<Animator>();
        legAnim = leg.GetComponent<Animator>();
        legScript = leg.GetComponent<Leg>();
        timeLine = Time.time;
        flipX = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FindTarget();
        GunControl();
        HeadControl();
        BulletControl();
        FlipControl();
        JumpUp();
        GetDown();
        //JumpDown();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Tilemap blocked")
        {
            cd.isTrigger = false;
        }
        if(other.tag=="Tilemap not blocked")
        {

        }
    }

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.tag == "Tilemap blocked")
    //    {
    //        //rb.simulated = true;
    //        Debug.Log("địa hình bị chặn");
    //        getDown = false;
    //    }
    //    if (other.tag == "Tilemap not blocked")
    //    {
    //        //rb.simulated = false;
    //        Debug.Log("địa hình không bị chặn");
    //        getDown = false;
    //    }
    //}

    //Điều khiển nhảy lên - ấn nút mũi tên lên
    public void JumpUp()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpUp == false && jumpDown == false
            && getDown == false)
        {
            Debug.Log("nhảy lên");
            jumpUp = true;
            cd.isTrigger = true;
            rb.velocity = Vector2.up * jumpSpeed;
        }
    }

    public float yAfter, yBefor;
    //Hạ xuống sau khi nhảy lên - bị động
    public void GetDown()
    {
        yBefor = transform.position.y;
        if (yBefor >= yAfter)
        {
            yAfter = yBefor;
        }
        if (jumpUp == false && getDown == false && jumpDown == false)
        {
            yAfter = yBefor;
        }
        if (yBefor < yAfter && jumpUp == true)
        {
            Debug.Log("rơi xuống");
            jumpUp = false;
            getDown = true;
            cd.isTrigger = false;
        }
    }

    //Chủ động nhảy xuống - ấn nút mũi tên xuống
    public void JumpDown()
    {
        if (jumpDown == true)
        {
            cd.isTrigger = true;
        }
    }

    //Điều khiển súng, nhìn về mục tiêu gần nhất
    void GunControl()
    {
        Vector3 a = target[location].transform.position;
        a = a - new Vector3(0, 0, target[location].transform.position.z);
        Vector3 direction = a - gun.transform.position;

        float angle;
        if (flipX == false)
        {
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
        else
        {
            angle = Mathf.Atan2(0 - direction.y, 0 - direction.x) * Mathf.Rad2Deg;
        }
        
        gun.GetComponent<Rigidbody2D>().rotation = angle;
    }

    //Xoay đầu hướng nhìn về mục tiêu gần nhất
    void HeadControl()
    {
        head.transform.position = headPosition.transform.position;

        Vector3 a = target[location].transform.position;
        a = a - new Vector3(0, 0, target[location].transform.position.z);
        Vector3 direction = a - head.transform.position;

        float angle;
        if (flipX == false)
        {
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
        else
        {
            angle = Mathf.Atan2(0 - direction.y, 0 - direction.x) * Mathf.Rad2Deg;
        }

        head.GetComponent<Rigidbody2D>().rotation = angle;
    }

    //Quản lý đạn
    void BulletControl()
    {
        bullet.transform.position = gun.transform.position + new Vector3(0, 0.1f, 0);
        //bullet.transform.rotation = gun.transform.rotation;
        bullet.GetComponent<SpriteRenderer>().flipX = gun.GetComponent<SpriteRenderer>().flipX;
        if (target.Length > 0)
        {
            if (Time.time > timeLine + 0.5)
            {
                gunAnim.Play("fire");
                Instantiate(bullet, bullet.transform.position, gun.transform.rotation);
                timeLine = Time.time;
            }
            
        }
    }

    //Tìm ra mục tiêu gần nhất với Player
    void FindTarget()
    {
        target = GameObject.FindGameObjectsWithTag("Target");

        for (int i = 1; i < target.Length; i++)
        {
            float b = Vector3.Distance(transform.position, target[i].transform.position);
            if (dis > b)
            {
                dis = b;
                location = i;
            }
        }
    }

    //Điều khiển lật
    void FlipControl()
    {
        if(transform.position.x > target[location].transform.position.x)
        {
            head.GetComponent<SpriteRenderer>().flipX = true;
            gun.GetComponent<SpriteRenderer>().flipX = true;
            bullet.GetComponent<SpriteRenderer>().flipX = true;
            leg.GetComponent<SpriteRenderer>().flipX = true;
            flipX = true;
        }
        else
        {
            head.GetComponent<SpriteRenderer>().flipX = false;
            gun.GetComponent<SpriteRenderer>().flipX = false;
            bullet.GetComponent<SpriteRenderer>().flipX = false;
            leg.GetComponent<SpriteRenderer>().flipX = false;
            flipX = false;
        }
    }

    IEnumerator CreateBullet(float second)
    {
        yield return new WaitForSeconds(second);

    }
    //a = a - new Vector3(0, 0, ray.origin.z);
    //Vector3 direction = a - arrow.transform.position;
    //angle = Mathf.Atan2(direction.y, direction.x)* Mathf.Rad2Deg;
    //arrowRb.rotation = angle;
}
