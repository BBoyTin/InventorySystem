using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{

	[SerializeField] Text _itemNameText;
	[SerializeField] Text _itemTypeText;
	[SerializeField] Text _itemDescriptionText;

	private void Awake()
	{
		gameObject.SetActive(false);
	}

	public void ShowTooltip(Item item)
	{
		_itemNameText.text = item.ItemName;
		_itemTypeText.text = item.GetItemType();
		_itemDescriptionText.text = item.GetDescription();
		gameObject.SetActive(true);
	}

	public void HideTooltip()
	{
		gameObject.SetActive(false);
	}
}
