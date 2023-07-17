using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ThrownWeapon : MonoBehaviour
{
    public float throwPower;
    public Rigidbody2D theRB;
    public float rotateSpeed;
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    public bool flipOptionRight; // Option to flip the sprite when spinning right
    public bool flipOptionLeft; // Option to flip the sprite when spinning left
    
    // Start is called before the first frame update
    void Start()
    {
        theRB.velocity = new Vector2(Random.Range(-throwPower, throwPower), throwPower);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotateSpeed * -360f * Time.deltaTime * Mathf.Sign(theRB.velocity.x)));

        if (theRB.velocity.x < 0)
        {
            // If spinning left, flip the sprite using flipOptionLeft
            FlipSprite(flipOptionLeft);
        }
        else if (theRB.velocity.x > 0)
        {
            // If spinning right, flip the sprite using flipOptionRight
            FlipSprite(flipOptionRight);
        }
    }

    void FlipSprite(bool flipDirection)
    {
        spriteRenderer.flipX = flipDirection;
    }
}