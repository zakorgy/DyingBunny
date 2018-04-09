using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RespawnScript : MonoBehaviour
{

    public Vector3 respawnPoint;

    // Use this for initialization
    void Start()
    {
        respawnPoint = transform.position;

        GameObject.FindGameObjectWithTag("GameManager").
            GetComponent<RespawnControllerScript>().
            respawnableObjects.Add(this.gameObject);
    }

    public void Respawn()
    {
        this.gameObject.transform.position = respawnPoint;
        this.gameObject.SetActive(true);
        if (this.gameObject.GetComponent<TrickyBrickScript>())
        {
            this.gameObject.GetComponent<TrickyBrickScript>().m_moveDown = false;
        }
    }

}

