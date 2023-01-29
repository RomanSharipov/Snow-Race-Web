using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class Autorization : MonoBehaviour
{
    public void OnButtonClick()
    {
        PlayerAccount.Authorize();
    }
}
