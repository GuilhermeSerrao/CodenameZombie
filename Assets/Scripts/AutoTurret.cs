using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurret : MonoBehaviour
{

    [SerializeField]
    private Bullet bullet;

    [SerializeField]
    private Transform shootPoint;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private float muzzleTime, shootCooldown, health, spread;

    [SerializeField]
    private int damage;

    private bool canShoot = true;

    private List<Collider2D> possibleTargets = new List<Collider2D>();

    private Transform target = null;

    private float distance;


    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (canShoot)
            {
                var newBullet = Instantiate(bullet, shootPoint.position, transform.rotation);
                newBullet.GetComponent<Transform>().Rotate(0, 0, Random.Range(-spread, spread));
                newBullet.damage = damage;

                StartCoroutine(MuzzleFlash(muzzleTime));
                StartCoroutine(SetShoot(shootCooldown));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Enemy>())
        {
            if (!possibleTargets.Contains(collision))
            {
                possibleTargets.Add(collision);
            }
            if (!target)
            {                
                target = collision.transform;
            }            
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (possibleTargets.Contains(collision))
        {
            possibleTargets.Remove(collision);
        }

        if (target == collision.transform)
        {
            target = null;
        }

       

        if (!target && possibleTargets.Count > 0)
        {
            target = possibleTargets[Random.Range(0, possibleTargets.Count)].transform;
            
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
}
