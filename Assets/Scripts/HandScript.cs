using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandScript : MonoBehaviour
{
    private static HandScript instance;

    public GraphicRaycaster raycaster;

    public static HandScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HandScript>();
            }

            return instance;
        }
    }

    public IMoveable MyMoveable { get; set; }

    private Image icon;

    [SerializeField]
    private Vector3 offset;

    void Start ()
    {
        icon = GetComponent<Image>();	
	}
	
	void Update ()
    {
        icon.transform.position = Input.mousePosition+offset;

        if (Input.GetMouseButton(0) && !IsCursorOverUIElement("Bag(Clone)") && !IsCursorOverUIElement("ChestUI") && !IsCursorOverUIElement("Bag bar") && !IsCursorOverUIElement("CharacterPanel") && !IsCursorOverUIElement("ActionBar") && MyInstance.MyMoveable != null)
        {
            DeleteItem();
        }
	}

    public void TakeMoveable(IMoveable moveable)
    {
        this.MyMoveable = moveable;
        icon.sprite = moveable.MyIcon;
        icon.color = Color.white;
    }

    public IMoveable Put()
    {
        IMoveable tmp = MyMoveable;
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
        return tmp;
    }

    public void Drop()
    {
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
        InventoryScript.MyInstance.FromSlot = null;
    }

    public bool IsCursorOverUIElement(string elementToCheck)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);

        bool foundTarget = false;

        foreach (var result in results)
        {
            if (result.gameObject.name == "fadeTransition")
                continue;

            if (result.gameObject.name == elementToCheck)
            {
                foundTarget = true;
                break;
            }
        }

        return foundTarget;
    }


    public void DeleteItem()
    {
        if (MyMoveable is Item && InventoryScript.MyInstance.FromSlot != null)
        {
            (MyMoveable as Item).MySlot.Clear();
        }

        Drop();

        InventoryScript.MyInstance.FromSlot = null;
    }
}
