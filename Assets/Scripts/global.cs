using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[CreateAssetMenu]
public class global : ScriptableObject
{
    public listOfTerrain lOT;
    public enemyBehavior[] enemy;
    public GameObject ground;
    public GameObject ball;
    public GameObject UIpuzzle;
    public string playerState;

    public GameObject puzzleOnScene;
    public GameObject ballOnScene;
    //metadata
    public int roomsCleared;
    public int soulsPurified;
    public int numEnemy;
    public float playerPositionX;
    public float playerPositionY;
    public float SplayerPositionX;
    public float SplayerPositionY;

    public bool isGameOver;
    public bool isPuzzle;

    //Score
    public int score;
    public int highScore;
    public int startingHealth = 5;

    public void newGame()
    {
        isGameOver = false;
        lOT.terrainGenerated = 1;
        isPuzzle = false;
        playerState = "menuState";
        lOT.terrainGenerated = 0;
    }
    public void freezeWhilePuzzling()
    {
        if (!isPuzzle)
        {
            Instantiate(UIpuzzle, new Vector3(playerPositionX, playerPositionY), Quaternion.identity);
            isPuzzle = true;
            Instantiate(ball, new Vector3(playerPositionX, playerPositionY, 0.0f), Quaternion.identity);
            ground = GameObject.FindGameObjectWithTag("ground");
            ground.GetComponent<TilemapCollider2D>().enabled = false;
            //Freeze Enemies
            enemy = GameObject.FindObjectsOfType<enemyBehavior>();
            numEnemy = enemy.Length;
            Debug.Log(numEnemy);
            for (int e = 0; e < numEnemy; e++)
            {
                enemy[e].GetComponent<enemyBehavior>().freeze();
                /*
                enemy[e].GetComponent<enemyBehavior>().speed = 0.0f;
                enemy[e].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                enemy[e].GetComponent<BoxCollider2D>().enabled = false;
                */
            }
        }
    }
    public void puzzleComplete()
    {
        puzzleOnScene = GameObject.FindGameObjectWithTag("puzzleActual");
        ballOnScene = GameObject.FindGameObjectWithTag("ball");
        ground.GetComponent<TilemapCollider2D>().enabled = true;
        Destroy(puzzleOnScene);
        Destroy(ballOnScene);
        //Unfreeze Enemies
        enemy = GameObject.FindObjectsOfType<enemyBehavior>();
        numEnemy = enemy.Length;
        for (int e = 0; e < numEnemy; e++)
        {
            enemy[e].GetComponent<enemyBehavior>().unFreeze();
            /*
            enemy[e].GetComponent<enemyBehavior>().speed = 0.005f;
            enemy[e].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            enemy[e].GetComponent<BoxCollider2D>().enabled = true;
            */
        }
        score += 100;
        playerState = "worldState";
        isPuzzle = false;
    }
    public void endGame()
    {
        isGameOver = true;
        //set highscore
        if (score >= highScore)
        {
            highScore = score;
        }
        //Instantiate Sprite
        SceneManager.LoadScene(0);
    }
}
