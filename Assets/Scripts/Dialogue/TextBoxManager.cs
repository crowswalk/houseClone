using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
    public GameObject textBox; //accesses text box obj
    public Text theText; //accesses text obj
    
    public TextAsset textFile; //TextAsset = block of text
    public string[] textLines; //empty array where the individul lines of the text will go

    public int currentLine;
    public int endAtLine;

    public MovePlayer player;
    
    //disable shooting while text is on screen
    public Shotgun shotgun;
    private float resetShootingTime;
    private float currentShootingTime;
    private bool startCountDown;
    
    public bool isActive;
    public bool stopPlayerMovement;

    private bool isTyping = false; //for when the text is scrolling across the screen, so we can set it to active
    private bool cancelTyping = false; //so we can have the player be able to skip through the scrolling and see all the text at once

    public float typeSpeed; 

    void Start()
    {
        player = FindObjectOfType<MovePlayer>(); //setting up to stop the player from moving
        shotgun = FindObjectOfType<Shotgun>(); //setting up to stop the player from using shotgun

        if(textFile != null){ //if there's a text file that exists...
            textLines = (textFile.text.Split('\n')); //grab the text and split it into seperate pieces wherever you see \n (an indent/when [return] has been pressed)
        }

        if(endAtLine == 0){ //if the lines end...
            endAtLine = textLines.Length - 1; //go as far as the last line and stop
        }

        if(isActive){ //if the textbox is active, enable it; otherwise, disable it
            EnableTextBox();
        } else{
            Debug.Log("not active");
            DisableTextBox(); 
        }

        resetShootingTime = 2.0f;
        currentShootingTime = resetShootingTime;
        startCountDown = false;
    }

    void Update()
    { //as you click, the text on the text object moves down the array
        if(!isActive){ 
            return; //if the textbox isn't active/being shown, nothing needs to be run
        }
        
        //theText.text = textLines[currentLine];

        if(Input.GetKeyDown(KeyCode.Space)){ //when [space] is pressed...
            if(!isTyping){
                currentLine += 1; //move to the next line
                if(currentLine > endAtLine){ //once you've reached the end of the lines...
                    FindObjectOfType<chasing_player>().resetSpeed(); //reset rat's speed
                    DisableTextBox();//go to the DisableTextBox
                } 
                else{ //otherwise, the text will scroll
                StartCoroutine(TextScroll(textLines[currentLine])); //when a line is on the screen, move onto the next and start the coroutine to scroll through the text
                } 
            }
            else if (isTyping && !cancelTyping){ //interrupting the text
            cancelTyping = true;
            } 
        }
    }

    private IEnumerator TextScroll(string lineOfText){ //setting up a coroutene, which works in it's own timeline, in a way (runs at the same time as everything else)
        int letter = 0;
        theText.text = ""; //display nothing in the box
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1)){ //while the text is true/cancel text is false/the letter is one less than the length of the text...
            theText.text += lineOfText[letter]; //look at string of text, and go to the number of th int. letter
            letter += 1; //move to the next letter
            yield return new WaitForSeconds(typeSpeed); //wait for however long the speed is set to, to let the text scroll across the screen
        }
        theText.text = lineOfText; //when cancelTyping becomes true, the while loop breaks and it'll print the whole line on screen
        isTyping = false;
        cancelTyping = false; //resets the value just in case
    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);
        isActive = true;
        
        //if(stopPlayerMovement){
            player.canMove = false; //stops the player from moving when the text is on screen
        shotgun.canShoot = false; //stops the player from shooting when the text is on screen
        //}

        StartCoroutine(TextScroll(textLines[currentLine])); 
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false); //get rid of the text box once you've reached the end of the lines. it's set to > rather than = so it doesn't just delete as soon as the last line shows up
        isActive = false;
        player.canMove = true;
        shotgun.reloading = true;
        shotgun.canShoot = true;
    }

    public void ReloadScript(TextAsset theText) //going to make it so I can use different text files
    {
        if(theText != null){
            textLines = new string[1]; //takes the array that already exists, remove it, and fill it in with a new one
            textLines = (theText.text.Split('\n'));
        }

    }

}