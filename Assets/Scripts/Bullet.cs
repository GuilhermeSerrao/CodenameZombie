using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage, speed;

    public string shooter;


    private void Start()
    {
        StartCoroutine("DestroySelf");
    }
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            collision.GetComponent<Enemy>().DealDamage(damage, shooter);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
