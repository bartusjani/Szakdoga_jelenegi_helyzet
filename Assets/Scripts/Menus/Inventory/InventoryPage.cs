using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    Item itemPrefab;
    [SerializeField]
    RectTransform contentPanel;

    [SerializeField]
    ItemDescription itemDesc;

    public List<Item> items = new List<Item>();

    public ScrollRect parentScrollRect;
    public GameObject pauseMenu;
    public GameObject gameMenu;
    public GameObject button;
    private bool openedFromPause = false;

    public void IntializeInv(int size)
    {
        for (int i = 0; i < size; i++)
        {
            Item item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            item.star.enabled = false;
            item.transform.SetParent(contentPanel);
            items.Add(item);
            item.OnItemClicked += HandleItemSelected;
            item.parentScrollRect = parentScrollRect;
            item.transform.localScale = Vector3.one;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentPanel.GetComponent<RectTransform>());
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
        button.SetActive(true);
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
        button.SetActive(false);
        foreach (var invItem in items)
        {
            invItem.Deselect();
        }
    }

}
