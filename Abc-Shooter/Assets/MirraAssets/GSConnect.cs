using GameScore;
using System.Collections.Generic;
using UnityEngine;
using System;
using InfimaGames.LowPolyShooterPack;

public class GSConnect : MonoBehaviour {

    static GSConnect instance;

    public static Action OnPurchaseWeapon;

    /// <summary>
    /// Состояние инициализации SDK.
    /// </summary>
    public static bool Ready => ready;
    static bool ready = false;

    /// <summary>
    /// Полностью отключает звук и игру.
    /// </summary>
    public static bool Pause { set { AudioListener.pause = value; } }

    // Ключи для rewarded награды:

    public const string
        GrenadesReward = nameof(GrenadesReward),
        ContinueReward = nameof(ContinueReward),
        MoneyReward = nameof(MoneyReward),
        DoubleMoneyReward = nameof(DoubleMoneyReward);

    // Ключи для внутриигровых покупок:

    public const string
        GrenadeLauncher = nameof(WeaponBehaviour.Name.GL01),
        RocketLauncher = nameof(WeaponBehaviour.Name.RL01),
        Battlepass = nameof(Battlepass),
        SuperGrenade = nameof(SuperGrenade),
        PartSpaceShip = nameof(PartSpaceShip);

    /// <summary>
    /// Вызывать сразу после важных событий,
    /// чтобы сохранить изменения на сервер.
    /// </summary>
    public static void Sync() {
        Debug.Log("GamePush: Sync request.");
        PlayerPrefs.Save();
        if (!Application.isEditor)
            GS_Player.Sync();
    }

    // Unity события:

