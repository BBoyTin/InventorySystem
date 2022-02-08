using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInWorld : MonoBehaviour
{
    [SerializeField] 
    Item _item;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    int _amaontOfThisItem=1;

    protected virtual void OnValidate()
    {
        UpdateSpriteToItemIcon();

    }

    private void Start()
    {
        UpdateSpriteToItemIcon();
    }

    private void UpdateSpriteToItemIcon()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_item != null)
            _spriteRenderer.sprite = _item.Icon;
    }

    public int GetAmountOfThisItem()
    {
        return _amaontOfThisItem;
    }
    public Item GetItem()
    {
        return _item;
    }
    public void SetItem(Item item)
    {
        _item = item;
    }
    public void SetAmount(int amountOfItemInWorld)
    {
        _amaontOfThisItem = amountOfItemInWorld;
    }

}
