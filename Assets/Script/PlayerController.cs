using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseCharacter
{
    [SerializeField] private float[] attackDamage;
    private float currentDamage;
    public override void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (!isAnim)
        {
            transform.position += new Vector3(movementSpeed * horizontalInput * Time.deltaTime, 0, movementSpeed * verticalInput * Time.deltaTime);
        }
        
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput); //Rotation

        if (moveDirection != Vector3.zero && isGround && !isAnim)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            SetAllAnimationFlagsToFalse();
            characterAnimator.SetBool("param_idletorunning", true);
        }
        else if (moveDirection == Vector3.zero && isGround && !isAnim)
        {
            SetAllAnimationFlagsToFalse();
            characterAnimator.SetBool("param_toidle", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround && !isAnim)
        {
            rigidB.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            SetAllAnimationFlagsToFalse();
            isGround = false;
            StartCoroutine(PlayAnim("param_idletojump"));
            
        }

        if (Input.GetKeyDown(KeyCode.Z) && isGround && !isAnim)
        {
            SetAllAnimationFlagsToFalse();
            isAttack = true;
            currentDamage = attackDamage[0];
            StartCoroutine(PlayAnim("param_idletohit01"));
        }

        if (Input.GetKeyDown(KeyCode.X) && isGround && !isAnim)
        {
            SetAllAnimationFlagsToFalse();
            isAttack = true;
            currentDamage = attackDamage[1];
            StartCoroutine(PlayAnim("param_idletohit02"));
        }

        if (Input.GetKeyDown(KeyCode.C) && isGround && !isAnim)
        {
            SetAllAnimationFlagsToFalse();
            isAttack = true;
            currentDamage = attackDamage[2];
            StartCoroutine(PlayAnim("param_idletohit03"));
        }
    }
    
    IEnumerator PlayAnim(string animationClipName)
    {
        isAnim = true;
        characterAnimator.SetBool(animationClipName, true);
        //Debug.Log(characterAnimator.GetCurrentAnimatorStateInfo(0).length - Mathf.Floor(characterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime));
        yield return new WaitForSeconds(characterAnimator.GetCurrentAnimatorStateInfo(0).length - Mathf.Floor(characterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime));
        characterAnimator.SetBool(animationClipName, false);
        isAnim = false;
        isAttack = false;
        characterAnimator.SetBool("param_toidle", true);
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && isAttack)
        {
            BaseCharacter enemyCharacter;
            enemyCharacter = other.gameObject.GetComponent<BaseCharacter>();
            enemyCharacter.TakeDamage(currentDamage);
        }
    }

    public void SetAllAnimationFlagsToFalse()
    {
        characterAnimator.SetBool("param_idletowalk", false);
        characterAnimator.SetBool("param_idletorunning", false);
        characterAnimator.SetBool("param_idletojump", false);
        characterAnimator.SetBool("param_idletowinpose", false);
        characterAnimator.SetBool("param_idletoko_big", false);
        characterAnimator.SetBool("param_idletodamage", false);
        characterAnimator.SetBool("param_idletohit01", false);
        characterAnimator.SetBool("param_idletohit02", false);
        characterAnimator.SetBool("param_idletohit03", false);
    }
}