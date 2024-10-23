using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//to manipulate ui elements

public class player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveAmount;
    public int health;

    //UI Part
    public Image[] hearts;//Image UI component public var 
    public Sprite fullHeart;//Sprite to replace using function
    public Sprite emptyHeart;

    public Animator hurtanim;//for screen to turn orange when player takes damage

    private SceneTransitions sceneTransitions;//to get scene transition script to access function loadscene()

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();//get rigidbody and animator component at the start
        rb = GetComponent<Rigidbody2D>();
        sceneTransitions = FindObjectOfType<SceneTransitions>();//get scenetransition script at start
    }
    // Update is called once per frame
    private void Update()
    {   //NOte:for keyboard input for movement you can also use Input.Getkey(Keycode.LeftArrow) which return true if left arrow key is pressed but this method is not efficient
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));//Take inputs from keyboard
        moveAmount = moveInput.normalized * speed;
        print(moveInput);

        if (moveInput != Vector2.zero)//Animation run or idle for player
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
    //Used for physics related stuff
    private void FixedUpdate()
    {

        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);//move rigid body to position(takes vector2 as input)

    }
    public void TakeDamage(int damageAmount)//take damage function
    {
        health -= damageAmount;
        UpdateHealthUI(health);//updating ui element of health
        hurtanim.SetTrigger("hurt");//for hurt orange scrren animation

        if (health <= 0)
        {
            Destroy(this.gameObject);
            sceneTransitions.LoadScene("Lose");

        }
    }
    public void ChangeWeapon(Weapon weaponToEquip)//change weapon function takes weapon to change as parameter
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));//destroy current weapon
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);//instantiate new weapon in current player position and rotation
        //and player as parent of weapon
    }

    void UpdateHealthUI(int currentHealth)//updating health sprite in UI
    {
        for (int i = 0; i < hearts.Length; i++)//run until no of health hearts assigned to player
        {

            if (i < currentHealth)//if ith (index) heart is less than current health(eg;health 2 and index[0] that is i=0)
            // make sprite of that heart to full else empty)
            {
                hearts[i].sprite = fullHeart;//make sprite full
            }
            else
            {
                hearts[i].sprite = emptyHeart;//else empty
            }
        }
    }

    public void Heal(int healAmount)//heal function if player picks up health
    {
        if (health + healAmount > 5)//if health exceeds 5
        {
            health = 5;
        }
        else
        {
            health += healAmount;
        }
        UpdateHealthUI(health);//Update health UI element
    }

}
