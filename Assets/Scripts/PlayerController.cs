using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetRotation(90);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetRotation(0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetRotation(270);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetRotation(180);
        }

    }

    private void SetRotation(float z)
    {
        transform.rotation = Quaternion.Euler(0,0,z);
    }
}
