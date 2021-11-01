using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public GameObject player;
    public Transform sight;
    public float range = 10.0f;

    public string enemyState;
    public listOfTerrain lOT;
    public int health;
    public bool isSpotted;
    public float speed = 0.0000000005f;
    public float patrolTime;
    public float chaseTime;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyState = "patrol";
        health = 10 + ((int)lOT.terrainGenerated / 2);
    }

    // Update is called once per frame
    void Update()
    {
        //patrol
        if (enemyState == "patrol")
        {
            patrolTime += 1 * Time.deltaTime;
            if (patrolTime >= 5)
            {
                speed = -speed;
                patrolTime = 0;
            }
            transform.Translate(new Vector3(speed, 0.0f, 0.0f));
            if (speed < 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(sight.position, Vector2.left, range, 1 << 6);
                Debug.DrawRay(sight.position, Vector2.left*range);
                if (hit.transform.gameObject.tag == "Player")
                {
                    enemyState = "chase";
                    isSpotted = true;
                }
            }
            else if (speed > 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(sight.position, Vector2.right, range, 1 << 6);
                Debug.DrawRay(sight.position, Vector2.right*range);
                if (hit.transform.gameObject.tag == "Player")
                {
                    enemyState = "chase";
                    isSpotted = true;
                }
            }
        }
        if (enemyState == "chase")
        {
            //chaseTime += 1 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.005f);
            if (chaseTime >= 5)
            {
                enemyState = "patrol";
                isSpotted = false;
            }
        }
        if (player.transform.position.x - this.gameObject.transform.position.x < 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        } else
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("fireball"))
        {
            health -= 7;
            Destroy(other.gameObject);
        }
    }
    public void freeze()
    {
        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
    }
    public void unFreeze()
    {
        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        //this.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }
}
