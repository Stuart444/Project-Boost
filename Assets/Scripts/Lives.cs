using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lives : MonoBehaviour {

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] static int lives = 3;
    Text livesUI;

    private void Start()
    {
        livesUI = GameObject.Find("Lives").GetComponent<Text>();
        livesUI.text = lives.ToString();
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

    public void LivesSystem()
    {
        lives--;
        livesUI.text = lives.ToString();
        print(lives);
        if (lives < 0)
        {
            Invoke("RestartGame", levelLoadDelay);
            livesUI.text = "0"; // Prevents -1 from showing on the Lives UI
            lives = 3; // Sets the Lives back to 3 everytime the game restarts
        }
        else
        {
            Invoke("RestartLevel", levelLoadDelay);
        }
    }

    public void SuccessLevel()
    {
        Invoke("LoadNextLevel", levelLoadDelay);
    }

}
