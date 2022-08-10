using UnityEngine;
using UnityEngine.InputSystem;

/* This object updates the inventory UI. */

public class InventoryUI : MonoBehaviour {

	public Transform itemsParent;	// The parent object of all the items
	public GameObject inventoryUI;	// The entire UI

	Inventory inventory;	// Our current inventory

	InventorySlot[] slots;  // List of all the slots
	
	public bool cantUse = false;

	void Start () {
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;    // Subscribe to the onItemChanged callback

		Input.inventory.performed += OpenInventoryUi;

		// Populate our slots array
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}

	public void InativeActItems()
    {
		foreach(var s in slots)
        {
			s.cantUse = true;
        }
    }
	public void AtiveActItems()
	{
		foreach (var s in slots)
		{
			s.cantUse = false;
		}
	}
	public void OpenInventoryUi(InputAction.CallbackContext context) {
		if (inventoryUI.activeSelf)
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		else Cursor.lockState = CursorLockMode.None;
		inventoryUI.SetActive(!inventoryUI.activeSelf);
	}

	void Update () {
		// Check to see if we should open/close the inventory


	}

	// Update the inventory UI by:
	//		- Adding items
	//		- Clearing empty slots
	// This is called using a delegate on the Inventory.
	public void UpdateUI ()
	{
		// Loop through all the slots
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.items.Count)	// If there is an item to add
			{
				slots[i].AddItem(inventory.items[i]);	// Add it
			} else
			{
				// Otherwise clear the slot
				slots[i].ClearSlot();
			}
		}
	}

	
}
