using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public AudioSource explosion;
    public AudioSource hit;

    public int HP;

    public float speedHI;
    public float speedLOW;

    public float rotateHI;
    public float rotateLOW;

    public Rigidbody2D body;

    float animationTimer = 0.2f;

    public SpriteRenderer spriteRenderer;
    public Sprite damage1;
    public Sprite damage2;

    bool dead = false;


    //for large only
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public GameObject small;

    void Start()
    {
        body.velocity = new Vector2(0, -Random.Range(speedLOW, speedHI));
        body.angularVelocity = Random.Range(rotateLOW, rotateHI);
    }

    void Update()
    {
        if (dead)
        {
            animationTimer -= Time.deltaTime;
            if (animationTimer <= 0.2f && animationTimer > 0.1f)
            {
                spriteRenderer.sprite = damage1;
            }
            else
            {
                spriteRenderer.sprite = damage2;
            }
            if (animationTimer <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (gameObject.transform.position.y <= -6f)
        {
            Destroy(gameObject);
        }
    }

    private void Boom()
    {
    explosion.Play();
        if (gameObject.tag == "large")
        {
            Destroy(body);
            dead = true;
            Instantiate(small, point1.position, point1.rotation);
            Instantiate(small, point2.position, point1.rotation);
            Instantiate(small, point3.position, point1.rotation);
        }
        else
        {
            Destroy(body);
            dead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet")
        {
            if (!dead)
            {
                hit.Play();
                HP--;
                Destroy(collision.gameObject);
                if (HP <= 0)
                {
                    Boom();
                }
            }
        }
    }
}
