using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[System.Serializable]
public class Item : MonoBehaviour
{

    [SerializeField]
    public Image itemIcon;

    [SerializeField]
    Image border;

    [SerializeField]
    public TMP_Text title;

    public TextMeshProUGUI a;
    [SerializeField]
    public TMP_Text desc;


    public event Action<Item> OnItemClicked;

    

    public void Awake()
    {
        Reset();
        ResetDesc();
        Deselect();
    }

    public void Deselect()
    {
        border.enabled = false;
    }
    public void Select()
    {
        border.enabled = true;
    }

    public void Reset()
    {
        this.itemIcon.gameObject.SetActive(false);
        
    }

    public void SetItem(Sprite icon)
    {

        this.itemIcon.gameObject.SetActive(true);
        this.itemIcon.sprite=icon;
        

    }

    public void ResetDesc()
    {
        this.title.text = "";
        this.desc.text = "";

    }

    public void SetDesc(string title, string desc)
    {
        this.title.text = title;
        this.desc.text = desc;

    }

    public void OnPointerClick(BaseEventData data)
    {
        PointerEventData pData = (PointerEventData)data;
        if(pData.button== PointerEventData.InputButton.Left)
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
