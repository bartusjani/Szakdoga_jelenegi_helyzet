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
    public GameObject pauseMenu;
    public GameObject gameMenu;
    private bool openedFromPause = false;

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
        foreach (var invItem in items)
        {
            invItem.Deselect();
        }

        
        //gameMenu.SetActive(false);
        pauseMenu.SetActive(false);
        itemPrefab.ResetDesc();
        itemDesc.ResetDesc();
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

    public void Show(bool fromPause=false)
    {
        openedFromPause = fromPause;

       
        gameMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameObject.SetActive(true);
        itemPrefab.ResetDesc();
        itemDesc.ResetDesc();

        if (!fromPause)
            Time.timeScale = 0f;
    }

    public void Hide()
    {
        if (openedFromPause)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
        }
        gameObject.SetActive(false);
        foreach (var invItem in items)
        {
            invItem.Deselect();
        }
    }

}
