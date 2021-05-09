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
    public bool toBeTriggered;
    private bool waitForPress;

    public GameObject rat; //to stop rat chasing code when in a dialogue

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
        if(toBeTriggered){
            if(other.gameObject.name == "Player"){
            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();

            //stop rat chasing
            rat.GetComponent<chasing_player>().speed = 0;

            if(destroyWhenActivated){
                Destroy(gameObject);
            }
        }
            
            // if(requireButtonPress){
            //     waitForPress = true;
            //     return;
            // }
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.name == "Player"){

            rat.GetComponent<chasing_player>().resetSpeed();
            waitForPress = false; //when the player leaves the zone, they can't activate the text box again 
        }
    }
}