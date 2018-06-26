using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lives : MonoBehaviour {

    private void Awake()
    {
        int numOfCanvas = FindObjectsOfType<Canvas>().Length;

        if (numOfCanvas > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject); // Could be done in Start but following Unity Doc example
        }
    }

    private void LoadNextLevel()
    {
        int currentlevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextlevelIndex = currentlevelIndex + 1;

        if (nextlevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextlevelIndex = 0;
        }

        SceneManager.LoadScene(nextlevelIndex);
    }

    private void RestartLevel()
    {
        /*TODO: Make a difficulty selection later on
        Easy: Restart from same level when dead
        Hard: Restart from level 1 when dead
        Maybe put level stuff into a level manager?*/
        //SceneManager.LoadScene(0);
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
