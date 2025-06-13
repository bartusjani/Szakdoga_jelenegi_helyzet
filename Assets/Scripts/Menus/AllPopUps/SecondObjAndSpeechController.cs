using System.Collections;
using UnityEngine;

public class SecondObjAndSpeechController : MonoBehaviour
{
    public ObjectiveBubble objPrefab;
    string objMessage;

    [SerializeField] GameObject enemyMusic;

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
    bool isBossRoom = false;

    ItemAdder adder;
    public InventoryPage inv;
    public Sprite itemImg;
    public string itemTitle = "";
    public string itemDesc = "";

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
        if (enemyManager != null)
        {
            bool allDead = enemyManager.GetComponent<GroundDoorTrigger>().allDead;
            if (allDead && !allScorpionDead)
            {
                enemyMusic.SetActive(false);
                PopUpCounter.Instance.secondTextIndex++;

                adder = GetComponent<ItemAdder>();
                adder.AddItemToInv(inv,itemImg,itemTitle,itemDesc);

                RefreshBubbles();
                StartCoroutine(SetObjAndSpeech(objectiveText, speechText));
            }
        }
        if (enemyHealth != null)
        {
            bool staticDead = enemyHealth.GetComponent<EnemyHealth>().isStaticDead;
            if (staticDead && !isSewer )
            {
                enemyMusic.SetActive(false);
                PopUpCounter.Instance.secondTextIndex++;
                if(inv!= null && itemImg!=null &&itemTitle!=null && itemDesc != null)
                {
                    adder = GetComponent<ItemAdder>();
                    adder.AddItemToInv(inv, itemImg, itemTitle, itemDesc);
                }

                RefreshBubbles();
                StartCoroutine(SetObjAndSpeech(objectiveText, speechText));
            }
            if (enemyHealth.GetComponent<EnemyHealth>().GetBossDeath() && !isBossRoom)
            {
                enemyMusic.SetActive(false);
                Debug.Log("OBJEKTÍV INDEX HALO" + objIndex + "SPEECH" + speechIndex);
                PopUpCounter.Instance.secondTextIndex++;

                if (inv != null && itemImg != null && itemTitle != null && itemDesc != null)
                {
                    adder = GetComponent<ItemAdder>();
                    adder.AddItemToInv(inv, itemImg, itemTitle, itemDesc);
                }

                RefreshBubbles();
                StartCoroutine(SetObjAndSpeech(objectiveText, speechText));
            }
        }
    }


    IEnumerator SetObjAndSpeech(string objMessage, string speechMessage)
    {
        if(enemyManager!=null) allScorpionDead = true;
        if(enemyHealth!=null) isSewer = true;
        if (EnemyHealth.isBossDead) isBossRoom = true;
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

    void ChooseTexts(int objectiveIndex,int speechIndex)
    {

        objectiveTexts = Resources.Load<TextAsset>("SecondPopUpsInARoom/SecondObjectiveTexts");
        string[] objectSorok = objectiveTexts.text.Split('\n');
        objectiveText = objectSorok[objectiveIndex].Trim();

        speechTexts = Resources.Load<TextAsset>("SecondPopUpsInARoom/SecondSpeechTexts");
        string[] speechSorok = speechTexts.text.Split('\n');
        speechText = speechSorok[speechIndex].Trim();

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

        ChooseTexts(objIndex,speechIndex);
        ClearAllBubbles();
    }
}
