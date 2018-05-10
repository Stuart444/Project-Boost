using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private static MusicPlayer instance = null;

    private void Awake()
    {
        // If Music PLayer doesn't exist
        // instance is set to Music Player
        // Music Player isn't destroyed on load
        // If Music Player Exists, it destroys itself
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
