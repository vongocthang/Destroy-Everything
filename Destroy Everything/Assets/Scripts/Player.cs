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
        JumpDown();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tilemap blocked")
        {
            //rb.simulated = false;
            getDown = false;
            cd.isTrigger = false;
            Debug.Log("chạm đất");
        }
        if(other.tag=="Tilemap not blocked")
        {
            //rb.simulated = false;
            getDown = false;
            cd.isTrigger = false;
            Debug.Log("chạm địa hình");
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

    public float y;
    //Điều khiển nhảy lên - ấn nút mũi tên lên
    public void JumpUp()
    {
        //Lấy vị trí tung độ của Player khi đang đứng trên địa hình
        if (jumpUp == false)
        {
            y = transform.position.y;
        }
        if (Input.GetKey(KeyCode.UpArrow) && getDown == false && jumpDown == false)
        {
            Debug.Log("nhảy lên");
            cd.isTrigger = true;
            jumpUp = true;
            rb.simulated = false;
            transform.Translate(Vector2.up * jumpSpeed * Time.deltaTime);
        }
    }

    //Hạ xuống sau khi nhảy lên - bị động
    public void GetDown()
    {
        if (jumpUp == true)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow) || transform.position.y > y + height)
            {
                Debug.Log("hạ xuống");
                rb.simulated = true;
                getDown = true;
                jumpUp = false;
            }
        }
        if (getDown == true)
        {
            //legAnim.get("ide");
            //legAnim.Play("jump down");
        }
    }

    //Chủ động nhảy xuống - ấn nút mũi tên xuống
    public void JumpDown()
    {
        if (getDown == false && getDown == false)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

            }
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
