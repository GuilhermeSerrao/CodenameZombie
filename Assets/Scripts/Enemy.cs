﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed, health, damage;

    [SerializeField]
    private int value;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
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

    public void DealDamage(float damage)
    {
        health -= damage;
        if (health<=0)
        {
            Death();
        }
    }

    private void Death()
    {
        FindObjectOfType<BuildMode>().AddMoney(value);
        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
}
