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
    
    public bool isActive;
    public bool stopPlayerMovement;

    private bool isTyping = false; //for when the text is scrolling across the screen, so we can set it to active
    private bool cancelTyping = false; //so we can have the player be able to skip through the scrolling and see all the text at once

    public float typeSpeed; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
