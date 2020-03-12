using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int curHp;
    public int maxHp;
    public float moveSpeed;
    public int xpToGive;

    [Header("Target")]
    public float chaseRange;
    public float attackRange;
    private RpgPlayer player;

    [Header("Attack")]
    public int damage;
    public float attackRate;
    private float lastAttackTime;

    //components
    private Rigidbody2D rig;


    void Awake()
    {
        //get the component
        rig = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<RpgPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // calculate the distance between us and the player
        float playerDist = Vector2.Distance(transform.position, player.transform.position);

        // if we're in attack range, try and attack the player
        if (playerDist <= attackRange)
        {
            // attack the player
            if (Time.time - lastAttackTime >= attackRate)
            {
                Attack();
            }
            rig.velocity = Vector2.zero;
        }
        // if we're in the chase range, chase after the player
        else if (playerDist <= chaseRange)
        {
            Chase();
        }
        else
        {
            rig.velocity = Vector2.zero;
        }
    }

    void Attack()
    {
        lastAttackTime = Time.time;

        player.TakeDamage(damage);
    }

    // move toward the player
    void Chase()
    {
        // calculate direction between us and the player
        Vector2 dir = (player.transform.position - transform.position).normalized;

        rig.velocity = dir * moveSpeed;
    }

    public void TakeDamage (int damageTaken)
    {
        curHp -= damageTaken;

        if (curHp <= 0)
            Die();
    }

    void Die()
    {
        // give the player xp 
        //player.AddXp(xpToGive);
        Destroy(gameObject);
    }
}
