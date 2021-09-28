using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class TargetControl : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public TargetHB heathBar;
    //public TMP_Text point;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        heathBar.SetHealth(health, maxHealth);
        //point.text = maxHealth.ToString();

        GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
        player = a[0].GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }

    //Chết
    void Die()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            Debug.Log("bi ban trung");
            health -= player.damge;
            heathBar.SetHealth(health, maxHealth);
            //point.text = health.ToString();
        }
    }
}
