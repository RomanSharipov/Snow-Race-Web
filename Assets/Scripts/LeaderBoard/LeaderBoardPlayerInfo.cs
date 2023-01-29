using UnityEngine;
using TMPro;

public class LeaderBoardPlayerInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _score;

    public void SetInfo(string name, string score)
    {
        _name.text = name;
        _score.text = score;
    }
}
