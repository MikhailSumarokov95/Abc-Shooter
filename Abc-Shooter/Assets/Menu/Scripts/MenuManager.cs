using UnityEngine;
using GameScore;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<ShopAttachment>(true).SetDefaultSetting();

        FindObjectOfType<AmmunitionShop>(true).ReplenishAmmunition();

        if (!Application.isEditor) PlayerPrefs.SetString("selectedLanguage", GS_Language.Current());
    }
}
