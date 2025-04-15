using System.Collections;
using UnityEngine;

public class SecondObjAndSpeechController : MonoBehaviour
{
    public ObjectiveBubble objPrefab;
    string objMessage;


    public SpeechBubble speechPrefab;
    string speechMessage;

    TextAsset objectiveTexts;
    string objectiveText = "";
    int objCounter = 0;

    TextAsset speechTexts;
    string speechText = "";

    private PopUpBubble activeBubble;
    private ObjectiveBubble objActiveBubble;
    private SpeechBubble speechActiveBubble;

    public GameObject enemyManager;
    public GameObject enemyHealth;

    bool allScorpionDead = false;
    bool isSewer = false;
    private void Update()
    {
        if (PopUpCounter.Instance.secondTextIndex != PopUpCounter.Instance.secondLastTextIndex)
        {
            RefreshBubbles();
            PopUpCounter.Instance.secondLastTextIndex = PopUpCounter.Instance.secondTextIndex;
        }
        if (enemyManager != null)
        {
            bool allDead = enemyManager.GetComponent<GroundDoorTrigger>().allDead;
            if (allDead && !allScorpionDead)
            {
                PopUpCounter.Instance.secondTextIndex++;
                RefreshBubbles();
                StartCoroutine(SetObjAndSpeech(objectiveText, speechText));
            }
        }
        if (enemyHealth != null)
        {
            bool staticDead = enemyHealth.GetComponent<EnemyHealth>().isStaticDead;
            if (staticDead && !isSewer )
            {
                PopUpCounter.Instance.secondTextIndex++;
                RefreshBubbles();
                StartCoroutine(SetObjAndSpeech(objectiveText, speechText));
            }
        }
    }


    IEnumerator SetObjAndSpeech(string objMessage, string speechMessage)
    {
        if(enemyManager!=null) allScorpionDead = true;
        if(enemyHealth!=null) isSewer = true;
        SetObjective(objMessage);
        SetSpeech(speechMessage);
        yield return new WaitForSeconds(2f);
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
        yield return new WaitForSeconds(0.5f);
        PopUpCounter.Instance.secondTextIndex++;
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
        if (speechActiveBubble == null)
        {
            Transform parent = GameObject.Find("PlayerSpeechBubbles").transform;

            speechActiveBubble = Instantiate(speechPrefab, parent);
            speechActiveBubble.SetText(message);
        }
    }

    void ChooseTexts(int index)
    {

        objectiveTexts = Resources.Load<TextAsset>("SecondPopUpsInARoom/SecondObjectiveTexts");
        string[] objectSorok = objectiveTexts.text.Split('\n');
        objectiveText = objectSorok[index].Trim();

        speechTexts = Resources.Load<TextAsset>("SecondPopUpsInARoom/SecondSpeechTexts");
        string[] speechSorok = speechTexts.text.Split('\n');
        speechText = speechSorok[index].Trim();

        Debug.Log($"secondtextIndex: {index}");
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

        ChooseTexts(PopUpCounter.Instance.secondTextIndex);
        ClearAllBubbles();
    }
}
