using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endPuzzle : MonoBehaviour
{
    public ExamplePlayerMovement eMP;
    public global Global;

    public void Start()
    {
        eMP = GameObject.FindObjectOfType<ExamplePlayerMovement>();
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            eMP.playerState = "worldState";
            eMP.numKeys++;
            Global.puzzleComplete();
        }
    }
}
