using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSprites : MonoBehaviour
{

    public static ItemSprites instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else instance = this;
    }

    public Sprite workbenchSprite;
    public Sprite furnaceSprite;
    public Sprite cookerSprite;
    public Sprite chestSprite;
    public Sprite fishSprite;
    public Sprite meatSprite;
    public Sprite carrotSprite;
    public Sprite fishingrodSprite;
    public Sprite axeSprite;
    public Sprite swordSprite;
    public Sprite pickaxeSprite;
    public Sprite houseSprite;
    public Sprite logSprite;
    public Sprite stoneSprite;
    public Sprite cobblestoneSprite;
    public Sprite boneSprite;
    public Sprite skullSprite;
    public Sprite portalSprite;

}