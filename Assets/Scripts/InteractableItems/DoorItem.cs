using UnityEngine;

public class DoorItem : Item
{

    private bool isDoorOpen = false;
    public override void InheritedInteract()
    {
        if (!isDoorOpen) {
            //OPEN DOOR
            AudioManager.instance.PlaySound("OPEN_DOOR");
            interactableTransform.localPosition = new Vector3(0.708f, 0.9f, -0.5f);
            interactableTransform.localRotation = Quaternion.identity;
            isDoorOpen = true;
        }
        else
        {
            //CLOSE DOOR
            AudioManager.instance.PlaySound("CLOSE_DOOR");
            interactableTransform.localPosition = new Vector3(0f, 0.9f, 0f);
            interactableTransform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            isDoorOpen = false;
        }
    }

}
