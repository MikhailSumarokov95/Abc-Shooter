using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private IconMoney[] iconsMoney;

    public int AmountOfMoney
    {
        get
        {
            return Progress.GetMoney();
        }

        set
        { 
            Progress.SetMoney(value);
            Display�hangeMoney(value);
        }
    }

    private void Start()
    {
        AmountOfMoney = Progress.GetMoney();
    }

    public bool SpendMoney(int value)
    {
        if (AmountOfMoney < value) return false;
        else
        {
            AmountOfMoney -= value;
            return true;
        }
    }

    public void MakeMoney(int value)
    {
        AmountOfMoney += value;
    }

    public void TryReward()
    {
        GSConnect.ShowRewardedAd(GSConnect.MoneyReward);
    }

    private void Display�hangeMoney(int value)
    {
        if (iconsMoney == null) return;
        foreach (var icon in iconsMoney)
            icon.SetMoney(value);
    }
}
