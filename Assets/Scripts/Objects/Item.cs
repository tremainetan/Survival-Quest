using UnityEngine;

public class Item : MonoBehaviour
{

    public float radius = 3f;

    private Transform playerTransform;
    public Transform interactableTransform;
    public bool touched = false;
    public bool touching = false;

    ///To Be Overwritten by classes that inherit Interactable
    public virtual void Touch() 
    {

    }
    public virtual void InheritedInteract()
    {

    }

    public virtual void InheritedUpdate()
    {

    }

    public void Interact()
    {
        if (touching) InheritedInteract();
    }

    private void Start()
    {
        playerTransform = PlayerMovement.instance.transform;
        if (interactableTransform == null) interactableTransform = transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, interactableTransform.position);
        if (distance <= radius)
        {
            touching = true;
            if (!touched)
            {
                Touch();
                touched = true;
            }
        }
        else
        {
            touched = false;
            touching = false;
        }
        InheritedUpdate();
    }

    private void OnDrawGizmosSelected()
    {
        if (interactableTransform == null) interactableTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactableTransform.position, radius);
    }

}
