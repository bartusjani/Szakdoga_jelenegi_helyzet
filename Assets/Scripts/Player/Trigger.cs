using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{

    public GameObject interactText;

    public PopUpBubble popUpPrefab;
    string popUpMessage;

    public ObjectiveBubble objPrefab;
    string objMessage;

    public SpeechBubble speechPrefab;
    string speechMessage;

    private PopUpBubble activeBubble;
    private ObjectiveBubble objActiveBubble;
    private SpeechBubble speechActiveBubble;

    private TextAsset popUpText;
    private TextAsset speechText;
    private TextAsset objectiveText;

    public Transform doorWaypointTarget;
    public WayPointUI waypoint;

    ItemAdder itemAdder;

    public bool isDoorOpen = false;
    bool onlypopUp = false;
    bool isPlayerInTrigger = false;
    bool wasSpeaking = false;
    bool wasButtonPressed = false;

    public InventoryPage invPage;
    public Sprite itemImage;
    string itemTitle = "Magic System";
    string itemDesc = "This place was built by man using the power of the gods. Some people had the affinity for their magic. Favored by them. My people the Goldens were the best of the best. We had a deep understanding of the flow of energy they emited just by existing. But now I dont feel their toutch. Even my crystal that stored the power of Sa'hur from the stas is drained. What happened to them? I can still feel their magic around but faint. I need to get closer to light, that way I might be able to trap some power in my crystal so I can use it. So much time must have passed, this place looks like a ruin at this rate, all the glory and elegance is a thing of the past. I hate these memories, I should have died, yet I'm here...yet I am.";
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInTrigger)
        {
            wasButtonPressed = true;
            itemAdder=gameObject.GetComponent<ItemAdder>();
            itemAdder.AddItemToInv(invPage,itemImage,itemTitle,itemDesc);
            interactText.SetActive(false);
            if (!wasSpeaking)
            {
                onlypopUp = true;
                ChooseTexts(2);
                StartCoroutine(SetPopUp(popUpMessage,objMessage, speechMessage));
            }

           waypoint.SetTarget(doorWaypointTarget.transform);
           isDoorOpen = true;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactText.SetActive(true);
            isPlayerInTrigger = true;
            if (!wasButtonPressed)
            {
                ChooseTexts(1);
                SetPopUp(popUpMessage);
                onlypopUp = false;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactText.SetActive(false);
            isPlayerInTrigger = false;
            if (activeBubble != null)
            {
                Destroy(activeBubble.gameObject);
                activeBubble = null;
            }
            if (objActiveBubble != null)
            {
                Destroy(objActiveBubble.gameObject);
                objActiveBubble = null;
            }
            if (speechActiveBubble != null)
            {
                Destroy(speechActiveBubble.gameObject);
                speechActiveBubble = null;
            }
            StopAllCoroutines();
            if (wasButtonPressed)
            {
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    void SetPopUp(string message)
    {
        if (activeBubble == null)
        {
            Transform parent = GameObject.Find("PopUps").transform;

            activeBubble = Instantiate(popUpPrefab, parent);
            activeBubble.SetText(message);
            
        }
    }
    IEnumerator SetPopUp(string message,string objMessage, string speechMessage)
    {
        if (objActiveBubble == null || activeBubble == null)
        {
            if (activeBubble != null)
            {
                Destroy(activeBubble.gameObject);
            }
            Transform parent = GameObject.Find("PopUps").transform;

            activeBubble = Instantiate(popUpPrefab, parent);
            activeBubble.SetText(message);

            yield return new WaitForSeconds(0.2f);
            SetObjective(objMessage);

            yield return new WaitForSeconds(0.1f);
            SetSpeech(speechMessage);
            wasSpeaking = true;
            PopUpCounter.Instance.secondTextIndex++;
        }
    }

    void SetObjective(string message)
    {
        if (objActiveBubble == null)
        {
            Transform parent = GameObject.Find("ObjectiveBubbles").transform;

            objActiveBubble = Instantiate(objPrefab, parent);
            objActiveBubble.SetText(message);

        }
    }
    void SetSpeech(string message)
    {
        if (speechActiveBubble == null && !wasSpeaking)
        {
            Transform parent = GameObject.Find("PlayerSpeechBubbles").transform;

            speechActiveBubble = Instantiate(speechPrefab, parent);
            speechActiveBubble.SetText(message);
            //wasSpeaking = true;
        }
    }

    void ChooseTexts(int index)
    {
        popUpText = Resources.Load<TextAsset>("PopUpTexts");
        string[] popUpSorok = popUpText.text.Split('\n');
        popUpMessage = popUpSorok[index].Trim();
        if (!onlypopUp)
        {
            popUpText = Resources.Load<TextAsset>("PopUpTexts");
            popUpSorok = popUpText.text.Split('\n');
            popUpMessage = popUpSorok[index].Trim();
            onlypopUp = true;
        }

        objectiveText = Resources.Load<TextAsset>("SecondPopUpsInARoom/SecondObjectiveTexts");
        string[] objectSorok = objectiveText.text.Split('\n');
        objMessage = objectSorok[index-1].Trim();

        speechText = Resources.Load<TextAsset>("SecondPopUpsInARoom/SecondSpeechTexts");
        string[] speechSorok = speechText.text.Split('\n');
        speechMessage = speechSorok[index].Trim();

    }
}
