using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamplePlayerMovement : MonoBehaviour {
    public global Global;

    public listOfTerrain lOT;

    public string playerState = "menuState";
    public Animator anim;

    public GameObject shoot;

    public bool isFighting;
    public bool isPressed;
    public bool isPuzzling;
    public bool isGrounded;
    public bool isFlipped;
    public bool isFired;

    public int numKeys;
    public int health = 5;

    public float MoveSpeed = 8.0f;
    public float cooldownTimer = 5.0f;
    public float pressTimer = 0.0f;
    public float jumpPower = 250.0f;
    public float jumpHigherPower = 450.0f;
    public float fireCooldown = 0.0f;

    public Animator plagueAnims;

    public AudioClip footSteps;
    public AudioClip fireAttack;
    public AudioClip bigJump;
    public AudioClip takeDamage;
    public AudioClip killThing;
    public AudioClip didPuzzle;
    AudioSource audioSource;

    public Transform projRight;
    public Transform projLeft;

    // Use this for initialization
    void Start () {
        Global.SplayerPositionX = this.gameObject.transform.position.x;
        Global.SplayerPositionY = this.gameObject.transform.position.y;
        health = Global.startingHealth;
        Global.newGame();
        playerState = "menuState";
        audioSource = GetComponent<AudioSource>();
        //audioSource.PlayOneShot(footSteps, 0.9f);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            playerState = "menuState";
            Global.isGameOver = true;
        }
        //Player in the world
        if (playerState == "worldState")
        {
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;

            if (FigmentInput.GetButton(FigmentInput.FigmentButton.LeftButton))
            {
                transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                isFlipped = true;
                if (isGrounded)
                {
                    if (plagueAnims.name != "MCattack")
                    {
                        plagueAnims.SetBool("isIdle", false);
                        plagueAnims.Play("MCwalkanim");
                    }
                }
            }
            else if (FigmentInput.GetButton(FigmentInput.FigmentButton.RightButton))
            {
                transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                isFlipped = false;
                if (isGrounded)
                {
                    if (plagueAnims.name != "MCattack")
                    {
                        plagueAnims.SetBool("isIdle", false);
                        plagueAnims.Play("MCwalkanim");
                    }
                }
            }
            else if (isGrounded)
            {
                audioSource.Pause();
                if (plagueAnims.name != "MCattack")
                {
                    plagueAnims.SetBool("isIdle", true);
                }
            }
            
            if (FigmentInput.GetButton(FigmentInput.FigmentButton.ActionButton))
            {
                isPressed = true;    
            }
            if (FigmentInput.GetButtonUp(FigmentInput.FigmentButton.ActionButton))
            {
                if (pressTimer <= 25)
                {
                    if (isPuzzling)
                    {
                        playerState = "puzzleState";
                        isPressed = false;
                    } else
                    {
                        if (isFlipped && !isFired)
                        {
                            //Left
                            Instantiate(shoot, projLeft.transform.position, Quaternion.identity);
                            isFired = true;
                        } else if (!isFlipped && !isFired)
                        {
                            //right
                            Instantiate(shoot, projRight.transform.position, Quaternion.identity);
                            isFired = true;
                        }
                        isPressed = false;
                        pressTimer = 0;
                        audioSource.PlayOneShot(fireAttack, 0.9f);
                        plagueAnims.Play("MCattack");
                    }
                }
                if (pressTimer >= 26 && pressTimer < 50)
                {
                    if (isPuzzling)
                    {
                        playerState = "puzzleState";
                        Global.playerState = "puzzleState";
                        isPressed = false;
                    } else if (isGrounded)
                    {
                        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0.0f, jumpPower, 0.0f));
                        isPressed = false;
                        isGrounded = false;
                        plagueAnims.Play("mcjump");
                        plagueAnims.SetBool("isIdle", false);
                        //audioSource.PlayOneShot(bigJump, 0.7f);
                    }
                }
                if (pressTimer >= 51 && isGrounded)
                {
                    this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0.0f, jumpHigherPower, 0.0f));
                    isPressed = false;
                    isGrounded = false;
                    plagueAnims.Play("mcjump");
                    plagueAnims.SetBool("isIdle", false);
                    //audioSource.PlayOneShot(bigJump, 0.7f);
                }
            }
            if (isPressed)
            {
                pressTimer++;
            }
            if (isFired)
            {
                fireCooldown += 1*Time.deltaTime;
            }
            if (fireCooldown >= 5)
            {
                isFired = false;
            }
        }
        //player in the puzzle
        else if (playerState == "puzzleState")
        {
            Global.playerPositionX = this.gameObject.transform.position.x;
            Global.playerPositionY = this.gameObject.transform.position.y;
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            Global.playerState = "puzzleState";
            Global.freezeWhilePuzzling();
        }
        if (playerState == "menuState")
        {
            Global.playerPositionX = this.gameObject.transform.position.x;
            Global.playerPositionY = this.gameObject.transform.position.y;
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
            Global.playerState = "menuState";
            if (FigmentInput.GetButton(FigmentInput.FigmentButton.LeftButton) && Global.isGameOver == true)
            {
                Global.endGame();
            }
            if (FigmentInput.GetButton(FigmentInput.FigmentButton.RightButton) && Global.isGameOver == true)
            {
                Application.Quit();
            }
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("puzzle"))
        {
            isPuzzling = true;
            if (playerState == "puzzleState")
            {
                Destroy(other.gameObject);
            }
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            isFighting = true;
            Global.score += 50;
            Destroy(other.gameObject);
            audioSource.PlayOneShot(killThing, 0.9f);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("puzzle"))
        {
            isPuzzling = false;
        }
        if (other.gameObject.CompareTag("delPrev"))
        {
            other.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            other.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            pressTimer = 0;
            plagueAnims.SetBool("isIdle", true);
        }
        if (other.gameObject.CompareTag("wall") && numKeys >= 1)
        {
            numKeys--;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (other.gameObject.CompareTag("Enemy") && plagueAnims.name != "MCattack")
        {
            health--;
            Destroy(other.gameObject);
            audioSource.PlayOneShot(takeDamage, 0.9f);
        }
    }

}
