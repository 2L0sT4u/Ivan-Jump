using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //zeitmessen
    public float zeit;
    public bool zeitläuft = false;
    public Text Anzeige, Anzeige2;
    public double min, sec;
    public double min1, sec1, min2, sec2;
    //.

    public PlayerController pc;
    private Rigidbody2D rb2D;

    private float MoveSpeed;
    private float JumpForce;
    private bool isJumping;
    private float MoveHorizontal;
    private bool DoubleJumpUsed;
    private bool atWall;
    private bool wallJumped;

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        MoveSpeed = 50f;
        JumpForce = 200f;
        isJumping = false;
        DoubleJumpUsed = false;
        atWall = false;
        wallJumped = false;


        //für zeitmessen
        if (File.Exists("Daten.Xml"))
        {
            laden();
        }
    }

    void Update()
    {
        MoveHorizontal = Input.GetAxisRaw("Horizontal");


        //für Zeitmessen
        if (zeitläuft == true)
        {
            zeit += Time.deltaTime;
        }

        min = (Mathf.Round(zeit) - Mathf.Round(zeit) % 60) / 60;
        sec = Mathf.Round(zeit) % 60;

        if (min < 10 && sec < 10)
        {
            Anzeige.text = "Zeit: 0" + min.ToString() + ":0" + sec.ToString();
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

    private void FixedUpdate()
    {
        if (MoveHorizontal != 0)
        {
            rb2D.AddForce(new Vector2(MoveHorizontal * MoveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            rb2D.AddForce(new Vector2(0f, -2 * JumpForce), ForceMode2D.Impulse);
        }

        if (!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.AddForce(new Vector2(0f, 7 * JumpForce), ForceMode2D.Impulse);
        }

        if (!DoubleJumpUsed && isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.AddForce(new Vector2(0f, 7 * JumpForce), ForceMode2D.Impulse);
            DoubleJumpUsed = true;
        }


        if (!wallJumped && atWall && isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.AddForce(new Vector2(0f, 7 * JumpForce), ForceMode2D.Impulse);
            wallJumped = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
            DoubleJumpUsed = false;
        }

        if (collision.gameObject.tag == "wall")
        {
            atWall = true;
            wallJumped = false;
        }

        if (collision.gameObject.tag == "RandUnten")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.gameObject.tag == "ziel" || collision.gameObject.tag == "ziel2")
        {
            Thread.Sleep(100);
            SceneManager.LoadScene("Hauptmenu");
            zeitläuft = false;

            switch (collision.gameObject.tag)
            {
                case "ziel":
                    min1 = min;
                    sec1 = sec;

                    Liste.Clear();
                    Liste.Add(min1);
                    Liste.Add(sec1);
                    Liste.Add(min2);
                    Liste.Add(sec2);
                    speichern();
                    break;

                case "ziel2":
                    min2 = min;
                    sec2 = sec;
                    Liste.Clear();

                    Liste.Add(min1);
                    Liste.Add(sec1);
                    Liste.Add(min2);
                    Liste.Add(sec2);
                    speichern();
                    break;
            }
        }

        if (collision.gameObject.tag == "start" || collision.gameObject.tag == "start2")
        {
            zeitläuft = true;
            zeit = 0;

        switch (collision.gameObject.tag)
            {
                case "start":
                    if (min1 < 10 && sec1 < 10)
                    {
                        Anzeige2.text = "0" + min1.ToString() + ":0" + sec1.ToString();
                    }
                    if (min1 < 10 && sec1 >= 10)
                    {
                        Anzeige2.text = "0" + min1.ToString() + ":" + sec1.ToString();
                    }
                    if (min1 >= 10 && sec1 < 10)
                    {
                        Anzeige2.text = min1.ToString() + ":0" + sec1.ToString();
                    }
                    if (min1 >= 10 && sec1 >= 10)
                    {
                        Anzeige2.text = min1.ToString() + ":" + sec1.ToString();
                    }
                    break;
                case "start2":
                    if (min2 < 10 && sec2 < 10)
                    {
                        Anzeige2.text = "0" + min2.ToString() + ":0" + sec2.ToString();
                    }
                    if (min2 < 10 && sec2 >= 10)
                    {
                        Anzeige2.text = "0" + min2.ToString() + ":" + sec2.ToString();
                    }
                    if (min2 >= 10 && sec2 < 10)
                    {
                        Anzeige2.text = min2.ToString() + ":0" + sec2.ToString();
                    }
                    if (min2 >= 10 && sec2 >= 10)
                    {
                        Anzeige2.text = min2.ToString() + ":" + sec2.ToString();
                    }
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }

        if (collision.gameObject.tag == "wall")
        {
            atWall = false;
            wallJumped = true;
        }
    }

    public bool sprachedt;
    public void Deutsch()
    {
        sprachedt = true;
    }

    public void Englisch()
    {
        sprachedt = false;
    }





    //speichern

    public List<double> Liste = new List<double>();

    public void speichern()
    {
        if (File.Exists("Daten.xml"))
        {
            File.Delete("Daten.xml");
        }

        XmlSerializer xmlserializer = new XmlSerializer(typeof(List<double>));
        TextWriter writer = new StreamWriter("Daten.xml");
        xmlserializer.Serialize(writer, Liste);
        writer.Close();
    }

    public void laden()
    {
        if (File.Exists("Daten.xml"))
        {
            XmlSerializer xmlserializer = new XmlSerializer(typeof(List<double>));
            FileStream filestream = new FileStream("Daten.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
            Liste = (List<double>)xmlserializer.Deserialize(filestream);
            int n = 1;
            foreach (double x in Liste)
            {
                switch (n)
                {
                    case 1:
                        min1 = x;
                        break;
                    case 2:
                        sec1 = x;
                        break;
                    case 3:
                        min2 = x;
                        break;
                    case 4:
                        sec2 = x;
                        break;
                }
                n++;
            }
        }
    }
}