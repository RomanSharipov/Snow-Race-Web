using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private Enemy _enemy;

    private void SetPositionCamera()
    {
        _cinemachineVirtualCamera.Priority = 20;
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnEnable()
    {
        _enemy.Won += SetPositionCamera;
    }

    private void OnDisable()
    {
        _enemy.Won -= SetPositionCamera;
    }
}
