using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public GameObject shotgunEffectArea; //this is the triigering box that check whether bullet hits enemy

    public MovePlayer player; //use to check which direction player is facing
    public SoundManager sound;

    public bool reloading; //to check whether plyer just fired
    [SerializeField]
    float reloadingTime, currentReloadingTime;

    Vector2 effectAreaPos;

    [SerializeField]
    float rangeX, rangeY; //range of the shotgun, setting x positiong of shotgunEffectArea

    [SerializeField]
    [Range(0, 10.0f)]
    float offsetX; //temperary adjustment of x position when player facing left

    [SerializeField]
    [Range(0, 2.0f)]
    float waitTime;

    public bool canShoot; //player cannot shoot while in dialogue

    // Start is called before the first frame update
    void Start()
    {
        effectAreaPos = transform.position;
        reloading = false;
        currentReloadingTime = reloadingTime;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (reloading)
        {
            countDown();
        }
    }

    public void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !reloading && canShoot)
        {
            sound.playSound(SoundEffects.ShotGunFire);
            if (player.dir.x > 0) //facing right
            {
                effectAreaPos = new Vector2(transform.position.x + rangeX, transform.position.y + rangeY);
            }
            else //facing left
            {
                effectAreaPos = new Vector2(transform.position.x - rangeX - offsetX, transform.position.y + rangeY);
            }
            GameObject bul = Instantiate(shotgunEffectArea, effectAreaPos, transform.rotation);
            if (player.dir.x > 0)
            {
                bul.GetComponent<SpriteRenderer>().flipX = false;
            } else
            {
                bul.GetComponent<SpriteRenderer>().flipX = true;
            }
            reloading = true;
        }
    }

    void countDown() //when count down end, reloading = false
    {
        if (currentReloadingTime > 0)
        {
            currentReloadingTime -= Time.deltaTime;
        } else
        {
            reloading = false;
            currentReloadingTime = reloadingTime;
        }
    }
}
