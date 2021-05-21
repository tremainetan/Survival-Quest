using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuHandler : MonoBehaviour
{

    public GameObject[] menus;

    public TextMeshProUGUI deathMessage;
    public TextMeshProUGUI achievementsText;

    private void Awake()
    {
        SerializableData data = SerializeScript.LoadData();
        string achievementsString = "MOST BOSS KILLS IN ONE GAME: " + data.highestBossKills;
        achievementsString += "\n \n";
        achievementsString += "MOST SKELETON KILLS IN ONE GAME: " + data.highestSkeletonKills;
        achievementsText.text = achievementsString;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (GameHandler.instance != null) Destroy(GameHandler.instance.gameObject);
    }

    private void Start()
    {
        deathMessage.text = StateHandler.instance.gameOverMessage;
    }

    private void Update()
    {
        if (StateHandler.instance.currentState != StateHandler.instance.anticipatedState)
        {
            //State Changed
            StateHandler.instance.currentState = StateHandler.instance.anticipatedState;
            ToggleMenu((int) StateHandler.instance.currentState);
        }
    }

    public void ToggleMenu(int index)
    {
        foreach (GameObject menu in menus) menu.SetActive(false);

        menus[index].SetActive(true);
    }

}
