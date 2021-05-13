using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BeartrapBehavior : MonoBehaviour
{
    public Inventory playerInv;
    public MovePlayer player;
    //private int inside;

    public bool isTrapPlaced = false; //true if the bear trap is been placed

    public Sprite trapOpen;
    public Sprite trapClosed;
    SpriteRenderer myRender;

    public TextBoxManager text; //check if text is active before letting player drop bear trap.

    [SerializeField]
    [Range(0.0f, 20.0f)]
    float trapX, trapY, trapXOffset; //indicates how far beartrap placed from player

    Vector2 trapPos;
    private bool endText;
    public Image spaceToUse;
    // Start is called before the first frame update
    void Start()
    {
        endText = false;
        myRender = GetComponent<SpriteRenderer>();
        //inside = 0;
    }

    // Update is called once per frame
    void Update()
    {
        trapPos = new Vector2(transform.position.x + trapX, transform.position.y + trapY);
        if (playerInv.holdingObj == gameObject)
        {
            spaceToUse.enabled = true;
            if (text.isActive)
            {
                endText = true;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                endText = false;
            }

            if (!text.isActive && !endText)
            { //only place trap if textbox is not active
                placing();
            }
        }


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
                    spaceToUse.enabled = false;
                    playerInv.holdingObj.GetComponent<SpriteRenderer>().enabled = true;//reset configuration
                    //player.changeSprites("default");
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    isTrapPlaced = true;
                    playerInv.holdingObj.layer = 0;
                    playerInv.holdingObj.GetComponent<BoxCollider2D>().enabled = true;//reset configuration
                    playerInv.holdingObj.transform.position = trapPos;
                    int removeIndex = playerInv.backpack.IndexOf(playerInv.holdingObj);
                    playerInv.icons[removeIndex].sprite = playerInv.emptyIcon; //remove icon from icon list
                    playerInv.backpack.RemoveAt(removeIndex); //remove object from backpack list
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
        if (collision.gameObject.name == "RatMonster" && isTrapPlaced)
        {
            collision.gameObject.GetComponent<RatMonster>().dead();
            Destroy(gameObject);

        }
    }
}
