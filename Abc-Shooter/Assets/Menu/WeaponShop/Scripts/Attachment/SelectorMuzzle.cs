public class SelectorMuzzle : SelectorAttachment
{
    public override void InitAttachment()
    {
        _attachmenAbsenteeNumber = 0;
        _countAttachment = _weaponAttachmentManager.GetGripBehaviourCount();
    }

    public override void SetActiveAttachment(int index)
    {
        _weaponAttachmentManager.SetEquippedMuzzle(index);
    }

    public override void BuyAttachment()
    {
        if (!_money.SpendMoney(cast)) return;

        Progress.SaveBuyMuzzle(_shop.CurrentWeaponName, _currentAttachment);
        ScrollThrough(0);
    }

    public override void SelectAttachment()
    {
        Progress.SaveSelectMuzzle(_shop.CurrentWeaponName, _currentAttachment);
        ScrollThrough(0);
    }

    public override bool IsSelectedAttachment(int index)
    {
        return Progress.IsSelectedMuzzle(_shop.CurrentWeaponName, index);
    }

    public override bool IsBoughtAttachment(int index)
    {
        return Progress.IsBoughtMuzzle(_shop.CurrentWeaponName, index);
    }
}
