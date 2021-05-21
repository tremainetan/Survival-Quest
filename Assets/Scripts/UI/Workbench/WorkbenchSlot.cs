using UnityEngine;
using UnityEngine.UI;

public class WorkbenchSlot : MonoBehaviour
{
    public Image[] icons;

    public GameObject selectedButton;
    public GameObject unselectedButton;

    public InventoryItem.ItemType itemType;
    public InventoryItem item;

    private void Start()
    {
        item = new InventoryItem(itemType, 1);
        foreach (Image icon in icons)
        {
            icon.sprite = item.GetSprite();
        }
    }

    public void OnClick()
    {
        AudioManager.instance.PlaySound("CLICK");
        WorkbenchSlots.instance.UnSelectAllSlots();
        ToggleSelect(true);
        WorkbenchSlots.instance.currentSelectedSlot = this;
        WorkbenchSlots.instance.RefreshIngredients();
    }

    public void ToggleSelect(bool select)
    {
        unselectedButton.SetActive(!select);
        selectedButton.SetActive(select);
    }

}
