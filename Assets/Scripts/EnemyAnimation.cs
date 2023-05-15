using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{

    public Transform sprite;

    public float speed;
    
    // These variables will be editable in the inspector
    public float minSpeedFactor = .75f;
    public float maxSpeedFactor = 1.25f;

    public float minSize, maxSize;

    private float activeSize;
    
    // Start is called before the first frame update
    void Start()
    {
        activeSize = maxSize;

        speed = speed * Random.Range(minSpeedFactor, maxSpeedFactor);
    }

    // Update is called once per frame
    void Update()
    {
        sprite.localScale = Vector3.MoveTowards(sprite.localScale, Vector3.one * activeSize, speed *Time.deltaTime);
        if (sprite.localScale.x == activeSize)
        {
            if (activeSize == maxSize)
            {
                activeSize = minSize;
            }
            else
            {
                activeSize = maxSize;
            }
        }
    }
}
