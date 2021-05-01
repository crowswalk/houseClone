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
                myRender.sprite = trapClosed;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isTrapPlaced = true;
                    playerInv.holdingObj.layer = 0;
                    playerInv.holdingObj.GetComponent<BoxCollider2D>().enabled = true;//reset configuration
                    playerInv.holdingObj.transform.position = trapPos;
                    Destroy(playerInv.icon[playerInv.backpack.IndexOf(playerInv.holdingObj)]);
                    playerInv.icon[playerInv.backpack.IndexOf(playerInv.holdingObj)] = null;
                    playerInv.backpack.RemoveAt(playerInv.backpack.IndexOf(playerInv.holdingObj));
                    playerInv.holdingObj = null;
                    myRender.sprite = trapOpen;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "RatMonster" && isTrapPlaced)
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "RatMonster" && isTrapPlaced)
        {
            Destroy(collision.gameObject);
        }
    }
}
