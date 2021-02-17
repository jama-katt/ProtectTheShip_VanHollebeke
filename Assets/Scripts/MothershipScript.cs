using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MothershipScript : MonoBehaviour
{
    public AudioSource damage;
    public AudioSource lose;
    public AudioSource win;

    bool gameOver = false;
    public KeyCode exit;
    public KeyCode restart;

    public Text timertext;
    public float timer = 60;
    
    public Text winlosetext;

    public SpriteRenderer sprite;

    float redTimer = 0f;

    public Text HPtext;
    public Image HPbar;
    Vector3 HPbarPos;

    public float HP = 100;

    //asteroid spawning
    float largeTimer = 8f;
    float smallTimer = 3f;
    public GameObject large;
    public GameObject small;
    public Transform asteroidSpawn;

    void Start()
    {
        HPbarPos = HPbar.transform.localPosition;
    }
    void Update()
    {
        if (Input.GetKeyDown(exit))
        {
            SceneManager.LoadScene("Menu");
        }
        else if (Input.GetKeyDown(restart))
        {
            SceneManager.LoadScene("Scene1");
        }

        if (!gameOver)
        {
            //spawn asteroids
            if (largeTimer > 0)
            {
                largeTimer -= Time.deltaTime;
            }
            else
            {
                Instantiate(large, asteroidSpawn.position + new Vector3(Random.Range(-5, 5), 0, 0), asteroidSpawn.rotation);
                largeTimer = Random.Range(6, 10);
            }

            if (smallTimer > 0)
            {
                smallTimer -= Time.deltaTime;
            }
            else
            {
                Instantiate(small, asteroidSpawn.position + new Vector3(Random.Range(-5, 5), 0, 0), asteroidSpawn.rotation);
                smallTimer = Random.Range(2, 4);
            }






            //turn red when hit
            if (redTimer > 0)
            {
                sprite.color = Color.red;
                HPtext.color = Color.red;
                HPbar.color = Color.red;
                redTimer -= Time.deltaTime;
            }
            else
            {
                if (sprite.color == Color.red)
                {
                    sprite.color = Color.white;
                    HPtext.color = Color.white;
                    HPbar.color = Color.white;
                }
                if (redTimer != 0f)
                    redTimer = 0f;
            }

            //hpbar
            if (HP > 0)
            {
                HPbar.transform.localScale = new Vector3(HP / 100, 1, 1);
                HPbar.transform.localPosition = new Vector3(-90 + 90 * (HP / 100), 0, 0) + HPbarPos;
            }
            else
            {
                HPbar.transform.localScale = new Vector3(0, 1, 1);
                Lose();
            }

            //timer
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                timertext.text = "PROTECT: " + (int)timer + "s";
            }
            else
            {
                timertext.text = "PROTECT: 0s";
                Win();
            }

        }
    }

    void Win()
    {
        win.Play();
        gameOver = true;
        winlosetext.text = "YOU WIN! Press R to restart\nor ESC to exit to menu";
        winlosetext.color = Color.green;
    }

    void Lose()
    {
        lose.Play();
        gameOver = true;
        winlosetext.text = "you lose :( Press R to restart\nor ESC to exit to menu";
        winlosetext.color = Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameOver)
        {
            if (collision.tag == "small")
            {
                damage.Play();
                redTimer += 0.2f;
                HP -= 10;
            }
            else if (collision.tag == "large")
            {
                damage.Play();
                redTimer += 0.2f;
                HP -= 50;
            }
        }
    }
}
