using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "TemplatesData/TemplatesData",fileName = "TemplatesData")]
public class TemplatesData : ScriptableObject
{
    [SerializeField] private List<Skin> _skinTemplates;

    public List<Skin> SkinTemplates => _skinTemplates;


    public Skin GetSkin(int id)
    {
        return _skinTemplates.FirstOrDefault(skinTemplate => skinTemplate.Id == id);
    }
    
    
}
