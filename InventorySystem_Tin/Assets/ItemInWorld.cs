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

    protected virtual void OnValidate()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        if(_item !=null)
            _spriteRenderer.sprite = _item.Icon;

    }

    private void Start()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        if(_item != null)
        _spriteRenderer.sprite = _item.Icon;
    }

    public Item GetItem()
    {
        return _item;
    }

}
