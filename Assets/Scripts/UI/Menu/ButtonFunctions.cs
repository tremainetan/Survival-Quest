using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void Play()
    {
        StateHandler.instance.gamePaused = false;
        StateHandler.instance.anticipatedState = StateHandler.STATE.GAME;
        StateHandler.instance.currentState = StateHandler.STATE.GAME;
        SceneManager.LoadScene("Overworld");
    }

    public void Options()
    {
        StateHandler.instance.anticipatedState = StateHandler.STATE.OPTIONS;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Tutorial()
    {
        StateHandler.instance.anticipatedState = StateHandler.STATE.TUTORIAL;
    }

    public void Achievements()
    {
        StateHandler.instance.anticipatedState = StateHandler.STATE.ACHIEVEMENTS;
    }

    public void BasicsTutorial()
    {
        StateHandler.instance.anticipatedState = StateHandler.STATE.BASICS;
    }

    public void ToolsTutorial()
    {
        StateHandler.instance.anticipatedState = StateHandler.STATE.TOOLS;
    }

    public void InteractionTutorial()
    {
        StateHandler.instance.anticipatedState = StateHandler.STATE.INTERACTION;
    }

    public void Home()
    {
        StateHandler.instance.anticipatedState = StateHandler.STATE.HOME;
    }

    

}
