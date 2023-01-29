using Agava.YandexGames;
using Agava.YandexMetrica;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  private int _index;

  public void LoadNewScene()
  {
#if !UNITY_EDITOR
        _index = PlayerPrefs.GetInt("MetricaIndex",1);
        YandexMetrica.Send($"End {_index} lvl");
        _index++;
        PlayerPrefs.SetInt("MetricaIndex", _index);
#endif
    if (SceneManager.sceneCountInBuildSettings - 1 == SceneManager.GetActiveScene().buildIndex)
    {
      SceneManager.LoadScene(1);
      return;
    }

    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  public void Restart()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}