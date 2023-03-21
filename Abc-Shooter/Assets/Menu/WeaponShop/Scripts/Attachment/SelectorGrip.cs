public class SelectorGrip : SelectorAttachment
{
    public override void InitAttachment()
    {
        _countAttachment = _weaponAttachmentManager.GetGripBehaviourCount();
    }

    public override void SetActiveAttachment(int index)
    {
        _weaponAttachmentManager.SetEquippedGrip(index);
    }

    public override void BuyAttachment()
    {
        if (!_money.SpendMoney(cast)) return;

        Progress.SetBuyGrip(_shop.CurrentWeaponName, _currentAttachment);
        ScrollThrough(0);
    }

    public override void SelectAttachment()
    {
        Progress.SetSelectGrip(_shop.CurrentWeaponName, _currentAttachment);
        ScrollThrough(0);
    }

    public override bool IsSelectedAttachment(int index)
    {
        return Progress.IsSelectedGrip(_shop.CurrentWeaponName, index);
    }

    public override bool IsBoughtAttachment(int index)
    {
        return Progress.IsBoughtGrip(_shop.CurrentWeaponName, index);
    }
}