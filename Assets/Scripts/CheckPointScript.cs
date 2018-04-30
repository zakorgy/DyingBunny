using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!(collision.gameObject.GetComponent<RespawnScript>().respawnPoint == this.gameObject.transform.position))
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().PlaySound("Checkpoint");
            }
            collision.gameObject.GetComponent<RespawnScript>().respawnPoint = this.gameObject.transform.position;
        }
    }
}
