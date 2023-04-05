using System;
using System.Collections.Generic;
using UnityEngine;
using static InfimaGames.LowPolyShooterPack.WeaponBehaviour;

public static class Progress
{
    readonly static string weaponsSelected = nameof(weaponsSelected);
    readonly static string weaponsBought = nameof(weaponsBought);
    readonly static string money = nameof(money);
    readonly static string level = nameof(level);
    readonly static string battlePass = nameof(battlePass);
    readonly static string grenades = nameof(grenades);
    readonly static string superGrenades = nameof(superGrenades);
    readonly static string sensitivity = nameof(sensitivity);
    readonly static string soundVolume = nameof(soundVolume);
    readonly static string musicVolume = nameof(musicVolume);
    readonly static string setDefaultWeapons = nameof(setDefaultWeapons);
    readonly static string shipAssemblyStage = nameof(shipAssemblyStage);
    readonly static string numberPartsFoundShip = nameof(numberPartsFoundShip);
    readonly static string kit = nameof(kit);
    readonly static string guide = nameof(guide);

    public static Action OnNewSaveWeapons;
    public static Action OnNewSaveGrenade;
    public static Action OnNewSaveSuperGrenade;
    public static Action OnNewSaveSensitivity;

    public static bool IsBoughtWeapon(Name name)
    {
        var weaponsBought = LoadWeaponsBought();
        return weaponsBought[name].IsBoughtWeapon;
    }

    public static bool IsBoughtScope(Name name, int scopeIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        for (int i = 0; i < weaponsBought[name].ScopeIndex.Count; i++)
            if (weaponsBought[name].ScopeIndex[i] == scopeIndex) return true;
        return false;
    }
    
    public static bool IsBoughtMuzzle(Name name, int muzzleIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        for (int i = 0; i < weaponsBought[name].MuzzleIndex.Count; i++)
            if (weaponsBought[name].MuzzleIndex[i] == muzzleIndex) return true;
        return false;
    } 
    
    public static bool IsBoughtLaser(Name name, int laserIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        for (int i = 0; i < weaponsBought[name].LaserIndex.Count; i++)
            if (weaponsBought[name].LaserIndex[i] == laserIndex) return true;
        return false;
    }  
    
    public static bool IsBoughtGrip(Name name, int gripIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        for (int i = 0; i < weaponsBought[name].GripIndex.Count; i++)
            if (weaponsBought[name].GripIndex[i] == gripIndex) return true;
        return false;
    }

    public static bool IsBoughtSkin(Name name, int skinIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        for (int i = 0; i < weaponsBought[name].SkinIndex.Count; i++)
            if (weaponsBought[name].SkinIndex[i] == skinIndex) return true;
        return false;
    }

    public static int[] GetBoughtScope(Name name)
    {
        var weaponsBought = LoadWeaponsBought();
        return weaponsBought[name].ScopeIndex.ToArray();
    } 
    
    public static int[] GetBoughtMuzzle(Name name)
    {
        var weaponsBought = LoadWeaponsBought();
        return weaponsBought[name].MuzzleIndex.ToArray();
    }   

    public static int[] GetBoughtLaser(Name name)
    {
        var weaponsBought = LoadWeaponsBought();
        return weaponsBought[name].LaserIndex.ToArray();
    }    
    
    public static int[] GetBoughtGrip(Name name)
    {
        var weaponsBought = LoadWeaponsBought();
        return weaponsBought[name].GripIndex.ToArray();
    }    
    
    public static int[] GetBoughtSkin(Name name)
    {
        var weaponsBought = LoadWeaponsBought();
        return weaponsBought[name].SkinIndex.ToArray();
    }

    public static int GetAmmunitionCount(Name name) 
    {
        var weaponsBought = LoadWeaponsBought();
        return weaponsBought[name].AmmunitionSum;
    }

    public static void SetBuyWeapon(Name name)
    {
        var weaponsBought = LoadWeaponsBought();
        weaponsBought[name].IsBoughtWeapon = true;
        SaveWeaponsBought(weaponsBought);
    }

