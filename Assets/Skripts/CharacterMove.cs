using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private float _speedMove;
    [SerializeField] private float _jumpPower;

    private float _gravityForce;
    private Vector3 _moveVector;

    private CharacterController ch_controller;
    private Animator ch_animator;

    private void Start()
    {
        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CharacterMover();
        GamingGravity();
    }

    private void CharacterMover()
    {
        if (ch_controller.isGrounded)
        {
            //вектор движения
            _moveVector = Vector3.zero;
            _moveVector.x = Input.GetAxis("Horizontal") * _speedMove;
            _moveVector.z = Input.GetAxis("Vertical") * _speedMove;

            //поворот вслед за движением
            //if (Vector3.Angle(Vector3.forward, _moveVector) > 1f || Vector3.Angle(Vector3.forward, _moveVector) == 0)
           // {
            //    Vector3 direct = Vector3.RotateTowards(transform.forward, _moveVector, _speedMove, 0.0f);
            //    transform.rotation = Quaternion.LookRotation(direct);
          //  }
        }

        //сила прыжка
        _moveVector.y = _gravityForce;

        //движение персонажа
        ch_controller.Move(_moveVector * Time.deltaTime);
    }


    //физика прыжка
    private void GamingGravity()
    {
        if (!ch_controller.isGrounded) _gravityForce -= 20f * Time.deltaTime;
        else _gravityForce = -1f;
        if (Input.GetKeyDown(KeyCode.Space) && ch_controller.isGrounded)
        {
            _gravityForce = _jumpPower;
        }
    }
}
