using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;//Tốc độ di chuyển của đạn
    bool flipX;//Tương ứng với flipX của Player

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
        flipX = a[0].GetComponent<Player>().flipX;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveForward();
    }

    //Bay về phía trước
    void MoveForward()
    {
        //Khi súng lật mặt thì đạn cũng phải lật
        if (flipX == false)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    //
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Tilemap blocked")
        {
            //Debug.Log("trung tuong");
            Destroy(gameObject);
        }
        if (other.tag == "Target")
        {
            //Debug.Log("trung muc tieu");
            Destroy(gameObject);
        }
    }
}
