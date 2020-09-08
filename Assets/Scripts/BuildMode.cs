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

    [SerializeField]
    private GameObject buildingPanel;

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
                FindObjectOfType<UiManager>().BuildMenu(building);                
            }
            else
            {
                FindObjectOfType<UiManager>().BuildMenu(building);                
                ResetStructure();                
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
                    selectedStructure = structures[0];
                    spriteRenderer.sprite = selectedStructure.GetComponent<Structure>().spr;
                }
                else
                {
                    selectedStructure = structures[0];
                    spriteRenderer.sprite = selectedStructure.GetComponent<Structure>().spr;
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (selectedStructure != null)
                {
                    ResetStructure();
                    selectedStructure = structures[1];
                    spriteRenderer.sprite = selectedStructure.GetComponent<Structure>().spr;
                }
                else
                {
                    selectedStructure = structures[1];
                    spriteRenderer.sprite = selectedStructure.GetComponent<Structure>().spr;
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
