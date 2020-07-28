using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private Transform nearestTarget;
    private List<Collider2D> targetColliders = new List<Collider2D>();
    private PlayerController player;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.name);
        if (!targetColliders.Contains(other))
        {
            targetColliders.Add(other);

            if (nearestTarget == null)
            {
                nearestTarget = other.transform;
                player.nearestTarget = nearestTarget;
            }
            else
            {
                GetTarget();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (targetColliders.Contains(other))
        {
            if (other.transform == nearestTarget)
            {
                if (targetColliders.Count <= 0)
                {
                    nearestTarget = null;
                    player.nearestTarget = null;
                }
                else
                {
                    nearestTarget = null;
                    player.nearestTarget = null;
                    GetTarget();
                }                
            }
            targetColliders.Remove(other);
            
            
        }
    }

    public void GetTarget()
    {
        
        if (nearestTarget != null)
        {
            distance = Vector3.Distance(player.transform.position, nearestTarget.position);
        }
        else
        {
            if (targetColliders.Count > 0)
            {
                distance = Vector3.Distance(player.transform.position, targetColliders[0].transform.position);
            }
            else
            {
                distance = 10000000000;
            }
        }

        if (targetColliders.Count > 0)
        {
            foreach (var item in targetColliders)
            {
                if (Vector3.Distance(player.transform.position, item.transform.position) < distance)
                {
                    nearestTarget = item.transform;
                }
            }
        }
        if (nearestTarget != null)
        {
            player.nearestTarget = nearestTarget;

        }

    }

    public void ClearTargets()
    {
        targetColliders.Clear();
    }
}
