﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public GameObject shotgunEffectArea; //this is the triigering box that check whether bullet hits enemy

    public MovePlayer player; //use to check which direction player is facing
    public SoundManager sound;

    Vector2 effectAreaPos;

    [SerializeField]
    float rangeX, rangeY; //range of the shotgun, setting x positiong of shotgunEffectArea

    [SerializeField]
    [Range(0, 10.0f)]
    float offsetX; //temperary adjustment of x position when player facing left

    // Start is called before the first frame update
    void Start()
    {
        effectAreaPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sound.playSound(SoundEffects.ShotGunFire);
            if (player.dir.x > 0)
            {
                effectAreaPos = new Vector2(transform.position.x + rangeX, transform.position.y + rangeY);
            }
            else
            {
                effectAreaPos = new Vector2(transform.position.x - rangeX - offsetX, transform.position.y + rangeY);
            }
            Instantiate(shotgunEffectArea, effectAreaPos, transform.rotation);
        }
    }
}
