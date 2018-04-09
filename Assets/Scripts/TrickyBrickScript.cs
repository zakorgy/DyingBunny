using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickyBrickScript : MonoBehaviour {

    public float m_MoveDownSpeed = 0.8f;
    public GameObject m_lowerBound;

    public bool m_moveDown = false;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update () {
        if (m_moveDown)
        {
            MoveDown();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            m_moveDown = true;
        }

    }

    void MoveDown()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - m_MoveDownSpeed);
        if (this.transform.position.y < m_lowerBound.transform.position.y)
        {
            m_moveDown = false;
            this.gameObject.SetActive(false);
        }
    }
}
