using UnityEngine;
using GameScore;

public class MenuManager : MonoBehaviour
{
    private PlatformManager _platformManager;
    private void Awake()
    {
        if (!Progress.IsSetDefaultWeapons())
        {
            FindObjectOfType<ShopAttachment>(true).SetDefaultSetting();
            FindObjectOfType<AmmunitionShop>(true).ReplenishAmmunition();
        }

        if (!Application.isEditor) PlayerPrefs.SetString("selectedLanguage", GS_Language.Current());
    }


    private void Start()
    {
        _platformManager = FindObjectOfType<PlatformManager>();
        StateGameManager.StateGame = StateGameManager.State.Game;
        OnPause(false);
    }

    public void OnPause(bool value)
    { 
        if (!_platformManager.IsMobile) Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        StateGameManager.StateGame = value ? StateGameManager.State.Pause: StateGameManager.State.Game;
    }
}
