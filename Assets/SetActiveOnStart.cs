using UnityEngine;

public class SetActiveOnStart : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private bool _enable;

    private void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            _gameObject.SetActive(_enable);
    }
}
