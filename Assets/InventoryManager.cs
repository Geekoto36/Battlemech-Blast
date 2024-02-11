using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory;
    public List<ItemInstance> itemInstances= new List<ItemInstance>();

    void Start()
    {
    }

    void Update()
    {

    }

    public ItemInstance GetRandomItem()
    {
        ItemInstance item = itemInstances[Random.Range(0, itemInstances.Count)];
        return item;
    } 
}
