using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel; 
    public GameObject slotPrefab;
    public int slotCount; 
    public GameObject[] itemsPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            if (i < itemsPrefab.Length)
            {
                GameObject item = Instantiate(itemsPrefab[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currItem = item; 
            }
        }   
    }
}
