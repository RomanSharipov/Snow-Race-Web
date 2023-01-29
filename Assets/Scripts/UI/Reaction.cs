using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Reaction : MonoBehaviour
{
    [SerializeField] private float _showDeley;
    [SerializeField] private float _resizeDeley;
    [SerializeField] private Emojie[] _positiveEmoji;
    [SerializeField] private Emojie[] _negativeEmoji;
    [SerializeField] private Transform _buble;

    private bool _isActive = false;

    private Stickman _stickman;

    private void OnEnable()
    {
        _stickman = GetComponent<Stickman>();
        _stickman.ITakeDamage += OnItakeDamage;
        _stickman.IGetDamage += OnIGetDamage;
    }

    private void OnDisable()
    {
        _stickman.ITakeDamage -= OnItakeDamage;
        _stickman.IGetDamage -= OnIGetDamage;
    }

    public void ShowReaction()
    {
        OnIGetDamage();
    }

    private void OnItakeDamage()
    {
        if (!_isActive)
        {
            _isActive = true;
            int index = Random.Range(0, _negativeEmoji.Length);
            _buble.gameObject.SetActive(true);
            _negativeEmoji[index].gameObject.SetActive(true);
            _negativeEmoji[index].Resizer.Resize(_resizeDeley, 1);
            StartCoroutine(ShowEmoji(_negativeEmoji[index]));
        }
    }

    private void OnIGetDamage()
    {
        if (!_isActive)
        {
            _isActive = true;
            int index = Random.Range(0, _positiveEmoji.Length);
            _buble.gameObject.SetActive(true);
            _positiveEmoji[index].gameObject.SetActive(true);
            _positiveEmoji[index].Resizer.Resize(_resizeDeley, 1);
            StartCoroutine(ShowEmoji(_positiveEmoji[index]));
        }
    }

    private IEnumerator ShowEmoji(Emojie emojie)
    {
        yield return new WaitForSeconds(_showDeley);
        emojie.gameObject.SetActive(false);
        _buble.gameObject.SetActive(false);
        _isActive = false;
    }
}