using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public MovePlayer player;

    public GameObject detectingObj; //the object that raycasting is detecting
    public GameObject holdingObj; //the object that player is holding

    public GameObject shortcutBG;
    public GameObject indicator; //indicates which item player is selecting

    public List<GameObject> shortcut; //this is the first line of backpack. During normal mode, only this line will show at the bottom
    public List<GameObject> icon; //this is the objects's icon that shows in shortcut & backpack, has the same index with them

    //backpack size
    public int shortcutMax;
    public int rowSize; //the size of one row in backpack 

    //raycasting
    public float sight; //distance of raycasting

    //backpack item drawing
    [SerializeField]
    [Range(0, 10.0f)]
    float initX, initY; //initY only exist in backpack mode, not shortcut
    Vector2 initPos;

    [SerializeField]
    float gapX, gapY;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> shortcut = new List<GameObject>();
        List<GameObject> icon = new List<GameObject>();

        initPos = new Vector2(shortcutBG.transform.position.x - initX, shortcutBG.transform.position.y - initY);
    }

    // Update is called once per frame
    void Update()
    {
        detectItem(); //raycasting to find the detectingObj
        clearLastEmpty(); //remove all empty space at the end of the list

        pickUp(); //press [c] or [left click] to changing detectingObj to holdingObj
        drawIconOnNormal();
        selectObjInShortcut();
        useHoldingObj(); //currently only for testing: press [x] to use item

        //making holdingObj always stay with player
        if (holdingObj != null)
        {
            holdingObj.transform.position = gameObject.transform.position;
            holdingObj.GetComponent<SpriteRenderer>().sortingOrder = player.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
        initPos = new Vector2(shortcutBG.transform.position.x - initX, shortcutBG.transform.position.y - initY);
    }

    void detectItem() //only detect objects that has tag "Item"
    {
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, player.dir, sight);
        Debug.DrawRay(player.transform.position, player.dir, Color.green);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Item")
            {
                detectingObj = hit.collider.gameObject;
            }
        } else
        {
            detectingObj = null;
        }
    }

    void pickUp() //press [c] or [left click] to changing detectingObj to holdingObj
    {
        detectItem();
        if (detectingObj != null && (shortcut.Count < shortcutMax || checkEmptySpace(shortcut, shortcutMax)))
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
                detectingObj = null;

                //when picking up, object goes into shortcut
                int shortcutIndex = getNearestEmpty(shortcut, shortcutMax);
                if (shortcutIndex > -1)
                {
                    shortcut.Insert(shortcutIndex, holdingObj);
                    clearInsert(shortcut, shortcutIndex);
                    drawIconToShort(holdingObj);
                }
            }
        }
    }

    void drawIconToShort(GameObject g)
    {
        Vector2 pos = new Vector2(shortcutBG.transform.position.x - gapX, shortcutBG.transform.position.y + gapY);
        GameObject o = Instantiate(g, pos, transform.rotation);
        o.GetComponent<SpriteRenderer>().sortingLayerName = "Objects on Interface";
        icon.Insert(shortcut.IndexOf(holdingObj), o);
        clearInsert(icon, shortcut.IndexOf(holdingObj));
        o.transform.position = new Vector2(initPos.x + (gapX * icon.IndexOf(o)), initPos.y);
        indicator.transform.position = o.transform.position;
    }

    void drawIconOnNormal()
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
    void selectObjInShortcut() //allows player to use [1] ~ [0] to select items in shortcut
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            int index = 0; //the index of shortcut list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (shortcut.Count >= index + 1)
            {
                if (shortcut[index] != null)
                {
                    holdingObj = shortcut[index];
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
            int index = 1; //the index of shortcut list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (shortcut.Count >= index + 1)
            {
                if (shortcut[index] != null)
                {

                    holdingObj = shortcut[index];
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
            int index = 2; //the index of shortcut list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (shortcut.Count >= index + 1)
            {
                if (shortcut[index] != null)
                {

                    holdingObj = shortcut[index];
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
            int index = 3; //the index of shortcut list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (shortcut.Count >= index + 1)
            {
                if (shortcut[index] != null)
                {

                    holdingObj = shortcut[index];
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
            int index = 4; //the index of shortcut list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (shortcut.Count >= index + 1)
            {
                if (shortcut[index] != null)
                {

                    holdingObj = shortcut[index];
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
            int index = 5; //the index of shortcut list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (shortcut.Count >= index + 1)
            {
                if (shortcut[index] != null)
                {

                    holdingObj = shortcut[index];
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
            int index = 6; //the index of shortcut list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (shortcut.Count >= index + 1)
            {
                if (shortcut[index] != null)
                {

                    holdingObj = shortcut[index];
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
            int index = 7; //the index of shortcut list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (shortcut.Count >= index + 1)
            {
                if (shortcut[index] != null)
                {

                    holdingObj = shortcut[index];
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
            int index = 8; //the index of shortcut list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (shortcut.Count >= index + 1)
            {
                if (shortcut[index] != null)
                {

                    holdingObj = shortcut[index];
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
            int index = 9; //the index of shortcut list, which player is selecting

            indicator.transform.position = new Vector2(initPos.x + (gapX * index), initPos.y); //draw indicator to corrosponding position

            if (holdingObj != null)
            {
                holdingObj.SetActive(false);
            }

            if (shortcut.Count >= index + 1)
            {
                if (shortcut[index] != null)
                {

                    holdingObj = shortcut[index];
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
            Destroy(icon[shortcut.IndexOf(holdingObj)]);
            Destroy(shortcut[shortcut.IndexOf(holdingObj)]);
        }
    }

    public void clearLastEmpty() //to clear empty space in last of the list
    {
        if (shortcut.Count > 0)
        {
            for (int i = shortcut.Count - 1; i >= 0; i--)
            {
                if (shortcut[i] == null)
                {
                    shortcut.RemoveAt(i);
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

    public void clearInsert(List<GameObject> list, int num) //to clear empty space in a list when insert an item in
    {
        if (list.Count > num + 1)
        {
            if (list[num + 1] == null)
            {
                list.RemoveAt(num + 1);
            }
        }
    }

    public bool checkEmptySpace(List<GameObject> li, int maxSize) //to check wether the list has empty space avaliable
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
