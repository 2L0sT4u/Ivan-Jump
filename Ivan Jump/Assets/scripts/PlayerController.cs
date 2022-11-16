using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using System.IO;
using System.Xml.Serialization;

public class PlayerController : MonoBehaviour
{
    public ZeitMessen zm;

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
    }

    void Update()
    {
        MoveHorizontal = Input.GetAxisRaw("Horizontal");
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
            zm.zeitläuft = false;

            switch (collision.gameObject.tag)
            {
                case "ziel":
                    zm.min1 = zm.min;
                    zm.sec1 = zm.sec;

                    Liste.Clear();
                    Liste.Add(zm.min1);
                    Liste.Add(zm.sec1);
                    Liste.Add(zm.min2);
                    Liste.Add(zm.sec2);
                    speichern();
                    break;

                case "ziel2":
                    zm.min2 = zm.min;
                    zm.sec2 = zm.sec;
                    Liste.Clear();

                    Liste.Add(zm.min1);
                    Liste.Add(zm.sec1);
                    Liste.Add(zm.min2);
                    Liste.Add(zm.sec2);
                    speichern();
                    break;
            }
        }

        if (collision.gameObject.tag == "start" || collision.gameObject.tag == "start2")
        {
            zm.zeitläuft = true;
            zm.zeit = 0;
        }
        if (collision.gameObject.tag == "start")
        {
            if (zm.min1 < 10 && zm.sec1 < 10)
            {
                zm.Anzeige2.text = "0" + zm.min1.ToString() + ":0" + zm.sec1.ToString();
            }
            if (zm.min1 < 10 && zm.sec1 >= 10)
            {
                zm.Anzeige2.text = "0" + zm.min1.ToString() + ":" + zm.sec1.ToString();
            }
            if (zm.min1 >= 10 && zm.sec1 < 10)
            {
                zm.Anzeige2.text = zm.min1.ToString() + ":0" + zm.sec1.ToString();
            }
            if (zm.min1 >= 10 && zm.sec1 >= 10)
            {
                zm.Anzeige2.text = zm.min1.ToString() + ":" + zm.sec1.ToString();
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
        TextWriter writer = new StreamWriter("Datenaten.xml");
        xmlserializer.Serialize(writer, Liste);
        writer.Close();
    }

    public void laden()
    {
        if (File.Exists("Kontodaten.xml"))
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
                        zm.min1 = x;
                        break;
                    case 2:
                        zm.sec1 = x;
                        break;
                    case 3:
                        zm.min2 = x;
                        break;
                    case 4:
                        zm.sec2 = x;
                        break;
                }
                n++;
            }
        }
    }
}