using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Bullet bullet;

    [SerializeField]
    private Transform shootPoint, playerArt;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private float muzzleTime, shootCooldown, health;

    private bool canShoot = true;
 
    void Update()
    {
        if (!BuildMode.building)
        {
            var pos = Camera.main.WorldToScreenPoint(transform.position);
            var dir = Input.mousePosition - pos;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (Input.GetMouseButton(0) && canShoot)
            {
                Shoot();
            }
        }           

    }


    private IEnumerator MuzzleFlash(float time)
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(time);
        muzzleFlash.SetActive(false);
    }

    private IEnumerator SetShoot(float time)
    {
        canShoot = false;
        yield return new WaitForSeconds(time);
        canShoot = true;

    }

    private void Shoot()
    {        
        Instantiate(bullet, shootPoint.position, playerArt.rotation);
        StartCoroutine(MuzzleFlash(muzzleTime));
        StartCoroutine(SetShoot(shootCooldown));
    }

    public void DealDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            print("You dead");
        }
    }
}
