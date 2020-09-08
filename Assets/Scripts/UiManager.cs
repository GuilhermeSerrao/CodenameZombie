using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverScreen, gameScreen, buildMenu;
   

    public void LoseGame()
    {
        gameScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void BuildMenu(bool building)
    {
        if (building)
        {
            buildMenu.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        {
            buildMenu.SetActive(false);
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }
}
