using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorialTrigger : MonoBehaviour
{
    public GameObject combatHelp;
    public GameObject blockHelp;
    public GameObject platformerHelp;
    public List<GameObject> tutorialSteps;
    public List<GameObject> combatSteps;
    public List<GameObject> healthSteps;
    PlayerHealth player;

    int currentStep = 0;
    int helpcounter = 0;
    bool healthHelp = false;

    private void Start()
    {
        player = GetComponent<PlayerHealth>();
    }
    void Update()
    {
        
        if (!healthHelp && player.GetHealth()< 100)
        {
            Time.timeScale = 0;
            ShowStep(0);
            healthHelp = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlatformCollider") && helpcounter<1)
        {
            //platform
            helpcounter++;
            platformerHelp.SetActive(true);
            Time.timeScale = 0;
        }
        else if (collision.CompareTag("SecondDoor") && helpcounter < 2)
        {
            //combat
            helpcounter++;
            combatHelp.SetActive(true);
            Time.timeScale = 0;
        }
        else if (collision.CompareTag("BlockCollider") && helpcounter < 3)
        {
            //block
            helpcounter++;
            blockHelp.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private void ShowStep(int index)
    {
        currentStep = index;
        for (int i = 0; i < healthSteps.Count; i++)
        {
            healthSteps[i].SetActive(i == index);
        }
    }

    public void NextTutorialStep()
    {
        if (currentStep < tutorialSteps.Count - 1)
        {
            tutorialSteps[currentStep].SetActive(false);
            currentStep++;
            tutorialSteps[currentStep].SetActive(true);
        }
    }

    public void NextCombatStep()
    {
        if (currentStep < combatSteps.Count - 1)
        {
            combatSteps[currentStep].SetActive(false);
            currentStep++;
            combatSteps[currentStep].SetActive(true);
        }
    }
    public void NextHealthStep()
    {
        if (currentStep < healthSteps.Count - 1)
        {
            healthSteps[currentStep].SetActive(false);
            currentStep++;
            healthSteps[currentStep].SetActive(true);
        }
    }

    public void CloseTutorial(GameObject gameObject)
    {

        gameObject.SetActive(false);
        currentStep = 0;
        Time.timeScale = 1;


    }
}
