using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    public float moveSpeed = 0;
    public Animator anim;
    
    public Transform[] children;
    public List<GameObject> shaderChangeExcludeList;
    public List<GameObject> materialChangeExcludeList;

    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1.0f;
    private float dashTimer = 0;
    private bool isDashing = false;

    public Shader normalShader;
    public Shader dashShader;
    private List<Renderer> renderers = new List<Renderer>();

    public Material normalMaterial;
    public Material dashMaterial;
    private List<Material> originalMaterials = new List<Material>();

    public bool isInvincible = false;

    public float pickupRange = 1.5f;

    public Weapon activeWeapon;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (!shaderChangeExcludeList.Contains(child.gameObject) && !materialChangeExcludeList.Contains(child.gameObject))
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderers.Add(renderer);
                    originalMaterials.Add(renderer.material);
                    renderer.material.shader = normalShader;
                    renderer.material = normalMaterial;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        
        moveInput.Normalize();

        if (isDashing)
        {
            transform.position += moveInput * dashSpeed * Time.deltaTime;
            dashTimer += Time.deltaTime;

            if (dashTimer >= dashDuration)
            {
                isDashing = false;
                dashTimer = 0;
                anim.SetBool("isDashing", false);
                for (int i = 0; i < renderers.Count; i++)
                {
                    renderers[i].material.shader = normalShader;
                    renderers[i].material = originalMaterials[i];
                }
                isInvincible = false;
            }
        }
        else
        {
            transform.position += moveInput * moveSpeed * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space) && dashTimer >= dashCooldown)
            {
                isDashing = true;
                dashTimer = 0;
                anim.SetBool("isDashing", true);
                foreach (var renderer in renderers)
                {
                    renderer.material.shader = dashShader;
                    renderer.material = dashMaterial;
                }
                isInvincible = true;
            }
            else
            {
                dashTimer += Time.deltaTime;
            }
        }

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
            
            // For Children
            foreach (Transform childTransform in children)
            {
                Vector3 childScale = childTransform.localScale;
                childScale.x = Mathf.Abs(childScale.x);
                childTransform.localScale = childScale;
            }
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // flip left
            
            // For Children
            foreach (Transform childTransform in children)
            {
                Vector3 childScale = childTransform.localScale;
                childScale.x = -Mathf.Abs(childScale.x);
                childTransform.localScale = childScale;
            }
        }
    }
}

