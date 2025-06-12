using System;
using TMPro;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    [SerializeField] Sprite itemIcon;
    [SerializeField] string title;
    [SerializeField] string desc;
    [SerializeField] InventoryController invController;

    InventoryPage playerInvPage;
    public GameObject interactText;
    bool isPlayerInTrigger = false;
    bool isItemAdded = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInTrigger)
        {
            if (playerInvPage != null)
            {
                AddItemToInv(playerInvPage, itemIcon, title, desc);
            }
            else
            {
                Debug.Log("nullos");

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            interactText.SetActive(true);
            InventoryPage invPage = collision.GetComponent<InventoryController>().invUI;
            playerInvPage = invPage;
        }
        if (isItemAdded)
        {
            isPlayerInTrigger = false;
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactText.SetActive(false);
            isPlayerInTrigger = false;
        }
        if (isItemAdded)
        {
            Destroy(gameObject);
        }
    }

    private void AddItemToInv(InventoryPage inventoryPage, Sprite icon, string itemTitle, string itemDesc)
    {
        Debug.Log("Attempting to add item...");
        foreach (Item item in inventoryPage.items)
        {
            if (!item.itemIcon.gameObject.activeSelf)
            {
                item.SetItem(icon);
                item.SetDesc(itemTitle, itemDesc);
                isItemAdded = true;
                break;
            }

        }
    }
}
