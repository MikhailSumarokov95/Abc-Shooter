public class SelectorLaser : SelectorAttachment
{
    public override void InitAttachment()
    {
        _countAttachment = _weaponAttachmentManager.GetGripBehaviourCount();
    }

    public override void SetActiveAttachment(int index)
    {
        _weaponAttachmentManager.SetEquippedLaser(index);
    }

    public override void BuyAttachment()
    {
        if (!_money.SpendMoney(cast)) return;

        Progress.SaveBuyLaser(_shop.CurrentWeaponName, _currentAttachment);
        ScrollThrough(0);
    }

    public override void SelectAttachment()
    {
        Progress.SaveSelectLaser(_shop.CurrentWeaponName, _currentAttachment);
        ScrollThrough(0);
    }

    public override bool IsSelectedAttachment(int index)
    {
        return Progress.IsSelectedLaser(_shop.CurrentWeaponName, index);
    }

    public override bool IsBoughtAttachment(int index)
    {
        return Progress.IsBoughtLaser(_shop.CurrentWeaponName, index);
    }
}