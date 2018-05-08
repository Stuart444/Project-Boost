using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevTools : MonoBehaviour {

    [SerializeField] int levelIndex;

    public static bool disableCol = false;

    // Use this for initialization
    void Start ()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
    }
	
	// Update is called once per frame
	void Update ()
    {
        LevelSkip();

        if (Debug.isDebugBuild)
        {
            DisableCol();
        }
	}

    private void LevelSkip()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            levelIndex = levelIndex + 1;

            if (levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(levelIndex);
            }
            else if (levelIndex >= SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void DisableCol()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            disableCol = !disableCol;
        }
    }
}
