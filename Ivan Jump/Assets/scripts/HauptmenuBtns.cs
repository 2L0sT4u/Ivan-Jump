using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class HauptmenuBtns : MonoBehaviour
{
    public void Spielen_btn_pressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //ändern
    }
    
    public void Ende_btn_pressed()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void lvl00_btn_pressed()
    {
        SceneManager.LoadScene("Lvl00_scn");
    }

    public void lvl01_btn_pressed()
    {
        SceneManager.LoadScene("Lvl01_scn");
    }
    public void german_pressed()
    {
        SceneManager.LoadScene("Hauptmenu");
    }
    public void englisch_pressed()
    {
        SceneManager.LoadScene("Mainmenu");
    }
}