using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject endPanel;
    public GameObject resumeBtn;
    public GameObject restartBtn;
    public GameObject continueBtn;
    public GameObject clearTxt;
    public GameObject failTxt;
    private Scene currActiveScene;
    private string activeSceneName;

    // Start is called before the first frame update
    void Start()
    {
        currActiveScene = SceneManager.GetActiveScene();
        activeSceneName = currActiveScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        endPanel.SetActive(true);
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        endPanel.SetActive(false);
    }

    public void continueGame()
    {
        Time.timeScale = 1;
        if (activeSceneName == "Main")
        {
            SceneManager.LoadScene("level2");
        }
        else if (activeSceneName == "level2")
        {
            SceneManager.LoadScene("level3");
        }
    }

    public void restartGame()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene(currActiveScene.name);
        SceneManager.LoadScene("Main");
        
    }

    public void endGame()
    {
        Debug.Log(activeSceneName);
        Time.timeScale = 0;
        endPanel.SetActive(true);
        resumeBtn.SetActive(false);
        clearTxt.SetActive(true);
        if (activeSceneName != "level3")
        {
            continueBtn.SetActive(true);
        }
            
    }

    public void failGame()
    {
        Time.timeScale = 0;
        endPanel.SetActive(true);
        resumeBtn.SetActive(false);
        failTxt.SetActive(true);
    }
}
