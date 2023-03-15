using InfimaGames.LowPolyShooterPack;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeapon : MonoBehaviour
{
    [SerializeField] private Price price;
    [SerializeField] private WeaponBehaviour.Name nameWeapon;
    [SerializeField] private Image buyWeaponImage;
    [SerializeField] private Image boughtWeaponImage;

    private void OnEnable()
    {
        InitShop();
        if (price.CurrencyPrice == Price.Currency.YAN) GSConnect.OnPurchaseWeapon += InitShop;
    }

    private void OnDisable()
    {
        if (price.CurrencyPrice == Price.Currency.YAN) GSConnect.OnPurchaseWeapon -= InitShop;
    }

    public void BuyWeapon()
    {
        if (price.CurrencyPrice == Price.Currency.YAN)
        {
            GSConnect.Purchase(nameWeapon.ToString());
        }

        else if (price.CurrencyPrice == Price.Currency.Money)
        {
            if (FindObjectOfType<Money>().SpendMoney(price.Count))
            {
                Progress.SaveBuyWeapon(nameWeapon);
                InitShop();
            }
        }
    }

    public void StartAttachmentShop()
    {
        var shopAttachment = FindObjectOfType<ShopAttachment>(true);
        shopAttachment.gameObject.SetActive(true);
        shopAttachment.StartInitWeapons(nameWeapon);
    }

    private void InitShop()
    {
        if (Progress.IsBoughtWeapon(nameWeapon))
        {
            boughtWeaponImage.gameObject.SetActive(true);
            buyWeaponImage.gameObject.SetActive(false);
        } 
        else
        {
            boughtWeaponImage.gameObject.SetActive(false);
            buyWeaponImage.gameObject.SetActive(true);
        }
    }

    [Serializable]
    private class Price
    {
        public enum Currency
        {
            YAN,
            Money
        }

        public Currency CurrencyPrice;
        public int Count; 
    }
}
