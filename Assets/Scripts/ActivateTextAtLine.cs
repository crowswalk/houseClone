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
        
    }

    void Update()
    {
        
    }
}
