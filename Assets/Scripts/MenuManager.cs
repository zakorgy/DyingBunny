using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    void Start()
    {
        //		PlayerPrefs.DeleteAll ();
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
        //SceneManager.LoadScene(2);
    }

}
