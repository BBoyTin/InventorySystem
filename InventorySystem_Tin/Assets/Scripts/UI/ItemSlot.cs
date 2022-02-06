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
    [SerializeField]
    private Text _amountText;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    

    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnMiddleClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;


    protected bool _isPointerOver;
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

            if (_item == null && Amount != 0)
                Amount = 0;

            if (_item == null)
            {
                _image.color = _disabledColor;
            }
            else
            {
                _image.sprite = _item.Icon;
                _image.color = _normalColor;
            }


            if (_isPointerOver)
            {
                //radim refresh tooltipa da izbjegnem bug
                OnPointerExit(null);
                OnPointerEnter(null);
            }
        }
    }


    private int _amount;
    public int Amount
    {
        get { return _amount; }
        set
        {
            _amount = value;

            if (_amount < 0) 
                _amount = 0;
            if (_amount == 0 && Item!=null)
            {
                Item = null;
            }

            if (_item != null && _item.MaximumStacks > 1 && _amount > 1)
            {
                _amountText.text = _amount.ToString();
            }
            else
            {
                _amountText.text = "";
            }
        }
    }
  

    protected virtual void OnValidate()
    {
        if (_image == null)
            _image = GetComponent<Image>();

        if (_amountText == null)
            _amountText = GetComponentInChildren<Text>();

        Item = _item;
        Amount = _amount;

    }

    protected virtual void OnDisable()
    {
        if(_isPointerOver)
            OnPointerExit(null);
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
        if (eventData != null && eventData.button == PointerEventData.InputButton.Middle)
        {
            OnMiddleClickEvent?.Invoke(this);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerOver = true;
        OnPointerEnterEvent?.Invoke(this);
        //_tooltip.ShowTooltip(Item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerOver = false;

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



    public bool CanAddStack(Item item, int amount )
    {
        if(amount>1)
            return Item != null && Item.ID == item.ID && amount<=item.MaximumStacks;
        else
            return Item != null && Item.ID == item.ID && 1 <= item.MaximumStacks;

    }
}
