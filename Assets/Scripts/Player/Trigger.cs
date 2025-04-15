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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInTrigger)
        {
            wasButtonPressed = true;
            itemAdder=gameObject.GetComponent<ItemAdder>();
            itemAdder.AddItemToInv(invPage,itemImage,"title","description");
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
