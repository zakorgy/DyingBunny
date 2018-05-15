using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    Text m_timeSinceStart;
    private float secondsCount;
    private int minuteCount = 0;
    // Use this for initialization
    void Start () {
        m_timeSinceStart = GameObject.FindGameObjectWithTag("Time").GetComponent<Text>();
	}

    // Update is called once per frame
    void Update()
    {
        UpdateTimerUI();
    }
    //call this on update
    public void UpdateTimerUI()
    {
        //set timer UI
        secondsCount += Time.deltaTime;
        m_timeSinceStart.text = minuteCount + ":" + (int)secondsCount;
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
    }
}
