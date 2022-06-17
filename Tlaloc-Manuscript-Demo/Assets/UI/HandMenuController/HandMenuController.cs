using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIElement {
    Inventory = 1,
}

public class HandMenuController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryGameobject;

    public void ChangeWindowStatus(int uielement) {
        switch ((UIElement)uielement) {
            case UIElement.Inventory:
                inventoryGameobject.SetActive(!inventoryGameobject.activeSelf);
                break;
            default:
                break;
        }
    }
}
