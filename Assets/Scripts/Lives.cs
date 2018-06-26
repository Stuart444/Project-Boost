using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
