public class SelectorSkin : SelectorAttachment
{ 
    public override void InitAttachment()
    {
        _countAttachment = _weaponAttachmentManager.GetSkinCount();
        _attachmenAbsenteeNumber = 0;
    }

    public override void SetActiveAttachment(int index)
    {
        _weaponAttachmentManager.SetEquippedSkin(index);
    }

    public override void BuyAttachment()
    {
        if (!_money.SpendMoney(cast)) return;

        Progress.SaveBuySkin(_shop.CurrentWeaponName, _currentAttachment);
        ScrollThrough(0);
    }

    public override void SelectAttachment()
    {
        Progress.SaveSelectSkin(_shop.CurrentWeaponName, _currentAttachment);
        ScrollThrough(0);
    }

    public override bool IsSelectedAttachment(int index)
    {
        return Progress.IsSelectedSkin(_shop.CurrentWeaponName, index);
    }

    public override bool IsBoughtAttachment(int index)
    {
        return Progress.IsBoughtSkin(_shop.CurrentWeaponName, index);
    }
}
