using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public MovePlayer player;

    public GameObject detectingObj; //the object that raycasting is detecting
    public GameObject holdingObj; //the object that player is holding

    public GameObject backpackBG;
    public GameObject indicator; //indicates which item player is selecting

    public List<GameObject> backpack; //all objects that player picked up will be in backpack
    public List<GameObject> icon; //this is the objects's icon that shows in backpackBG, has the same index with them in List backpack

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
        List<GameObject> icon = new List<GameObject>();

        initPos = new Vector2(backpackBG.transform.position.x - initX, backpackBG.transform.position.y - initY);
    }

    // Update is called once per frame
    void Update()
    {
        //detectItem(); //raycasting to find the detectingObj
        clearLastEmpty(); //remove all empty space at the end of the list

        pickUp(); //press [c] or [left click] to changing detectingObj to holdingObj
        drawIconInGame(); //make icon stay where they should stay
        selectObjInBackpack(); //use [1] to [0] to select items in backpack
        useHoldingObj(); //currently only for testing: press [x] to use item

        //making holdingObj always stay with player
        if (holdingObj != null)
        {
            Vector2 holdingpos = new Vector2(gameObject.transform.position.x + holdingObjX, gameObject.transform.position.y + holdingObjY);
            holdingObj.transform.position = holdingpos;
            holdingObj.GetComponent<SpriteRenderer>().sortingOrder = player.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            // Debug.Log(holdingObj);
            if (holdingObj.GetComponent<Key>() != null)
            {
                Key.haskey = true;
                Debug.Log("haskey");
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
             

                Instantiate(holdingObj, player.transform.position, Quaternion.identity);
            }

<<<<<<< HEAD
            if (checkShotgun()) //allow player to pree [space] to shoot when holdingObj is shotgun
=======
            if (checkShotgun())
>>>>>>> testShooting
            {
                holdingObj.GetComponent<Shotgun>().shoot();
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

                //when picking up, object goes into icon
                int iconIndex = getNearestEmpty(backpack, backpackMax);
                if (iconIndex > -1)
                {
                    backpack.Insert(iconIndex, holdingObj);
                    clearInsert(backpack, iconIndex);
                    drawIcon(holdingObj);
                }
            }
        }
    }

    //NOTE: This method needs to be changed, so that it is fixed to the ui instead of dragging in the world space. 
    void drawIcon(GameObject g) //draw icon to the backpackBG
    {
        Vector2 pos = new Vector2(backpackBG.transform.position.x - gapX, backpackBG.transform.position.y + gapY);
        GameObject o = Instantiate(g, pos, transform.rotation);
        o.GetComponent<SpriteRenderer>().sortingOrder = g.GetComponent<SpriteRenderer>().sortingOrder + 1;
        icon.Insert(backpack.IndexOf(holdingObj), o);
        clearInsert(icon, backpack.IndexOf(holdingObj));
        o.transform.position = new Vector2(initPos.x + (gapX * icon.IndexOf(o)), initPos.y);
        indicator.transform.position = o.transform.position;
    }

    void drawIconInGame() //make icon stay in the specific position
    {
        if (icon.Count > 0)
        {
            for (int i = 0; i < icon.Count; i++)
            {
                Vector2 pos = new Vector2(initPos.x + (gapX * i), initPos.y);
                if (icon[i] != null)
                {
                    icon[i].transform.position = pos;
                }
            }
        }
    }

    void selectObj(int index)
    {
        indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y);
        if (holdingObj != null)
        {
            holdingObj.SetActive(false);
        }

        if (icon.Count >= index + 1 && icon[index] != null)
        {
            holdingObj = backpack[index];
            holdingObj.SetActive(true);
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
            destroyHoldingObj();
        }
    }

    public void destroyHoldingObj() //use/destroy item that player is holding 
    {
        if (holdingObj != null)
        {
            Destroy(icon[backpack.IndexOf(holdingObj)]);
            Destroy(backpack[backpack.IndexOf(holdingObj)]);
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
                icon.RemoveAt(i);
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

    bool checkShotgun()
    {
        if (holdingObj.GetComponent<Shotgun>() != null)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
