using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    Item itemPrefab;
    [SerializeField]
    RectTransform contentPanel;

    [SerializeField]
    ItemDescription itemDesc;

    public List<Item> items = new List<Item>();

    public void IntializeInv(int size)
    {
        for (int i = 0; i < size; i++)
        {
            Item item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(contentPanel);
            items.Add(item);
            item.OnItemClicked += HandleItemSelected;
        }
    }

    private void Awake()
    {
        Hide();
    }

    private void HandleItemSelected(Item item)
    {
        foreach (var invItem in items)
        {
            invItem.Deselect();
        }
        item.Select();

        item.SetDesc(item.title.text,item.desc.text);
        itemDesc.SetDesc(item.title.text, item.desc.text);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemPrefab.ResetDesc();
        itemDesc.ResetDesc();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        foreach (var invItem in items)
        {
            invItem.Deselect();
        }
    }

}
