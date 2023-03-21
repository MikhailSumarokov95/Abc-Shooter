using InfimaGames.LowPolyShooterPack;
using System;
using UnityEngine;

public class AmmunitionShop : MonoBehaviour
{
    public Action OnReplenished;
    [SerializeField] private int price;
    [SerializeField] private MaxAmmunitionWeapon[] _maxAmmunitionWeapon;
    private Money _money;

    private void Start()
    {
        _money = FindObjectOfType<Money>();
    }

    public void BuyAmmunition()
    {
        if (!_money.SpendMoney(price)) return;

        ReplenishAmmunition();
    }

    public void ReplenishAmmunition()
    {
        for (var i = 0; i < _maxAmmunitionWeapon.Length; i++)
            Progress.SetAmmunitionCount(_maxAmmunitionWeapon[i].NameWeapon, _maxAmmunitionWeapon[i].Count);

        OnReplenished?.Invoke();
    }

    [Serializable]
    private class MaxAmmunitionWeapon
    {
        public WeaponBehaviour.Name NameWeapon;
        public int Count;
    }
}