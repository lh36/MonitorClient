using UnityEngine;
using System.Collections;

public class PlayerRidController : MonoBehaviour {


    [SerializeField] private float m_MaxSpeed = 200f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float m_JumpForce = 10000f;                  // Amount of force added when the player jumps.
    [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

    private bool m_Grounded;            // Whether or not the player is grounded.
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        /*int buildingId = GameManager.Instance.BuildingId;
        if(buildingId != 0)
        {
            Vector3 pos = GameObject.Find ("Building" + buildingId).transform.position;
            gameObject.transform.position = new Vector3 (pos.x, pos.y - 50, gameObject.transform.position.z);
        }*/
        m_Anim.SetInteger (Constant.Par_State, 0);
    }

    void OnCollisionEnter2D(Collision2D coll) 
    {
        if(coll.gameObject.tag == Constant.Tag_Ground)
        {
            m_Grounded = true;
        }
    }

    public void Move(float move, bool jump)
    {

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Move the character
            m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
        }

        if(m_Grounded && (Mathf.Abs(m_Rigidbody2D.velocity.x) > 0.01 || Mathf.Abs(move - 0) > 0.01))
        {
            //m_Anim.SetInteger ();
        }


        if(m_Grounded && Mathf.Abs(move - 0) < 0.01 && m_Anim.GetInteger(Constant.Par_State) != 0)
        {
            //m_Anim.SetInteger ();
        }

        // If the player should jump...
        if (m_Grounded && jump)
        {
            //m_Anim.SetInteger ();
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }

    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }


}
