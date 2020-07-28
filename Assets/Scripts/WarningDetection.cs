using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningDetection : MonoBehaviour
{
    [SerializeField]
    private int direction;

    private List<Collider2D> targetColliders = new List<Collider2D>();
    private GameObject warning;
    private bool hasMouse;

    private void Start()
    {
        warning = transform.GetChild(0).gameObject;
        UpdateWarning();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<MouseTrigger>())
        {
            hasMouse = true;
            warning.SetActive(false);
        }

        if (!targetColliders.Contains(collision) && !collision.GetComponent<MouseTrigger>())
        {
            targetColliders.Add(collision);
            if (!hasMouse)
            {
                UpdateWarning();
            }            
        }      

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.GetComponent<MouseTrigger>())
        {
            hasMouse = false;
            UpdateWarning();
        }
        
        if (targetColliders.Contains(collision) && !collision.GetComponent<MouseTrigger>())
        {
            targetColliders.Remove(collision);

            if (!hasMouse)
            {
                UpdateWarning();
            }

        }       
        
        
    }

    public void UpdateWarning()
    {
        if (targetColliders.Count > 0)
        {
            warning.SetActive(true);
        }
        else
        {
            warning.SetActive(false);
        }
    }
}
