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
    public int rowSize; //the size of one row in backpack 

    //raycasting
    public float sight; //distance of raycasting

    //backpack item drawing
    [SerializeField]
    [Range(0, 10.0f)]
    float initX, initY; //use to determine where icon should draw
    Vector2 initPos; //vector 2 of the starting position of drawing icon

    [SerializeField]
    float gapX, gapY; //use to determine gap between icons

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
            holdingObj.transform.position = gameObject.transform.position;
            holdingObj.GetComponent<SpriteRenderer>().sortingOrder = player.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
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
                Debug.Log("detecting an item");
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

                //setting detectingObj to null 
                //detectingObj = null;

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

    //this function probably has a easier way to do it.......
    void selectObjInBackpack() //allows player to use [1] ~ [0] to select items in icon
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            int index = 0; //the index of icon list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (icon.Count >= index + 1)
            {
                if (icon[index] != null)
                {
                    holdingObj = icon[index];
                    holdingObj.SetActive(true);
                }
            }
            else
            {
                holdingObj = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            int index = 1; //the index of icon list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (icon.Count >= index + 1)
            {
                if (icon[index] != null)
                {

                    holdingObj = icon[index];
                    holdingObj.SetActive(true);
                }
            }
            else
            {
                holdingObj = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            int index = 2; //the index of icon list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (icon.Count >= index + 1)
            {
                if (icon[index] != null)
                {

                    holdingObj = icon[index];
                    holdingObj.SetActive(true);
                }
            }
            else
            {
                holdingObj = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            int index = 3; //the index of icon list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (icon.Count >= index + 1)
            {
                if (icon[index] != null)
                {

                    holdingObj = icon[index];
                    holdingObj.SetActive(true);
                }
            }
            else
            {
                holdingObj = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            int index = 4; //the index of icon list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (icon.Count >= index + 1)
            {
                if (icon[index] != null)
                {

                    holdingObj = icon[index];
                    holdingObj.SetActive(true);
                }
            }
            else
            {
                holdingObj = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            int index = 5; //the index of icon list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (icon.Count >= index + 1)
            {
                if (icon[index] != null)
                {

                    holdingObj = icon[index];
                    holdingObj.SetActive(true);
                }
            }
            else
            {
                holdingObj = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            int index = 6; //the index of icon list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (icon.Count >= index + 1)
            {
                if (icon[index] != null)
                {

                    holdingObj = icon[index];
                    holdingObj.SetActive(true);
                }
            }
            else
            {
                holdingObj = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            int index = 7; //the index of icon list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (icon.Count >= index + 1)
            {
                if (icon[index] != null)
                {

                    holdingObj = icon[index];
                    holdingObj.SetActive(true);
                }
            }
            else
            {
                holdingObj = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            int index = 8; //the index of icon list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (icon.Count >= index + 1)
            {
                if (icon[index] != null)
                {

                    holdingObj = icon[index];
                    holdingObj.SetActive(true);
                }
            }
            else
            {
                holdingObj = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            int index = 9; //the index of icon list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (icon.Count >= index + 1)
            {
                if (icon[index] != null)
                {

                    holdingObj = icon[index];
                    holdingObj.SetActive(true);
                }
            }
            else
            {
                holdingObj = null;
            }
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
        if (holdingObj != null)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                destroyHoldingObj();
            }
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
                if (backpack[i] == null)
                {
                    backpack.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }
        }

        if (icon.Count > 0)
        {
            for (int i = icon.Count - 1; i >= 0; i--)
            {
                if (icon[i] == null)
                {
                    icon.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }
        }
    }

    public void clearInsert(List<GameObject> list, int num) //after inserting object in a list, clear the empty space right after the object
    {
        if (list.Count > num + 1)
        {
            if (list[num + 1] == null)
            {
                list.RemoveAt(num + 1);
            }
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
}
