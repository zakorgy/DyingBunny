using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodManager : MonoBehaviour {

    float m_startTime;
    public float m_lifeTime = 10.0f;

	// Use this for initialization
	void Start () {        
    }

    private void OnEnable()
    {
        m_startTime = Time.time;
        this.GetComponent<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void Update () {
        if ((Time.time - m_startTime) > m_lifeTime)
        {
            this.gameObject.SetActive(false);
            this.GetComponent<ParticleSystem>().Stop();
        }
	}
}
