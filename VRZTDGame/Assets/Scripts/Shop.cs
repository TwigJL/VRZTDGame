using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int[] towerPrices = new int[7];
    public Text[] towerPriceTexts = new Text[7]; // Add this line to store the Text UI elements for tower prices
    public Text currencyText;
    public Inventory inventory;
    public GameManager gameManager;


    private void Start()
    {
        UpdateTowerPricesText(); // Call this method at the start to update tower prices text
    }
    private void Update(){
        UpdateCurrencyText();
    }
    public void PurchaseTower(int towerIndex)
    {
        if (gameManager.currency >= towerPrices[towerIndex])
        {
            gameManager.currency -= towerPrices[towerIndex];
            inventory.AddTower(towerIndex);
            UpdateCurrencyText();
        }
        else
        {
            Debug.Log("Not enough currency!");
        }
    }

    public void UpdateCurrencyText()
    {
        currencyText.text = gameManager.currency.ToString();
    }

    // Add this method to update the tower prices text based on the towerPrices array
    private void UpdateTowerPricesText()
    {
        for (int i = 0; i < towerPrices.Length; i++)
        {
            towerPriceTexts[i].text = "Price: " + towerPrices[i].ToString() + " Gold";
        }
    }
    // Button methods for purchasing towers
    public void PurchasePhotonBlaster()
    {
        PurchaseTower(0);
    }

    public void PurchaseFlameThrower()
    {
        PurchaseTower(1);
    }

    public void PurchaseFreezeRay()
    {
        PurchaseTower(2);
    }

    public void PurchaseGaussCannon()
    {
        PurchaseTower(3);
    }

    public void PurchaseIonCannon()
    {
        PurchaseTower(4);
    }

    public void PurchaseRocketLauncher()
    {
        PurchaseTower(5);
    }

    public void PurchaseSlicer()
    {
        PurchaseTower(6);
    }
}
