using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningDetection : MonoBehaviour
{
    [SerializeField]
    private int direction;

    private List<Collider2D> targetColliders = new List<Collider2D>();
    private GameObject warning;
    private PlayerController player;

    private void Start()
    {
        warning = transform.GetChild(0).gameObject;
        player = FindObjectOfType<PlayerController>();
        UpdateWarning();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!targetColliders.Contains(collision))
        {
            targetColliders.Add(collision);
        }
        UpdateWarning();
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (targetColliders.Contains(collision))
        {
            targetColliders.Remove(collision);
        }
        UpdateWarning();
    }

    public void UpdateWarning()
    {
        if (targetColliders.Count > 0 && direction != player.direction)
        {
            warning.SetActive(true);
        }
        else
        {
            warning.SetActive(false);
        }
    }
}
