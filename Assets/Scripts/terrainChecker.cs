using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class terrainChecker : MonoBehaviour
{
    public GameObject parent;
    public ExamplePlayerMovement eMP;
    public listOfTerrain lOT;
    public Vector3 position;
    public bool nextLevelSpawn = false;

    float timeToDelete = 0.0f;
    public void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        eMP = GameObject.FindObjectOfType<ExamplePlayerMovement>();
    }
    public void Update()
    {
        if (nextLevelSpawn)
        {
            timeToDelete += 1 * Time.deltaTime;
        }
        if(timeToDelete >= 5.0f)
        {
            deleteLevel();
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        {
            position.x = this.gameObject.transform.position.x;
            position.y = this.gameObject.transform.position.y;
            if (other.gameObject.CompareTag("Player") && eMP.numKeys >= 1)
            {
                Instantiate(lOT.randomSelector(), new Vector3(position.x + 27.5f, position.y + 2.65f, 0.0f), Quaternion.identity);
                nextLevelSpawn = true;
            }
        }
    }
    public void deleteLevel()
    {
        Debug.Log("del");
        Destroy(parent);
    }
}