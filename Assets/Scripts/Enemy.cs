using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed, health, damage, pushedForce;

    [SerializeField]
    private int value;

    [SerializeField]
    private bool bePushed;

    private PlayerController player;

    private Rigidbody2D rb;

    private bool killedByPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (rb.velocity != Vector2.zero)
        {
            rb.velocity = rb.velocity * 0.95f;
        }

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerController>())
        {
            player.DealDamage(damage);
            Death();
        }

        if (!collision.gameObject.GetComponent<PlayerController>())
        {            
            Destroy(collision.gameObject);
        }
    }

    public void DealDamage(float damage, string bulletShooter)
    {
        if (bePushed)
        {
            rb.AddForce(-(player.transform.position - transform.position).normalized * pushedForce, ForceMode2D.Impulse);           

        }

        health -= damage;
        if (health<=0)
        {
            if (bulletShooter != null)
            {
                if (bulletShooter == "Player")
                {
                    killedByPlayer = true;
                }
            }
            Death();
        }
    }

    private void Death()
    {
        if (!killedByPlayer)
        {
            FindObjectOfType<BuildMode>().AddMoney(value/2);
        }
        else
        {
            FindObjectOfType<BuildMode>().AddMoney(value);
        }
        
        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
}
