using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputOnElevator : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0F;
    [SerializeField] private float _rotationSpeed = 3.0F;
    
    [SerializeField] private Snowball _snowball;

    private CharacterController _controller;
    private Quaternion _rotation;
    private Joystick _joystick;
    private float _startSpeed;
    private float _startRotationSpeed;
    private void Awake()
    {
        _startSpeed = _speed;
        _startRotationSpeed = _rotationSpeed;
    }
    
    public void Init(Joystick joystick, CharacterController controller)
    {
        _controller = controller;
        _joystick = joystick;
    }
   
    public void ChangeSpeed(float speed)
    {
        _speed = speed;
        _rotationSpeed = speed;
    }

    public void ReturnStartSpeed()
    {
        _speed = _startSpeed;
        _rotationSpeed = _startRotationSpeed;
    }

    
    private void Update()
    {
        Vector3 direction=Vector3.zero;
        
        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.D))
        {
            direction = new Vector3(Input.GetAxis("Horizontal")* _speed, 0, Input.GetAxis("Vertical") * _speed);
        }
        else
        {
            direction = new Vector3(_joystick.Horizontal * _speed, 0, _joystick.Vertical * _speed);
        }

        if (direction == Vector3.zero)
            return;

        _snowball.SwitchOffSnowTrail();
        _controller.SimpleMove(direction);
        _rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotation, _rotationSpeed);
    }
}