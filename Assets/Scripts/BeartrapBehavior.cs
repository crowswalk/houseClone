using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeartrapBehavior : MonoBehaviour
{
    public Inventory playerInv;
    
    public bool isTrapPlaced = false; //true if the bear trap is been placed

    public Sprite trapOpen;
    public Sprite trapClosed;
    SpriteRenderer myRender;

    [SerializeField]
    [Range(0.0f, 20.0f)]
    float trapX, trapY, trapXOffset; //indicates how far beartrap placed from player

    Vector2 trapPos;
    // Start is called before the first frame update
    void Start()
    {
        myRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        trapPos = new Vector2(transform.position.x + trapX, transform.position.y + trapY);
        placing();
    }

    void placing() //press [space] to place trap, making isPlaced = true
    {
        if (playerInv.holdingObj != null)
        {
            if (playerInv.holdingObj == gameObject)
            {
                isTrapPlaced = false;
                myRender.sprite = trapClosed; //sprite become beartrap_closed when holding on hand
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isTrapPlaced = true;
                    playerInv.holdingObj.layer = 0;
                    playerInv.holdingObj.GetComponent<BoxCollider2D>().enabled = true;//reset configuration
                    playerInv.holdingObj.transform.position = trapPos;
                    Destroy(playerInv.icon[playerInv.backpack.IndexOf(playerInv.holdingObj)]); //destroy icon  
                    playerInv.icon[playerInv.backpack.IndexOf(playerInv.holdingObj)] = null; //remove icon from icon list
                    playerInv.backpack.RemoveAt(playerInv.backpack.IndexOf(playerInv.holdingObj)); //remove object from backpack list
                    playerInv.holdingObj = null;
                    myRender.sprite = trapOpen; //sprite become beartrap_open when putting on ground
                }
            }
        }
    }

    //when ratMonster collide with bear trap, the death anime will show, and ratMonster will be destroyed
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "RatMonster" && isTrapPlaced)
        {
            collision.gameObject.GetComponent<RatMonster>().dead();
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "RatMonster" && isTrapPlaced)
        {
            collision.gameObject.GetComponent<RatMonster>().dead();
            Destroy(gameObject);
        }
    }
}
