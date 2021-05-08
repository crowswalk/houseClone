using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public MovePlayer player;

    public GameObject detectingObj; //the object that raycasting is detecting
    public GameObject holdingObj; //the object that player is holding

    public GameObject backpackBG;
    public GameObject indicator; //indicates which item player is selecting

    public List<GameObject> backpack; //all objects that player picked up will be in backpack
    public Image[] icons = new Image[3]; //this is the objects's icon that shows in backpackBG, has the same index with them in List backpack
    public Sprite emptyIcon; //empty image that shows when there is no item in this backpack slot


    //backpack size
    public int backpackMax; //max number of items that player can pack
    //keys for inventory slots
    private KeyCode[] keys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6 };

    //raycasting
    public float sight; //distance of raycasting

    //holdingObj position
    [SerializeField]
    [Range(0, 50.0f)]
    int holdingObjX, holdingObjY;

    //backpack item drawing
    [SerializeField]
    [Range(-50, 50)]
    int initX, initY; //use to determine where icon should draw
    Vector2 initPos; //vector 2 of the starting position of drawing icon

    [SerializeField]
    int gapX, gapY; //use to determine gap between icons

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> backpack = new List<GameObject>();
        initPos = new Vector2(backpackBG.transform.position.x - initX, backpackBG.transform.position.y - initY);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        clearLastEmpty(); //remove all empty space at the end of the list
        pickUp(); //press [c] or [left click] to changing detectingObj to holdingObj
        selectObjInBackpack(); //use [1] to [0] to select items in backpack
        useHoldingObj(); //currently only for testing: press [x] to use item

        //making holdingObj always stay with player
        if (holdingObj != null)
        {
            Vector2 holdingpos = new Vector2(gameObject.transform.position.x + holdingObjX, gameObject.transform.position.y + holdingObjY);
            holdingObj.transform.position = holdingpos;
            holdingObj.GetComponent<SpriteRenderer>().enabled = false;

            if (holdingObj.GetComponent<Key>() != null)
            {
                Key.haskey = true;
            }
            else
            {
                Key.haskey = false;
            }
            if (Input.GetKeyDown(KeyCode.X))//when holding object, press x to drop object
            {
                holdingObj.layer = 0;
                holdingObj.GetComponent<BoxCollider2D>().enabled = true;//reset configuration

                if (holdingObj.GetComponent<bowling_ball>() != null)
                {
                    bowling_ball.drop = true;
                }

                holdingObj.GetComponent<SpriteRenderer>().enabled = true;
                Instantiate(holdingObj, player.transform.position, Quaternion.identity);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (holdingObj.GetComponent<plunger>() != null)
                {
                    plunger.use = true;
                }
                if (holdingObj.GetComponent<axe>() != null)
                {
                    axe.useaxe = true;
                }
            }

            if (checkShotgun())
            {
                holdingObj.GetComponent<Shotgun>().shoot();
            }

            if (checkBowling())
            {
                holdingObj.GetComponent<bowling_ball>().dropBowling();
            }
        }
        initPos = new Vector2(backpackBG.transform.position.x - initX, backpackBG.transform.position.y - initY);
    }

    void detectItem() //only detect objects that has tag "Item"
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, player.dir, sight);
        Debug.DrawRay(player.transform.position, player.dir * sight, Color.green);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Item")
            {
                detectingObj = hit.collider.gameObject;
            }
        }
        else
        {
            detectingObj = null;
        }
    }

    void pickUp() //press [c] or [left click] to changing detectingObj to holdingObj
    {
        detectItem();
        if (detectingObj != null && (backpack.Count < backpackMax || checkEmptySpace(backpack, backpackMax)))
        {
            if (Input.GetKeyDown(KeyCode.C) || (Input.GetMouseButtonDown(0)))
            {
                if (holdingObj != null)
                {
                    holdingObj.SetActive(false);
                }
                holdingObj = detectingObj;
                holdingObj.layer = 2; //making object on hand ignore raycasting
                holdingObj.transform.position = gameObject.transform.position;
                player.changeSprites(holdingObj.name);

                //when picking up, object goes into icon
                int iconIndex = getNearestEmpty(backpack, backpackMax);
                if (iconIndex > -1)
                {
                    backpack.Insert(iconIndex, holdingObj);
                    clearInsert(backpack, iconIndex);
                    drawIcon(holdingObj, iconIndex);
                    selectObj(iconIndex);
                }
            }
        }
    }

    void drawIcon(GameObject g, int iconIndex) //draw icon to the backpackBG
    {
        indicator.transform.position = new Vector2(initPos.x + (gapX * iconIndex), initPos.y);
        Sprite itemicon = g.GetComponent<ItemIcon>().inventoryIcon;
        icons[iconIndex].sprite = itemicon;
    }

    void selectObj(int index)
    {
        indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y);
        if (holdingObj != null)
        {
            holdingObj.SetActive(false);
        }
        else
        {
            player.changeSprites("default");
        }

        if (backpack.Count >= index + 1 && backpack[index] != null)
        {
            holdingObj = backpack[index];
            holdingObj.SetActive(true);
            Debug.Log(holdingObj.name);
            player.changeSprites(holdingObj.name);
        }
        else
        {
            holdingObj = null;
        }
    }

    void selectObjInBackpack() //allows player to use [1] ~ [0] to select items in icon
    {
        for (int i = 1; i < backpackMax; i++)
        {
            if (Input.GetKeyDown(keys[i - 1])) { selectObj(i - 1); }
        }
    }

    int getNearestEmpty(List<GameObject> li, int maxNum)
    {
        if (li.Count < maxNum || checkEmptySpace(li, maxNum))
        {
            for (int i = 0; i < li.Count; i++)
            {
                if (li[i] == null)
                {
                    return i;
                }
            }
            return li.Count;
        }
        return -1;
    }

    void useHoldingObj() //press [x] to use item
    {
        if (holdingObj != null && Input.GetKeyDown(KeyCode.X))
        {
            holdingObj.GetComponent<SpriteRenderer>().enabled = true;
            int removeIndex = backpack.IndexOf(holdingObj);
            icons[removeIndex].sprite = emptyIcon;
            Destroy(backpack[removeIndex]);
            player.changeSprites("default");
        }
    }

    public void clearLastEmpty() //to clear empty space in last of the list
    {
        if (backpack.Count > 0)
        {
            for (int i = backpack.Count - 1; i >= 0; i--)
            {
                if (backpack[i] != null)
                {
                    break;
                }
                backpack.RemoveAt(i);
            }
        }

    }

    public void clearInsert(List<GameObject> list, int num) //after inserting object in a list, clear the empty space right after the object
    {
        if (list.Count > num + 1 && list[num + 1] == null)
        {
            list.RemoveAt(num + 1);
        }
    }

    public bool checkEmptySpace(List<GameObject> li, int maxSize) //to check whether the list has empty space avaliable
    {
        for (int i = 0; i < maxSize; i++)
        {
            if (li[i] == null)
            {
                return true;
            }
        }
        return false;
    }

    bool checkShotgun() //if holdingObj is shotgun, return true
    {
        if (holdingObj.GetComponent<Shotgun>() != null)
        {
            return true;
        }
        return false;
    }

    bool checkBowling() //if holdingObj is bowling, return true
    {
        if (holdingObj.GetComponent<bowling_ball>() != null)
        {
            return true;
        }
        return false;
    }
}
