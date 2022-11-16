using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ZeitMessen : MonoBehaviour
{
    public float zeit;
    public bool zeitläuft = false;
    public Text Anzeige, Anzeige2;
    public double min, sec;
    public double min1, sec1, min2, sec2;

    public PlayerController pc;

    private void Start()
    {
        if (File.Exists("Daten.Xml"))
        { 
            pc.laden();
        }
    }

    void Update()
    {
        if (zeitläuft == true)
        {
            zeit += Time.deltaTime;            
        }

        min = (Mathf.Round(zeit) - Mathf.Round(zeit) % 60) / 60;
        sec = Mathf.Round(zeit) % 60;

        if (min < 10 && sec < 10)
        {
            Anzeige.text = "Zeit: 0" + min.ToString() + ":0" + sec.ToString() ;
        }
        if (min < 10 && sec >= 10)
        {
            Anzeige.text = "Zeit: 0" + min.ToString() + ":" + sec.ToString();
        }
        if (min >= 10 && sec < 10)
        {
            Anzeige.text = "Zeit: " + min.ToString() + ":0" + sec.ToString();
        }
        if (min >= 10 && sec >= 10)
        {
            Anzeige.text = "Zeit: " + min.ToString() + ":" + sec.ToString();
        }    
    }
}
