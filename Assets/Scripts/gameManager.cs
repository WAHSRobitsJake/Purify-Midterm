using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public Text scoreText;
    public Text healthText;
    public GameObject startBackground;
    public GameObject endBackground;

    public bool isMade = false;

    public ExamplePlayerMovement ePM;
    public global Global;
    void Start()
    {
        ePM = GameObject.FindObjectOfType<ExamplePlayerMovement>();
        Instantiate(startBackground, new Vector3(4975.6f, 25.53151f, 0.0f), Quaternion.identity);
    }


    void Update()
    {
        scoreText.text = "Score:" + Global.score;
        healthText.text = "Health:" + ePM.health;

        if (Global.playerState == "menuState" && Global.isGameOver)
        {
            Debug.Log("help");
            if (!isMade)
            {
                endBackground = GameObject.FindGameObjectWithTag("endMenu");
                Instantiate(endBackground, new Vector3(Global.playerPositionX, Global.playerPositionY, 0.0f), Quaternion.identity);
                isMade = true;
            }
            scoreText.transform.position = new Vector3(Screen.width / 2, Screen.height / 2);
            healthText.text = "Highscore: " + Global.highScore;
            healthText.transform.position = new Vector3(Screen.width / 2, (Screen.height / 2)+10.0f);
        } 
        if (Global.playerState == "menuState" && !Global.isGameOver)
        {
            startBackground = GameObject.FindGameObjectWithTag("startMenu");
            if (FigmentInput.GetButton(FigmentInput.FigmentButton.LeftButton))
            {
                Destroy(startBackground);
                Debug.Log("kill");
                Global.playerState = "worldState";
                ePM.playerState = "worldState";
            }
        }
    }
}
