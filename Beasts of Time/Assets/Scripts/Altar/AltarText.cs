using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AltarText : MonoBehaviour
{
    [SerializeField] private GameObject altarTextObject;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private TMP_Text text;

    public string interactionKey = "E";
    

    public void DisplayCompleteAltarMessage(bool altarActivated)
    {
        string message;

        if (!altarActivated)    message = "Activate [" + interactionKey + "]";
        else                    message = "Deactivate [" + interactionKey + "]";

        SetupMessageBubble(message);
    }

    public void DisplayIncompleteAltarMessage()
    {
        string message = "Place orb [" + interactionKey + "]";
        SetupMessageBubble(message);
    }

    private void SetupMessageBubble(string message)
    {
        // Set message to bubble
        text.SetText(message);

        // Get text size and adjust background size
        text.ForceMeshUpdate();
        Vector2 textSize = text.GetRenderedValues(false);
        Vector2 padding = new Vector2(2f, 1f);
        background.size = textSize + padding;

        // Enable text object
        altarTextObject.SetActive(true);
    }
    public void HideMessageBubble()
    {
        altarTextObject.SetActive(false);
    }
}
