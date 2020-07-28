using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMode : MonoBehaviour
{
    public static bool building = false;

    [SerializeField]
    private Text moneyAmmount;

    [SerializeField]
    private GameObject[] structures;

    private GameObject selectedStructure = null;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private int money;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateMoneyUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            building = !building;
            if (building)
            {
                Time.timeScale = 0;
            }
            else
            {
                ResetStructure();
                Time.timeScale = 1;
            }
        }

        if (building)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (selectedStructure!=null)
                {
                    ResetStructure();
                }
                else
                {
                    selectedStructure = structures[0];
                    spriteRenderer.sprite = selectedStructure.GetComponent<SpriteRenderer>().sprite;
                }
                
            }
            if (selectedStructure!=null && Input.GetMouseButtonDown(0))
            {
                if (money >= selectedStructure.GetComponent<Structure>().cost)
                {
                    RemoveMoney(selectedStructure.GetComponent<Structure>().cost);
                    Instantiate(selectedStructure, transform.position, Quaternion.identity);
                }
                
            }
        }        
    }

    private void ResetStructure()
    {
        selectedStructure = null;
        spriteRenderer.sprite = null;
    }

    public void AddMoney(int value)
    {
        money += value;
        UpdateMoneyUI();
    }

    public void RemoveMoney(int value)
    {
        money -= value;
        UpdateMoneyUI();
    }

    private void UpdateMoneyUI()
    {
        moneyAmmount.text = money.ToString();
    }
}
