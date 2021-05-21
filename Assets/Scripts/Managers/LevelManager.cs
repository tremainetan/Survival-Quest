using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

	public static LevelManager instance;

	private void Awake()
	{

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else instance = this;

    }

	public void MoveToOverworld()
    {
        GameHandler.instance.SaveUnderworldState();
        SceneManager.LoadScene("Overworld");
	}

	public void MoveToUnderworld()
    {
        GameHandler.instance.SaveOverworldState();
		SceneManager.LoadScene("Underworld");
    }


}
