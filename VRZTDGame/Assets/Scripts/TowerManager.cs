using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
   private GameManager gameManager;
   private bool upgradeMaxed = false;
   public int originalCost = 50;
   public List<int> upgradeCosts = new List<int> { 50, 100 }; // The costs to upgrade the tower to levels 2 and 3
   public List<int> sellPrices = new List<int> { 25, 40, 60 }; // The amount of currency received when selling the tower at levels 1, 2, and 3
   public Text upgradeButtonText;
   public Text sellButtonText;

   public TowerBehavior towerBehavior;

   void Start()
   {
      // Find the GameManager object with the appropriate tag
      gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
      
   }

   public void UpgradeTower()
   {
      if (!upgradeMaxed)
      {
         int upgradeCost = upgradeCosts[towerBehavior.towerLevel - 1];

         if (gameManager.currency >= upgradeCost)
         {
            towerBehavior.towerLevel += 1;
            gameManager.currency -= upgradeCost;

            if (towerBehavior.towerLevel == upgradeCosts.Count + 1)
            {
               upgradeMaxed = true;
            }
         }
         else
         {
            Debug.Log("Not enough currency to upgrade the tower.");
         }
      }
      else
      {
         Debug.Log("Tower is already maxed.");
      }
   }


   public void SellTower()
   {
      // Add logic to sell the tower and update the GameManager's currency
      int sellPrice = sellPrices[towerBehavior.towerLevel - 1];
      gameManager.currency += Mathf.RoundToInt((sellPrice + (float)originalCost) * 0.5f);
      // Destroy the tower object
      Destroy(gameObject);
   }
   
   void Update()
   {

      // Update the upgrade and sell button texts as before
      if (!upgradeMaxed)
      {
         upgradeButtonText.text = "Upgrade: " + upgradeCosts[towerBehavior.towerLevel - 1].ToString() + " Gold";
      }
      else
      {
         upgradeButtonText.text = "Upgrade: Maxed";
      }
      int sellPrice = sellPrices[towerBehavior.towerLevel - 1];
      sellButtonText.text = "Sell: " + Mathf.RoundToInt((sellPrice + (float)originalCost) * 0.5f).ToString() + " Gold";
   }
}


