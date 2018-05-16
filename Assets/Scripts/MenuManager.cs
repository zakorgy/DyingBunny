using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text m_spikeDeath;
    public Text m_bestTime;

    void Start()
    {
        // Set values for achievement scene
        if (SceneManager.GetActiveScene().buildIndex ==2)
        {
            m_bestTime.text = "Your best time is: " + PlayerPrefs.GetInt("TimeAchievment") + " seconds";
            m_spikeDeath.text = "You died a bloody death " + PlayerPrefs.GetInt("SpikeAchievment") + " times";
        }
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void backToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void gotoAcchievements()
    {
        SceneManager.LoadScene(2);
    }

}