    public static void SetBuyScope(Name name, int scopeIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        if (weaponsBought[name].ScopeIndex.Contains(scopeIndex)) return;
        weaponsBought[name].ScopeIndex.Add(scopeIndex);
        SaveWeaponsBought(weaponsBought);
    }
      
    public static void SetBuyMuzzle(Name name, int muzzleIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        if (weaponsBought[name].MuzzleIndex.Contains(muzzleIndex)) return;
        weaponsBought[name].MuzzleIndex.Add(muzzleIndex);
        SaveWeaponsBought(weaponsBought);
    }
       
    public static void SetBuyLaser(Name name, int laserIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        if (weaponsBought[name].LaserIndex.Contains(laserIndex)) return;
        weaponsBought[name].LaserIndex.Add(laserIndex);
        SaveWeaponsBought(weaponsBought);
    }
      
    public static void SetBuyGrip(Name name, int gripIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        if (weaponsBought[name].GripIndex.Contains(gripIndex)) return;
        weaponsBought[name].GripIndex.Add(gripIndex);
        SaveWeaponsBought(weaponsBought);
    }

    public static void SetBuySkin(Name name, int skinIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        if (weaponsBought[name].SkinIndex.Contains(skinIndex)) return;
        weaponsBought[name].SkinIndex.Add(skinIndex);
        SaveWeaponsBought(weaponsBought);
    }

    public static void SetBuyAttachments(Name name, int scopeIndex, int muzzleIndex, int laserIndex, int gripIndex, int skinIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        if (!weaponsBought[name].ScopeIndex.Contains(scopeIndex)) 
            weaponsBought[name].ScopeIndex.Add(scopeIndex);
        if (!weaponsBought[name].MuzzleIndex.Contains(muzzleIndex)) 
            weaponsBought[name].MuzzleIndex.Add(muzzleIndex);
        if (!weaponsBought[name].LaserIndex.Contains(laserIndex)) 
            weaponsBought[name].LaserIndex.Add(laserIndex);
        if (!weaponsBought[name].GripIndex.Contains(gripIndex)) 
            weaponsBought[name].GripIndex.Add(gripIndex);
        if (!weaponsBought[name].SkinIndex.Contains(skinIndex)) 
            weaponsBought[name].SkinIndex.Add(skinIndex);
        SaveWeaponsBought(weaponsBought);
    }

    public static void SetBuySkinsForAllWeapons(int[] skins)
    {
        var weaponsBought = LoadWeaponsBought();
        foreach (Name weapon in Enum.GetValues(typeof(Name)))
        {
            for (var i = 0; i < skins.Length; i++)
            {
                if (!(weaponsBought[weapon].SkinIndex.Contains(skins[i])))
                {
                    weaponsBought[weapon].SkinIndex.Add(skins[i]);
                }
            }
        }
        SaveWeaponsBought(weaponsBought);
    }

    public static void SetAmmunitionCount(Name name, int ammunitionCount)
    {
        var weaponsBought = LoadWeaponsBought();
        weaponsBought[name].AmmunitionSum = ammunitionCount;
        SaveWeaponsBought(weaponsBought);
    }

    public static void SetAmmunitionCountAllWeapon(Dictionary<Name, int> ammunitionWeapon)
    {
        var weaponsBought = LoadWeaponsBought();
        foreach (var weapon in ammunitionWeapon.Keys)
            weaponsBought[weapon].AmmunitionSum = ammunitionWeapon[weapon];
        SaveWeaponsBought(weaponsBought);
    }

