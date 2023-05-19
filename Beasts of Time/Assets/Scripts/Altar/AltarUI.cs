using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AltarUI : MonoBehaviour
{
    private TMP_Text altarText;
    public Transform altarPosition;

    public string interactionKey = "E";
    private string message;

    // Start is called before the first frame update
    void Start()
    {
        altarText = GetComponent<TMP_Text>();

        // Set the position of the text top left of the altar
        //transform.position = altarPosition.position + new Vector3(3f, -1f);
    }

    public void DisplayCompleteAltarMessage(bool altarActivated)
    {
        if (!altarActivated)
        {
            message = "Activate [" + interactionKey + "]";
        }
        else
        {
            message = "Deactivate [" + interactionKey + "]";
        }
        
        altarText.text = message;
        altarText.enabled = true;
    }

    public void DisplayIncompleteAltarMessage()
    {
        message = "Place orb [" + interactionKey + "]";
        altarText.text = message;
        altarText.enabled = true;
    }

    public void HideMessage ()
    {
        altarText.enabled = false;
    }
}
