using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Skin : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _id;
    [SerializeField] private bool _isBuyed=false;
    [SerializeField] private bool _isSkinForAdversting;
    [SerializeField] private SkinRarity _skinRarity;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _skelet;
    [SerializeField] private List<GameObject> _ski = new List<GameObject>();

    private ScalerPerson _scalerPerson;
    private AnimatorPerson _animatorPerson;
    
    public SkinRarity SkinRarity => _skinRarity;
    public int Price => _price;
    public string Name => _name;
    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuyed;
    public bool IsSkinForAdvesting=>_isSkinForAdversting;
    public int Id => _id;
    public AnimatorPerson AnimatorPerson => _animatorPerson;
    public ScalerPerson ScalerPerson => _scalerPerson;
    public Animator Animator => _animator;
    public GameObject Skelet => _skelet;
    public List<GameObject> Ski => _ski;

    private void Awake()
    {
        _animatorPerson = GetComponent<AnimatorPerson>();
        _scalerPerson = GetComponent<ScalerPerson>();
    }
    
    public void Buy()
    {
//        Debug.Log("buyed");
        _isBuyed = true;
    }
}
