using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int health;
    public int maxHealth;
    public int hunger;
    public int maxHunger;

    public int skeletonKills;
    public int bossKills;

    public PlayerBar healthBar;
    public PlayerBar hungerBar;

    private float hungerCounter = 0f;
    private float healthCounter = 0f;

    private void Awake()
    {
        instance = this;
        health = maxHealth;
        hunger = maxHunger;
        healthBar.SetMaxValue(maxHealth);
        hungerBar.SetMaxValue(maxHunger);
    }

    private void Update()
    {

        if (!StateHandler.instance.gamePaused)
        {
            hungerCounter += Time.deltaTime;
            if (hungerCounter >= 20)
            {
                RemoveHunger(1);
                hungerCounter = 0f;
            }

            if (hunger == maxHunger)
            {
                healthCounter += Time.deltaTime;
                if (healthCounter >= 5)
                {
                    healthCounter = 0f;
                    AddHealth(1);
                }
            }
            else healthCounter = 0f;

            if (hunger <= 0 || health <= 0) Die();
        }

    }

    public void AddHunger(int value)
    {
        hunger += value;
        if (hunger >= maxHunger) hunger = maxHunger;
        hungerBar.SetValue(hunger);
    }

    private void RemoveHunger(int value)
    {
        hunger -= value;
        hungerBar.SetValue(hunger);
    }

    private void AddHealth(int value)
    {
        health += value;
        if (health >= maxHealth) health = maxHealth;
        healthBar.SetValue(health);
    }

    public void TakeDamage(int value)
    {
        AudioManager.instance.PlaySound("PLAYER_HIT");
        health -= value;
        healthBar.SetValue(health);
    }

    public void Die()
    {
        //Initialise Death Message
        string deathMessage = "";
        deathMessage += "NUMBER OF SKELETONS KILLED: " + skeletonKills;
        deathMessage += "\n \n";
        deathMessage += "NUMBER OF SKELETON BOSSES KILLED: " + bossKills;
        StateHandler.instance.gameOverMessage = deathMessage;

        //Check for High Score Change
        SerializableData oldData = SerializeScript.LoadData();
        if (skeletonKills >= oldData.highestSkeletonKills) oldData.highestSkeletonKills = skeletonKills;
        if (bossKills >= oldData.highestBossKills) oldData.highestBossKills = bossKills;
        SerializeScript.SaveData(oldData);

        GameHandler.instance.ResetData();

        //Change Scene to Menu Scene
        StateHandler.instance.anticipatedState = StateHandler.STATE.GAMEOVER;
        SceneManager.LoadScene("Menu");
    }

    public void ParseStats(int health, int hunger)
    {
        this.health = health;
        this.hunger = hunger;
        healthBar.SetValue(health);
        hungerBar.SetValue(hunger);
    }

}
