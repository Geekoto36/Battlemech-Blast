using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static RewardManager;

public class RewardManager : MonoBehaviour
{
    [System.Serializable]
    public class Reward
    {

        public ItemInstance rewardItem;


        public bool isClaimed = false;
        public bool isUnlocked;

        public Reward(ItemInstance item)
        {
            rewardItem = item;
        }
    }

    public InventoryManager inventoryManager;
    public Stack<Reward> rewardQueue = new Stack<Reward>();
    public Button claimRewardButton;



    private void Start()
    {
        claimRewardButton.onClick.AddListener(() =>
        {
            ClaimReward(rewardQueue.Peek());
        });
    }
    private void Update()
    {
        if (claimRewardButton != null)
        {
            claimRewardButton.interactable = rewardQueue.Count > 0;
        }
    }
    public void GetReward()
    {
        rewardQueue.Push(GenerateReward());
        Debug.Log("Your reward is " + rewardQueue.Peek().rewardItem.itemData.itemName);
    }
    public Reward GenerateReward()
    {
        Reward r = new Reward(inventoryManager.GetRandomItem());
        r.isUnlocked = true;
        return r;
    }


    public void ClaimReward(Reward r)
    {
        //Add to your inventory
        if (!r.isClaimed)
        {
            inventoryManager.inventory.AddItem(r.rewardItem);
            r.isClaimed = true;
            Debug.Log("You claimed " + r.rewardItem.itemData.itemName);
            rewardQueue.Pop();
        }
    }


}
