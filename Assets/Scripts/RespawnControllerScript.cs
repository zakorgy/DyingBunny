using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnControllerScript : MonoBehaviour {

    public List<GameObject> respawnableObjects = new List<GameObject>();

    public void resetGame()
    {
        foreach (GameObject element in respawnableObjects)
        {
            element.GetComponent<RespawnScript>().Respawn();
        }
    }
}
