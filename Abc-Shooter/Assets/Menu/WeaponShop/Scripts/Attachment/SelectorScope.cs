public class SelectorScope : SelectorAttachment
{
    public override void InitAttachment()
    {
        _countAttachment = _weaponAttachmentManager.GetGripBehaviourCount();
        SetNumberAttachment(Progress.GetSelectedScope(_shop.CurrentWeaponName));
    }

    public override void SetActiveAttachment(int index)
    {
        _weaponAttachmentManager.SetEquippedScope(index);
    }

    public override void BuyAttachment()
    {
        if (!_money.SpendMoney(cast)) return;

        Progress.SetBuyScope(_shop.CurrentWeaponName, _currentAttachment);
        ScrollThrough(0);
    }

    public override void SelectAttachment()
    {
        Progress.SetSelectScope(_shop.CurrentWeaponName, _currentAttachment);
        ScrollThrough(0);
    }

    public override bool IsSelectedAttachment(int index)
    {
        return Progress.IsSelectedScope(_shop.CurrentWeaponName, index);
    }

    public override bool IsBoughtAttachment(int index)
    {
        return Progress.IsBoughtScope(_shop.CurrentWeaponName, index);
    }
}