    public static bool IsSelectedScope(Name name, int scopeIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].ScopeIndex == scopeIndex;
    }

    public static bool IsSelectedMuzzle(Name name, int muzzleIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].MuzzleIndex == muzzleIndex;
    }  
    
    public static bool IsSelectedLaser(Name name, int laserIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].LaserIndex == laserIndex;
    }
     
    public static bool IsSelectedGrip(Name name, int gripIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].GripIndex == gripIndex;
    }

    public static bool IsSelectedSkin(Name name, int skinIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].SkinIndex == skinIndex;
    }

    public static int GetSelectedScope(Name name)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].ScopeIndex;
    }
    
    public static int GetSelectedMuzzle(Name name)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].MuzzleIndex;
    }  
    
    public static int GetSelectedLaser(Name name)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].LaserIndex;
    }

    public static int GetSelectedGrip(Name name)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].GripIndex;
    }

    public static int GetSelectedSkin(Name name)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].SkinIndex;
    }

    public static void SetSelectScope(Name name, int scopeIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        weaponsSelected[name].ScopeIndex = scopeIndex;
        SaveWeaponsSelected(weaponsSelected);
    }

    public static void SetSelectMuzzle(Name name, int muzzleIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        weaponsSelected[name].MuzzleIndex = muzzleIndex;
        SaveWeaponsSelected(weaponsSelected);
    }
      
    public static void SetSelectLaser(Name name, int laserIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        weaponsSelected[name].LaserIndex = laserIndex;
        SaveWeaponsSelected(weaponsSelected);
    }
     
    public static void SetSelectGrip(Name name, int gripIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        weaponsSelected[name].GripIndex = gripIndex;
        SaveWeaponsSelected(weaponsSelected);
    }

    public static void SetSelectSkin(Name name, int skinIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        weaponsSelected[name].SkinIndex = skinIndex;
        SaveWeaponsSelected(weaponsSelected);
    }

    public static void SetSelectAttachments(Name name, int scopeIndex, int muzzleIndex, int laserIndex, int gripIndex, int skinIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        weaponsSelected[name].ScopeIndex = scopeIndex;
        weaponsSelected[name].MuzzleIndex = muzzleIndex;
        weaponsSelected[name].LaserIndex = laserIndex;
        weaponsSelected[name].GripIndex = gripIndex;
        weaponsSelected[name].SkinIndex = skinIndex;
        SaveWeaponsSelected(weaponsSelected);
    }

    public static void SaveSettedDefaultWeapons()
    {
        GSPrefs.SetInt(setDefaultWeapons, 1);
        GSPrefs.Save();
    }

    public static bool IsSetDefaultWeapons()
    {
        return GSPrefs.GetInt(setDefaultWeapons, 0) == 1;
    }

    public static void SetMoney(int value)
    {
        GSPrefs.SetInt(money, value);
        GSPrefs.Save();
    }

    public static int GetMoney()
    {
        return GSPrefs.GetInt(money, 0);
    }

    public static void SetLevel(int value)
    {
        GSPrefs.SetInt(level, value);
        GSPrefs.Save();
    }

    public static int GetLevel()
    {
        return GSPrefs.GetInt(level, 1);
    }

    public static void SetBoughtBattlePass()
    {
        GSPrefs.SetInt(battlePass, 1);
        GSPrefs.Save();
    }

    public static bool IsBoughtBattlePass()
    {
        return GSPrefs.GetInt(battlePass, 0) == 1;
    }

    public static void SetBoughtKit(GSConnect.PurchaseTag purchaseTag)
    {
        GSPrefs.SetInt(KitScrambler(purchaseTag), 1);
        GSPrefs.Save();
    }
    
    public static bool IsBoughtKit(GSConnect.PurchaseTag purchaseTag)
    {
        return GSPrefs.GetInt(KitScrambler(purchaseTag), 0) == 1;
    }

    private static string KitScrambler(GSConnect.PurchaseTag purchaseTag)
    {
        return (kit + purchaseTag).ToString();
    }

    public static void SetGrenades(int value)
    {
        GSPrefs.SetInt(grenades, value);
        GSPrefs.Save();
        OnNewSaveGrenade?.Invoke();
    }

    public static int GetGrenades()
    {
        return GSPrefs.GetInt(grenades, 0);
    }
    
    public static void SetSuperGrenades(int value)
    {
        GSPrefs.SetInt(superGrenades, value);
        GSPrefs.Save();
        OnNewSaveSuperGrenade?.Invoke();
    }

    public static int GetSuperGrenades()
    {
        return GSPrefs.GetInt(superGrenades, 1);
    }

    public static void SetSensitivity(float value)
    {
        PlayerPrefs.SetFloat(sensitivity, value);
        OnNewSaveSensitivity?.Invoke();
    }

    public static float GetSensitivity()
    {
        return PlayerPrefs.GetFloat(sensitivity, 1);
    }

    public static void SetVolume(float value)
    {
        PlayerPrefs.SetFloat(soundVolume, value);
    }

    public static float GetVolume()
    {
        return PlayerPrefs.GetFloat(soundVolume, 1);
    }

    public static void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(musicVolume, value);
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(musicVolume, 1);
    }

    public static void SetNumberPartsFoundShip(int value)
    {
        GSPrefs.SetInt(numberPartsFoundShip, value);
        GSPrefs.Save();
    }

    public static int GetNumberPartsFoundShip()
    {
        return GSPrefs.GetInt(numberPartsFoundShip, 0);
    }
    
    public static void SetShipAssemblyStage(int value)
    {
        GSPrefs.SetInt(shipAssemblyStage, value);
        GSPrefs.Save();
    }

    public static int GetShipAssemblyStage()
    {
        return GSPrefs.GetInt(shipAssemblyStage, 0);
    }

    public static void SaveGuideCompleted()
    {
        GSPrefs.SetInt(guide, 1);
        GSPrefs.Save();
    }

    public static bool IsGuideCompleted()
    {
        return GSPrefs.GetInt(guide, 0) == 1;
    }

    private static void SaveWeaponsSelected(TFG.Generic.Dictionary<Name, WeaponAttachmentSelected> weapons)
    {
        GSPrefs.SetString(weaponsSelected, JsonUtility.ToJson(weapons));
        GSPrefs.Save();
        OnNewSaveWeapons?.Invoke();
    }

    private static TFG.Generic.Dictionary<Name, WeaponAttachmentSelected> LoadWeaponsSelected()
    {
        return JsonUtility.FromJson<TFG.Generic.Dictionary<Name, WeaponAttachmentSelected>>
            (GSPrefs.GetString(weaponsSelected, GetDefaultWeaponAttachmentSelected()));
    }

    private static void SaveWeaponsBought(TFG.Generic.Dictionary<Name, WeaponAttachmentsBought> weapons)
    {
        GSPrefs.SetString(weaponsBought, JsonUtility.ToJson(weapons));
        GSPrefs.Save();
        OnNewSaveWeapons?.Invoke();
    }

    private static TFG.Generic.Dictionary<Name, WeaponAttachmentsBought> LoadWeaponsBought()
    {
        return JsonUtility.FromJson<TFG.Generic.Dictionary<Name, WeaponAttachmentsBought>>
            (GSPrefs.GetString(weaponsBought, GetDefaultWeaponAttachmentsBought()));
    }

    private static string GetDefaultWeaponAttachmentSelected()
    {
        var dict = new TFG.Generic.Dictionary<Name, WeaponAttachmentSelected>();
        foreach (Name name in Enum.GetValues(typeof(Name)))
        {
            dict.Add(name, new WeaponAttachmentSelected());
        }
        return JsonUtility.ToJson(dict);
    } 
    
    private static string GetDefaultWeaponAttachmentsBought()
    {
        var dict = new TFG.Generic.Dictionary<Name, WeaponAttachmentsBought>();
        foreach (Name name in Enum.GetValues(typeof(Name)))
        {
            dict.Add(name, new WeaponAttachmentsBought()
            {
                ScopeIndex = new List<int>(),
                MuzzleIndex = new List<int>(),
                LaserIndex = new List<int>(),
                GripIndex = new List<int>(),
                SkinIndex = new List<int>(),
            });
        }
        return JsonUtility.ToJson(dict);
    }

    [Serializable]
    private class WeaponAttachmentSelected
    {
        public int ScopeIndex;
        public int MuzzleIndex;
        public int LaserIndex;
        public int GripIndex;
        public int SkinIndex;
    }

    [Serializable]
    private class WeaponAttachmentsBought
    {
        public bool IsBoughtWeapon;
        public List<int> ScopeIndex;
        public List<int> MuzzleIndex;
        public List<int> LaserIndex;
        public List<int> GripIndex;
        public List<int> SkinIndex;
        public int AmmunitionSum;
    }
}