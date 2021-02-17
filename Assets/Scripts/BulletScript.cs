using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Rigidbody2D body;
    public float speed;

    void Update()
    {
        body.velocity = new Vector2(0, speed);

        if (gameObject.transform.position.y > 5.1)
            Destroy(gameObject);
    }
}
