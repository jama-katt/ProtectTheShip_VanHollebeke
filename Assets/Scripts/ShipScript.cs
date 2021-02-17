using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipScript : MonoBehaviour
{
    public AudioSource dashSound;
    public AudioSource shootSound;

    public Transform gun1;
    public Transform gun2;

    public GameObject bullet;

    public Image dashBar;
    Vector3 dashBarPos;
    public Image shootBar;
    Vector3 shootBarPos;

    public float speed;
    float currentSpeed;
    bool dashing = false;

    public KeyCode dashKey;
    public KeyCode shootKey;

    float shootCounter = 0.2f;
    bool shootCD = false;

    float dashLength = 0.2f;
    float dashCounter = 1f;
    bool dashCD = false;

    void Start()
    {
        dashBarPos = dashBar.transform.localPosition;
        shootBarPos = shootBar.transform.localPosition;
    }

    void Update()
    {


        if (Input.GetKeyDown(dashKey) && dashCD == false)
        {
            dashSound.Play();
            dashing = true;
            dashCD = true;
        }

        if (Input.GetKey(shootKey) && shootCD == false)
        {
            shootSound.Play();
            Instantiate(bullet, gun1.transform.position, gun1.transform.rotation);
            Instantiate(bullet, gun2.transform.position, gun2.transform.rotation);
            shootCD = true;
        }


        if (dashing)
            currentSpeed = speed * 3f;
        else
            currentSpeed = speed;

        //iterate counters
        if (dashing)
        {
            dashLength -= Time.deltaTime;
            if (dashLength <= 0f)
            {
                dashLength = 0.2f;
                dashing = false;
            }
        }
        if (dashCD)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0f)
            {
                dashCounter = 1f;
                dashCD = false;
            }
        }
        if (shootCD)
        {
            shootCounter -= Time.deltaTime;
            if (shootCounter <= 0f)
            {
                shootCounter = 0.2f;
                shootCD = false;
            }
        }



        //CD bars
        if (dashCD)
        {
            dashBar.transform.localScale = new Vector3(1f - dashCounter, 1f, 1f);
            dashBar.transform.localPosition = new Vector3(-40 + 40 * (1f - dashCounter), 0, 0) + dashBarPos;
        }
        else
        {
            dashBar.transform.localScale = new Vector3(1f, 1f, 1f);
            dashBar.transform.localPosition = dashBarPos;
        }
        if (shootCD)
        {
            shootBar.transform.localScale = new Vector3((0.2f - shootCounter) * 5f, 1f, 1f);
            shootBar.transform.localPosition = new Vector3(-40 + 40 * ((0.2f - shootCounter) * 5f), 0, 0) + shootBarPos;
        }
        else
        {
            shootBar.transform.localScale = new Vector3(1f, 1f, 1f);
            shootBar.transform.localPosition = shootBarPos;
        }
        


        //move
        if (Mathf.Abs(gameObject.transform.position.x + Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime) <= 5.8f)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime, gameObject.transform.position.y, 0);
    }
}
