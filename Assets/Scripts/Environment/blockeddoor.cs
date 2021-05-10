using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockeddoor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public static bool blockdoor;
    private bool cond;

    public SoundManager sound;
    // Start is called before the first frame update
    void Start()
    {
        cond = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        blockdoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(cond);
        if (cond && axe.useaxe == true)
        {
            sound.playSound(SoundEffects.AxeBreakWood);
            spriteRenderer.sprite = newSprite;
            blockdoor = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            cond = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            cond = false;
        }
    }
}
