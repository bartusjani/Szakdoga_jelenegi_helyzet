using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;
using TextAsset = UnityEngine.TextAsset;

public class AllSecondPopupController : MonoBehaviour
{

    public PopUpBubble popPrefab;
    string popMessage;


    public ObjectiveBubble objPrefab;
    string objMessage;


    public SpeechBubble speechPrefab;
    string speechMessage;


    private PopUpBubble activeBubble;
    private ObjectiveBubble objActiveBubble;
    private SpeechBubble speechActiveBubble;

    public GameObject platformerHelp;
    int helpCounter = 0;

    TextAsset popUpTexts;
    string popUpText = "";

    TextAsset objectiveTexts;
    string objectiveText = "";
    int objCounter = 0;

    TextAsset speechTexts;
    string speechText = ""; 


    public Trigger tr;
    private bool isObjandSpeech=true;
    bool isPlayerInTrigger = false;
    public bool wasSpeaking = false;

    bool isWaypointPointed = false;
    public WayPointUI wp;
    public Transform groundDoorTarget;

    public int popUpIndex;
    public int objIndex;
    public int speechIndex;

    private void Update()
    {
        if (PopUpCounter.Instance.secondTextIndex != PopUpCounter.Instance.secondLastTextIndex)
        {
            RefreshBubbles();
            PopUpCounter.Instance.secondLastTextIndex = PopUpCounter.Instance.secondTextIndex;
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tr != null)
        {

            if (collision.CompareTag("Player") && !tr.isDoorOpen)
            {

                isPlayerInTrigger = true;
                if (!wasSpeaking)
                {
                    ChooseTexts(objIndex,speechIndex,popUpIndex);
                    StartCoroutine(SetPopUp(popUpText,objectiveText, speechText));
                }
                else
                {
                    if (activeBubble == null && objActiveBubble == null)
                    {
                        ChooseTexts(objIndex, speechIndex, popUpIndex);
                        if (objCounter == 1)
                        {
                            SetPopUp(popUpText);
                            objCounter = 0;
                        }
                        else
                        {
                            StartCoroutine(SetPopUp(popUpText, objectiveText));
                        }
                    }
                    wasSpeaking = false;
                }
            }
        }
        
        if (collision.CompareTag("Player"))
        {

            isPlayerInTrigger = true;
            if (!wasSpeaking)
            {
                wp.SetTarget(groundDoorTarget);
                isWaypointPointed = true;
                ChooseTexts(objIndex, speechIndex, popUpIndex);
                StartCoroutine(SetPopUp(popUpText, objectiveText, speechText));
            }
            else
            {
                if (activeBubble == null && objActiveBubble == null)
                {
                    
                    ChooseTexts(objIndex, speechIndex, popUpIndex);
                    StartCoroutine(SetPopUp(popUpText, objectiveText));
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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

        }
    }
    IEnumerator SetPopUp(string message ,string objMessage)
    {

        if (activeBubble == null)
        {
            SetPopUp(message);
            yield return new WaitForSeconds(1f);
            SetObjective(objMessage);


        }
    }

    IEnumerator SetPopUp(string message, string objMessage, string speechMessage)
    {
        
        if (activeBubble == null)
        {
            objCounter = 1;
            SetPopUp(message);
            yield return new WaitForSeconds(0.2f);
            SetSpeech(speechMessage);

            yield return new WaitForSeconds(0.2f);
            SetObjective(objMessage);
            wasSpeaking = true;
            PopUpCounter.Instance.secondTextIndex++;
        }
    }

    void SetPopUp(string message)
    {

        if (activeBubble == null)
        {
            Transform parent = GameObject.Find("PopUps").transform;

            activeBubble = Instantiate(popPrefab, parent);
            activeBubble.SetText(message);
        }
    }
    
    public void SetObjective(string message)
    {
        if (objActiveBubble != null)
        {
            Destroy(objActiveBubble.gameObject);
            objActiveBubble = null;
        }
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
            wasSpeaking = true;
        }
    }

    void ChooseTexts(int objIndex, int speechIndex, int popupIndex)
    {

        objectiveTexts = Resources.Load<TextAsset>("SecondPopUpsInARoom/SecondObjectiveTexts");
        string[] objectSorok = objectiveTexts.text.Split('\n');
        objectiveText = objectSorok[objIndex].Trim();

        speechTexts = Resources.Load<TextAsset>("SecondPopUpsInARoom/SecondSpeechTexts");
        string[] speechSorok = speechTexts.text.Split('\n');
        speechText = speechSorok[speechIndex].Trim();

        popUpTexts = Resources.Load<TextAsset>("SecondPopUpsInARoom/SecondPopUpTexts");
        string[] popUpSorok = popUpTexts.text.Split('\n');
        popUpText = popUpSorok[popupIndex].Trim();


        Debug.Log($"secondPopUpText: {popUpText}");
        Debug.Log($"secondObjectiveText: {objectiveText}");
        Debug.Log($"secondSpeechText: {speechText}");
    }

    public void ClearAllBubbles()
    {
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
    }

    public void RefreshBubbles()
    {
        Debug.Log($"RefreshBubbles called for textIndex: {PopUpCounter.Instance.secondTextIndex}");

        ChooseTexts(objIndex, speechIndex, popUpIndex);
        ClearAllBubbles();
    }
}
