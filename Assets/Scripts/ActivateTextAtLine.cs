using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour
{
    public TextAsset theText;

    public int startLine;
    public int endLine;

    public TextBoxManager theTextBox;

    public bool destroyWhenActivated;
    public bool requireButtonPress;
    private bool waitForPress;

    //CHANGE THIS SO IT'S WHEN YOU ENTER THE SHOUT ZONE, NOT WHEN YOU CLICK A BUTTON

    void Start()
    {
        theTextBox = FindObjectOfType<TextBoxManager>();   
    }

    void Update()
    {
        if(waitForPress && Input.GetMouseButton(0)){ //if we're waiting for a button to be pressed and the mouse is left-clicked...
            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();


            if(destroyWhenActivated){
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.name == "Player"){
            if(requireButtonPress){
                waitForPress = true;
                return;
            }
        }
    }

    void onTriggerExit2D(Collider2D other){
        if(other.gameObject.name == "Player"){
            waitForPress = false; //when the player leaves the zone, they can't activate the text box again 
        }
    }
}