    void OnEnable() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        // Подключить события для SDK.
        GS_SDK.OnReady += OnReady;
        // Межстраничная реклама.
        GS_Ads.OnFullscreenClose += OnMidgameClosed;
        // Rewarded реклама.
        GS_Ads.OnRewardedReward += OnRewardedSuccess;
        GS_Ads.OnRewardedClose += OnRewardedClosed;
        // In-app покупки.
        GS_Payments.OnPaymentsFetchProducts += OnFetchProductsSuccess;
        GS_Payments.OnPaymentsPurchase += OnPurchaseSuccess;
    }

    void OnDisable() {
        // Отключить события от SDK.
        GS_SDK.OnReady -= OnReady;
        // Межстраничная реклама.
        GS_Ads.OnFullscreenClose -= OnMidgameClosed;
        // Rewarded реклама.
        GS_Ads.OnRewardedReward -= OnRewardedSuccess;
        GS_Ads.OnRewardedClose -= OnRewardedClosed;
        // In-app покупки.
        GS_Payments.OnPaymentsFetchProducts -= OnFetchProductsSuccess;
        GS_Payments.OnPaymentsPurchase -= OnPurchaseSuccess;
    }

    // События SDK:

    void OnReady() {
        ready = true;
        if (Application.isEditor) {
            Debug.Log("GamePush: Fetch products.");
        }
        else GS_Payments.FetchProducts();
        Debug.Log("GamePush: SDK ready.");
    }

    // Межстраничная реклама:

    public static void ShowMidgameAd() {
        if (Application.isEditor) {
            Debug.Log("GamePush: Midgame AD.");
            return;
        }
        if (!GS_Ads.IsFullscreenAvailable()) return;
        GS_Ads.ShowFullscreen();
        Pause = true;
    }

    void OnMidgameClosed(bool success) {
        Pause = false;
    }

    // Rewarded реклама:

    /// <summary>
    /// Безопасная проверка доступности
    /// Rewarded рекламы.
    /// </summary>
    public static bool RewardedAvailable {
        get {
            if (Application.isEditor) return true;
            return GS_Ads.IsRewardedAvailable();
        }
    }

    /// <summary>
    /// Безопасная проверка доступности
    /// межстраничной рекламы.
    /// </summary>
    public static bool MidgameAvailable {
        get {
            if (Application.isEditor) return true;
            return GS_Ads.IsFullscreenAvailable();
        }
    }

    public static void ShowRewardedAd(string reward) {
        if (Application.isEditor) {
            Debug.Log($"GamePush: Rewarded AD {reward}.");
            instance.OnRewardedSuccess(reward);
            return;
        }
        if (!GS_Ads.IsRewardedAvailable()) return;
        GS_Ads.ShowRewarded(reward);
        Pause = true;
    }

    /// <summary>
    /// Rewarded успешно просмотрен,
    /// дать игроку его награду.
    /// </summary>
    void OnRewardedSuccess(string reward) {
        switch (reward) {
            case ContinueReward:
                {
                    FindObjectOfType<LevelManager>().Respawn();
                    break;
                }
            case GrenadesReward:
                {
                    FindObjectOfType<GrenadeShop>().RewardFull();
                    break;
                }
            case MoneyReward:
                {
                    FindObjectOfType<Money>().MakeMoney(1000);
                    break;
                }
            case DoubleMoneyReward:
                {
                    FindObjectOfType<RewardGame>().CountRewardPerWave(2);
                    break;
                }
        }
        Sync();
    }

    void OnRewardedClosed(bool success) {
        Pause = false;
    }

    // In-app покупки:

    public static bool ProductsReady => productsReady;
    static bool productsReady = false;

    /// <summary>
    /// Перечень товаров и их цен успешно получен.
    /// </summary>
    void OnFetchProductsSuccess(List<FetchProducts> products) {
        prices.Clear();
        foreach (var product in products) {
            prices.Add(
                product.tag,
                $"{product.price} {product.currencySymbol}"
            );
        }
        productsReady = true;
    }

    static readonly Dictionary<string, string> prices = new();

    /// <summary>
    /// Возвращает цену на товар в виде:
    /// "100 YAN", "50 Голосов", итд.
    /// При неудаче, возвращает "Error".
    /// </summary>
    public static string GetPrice(string purchaseTag) {
        return prices.GetValueOrDefault(purchaseTag, "Error");
    }

    /// <summary>
    /// Начать процесс покупки товара.
    /// </summary>
    public static void Purchase(string purchaseTag) {
        if (Application.isEditor) {
            Debug.Log($"GamePush: Purchase {purchaseTag}.");
            instance.OnPurchaseSuccess(purchaseTag);
            return;
        }
        GS_Payments.Purchase(purchaseTag);
    }

    /// <summary>
    /// Товар приобретен успешно,
    /// дать игроку то за что он платил.
    /// </summary>
    void OnPurchaseSuccess(string purchaseTag)
    {
        switch (purchaseTag)
        {
            case GrenadeLauncher:
                Progress.SetBuyWeapon(WeaponBehaviour.Name.GL01);
                OnPurchaseWeapon?.Invoke();
                break;

            case RocketLauncher:
                Progress.SetBuyWeapon(WeaponBehaviour.Name.RL01);
                OnPurchaseWeapon?.Invoke();
                break;

            case Battlepass:
                FindObjectOfType<BattlePassRewarder>(true).BoughtBattlePass();
                break;

            case SuperGrenade:
                FindObjectOfType<SuperGrenadeShop>().RewardCount(5);
                break;

            case PartSpaceShip:
                FindObjectOfType<BuilderSpaceShip>().RewarShipStage();
                break;
        }

        var purchaseButtons = FindObjectsOfType<PurchaseButton>();
        foreach (var button in purchaseButtons)
            button.RefreshBoughtText();
    }

    // Социальные сети:

    /// <summary>
    /// Сделать пост в социальной сети.
    /// Текст должен быть локализован!
    /// </summary>
    public static void CreatePost(string text) {
        GS_Socials.Post(text);
    }

    public static bool IsBought(string purchaseTag)
    {
        switch (purchaseTag) 
        {
            case GrenadeLauncher:
                return Progress.IsBoughtWeapon(WeaponBehaviour.Name.GL01);
                    
            case RocketLauncher:
                return Progress.IsBoughtWeapon(WeaponBehaviour.Name.RL01);

            case Battlepass:
                return Progress.IsBoughtBattlePass();

                default: return false;
        }
    }
}