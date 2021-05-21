using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    public TextMeshProUGUI currentItemName;
    public Image loadingCircleFill;
    public GameObject loadingObject;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (loadingCircleFill.fillAmount == 0f) loadingObject.SetActive(false);
        else loadingObject.SetActive(true);
    }

    public void UpdateCurrentItemName()
    {
        currentItemName.SetText(Inventory.instance.currentSelectedSlot.item.GetName());
    }

    public void UpdateLoadingCircle(float value)
    {
        loadingCircleFill.fillAmount = value / 2.5f;
    }

}
