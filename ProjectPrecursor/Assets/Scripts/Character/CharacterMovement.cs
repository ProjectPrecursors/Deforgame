using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    

    public float jumpForce = 10;
    [Range(1, 10)] public float walkSpeed = 4;
    [Range(1, 10)] public float runSpeed = 6;
    [Range(1, 10)] public float crouchSpeed = 2;
    [Range(0.1f, 10)] public float jumpPower = 0.9f;
    [Range(0.1f, 10)] public float jumpSpeed = 0.1f;

    public Vector3 charSize;
    public GameObject charSpriteObj;

    private float horizontalMovement;
    private float horizontalSpeed;

    private bool isJump = false;
    private bool canJump = false;
    private bool isGrounded = false;
    private float verticalMovement = 0;
    public float gravity;
    public float faceDir = 0;
    public LayerMask groundLayer;

    private CharacterState charStateScript;

    // Use this for initialization
    void Start () {
        charStateScript = GetComponent<CharacterState>();

    }
	
	// Update is called once per frame
	void Update () {

        
        if (Input.GetKeyDown(GlobalSettings.keyBinds["Run"]) 
            && (charStateScript.currPriState == CharacterState.priCharState.stand || charStateScript.currPriState == CharacterState.priCharState.walking))
        {
            horizontalSpeed = runSpeed;
            charStateScript.currPriState = CharacterState.priCharState.running;
        }
        else if (Input.GetKeyUp(GlobalSettings.keyBinds["Run"]) && charStateScript.currPriState == CharacterState.priCharState.running)
        {
            horizontalSpeed = walkSpeed;
            charStateScript.currPriState = CharacterState.priCharState.stand;
        }

        if (Input.GetKeyDown(GlobalSettings.keyBinds["Crouch"])
            && (charStateScript.currPriState == CharacterState.priCharState.stand || charStateScript.currPriState == CharacterState.priCharState.walking
            || charStateScript.currPriState == CharacterState.priCharState.running))
        {
            horizontalSpeed = crouchSpeed;
            //Change Charactersize
            charSize.y /= 2;
            charSpriteObj.transform.localScale = new Vector3(charSpriteObj.transform.localScale.x, charSpriteObj.transform.localScale.y / 2, charSpriteObj.transform.localScale.z);
            charSpriteObj.transform.position = new Vector3(charSpriteObj.transform.position.x, charSpriteObj.transform.position.y - charSpriteObj.transform.localScale.y / 4, charSpriteObj.transform.position.z);
            GetComponent<BoxCollider2D>().size = new Vector2(charSize.x, charSize.y);
            GetComponent<BoxCollider2D>().offset = new Vector2(0f, GetComponent<BoxCollider2D>().offset.y - GetComponent<BoxCollider2D>().offset.y/2);
            charStateScript.currPriState = CharacterState.priCharState.crouching;
            Debug.Log(charStateScript.currPriState);
        }
        else if (!Input.GetKey(GlobalSettings.keyBinds["Crouch"]) && charStateScript.currPriState == CharacterState.priCharState.crouching )
        {
            if (CharCeilingCheck())
            {
                charSize.y *= 2;
                charSpriteObj.transform.position = new Vector3(charSpriteObj.transform.position.x, charSpriteObj.transform.position.y + charSpriteObj.transform.localScale.y / 4, charSpriteObj.transform.position.z);
                charSpriteObj.transform.localScale = new Vector3(charSpriteObj.transform.localScale.x, charSpriteObj.transform.localScale.y * 2, charSpriteObj.transform.localScale.z);
                GetComponent<BoxCollider2D>().offset = new Vector2(0f, GetComponent<BoxCollider2D>().offset.y + GetComponent<BoxCollider2D>().offset.y);
                GetComponent<BoxCollider2D>().size = new Vector2(charSize.x, charSize.y);
                horizontalSpeed = walkSpeed;
                charStateScript.currPriState = CharacterState.priCharState.stand;
            }
           
        }

        if (charStateScript.currPriState == CharacterState.priCharState.stand)
        {
            horizontalSpeed = walkSpeed;
        }

        if (Input.GetKey(GlobalSettings.keyBinds["Left"]))
        {   
            if (charStateScript.currPriState == CharacterState.priCharState.stand) charStateScript.currPriState = CharacterState.priCharState.walking;
            horizontalMovement = -horizontalSpeed * Time.deltaTime;
            faceDir = 180;
        }
        else if (Input.GetKey(GlobalSettings.keyBinds["Right"]))
        {
            if (charStateScript.currPriState == CharacterState.priCharState.stand) charStateScript.currPriState = CharacterState.priCharState.walking;
            horizontalMovement = horizontalSpeed * Time.deltaTime;
            faceDir = 0;
        }
       else
        {
            horizontalMovement = 0;
        }
        Debug.Log(charStateScript.currPriState);
        if (Input.GetKeyDown(GlobalSettings.keyBinds["Jump"]) && canJump)
        {
            isJump = true;
            canJump = false;
            isGrounded = false;
            verticalMovement = jumpPower;
            charStateScript.currPriState = CharacterState.priCharState.jumping;
        }
       if (isJump)
       {
            ///https://youtu.be/PlT44xr0iW0?t=13m50ss //Alternative Method
            if (verticalMovement > -charSize.y)
            {
                verticalMovement = verticalMovement - (jumpSpeed * Time.deltaTime);
                if (verticalMovement <= 0)
                {
                    isJump = false;
                    if (charStateScript.currPriState == CharacterState.priCharState.jumping)
                        charStateScript.currPriState = CharacterState.priCharState.inair; //Here might have problems
                }
            }
            else if (verticalMovement <= -charSize.y)
            {
                verticalMovement = -charSize.y; //Make Sure character dont fall too fast for collision detection
                
            }
            
        }
       if (!isGrounded)
        {
            verticalMovement -= gravity * Time.deltaTime;
        }

        if (isGrounded)
        {
            CharGroundCheck();
        }
        Debug.Log(charStateScript.currPriState);
        Vector3 newPosition = new Vector3(transform.position.x + horizontalMovement, transform.position.y + verticalMovement, transform.position.z);
        newPosition = CheckNextPosition(newPosition);

        transform.position = newPosition;
    }

    private Vector3 CheckNextPosition(Vector3 nextPos)
    {
        Vector3 modifyPos = nextPos;
        Vector2 nextPosY = new Vector2(transform.position.x, nextPos.y + (charSize.y/2));
        if (Physics2D.OverlapBox(nextPosY, charSize, 0f, groundLayer) && !isGrounded && !isJump) //Returns the first collider that overlaps
        {
            float newDy = Physics2D.OverlapBox(nextPosY, charSize, 0f, groundLayer).Distance(gameObject.GetComponent<Collider2D>()).distance;
            //Debug.Log("Dy" + newDy);
            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.transform.position = nextPosY;
            //cube.transform.localScale = charSize;
            //stope = true;

            
            modifyPos.y = transform.position.y - newDy;
            isGrounded = true;
            charStateScript.currPriState = CharacterState.priCharState.stand;
            verticalMovement = 0;
            isJump = false;
            canJump = true;
        }

        return modifyPos;
    }

    private void CharGroundCheck()
    {
        Vector3 groundCheckSize = new Vector3(charSize.x, 0.1f, 1f);
        if (Physics2D.OverlapBox((transform.position + Vector3.down*0.1f), groundCheckSize, 0f, groundLayer) == null)
        {
            isGrounded = false;
            charStateScript.currPriState = CharacterState.priCharState.inair;
            canJump = false;
        }
    }


    private bool CharCeilingCheck()
    {
        Vector3 ceilingCheckSize = new Vector3(charSize.x, charSize.y, 1f);
        if (Physics2D.OverlapBox((transform.position + Vector3.up * (charSize.y + charSize.y)), ceilingCheckSize, 0f, groundLayer) == null)
        {
            return true;
        }
        return false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.contacts[0].point.y < transform.position.y + 1.0f && !isGrounded)
        //{
        //    isGrounded = true;
        //    transform.position = new Vector3(transform.position.x, collision.contacts[0].point.y, transform.position.z);
        //    verticalMovement = 0;
        //    isJump = false;
        //    Debug.Log("STOP");
        //}
        //CHANGE TO RAYCAST 
    }

}
