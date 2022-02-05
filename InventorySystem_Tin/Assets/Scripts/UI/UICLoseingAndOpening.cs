using UnityEngine;

public class UICLoseingAndOpening : MonoBehaviour
{



    [SerializeField]
    GameObject _inventoryGameObject;

    [SerializeField]
    KeyCode[] _toggleInventoryKeys;

    [SerializeField]
    GameObject _inventoryButton;


    [Space]

    [SerializeField]
    GameObject _attributesGameObject;

    [SerializeField]
    KeyCode[] _toggleAttributesKeys;

    [SerializeField]
    GameObject _attributeButton;

    [Space]

    [SerializeField]
    GameObject _equippmentGameObject;

    [SerializeField]
    KeyCode[] _toggleEquipmentKeys;

    [SerializeField]
    GameObject _equipButton;


    private void Start()
    {

        //scena moze krenuti sa i bez otvorenih UI elemenata, ispravno ce prikazati gumbe
        _equipButton.SetActive(!_equippmentGameObject.activeSelf);
        _attributeButton.SetActive(!_attributesGameObject.activeSelf);
        _inventoryButton.SetActive(!_inventoryGameObject.activeSelf);
    }
    private void Update()
    {
        for (int i = 0; i < _toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(_toggleInventoryKeys[i]))
            {
                _inventoryGameObject.SetActive(!_inventoryGameObject.activeSelf);
                _inventoryButton.SetActive(!_inventoryGameObject.activeSelf);
                break;
            }
        }

        for (int i = 0; i < _toggleEquipmentKeys.Length; i++)
        {
            if (Input.GetKeyDown(_toggleEquipmentKeys[i]))
            {
                _equippmentGameObject.SetActive(!_equippmentGameObject.activeSelf);

                _equipButton.SetActive(!_equippmentGameObject.activeSelf);

                break;
            }
        }
        for (int i = 0; i < _toggleAttributesKeys.Length; i++)
        {
            if (Input.GetKeyDown(_toggleAttributesKeys[i]))
            {
                _attributesGameObject.SetActive(!_attributesGameObject.activeSelf);
                _attributeButton.SetActive(!_attributesGameObject.activeSelf);
                break;
            }
        }
    }

    //ovo sve moglo biti lijepse da nisam isao poredu po dokumentu kada sam dodavao buttone ... 
    //note: kasnije doraditi
    public void OpenEquipScreenCloseThisButton()
    {
        _equippmentGameObject.SetActive(true);

        _equipButton.SetActive(false);
    }
    public void CloseEquipScreenOpenEquipButton()
    {
        _equippmentGameObject.SetActive(false);

        _equipButton.SetActive(true);
    }

    public void OpenInventoryScreenCloseThisButton()
    {
        _inventoryGameObject.SetActive(true);

        _inventoryButton.SetActive(false);
    }
    public void CloseInventoryScreenOpenItemButton()
    {
        _inventoryGameObject.SetActive(false);

        _inventoryButton.SetActive(true);
    }

    public void OpenAttributeScreenCloseThisButton()
    {
        _attributesGameObject.SetActive(true);

        _attributeButton.SetActive(false);
    }
    public void CloseAttributeScreenOpenItemButton()
    {
        _attributesGameObject.SetActive(false);

        _attributeButton.SetActive(true);
    }

}
