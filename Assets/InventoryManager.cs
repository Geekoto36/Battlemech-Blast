using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory;
    public GameObject itemUnitPrefab;
    public List<ItemInstance> itemInstances = new List<ItemInstance>();

    void Start()
    {
    }

    void Update()
    {

    }

    public void LoadItems()
    {
        if (inventory == null || inventory.itemInstances.Count == 0)
            return;


        //Loop through inventory items
        for (int i = 0; i < inventory.itemInstances.Count; i++)
        {
            ItemUnit unit = Instantiate(itemUnitPrefab.GetComponent<ItemUnit>());
            unit.InitializeItemData(inventory.itemInstances[i].itemData);
        }


    }
    public ItemInstance GetRandomItem()
    {
        ItemInstance item = itemInstances[Random.Range(0, itemInstances.Count)];
        return item;
    }
}
