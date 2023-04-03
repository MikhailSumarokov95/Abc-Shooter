using UnityEngine;
using GameScore;
using InfimaGames.LowPolyShooterPack;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Character player;
    [SerializeField] private GameObject gameUI;
    private PlatformManager _platformManager;
    
    private void Awake()
    {
        if (!Progress.IsSetDefaultWeapons())
        {
            FindObjectOfType<ShopAttachment>(true).SetDefaultSetting();
            FindObjectOfType<AmmunitionShop>(true).ReplenishAmmunition();
        }
        player.gameObject.SetActive(true);
        if (!Application.isEditor) PlayerPrefs.SetString("selectedLanguage", GS_Language.Current());
    }

    private void Start()
    {
        _platformManager = FindObjectOfType<PlatformManager>();
        OnPause(false);
    }

    public void OnPause(bool value)
    { 
        if (!_platformManager.IsMobile) Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
        StateGameManager.StateGame = value ? StateGameManager.State.Pause : StateGameManager.State.Game;
        gameUI.SetActive(!value);
    }
}
