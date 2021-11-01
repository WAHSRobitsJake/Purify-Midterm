using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class listOfTerrain : ScriptableObject
{
    public List<GameObject> terrain;
    public int terrainGenerated = 2;

    public GameObject randomSelector()
    {
        terrainGenerated++;
        int randTerrain = Random.Range(0, 2);
        return terrain[randTerrain];
    }
}
