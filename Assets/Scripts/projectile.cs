using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public ExamplePlayerMovement ePM;
    public float timeAlive = 0.0f;
    void Start()
    {
        ePM = GameObject.FindObjectOfType<ExamplePlayerMovement>();
       if (ePM.isFlipped)
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100.0f, 0.0f));
        } else if (!ePM.isFlipped)
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(100.0f, 0.0f));
        }
    }

    void Update()
    {
        timeAlive += 1 * Time.deltaTime;
        if (timeAlive >= 5.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
