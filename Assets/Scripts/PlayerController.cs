using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Bullet bullet;

    [SerializeField]
    private Transform shootPoint;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private Gun[] weapons;

    private Gun selectedWeapon;

    [SerializeField]
    private float muzzleTime, shootCooldown, health;

    private bool canShoot = true;


    private void Start()
    {
        selectedWeapon = weapons[0];
    }
    void Update()
    {
        if (!BuildMode.building)
        {
            var pos = Camera.main.WorldToScreenPoint(transform.position);
            var dir = Input.mousePosition - pos;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedWeapon = weapons[0];
                shootCooldown = selectedWeapon.fireRate;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selectedWeapon = weapons[1];
                shootCooldown = selectedWeapon.fireRate;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                selectedWeapon = weapons[2];
                shootCooldown = selectedWeapon.fireRate;
            }


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
        if (selectedWeapon == weapons[2])
        {
            for (int i = 0; i < selectedWeapon.shotgunBullets; i++)
            {
                var newBullet = Instantiate(bullet, shootPoint.position, transform.rotation);
                newBullet.GetComponent<Transform>().Rotate(0, 0, Random.Range(-selectedWeapon.spread, selectedWeapon.spread));
                newBullet.damage = selectedWeapon.damage;
                newBullet.speed = selectedWeapon.bulletSpeed;
            }           
            StartCoroutine(MuzzleFlash(muzzleTime));
            StartCoroutine(SetShoot(shootCooldown));
        }
        else
        {
            var newBullet = Instantiate(bullet, shootPoint.position, transform.rotation);
            newBullet.GetComponent<Transform>().Rotate(0, 0, Random.Range(-selectedWeapon.spread, selectedWeapon.spread));
            newBullet.damage = selectedWeapon.damage;
            newBullet.speed = selectedWeapon.bulletSpeed;
            StartCoroutine(MuzzleFlash(muzzleTime));
            StartCoroutine(SetShoot(shootCooldown));
        }
        

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
