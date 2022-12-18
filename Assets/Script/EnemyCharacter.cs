using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{
    [SerializeField] private GameObject player;
    private BaseCharacter playerCharacter;  
    private Vector3 enemyMovement;
    [SerializeField] private float currentDamage;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerCharacter = player.GetComponent<BaseCharacter>();
        characterAnimator = GetComponent<Animator>();
        
    }

    public override void Move()
    {
        if(!playerCharacter.isDead)
        {
            enemyMovement = player.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(enemyMovement.normalized, Vector3.up); //Rotasi to player
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }        
        //transform.forward = player.transform.position - transform.position;

        if (enemyMovement.sqrMagnitude < 3f && !isAnim && !playerCharacter.isDead)
        {
            SetAllAnimationFlagsToFalse();
            isAnim = true;
            StartCoroutine(PlayAnim("SprintJump"));
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime / 10);
        }
        else if(enemyMovement.sqrMagnitude >= 3f && !isAnim && !playerCharacter.isDead)
        {
            StopCoroutine(PlayAnim("SprintJump"));
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
            SetAllAnimationFlagsToFalse();
            characterAnimator.SetBool("Walk", true);
        }
    }

    IEnumerator PlayAnim(string animationClipName)
    {
        yield return new WaitForSeconds (3f);
        
        if(!playerCharacter.isDead)
        {
        isAttack = true;
        characterAnimator.SetBool(animationClipName, true);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime * 10);
        }
        
        yield return new WaitForSeconds(characterAnimator.GetCurrentAnimatorStateInfo(0).length - Mathf.Floor(characterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime));
        
        characterAnimator.SetBool(animationClipName, false);
        isAnim = false;
        isAttack = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && isAttack)
        {
            playerCharacter.TakeDamage(currentDamage);
        }
    }

    public void SetAllAnimationFlagsToFalse()
    {
        characterAnimator.SetBool("Walk", false);
        characterAnimator.SetBool("SprintJump", false);
        characterAnimator.SetBool("SprintSlide", false);
    }
}
