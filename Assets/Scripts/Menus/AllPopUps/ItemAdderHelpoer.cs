using UnityEngine;

public class ItemAdderHelpoer : MonoBehaviour
{

    ItemAdder adder;
    public InventoryPage inv;
    public Sprite itemImg;
    public string itemTitle = "";
    public string itemDesc = "";

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            adder = GetComponent<ItemAdder>();
            adder.AddItemToInv(inv, itemImg, itemTitle, itemDesc);
        }
    }
}
