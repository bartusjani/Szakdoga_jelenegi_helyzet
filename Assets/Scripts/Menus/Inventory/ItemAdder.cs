using UnityEngine;

public class ItemAdder : MonoBehaviour
{

    public void AddItemToInv(InventoryPage inventoryPage, Sprite icon, string itemTitle, string itemDesc)
    {
        Debug.Log("Attempting to add item...");
        foreach (Item item in inventoryPage.items)
        {
            if (!item.itemIcon.gameObject.activeSelf)
            {
                item.star.enabled = true;
                item.SetItem(icon);
                item.SetDesc(itemTitle, itemDesc);
                break;
            }

        }
    }
}
