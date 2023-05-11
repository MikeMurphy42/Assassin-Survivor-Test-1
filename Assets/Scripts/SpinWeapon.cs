using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : MonoBehaviour
{

    public float rotateSpeed;

    public Transform holder, blackHoleToSpawn;

    public float timeBetweenSpawn;
    private float spawnCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));

        spawnCounter -= Time.deltaTime;
        if (spawnCounter <0)
        {
            spawnCounter = timeBetweenSpawn;

            Instantiate(blackHoleToSpawn, blackHoleToSpawn.position, blackHoleToSpawn.rotation, holder).gameObject.SetActive(true);
        }
    }
}
