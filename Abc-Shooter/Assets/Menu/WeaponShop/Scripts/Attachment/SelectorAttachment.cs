using System;
using UnityEngine;
using UnityEngine.UI;
using InfimaGames.LowPolyShooterPack;
using TMPro;

public abstract class SelectorAttachment : MonoBehaviour
{
    [SerializeField] protected int cast;
    [SerializeField] private Button buyButton;
    [SerializeField] protected TMP_Text buyText;
    [SerializeField] private Button selectButton;
    [SerializeField] private Image selectedButton;
    protected int _currentAttachment;
    protected ShopAttachment _shop;
    protected WeaponAttachmentManager _weaponAttachmentManager;
    protected int _countAttachment;
    protected Money _money;
    protected int _attachmenAbsenteeNumber = -1 ;

    private void Start()
    {
        _money = FindObjectOfType<Money>();
    }

    public void InitSelectorAttachment(WeaponAttachmentManager weaponAttachmentManager, ShopAttachment shop)
    {
        _shop = shop;
        _weaponAttachmentManager = weaponAttachmentManager;

        InitAttachment();
        ScrollThrough(0);
    }

    public void ScrollThrough(int direction)
    {
        var nextAttachment = Math.Sign(direction) + _currentAttachment;
        nextAttachment = MathPlus.SawChart(nextAttachment, _attachmenAbsenteeNumber, _countAttachment - 1);
        _currentAttachment = nextAttachment;

        SetActiveAttachment(nextAttachment);

        buyButton.gameObject.SetActive(false);
        selectButton.gameObject.SetActive(false);
        selectedButton.gameObject.SetActive(false);

        if (IsSelectedAttachment(nextAttachment))
        {
            selectedButton.gameObject.SetActive(true);
        }

        else if (IsBoughtAttachment(nextAttachment))
        {
            selectButton.gameObject.SetActive(true);
        }

        else
        {
            buyButton.gameObject.SetActive(true);
            buyText.text = cast.ToString();
        }
    }

    public abstract void SetActiveAttachment(int index);
    public abstract void InitAttachment();
    public abstract void BuyAttachment();
    public abstract void SelectAttachment();
    public abstract bool IsSelectedAttachment(int index);
    public abstract bool IsBoughtAttachment(int index);
}