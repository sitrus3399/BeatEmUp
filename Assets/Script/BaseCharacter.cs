using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected Animator characterAnimator; 
    [SerializeField] protected bool isGround;
    [SerializeField] protected bool isAnim;
    [SerializeField] protected bool isAttack;
    public bool isDead;
    [SerializeField] protected float health;

    protected Rigidbody rigidB;
    
    void Start()
    {
        characterAnimator = this.gameObject.GetComponent<Animator>();
        isAnim = false;
        isDead = false;
        rigidB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    public abstract void Move();

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }
}
