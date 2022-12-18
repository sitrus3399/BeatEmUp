using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{
    [SerializeField] private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        characterAnimator = GetComponent<Animator>();
    }

    public override void Move()
    {
        Vector3 zMovement = player.transform.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(zMovement.normalized, Vector3.up); //Rotasi to player
        
        //transform.forward = player.transform.position - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        
        if (zMovement.sqrMagnitude < 3f)
        {
            SetAllAnimationFlagsToFalse();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
            characterAnimator.SetBool("SprintJump", true);
        }
    }

    public void SetAllAnimationFlagsToFalse()
    {
        characterAnimator.SetBool("Walk", false);
        characterAnimator.SetBool("SprintJump", false);
        characterAnimator.SetBool("SprintSlide", false);
    }
}
