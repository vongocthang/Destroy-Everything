using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public GameObject gun;
    Animator gunAnim;
    public GameObject bullet;
    public GameObject leg;
    public GameObject head;
    public GameObject[] target;//Tập các mục tiêu cần tiêu diệt
    float dis;//Khoảng cách từ Player đến mục tiêu gần nhất
    int location;//Vị trí của mục tiêu gần nhất trong mảng target
    public bool flipX;

    public MainUI mainUI;
    

    //Biến tạm thời
    float timeLine;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("Target");
        dis = Vector3.Distance(transform.position, target[0].transform.position);
        gunAnim = gun.GetComponent<Animator>();
        timeLine = Time.time;
        flipX = false;

        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FindTarget();
        GunControl();
        //HeadControl();
        BulletControl();
        FlipControl();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tilemap blocked")
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            Debug.Log("cham dat");
            mainUI.down = false;
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
