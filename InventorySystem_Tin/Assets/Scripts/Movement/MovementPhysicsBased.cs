using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPhysicsBased : MonoBehaviour
{
    public Rigidbody2D playerRigidbody2D;
    public Animator playerAnimator;

    
    [SerializeField]
    private float _speed = 5f;


    private float _moveHorizontal;
    private float _moveVertical;
    private Vector2 _movement;


    private void Update()
    {
        InputForPlayer();
        AnimationOfPlayer();
    }



    private void FixedUpdate()
    {
        MovementOfPlayer();

    }
    private void InputForPlayer()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
    }
    private void AnimationOfPlayer()
    {
        playerAnimator.SetFloat("Horizontal", _movement.x);
        playerAnimator.SetFloat("Vertical", _movement.y);
        playerAnimator.SetFloat("Speed", _movement.sqrMagnitude);
    }


    private void MovementOfPlayer()
    {
        playerRigidbody2D.MovePosition(playerRigidbody2D.position + _movement * _speed * Time.fixedDeltaTime);
    }
}