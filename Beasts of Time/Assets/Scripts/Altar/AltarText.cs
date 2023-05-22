using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AltarText : MonoBehaviour
{
    [SerializeField] private Sprite bubbleSpriteLeft;
    [SerializeField] private Sprite bubbleSpriteRight;
    [SerializeField] private SpriteRenderer altarTextBackground;
    [SerializeField] private TMP_Text altarText;
    [SerializeField] private RectTransform altarTextTransform;

    private Player player;
    public string interactionKey = "E";

    private void Start()
    {
        player = Player.Instance;
    }

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
    public void HideMessageBubble()
    {
        altarTextBackground.enabled = false;
        altarText.enabled = false;
    }
    private void SetupMessageBubble(string message)
    {
        // Set message to bubble
        altarText.SetText(message);

        // Decide on chatbubble
        ChooseMessageBubble();

        // Get text size and adjust background size
        altarText.ForceMeshUpdate();
        Vector2 textSize = altarText.GetRenderedValues(false);

        Vector2 padding = new Vector2(0.8f, 0.3f);
        altarTextBackground.size = textSize + padding;

        if (altarTextBackground.sprite == bubbleSpriteRight)
        {
            altarTextBackground.transform.localPosition = new Vector2((-altarTextBackground.size.x / 2f) + 0.15f, 0.2f);
            altarTextTransform.localPosition = new Vector2(-0.3f, 0.25f);
        }
        else if (altarTextBackground.sprite == bubbleSpriteLeft)
        {
            altarTextBackground.transform.localPosition = new Vector2((altarTextBackground.size.x / 2f) - 0.15f, 0.2f);
            altarTextTransform.localPosition = new Vector2(0.3f, 0.25f);
        }

        // Enable text object
        altarTextBackground.enabled = true;
        altarText.enabled = true;
    }
    private void ChooseMessageBubble()
    {
        Vector2 relativePosition = transform.InverseTransformPoint(player.transform.position);

        if (relativePosition.x > 0)
        {
            // Player is to the right of the altar
            altarTextBackground.sprite = bubbleSpriteRight;
            altarText.alignment = TextAlignmentOptions.MidlineRight;
        }
        else if (relativePosition.x < 0)
        {
            // Player is to the left of the altar
            altarTextBackground.sprite = bubbleSpriteLeft;
            altarText.alignment = TextAlignmentOptions.MidlineLeft;
        }
    }
}
