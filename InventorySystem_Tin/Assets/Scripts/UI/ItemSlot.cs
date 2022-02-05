using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler,IDropHandler
{
    [SerializeField]
    private Image _image;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    

    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;


    //ne zelim gasiti image nego staviti da bude transparentna
    private Color _normalColor = Color.white;
    private Color _disabledColor = new Color(1, 1, 1, 0);


    [SerializeField]
    private Item _item;

    public Item Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item == null)
            {
                _image.color = _disabledColor;
            }
            else
            {
                _image.sprite = _item.Icon;
                _image.color = _normalColor;
            }

        }
    }



  

    protected virtual void OnValidate()
    {
        if (_image == null)
            _image = GetComponent<Image>();

    }

    public virtual bool CanReciveItem(Item item)
    {
        return true;
    }

  public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClickEvent?.Invoke(this);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

        OnPointerEnterEvent?.Invoke(this);
        //_tooltip.ShowTooltip(Item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitEvent?.Invoke(this);
        //_tooltip.HideTooltip();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDropEvent?.Invoke(this);
    }
}
