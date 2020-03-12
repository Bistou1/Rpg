using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RpgPlayer : MonoBehaviour
{
    [Header("Stats")]
    public Level level;
    public int curHp;
    public int maxHp;
    public float moveSpeed;
    public int damage;
    public float interactRange;

    private Vector2 facingDirection;

    [Header("Combat")]
    public KeyCode attackKey;
    public KeyCode interactKey;
    public KeyCode releaseKey;
    public float attackRange;
    public float attackRate;
    private float lastAttackTime;

    //[Header("Experience")]
    //public int curLevel;
    //public int curXp;
    //public int xpToNextLevel;
    //public float levelXpModifier;

    [Header("Sprites")]
    public Sprite downSprite;
    public Sprite upSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    //components
    private Rigidbody2D rig;
    private SpriteRenderer sr;
    private ParticleSystem hitEffect;
    private PlayerUI ui;


    void Awake()
    {
        //get the component
        rig = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        hitEffect = gameObject.GetComponentInChildren<ParticleSystem>();
        ui = FindObjectOfType<PlayerUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        level = new Level(1, OnLevelUp);

        // initialize the UI elements
        ui.UpddateHealthBar();
        ui.UpddateXpBar();
        ui.UpdateLevelText();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKeyDown(attackKey))
        {
            if(Time.time - lastAttackTime >= attackRate)
                Attack();
        }

        if (Input.GetKeyDown(KeyCode.Q)) // quick shit just to see if it works
        {
            level.AddXp(50);
            ui.UpddateXpBar();
        }

        CheckInteract();
    }

    private void FixedUpdate()
    {
        Release();
    }

    void Release()
    {
        if (Input.GetKeyDown(releaseKey))
        {
            if (GetComponent<Joint2D>().attachedRigidbody != null)
            {
                GetComponent<FixedJoint2D>().enabled = false;
            }
        }
    }

    void CheckInteract()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDirection, interactRange, 1 << 9);

        if (hit.collider != null)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            ui.SetInteractText(hit.collider.transform.position, interactable.interactDescription);

            if (Input.GetKeyDown(interactKey))
                interactable.Interact();
        }
        else
        {
            ui.DisableInteractText();
        }
    }

    void Attack()
    {
        lastAttackTime = Time.time;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDirection, attackRange, 1 << 8);

        if (hit.collider != null)
        {
            hit.collider.GetComponent<Enemy>().TakeDamage(damage);

            // play the hit effect
            hitEffect.transform.position = hit.collider.transform.position;
            hitEffect.Play();
        }
    }

    void Move()
    {
        //get the horizontal and vertical keyboard inputs
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // calculate the velocity we're going to move at
        Vector2 vel = new Vector2(x, y);


        // calculate the facing direction
        if(vel.magnitude != 0)
        {
            facingDirection = vel;
        }

        UpdateSpriteDirection();

        // set the velocity
        rig.velocity = vel * moveSpeed;
    }

    void UpdateSpriteDirection()
    {
        if(facingDirection == Vector2.up)
        {
            sr.sprite = upSprite;
        }
        else if (facingDirection == Vector2.down)
        {
            sr.sprite = downSprite;
        }
        else if (facingDirection == Vector2.left)
        {
            sr.sprite = leftSprite;
        }
        else if (facingDirection == Vector2.right)
        {
            sr.sprite = rightSprite;
        }
    }

    public void OnLevelUp()
    {
        ui.UpddateXpBar();
        ui.UpdateLevelText();
        print("I leveled up!");
    }

    public void TakeDamage(int damageTaken)
    {
        curHp -= damageTaken;

        ui.UpddateHealthBar();

        if (curHp <= 0)
            Die();
    }

    void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RpgGame");
    }

    //public void AddXp (int xp) // in its own script now
    //{
    //    curXp += xp;

    //    ui.UpddateXpBar();

    //    if (curXp >= xpToNextLevel)
    //        LevelUp();
    //}

    //void LevelUp()
    //{
    //    curXp -= xpToNextLevel;
    //    curLevel++;

    //    xpToNextLevel = Mathf.RoundToInt((float)xpToNextLevel * levelXpModifier);

    //    ui.UpdateLevelText();
    //    ui.UpddateXpBar();
    //}

    //public void AddItemToInventory(string item)
    //{
    //    inventory.Add(item);
    //    ui.UpdateInventoryText();
    //}
}
