using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Bullet bullet;

    [SerializeField]
    private Transform shootPoint, playerArt;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private float muzzleTime;

    private PlayerDetection detection;

    public Transform nearestTarget;

    public int direction;

    private void Start()
    {
        detection = FindObjectOfType<PlayerDetection>();        
    }

    void Update()
    {
        if (nearestTarget != null)
        {
            Vector3 direction = nearestTarget.position - playerArt.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            playerArt.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            playerArt.rotation = transform.rotation;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, shootPoint.position, playerArt.rotation);
            StartCoroutine(MuzzleFlash(muzzleTime));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = 1;
            SetRotation(90);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = 2;
            SetRotation(0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = 3;
            SetRotation(270);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = 4;
            SetRotation(180);
        }

    }

    private void SetRotation(float z)
    {
        var warnings = FindObjectsOfType<WarningDetection>();
        foreach (var item in warnings)
        {
            item.UpdateWarning();
        }
        detection.ClearTargets();
        nearestTarget = null;            
        transform.rotation = Quaternion.Euler(0,0,z);
        detection.GetTarget();

    }

    private IEnumerator MuzzleFlash(float time)
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(time);
        muzzleFlash.SetActive(false);
    }
}
