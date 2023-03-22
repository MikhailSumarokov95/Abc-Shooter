using InfimaGames.LowPolyShooterPack;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeapon : MonoBehaviour
{
    [SerializeField] private WeaponBehaviour.Name nameWeapon;
    [SerializeField] private Image buyWeaponImage;
    [SerializeField] private Image boughtWeaponImage;

    private void OnEnable()
    {
        InitShop();
        GSConnect.OnPurchaseWeapon += InitShop;
    }

    private void OnDisable()
    {
        GSConnect.OnPurchaseWeapon -= InitShop;
    }

    public void BuyWeaponForMoney(int price)
    {
        if (FindObjectOfType<Money>().SpendMoney(price))
        {
            Progress.SetBuyWeapon(nameWeapon);
            InitShop();
        }
    }

    public void BuyWeaponForYAN()
    {
        GSConnect.Purchase(nameWeapon.ToString());
    }

    public void StartAttachmentShop()
    {
        var shopAttachment = FindObjectOfType<ShopAttachment>(true);
        shopAttachment.gameObject.SetActive(true);
        shopAttachment.StartInitWeapons(nameWeapon);
        transform.parent.gameObject.SetActive(false);
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
}