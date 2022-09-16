using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HauptmenuBtns : MonoBehaviour
{
    // Start is called before the first frame update
    public void Spielen_btn_pressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
}