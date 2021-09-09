using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;//Tốc độ di chuyển của đạn

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveForward();
    }

    //Bay về phía trước
    void MoveForward()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    //
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Tilemap blocked")
        {
            Destroy(gameObject);
        }
        if (other.tag == "Target")
        {
            Destroy(gameObject);
        }
    }
}
