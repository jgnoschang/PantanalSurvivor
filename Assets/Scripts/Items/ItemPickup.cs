using UnityEngine;



public enum TypeItem
{
	none = 0,
	food = 1,
	toxic = 2,
	water = 3,
}

public class ItemPickup : Interactable {

	public Item item;   // Item to put in the inventory on pickup
	public TypeItem typeItem;
	Hudmanager hudmanager;

	void Start()
	{
		hudmanager = GameObject.FindObjectOfType<Hudmanager>();
	}


	// When the player interacts with the item
	public override void Interact()
	{
		base.Interact();
		
		PickUp();	// Pick it up!
	}

	// Pick up the item
	void PickUp ()
	{
		
		Debug.Log("Picking up " + item.name);
		bool wasPickedUp = Inventory.instance.Add(item);    // Add to inventory

		// If successfully picked up
		if (wasPickedUp)
			Destroy(gameObject);	// Destroy item from scene
	}
    private void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Player") {

			OnFocused(other.transform);


			//----------------------------------------------------------------------------------------------
			if (typeItem.GetHashCode() == 3)
				hudmanager.progressBar[2].fillAmount += 0.33f;
			else if (typeItem.GetHashCode() == 1)
				hudmanager.progressBar[1].fillAmount += 0.33f;
			else if (typeItem.GetHashCode() == 2)
				hudmanager.progressBar[0].fillAmount -= hudmanager.hitValue;

			Destroy(gameObject);
			//----------------------------------------------------------------------------------------------


		}
	}
    private void OnTriggerExit(Collider other)
    {
		if (other.tag == "Player")
		{

			OnDefocused();
		}

	}

}
