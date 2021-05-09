using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SisterActivateTextAtLines : MonoBehaviour
{
    public TextAsset theText;

    public int startLine;
    public int endLine;

    public TextBoxManager theTextBox;

    public bool destroyWhenActivated;
    public bool requireButtonPress;
    private bool waitForPress;

    public GameObject rat; //to stop rat chasing code when in a dialogue


    // Start is called before the first frame update
    void Start()
    {
        theTextBox = FindObjectOfType<TextBoxManager>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
