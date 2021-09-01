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

    //Di chuyển
    void MoveForward()
    {
        if (flipX == false)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
