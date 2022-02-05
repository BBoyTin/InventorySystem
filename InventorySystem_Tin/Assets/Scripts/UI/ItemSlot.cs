using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,    IPointerExitHandler
{
    [SerializeField]
    private Image _image;

    public event Action<Item> OnRightClickEvent;

    [SerializeField]
    private Item _item;
    [SerializeField]
    private ItemTooltip _tooltip;
    public Item Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item == null)
            {
                _image.enabled = false;
            }
            else
            {
                _image.sprite = _item.Icon;
                _image.enabled = true;
            }

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            
            if (OnRightClickEvent == null)
                Debug.Log("event problem");
           if(Item !=null && OnRightClickEvent != null)
            {
                OnRightClickEvent(Item);
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (_image == null)
            _image = GetComponent<Image>();

        if (_tooltip == null)
        {
            _tooltip = FindObjectOfType<ItemTooltip>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tooltip.ShowTooltip(Item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltip.HideTooltip();
    }
}
