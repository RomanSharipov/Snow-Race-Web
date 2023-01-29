using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputOnBridge : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0F;
    [SerializeField] private Snowball _snowball;

    private CharacterController _controller;
    
    private Joystick _joystick;
    private Vector3 _previousFramePosition = new Vector3();
    private Vector3 _targetRotation = new Vector3();
    private float _startSpeed;

    public event Action Stopped;
    public event Action Walked;

    private void Awake()
    {
        _startSpeed = _speed;
    }
    
    public void Init(Joystick joystick, CharacterController controller)
    {
        _controller = controller;
        _joystick = joystick;
    }
    public void ChangeSpeed(float speed)
    {
        _speed = speed;
    }

    public void ReturnStartSpeed()
    {
        _speed = _startSpeed;
    }
    
    private void Update()
    {
        Vector3 direction=Vector3.zero;
        
        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.D))
        {
            direction = new Vector3(0, 0, Input.GetAxis("Vertical") * _speed);
        }
        else
        {
            direction = new Vector3(0, 0, _joystick.Vertical * _speed);
        }
        

        if (direction == Vector3.zero)
        {
            Stopped?.Invoke();
            return;
        }
        Walked.Invoke();
        _snowball.Roll();

        if (direction.z > 0)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        if (direction.z < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        _controller.SimpleMove(direction);
    }
}
