using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour
{

    public static StateHandler instance;

    public string gameOverMessage = "";

    public bool gamePaused = false;

    public enum STATE
    {
        HOME,
        OPTIONS,
        TUTORIAL,
        BASICS,
        TOOLS,
        INTERACTION,
        ACHIEVEMENTS,
        GAMEOVER,
        GAME
    }

    public STATE currentState;
    public STATE anticipatedState;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
