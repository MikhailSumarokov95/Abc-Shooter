using System;
using System.Collections.Generic;
using UnityEngine;
using InfimaGames.LowPolyShooterPack;

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

    public static bool IsBoughtWeapon(WeaponBehaviour.Name name)
    {
        var weaponsBought = LoadWeaponsBought();
        return weaponsBought[name].IsBoughtWeapon;
    }

    public static bool IsBoughtScope(WeaponBehaviour.Name name, int scopeIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        for (int i = 0; i < weaponsBought[name].ScopeIndex.Count; i++)
            if (weaponsBought[name].ScopeIndex[i] == scopeIndex) return true;
        return false;
    }
    
    public static bool IsBoughtMuzzle(WeaponBehaviour.Name name, int muzzleIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        for (int i = 0; i < weaponsBought[name].MuzzleIndex.Count; i++)
            if (weaponsBought[name].MuzzleIndex[i] == muzzleIndex) return true;
        return false;
    } 
    
    public static bool IsBoughtLaser(WeaponBehaviour.Name name, int laserIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        for (int i = 0; i < weaponsBought[name].LaserIndex.Count; i++)
            if (weaponsBought[name].LaserIndex[i] == laserIndex) return true;
        return false;
    }  
    
    public static bool IsBoughtGrip(WeaponBehaviour.Name name, int gripIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        for (int i = 0; i < weaponsBought[name].GripIndex.Count; i++)
            if (weaponsBought[name].GripIndex[i] == gripIndex) return true;
        return false;
    }

    public static int AmmunitionCount(WeaponBehaviour.Name name) 
    {
        var weaponsBought = LoadWeaponsBought();
        return weaponsBought[name].AmmunitionSum;
    }

    public static void SaveBuyWeapon(WeaponBehaviour.Name name)
    {
        var weaponsBought = LoadWeaponsBought();
        weaponsBought[name].IsBoughtWeapon = true;
        SaveWeaponsBought(weaponsBought);
    }

    public static void SaveBuyScope(WeaponBehaviour.Name name, int scopeIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        weaponsBought[name].ScopeIndex.Add(scopeIndex);
        SaveWeaponsBought(weaponsBought);
    }
      
    public static void SaveBuyMuzzle(WeaponBehaviour.Name name, int muzzleIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        weaponsBought[name].MuzzleIndex.Add(muzzleIndex);
        SaveWeaponsBought(weaponsBought);
    }
       
    public static void SaveBuyLaser(WeaponBehaviour.Name name, int laserIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        weaponsBought[name].LaserIndex.Add(laserIndex);
        SaveWeaponsBought(weaponsBought);
    }
      
    public static void SaveBuyGrip(WeaponBehaviour.Name name, int gripIndex)
    {
        var weaponsBought = LoadWeaponsBought();
        weaponsBought[name].GripIndex.Add(gripIndex);
        SaveWeaponsBought(weaponsBought);
    }

    public static void SaveAmmunitionCount(WeaponBehaviour.Name name, int ammunitionCount)
    {
        var weaponsBought = LoadWeaponsBought();
        weaponsBought[name].AmmunitionSum = ammunitionCount;
        SaveWeaponsBought(weaponsBought);
    }

    public static bool IsSelectedScope(WeaponBehaviour.Name name, int scopeIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].ScopeIndex == scopeIndex;
    }

    public static bool IsSelectedMuzzle(WeaponBehaviour.Name name, int muzzleIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].MuzzleIndex == muzzleIndex;
    }  
    
    public static bool IsSelectedLaser(WeaponBehaviour.Name name, int laserIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].LaserIndex == laserIndex;
    }
     
    public static bool IsSelectedGrip(WeaponBehaviour.Name name, int gripIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].GripIndex == gripIndex;
    }

    public static int SelectedScope(WeaponBehaviour.Name name)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].ScopeIndex;
    }
    
    public static int SelectedMuzzle(WeaponBehaviour.Name name)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].MuzzleIndex;
    }  
    
    public static int SelectedLaser(WeaponBehaviour.Name name)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].LaserIndex;
    }

    public static int SelectedGrip(WeaponBehaviour.Name name)
    {
        var weaponsSelected = LoadWeaponsSelected();
        return weaponsSelected[name].GripIndex;
    }

    public static void SaveSelectScope(WeaponBehaviour.Name name, int scopeIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        weaponsSelected[name].ScopeIndex = scopeIndex;
        SaveWeaponsSelected(weaponsSelected);
    }

    public static void SaveSelectMuzzle(WeaponBehaviour.Name name, int muzzleIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        weaponsSelected[name].MuzzleIndex = muzzleIndex;
        SaveWeaponsSelected(weaponsSelected);
    }
      
    public static void SaveSelectLaser(WeaponBehaviour.Name name, int laserIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        weaponsSelected[name].LaserIndex = laserIndex;
        SaveWeaponsSelected(weaponsSelected);
    }
     
    public static void SaveSelectGrip(WeaponBehaviour.Name name, int gripIndex)
    {
        var weaponsSelected = LoadWeaponsSelected();
        weaponsSelected[name].GripIndex = gripIndex;
        SaveWeaponsSelected(weaponsSelected);
    }

    public static void SaveMoney(int value)
    {
        GSPrefs.SetInt(money, value);
        GSPrefs.Save();
    }

    public static int LoadMoney()
    {
        return GSPrefs.GetInt(money, 0);
    }

    public static void SaveLevel(int value)
    {
        GSPrefs.SetInt(level, value);
        GSPrefs.Save();
    }

    public static int LoadLevel()
    {
        return GSPrefs.GetInt(level, 1);
    }

    public static void SaveBattlePass()
    {
        GSPrefs.SetInt(battlePass, 1);
        GSPrefs.Save();
    }

    public static bool LoadBattlePass()
    {
        return GSPrefs.GetInt(battlePass, 0) == 1;
    }

    public static void SaveGrenades(int value)
    {
        GSPrefs.SetInt(grenades, value);
        GSPrefs.Save();
    }

    public static int LoadGrenades()
    {
        return GSPrefs.GetInt(grenades, 0);
    }
    
    public static void SaveSuperGrenades(int value)
    {
        GSPrefs.SetInt(superGrenades, value);
        GSPrefs.Save();
    }

    public static int LoadSuperGrenades()
    {
        return GSPrefs.GetInt(superGrenades, 1);
    }

    public static void SaveSensitivity(float value)
    {
        PlayerPrefs.SetFloat(sensitivity, value);
    }

    public static float LoadSensitivity()
    {
        return PlayerPrefs.GetFloat(sensitivity, 1);
    }

    public static void SaveVolume(float value)
    {
        PlayerPrefs.SetFloat(soundVolume, value);
    }

    public static float LoadVolume()
    {
        return PlayerPrefs.GetFloat(soundVolume, 1);
    }

    public static void SaveMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(musicVolume, value);
    }

    public static float LoadMusicVolume()
    {
        return PlayerPrefs.GetFloat(musicVolume, 1);
    }

    private static void SaveWeaponsSelected(TFG.Generic.Dictionary<WeaponBehaviour.Name, WeaponAttachmentSelected> weapons)
    {
        GSPrefs.SetString(weaponsSelected, JsonUtility.ToJson(weapons).ToString());
        GSPrefs.Save();
    }

    private static TFG.Generic.Dictionary<WeaponBehaviour.Name, WeaponAttachmentSelected> LoadWeaponsSelected()
    {
        return JsonUtility.FromJson<TFG.Generic.Dictionary<WeaponBehaviour.Name, WeaponAttachmentSelected>>
            (GSPrefs.GetString(weaponsSelected, GetDefaultWeaponAttachmentSelected().ToString()));
    }

    private static void SaveWeaponsBought(TFG.Generic.Dictionary<WeaponBehaviour.Name, WeaponAttachmentsBought> weapons)
    {
        GSPrefs.SetString(weaponsBought, JsonUtility.ToJson(weapons).ToString());
        GSPrefs.Save();
    }

    private static TFG.Generic.Dictionary<WeaponBehaviour.Name, WeaponAttachmentsBought> LoadWeaponsBought()
    {
        return JsonUtility.FromJson<TFG.Generic.Dictionary<WeaponBehaviour.Name, WeaponAttachmentsBought>>
            (GSPrefs.GetString(weaponsBought, GetDefaultWeaponAttachmentsBought().ToString()));
    }

    private static string GetDefaultWeaponAttachmentSelected()
    {
        var dict = new TFG.Generic.Dictionary<WeaponBehaviour.Name, WeaponAttachmentSelected>();

        foreach (WeaponBehaviour.Name name in Enum.GetValues(typeof(WeaponBehaviour.Name)))
        {
            dict.Add(name, new WeaponAttachmentSelected());
        }

        return (JsonUtility.ToJson(dict)).ToString();
    } 
    
    private static string GetDefaultWeaponAttachmentsBought()
    {
        var dict = new TFG.Generic.Dictionary<WeaponBehaviour.Name, WeaponAttachmentsBought>();

        foreach (WeaponBehaviour.Name name in Enum.GetValues(typeof(WeaponBehaviour.Name)))
        {
            dict.Add(name, new WeaponAttachmentsBought()
            {
                ScopeIndex = new List<int>(),
                MuzzleIndex = new List<int>(),
                LaserIndex = new List<int>(),
                GripIndex = new List<int>(),
            });
        }

        return (JsonUtility.ToJson(dict)).ToString();
    }

    [Serializable]
    private class WeaponAttachmentSelected
    {
        public int ScopeIndex;
        public int MuzzleIndex;
        public int LaserIndex;
        public int GripIndex;
    }

    [Serializable]
    private class WeaponAttachmentsBought
    {
        public bool IsBoughtWeapon;
        public List<int> ScopeIndex;
        public List<int> MuzzleIndex;
        public List<int> LaserIndex;
        public List<int> GripIndex;
        public int AmmunitionSum;
    }
}