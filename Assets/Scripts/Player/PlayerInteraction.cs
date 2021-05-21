using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    private Camera playerCam;
    public static bool interactingWithWorkstation = false;
    public static PlayerInteraction instance;
    public bool touchingTurret;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerCam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !StateHandler.instance.gamePaused)
        {
            if (!interactingWithWorkstation)
            {
                if (!touchingTurret)
                {
                    Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        Item itemComponent = hit.collider.GetComponent<Item>();

                        if (itemComponent != null)
                        {
                            itemComponent.Interact();
                            AudioManager.instance.StopSound("FOOTSTEP");
                        }
                    }
                }
                else
                {
                    TurretItem.instance.Interact();
                    AudioManager.instance.StopSound("FOOTSTEP");
                }
            }
            else
            {
                AudioManager.instance.PlaySound("INTERACT");
                ChestSlots.instance.currentChestOpen = null;
                PlayerMovement.chestOpen = false;
                InventoryUI.instance.ToggleInventory(false);
                ChestSlots.instance.chestUI.SetActive(false);
                WorkbenchSlots.instance.workbenchUI.SetActive(false);
                interactingWithWorkstation = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

    }



}
