using UnityEngine;

public class Vendor : MonoBehaviour
{
    [SerializeField]
    private VendorWindow vendorWindow;

    [SerializeField]
    private VendorItem[] items;

    private bool isPlayerInTrigger = false;

    public bool IsOpen { get; set; }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    private void Interact()
    {
        if (!IsOpen)
        {
            IsOpen= true;
            vendorWindow.CreatePages(items);
            vendorWindow.Open(this);
        }
    }

    public void StopInteract()
    {
        if (IsOpen)
        {
            IsOpen = false;
            vendorWindow.Close();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isPlayerInTrigger = false;
        }
    }
}
