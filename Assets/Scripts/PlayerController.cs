using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0;
    public Animator anim;
    
    public Transform childTransform;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        
        //Debug.Log(moveInput);
        
        moveInput.Normalize();
        
        Debug.Log(moveInput);

        transform.position += moveInput * moveSpeed * Time.deltaTime;

        if (moveInput != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // flip right
            
            // For Child
            Vector3 childScale = childTransform.localScale;
            childScale.x = Mathf.Abs(childScale.x);
            childTransform.localScale = childScale;
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // flip left
            
            // For Child
            Vector3 childScale = childTransform.localScale;
            childScale.x = -Mathf.Abs(childScale.x);
            childTransform.localScale = childScale;
        }
        
        
        
        
        
    }
}



