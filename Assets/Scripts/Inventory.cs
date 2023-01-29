using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Inventory 
{
    [SerializeField] private List<int> _buyedGuid = new List<int>();
    [SerializeField] private int _selectedGuid;

    private const string ClothInventoryKey = nameof(ClothInventoryKey);
    
    public int SelectedGuid => _selectedGuid;

    
    
    public void AddSkin(Skin skin)
    {
        _buyedGuid.Add(skin.Id);
  
    }

    public void SelectSkin(Skin skin)
    {
        _selectedGuid = skin.Id;
    }

    public static Inventory Load()
    {
        
        if (PlayerPrefs.HasKey(Constants.InventoryKey) == false) 
            return new Inventory();
        
        var jsonString = PlayerPrefs.GetString(Constants.InventoryKey);
        return JsonUtility.FromJson<Inventory>(jsonString);
    }

    public void Save()
    {
        var jsonString = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(Constants.InventoryKey, jsonString);
    }

    public bool Contains(Skin skin)
    {
        bool result = _buyedGuid.Contains(skin.Id);
        return result;
    }
}