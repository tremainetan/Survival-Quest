using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientSlot : MonoBehaviour
{
    public int amount = 0;
    public TextMeshProUGUI amountText;

    private void Update()
    {
        string amountString = amount.ToString();
        amountText.SetText(amountString);
    }

    public void Initialize(int amount)
    {
        this.amount = amount;
        if (amount != 0) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }

}
