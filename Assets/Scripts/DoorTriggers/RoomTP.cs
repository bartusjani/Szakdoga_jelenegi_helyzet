using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RoomTP : MonoBehaviour
{
    public GameObject interactText;

    bool isPlayerInTrigger = false;
    public bool isResearchRoom = false;
    public bool isBossRoom = false;
    public bool isTutorialRoom = false;

    public Transform player;
    Movement playerMovement;
    Rigidbody2D playerRb;
    Animator animator;
    public Transform room;

    ScreenFading fader;
    public bool isFading = false;
    public Image faderImage;

    public float teleportCooldown = 1.5f;
    private float lastTeleportTime;

    public GroundDoorTrigger gd;
    public Trigger tr;

    bool wasCountAdded = false;
    public GameObject secondDoor;
    public GameObject firstDoor;

    private void Start()
    {
        fader = GetComponent<ScreenFading>();
        lastTeleportTime = -teleportCooldown;
        tr = FindFirstObjectByType<Trigger>();
    }
    private void Update()
    {
        if (isPlayerInTrigger)
        {
            if (isBossRoom)
            {
                if (Input.GetKeyDown(KeyCode.E) && EnemyHealth.isBossDead)
                {
                    StartCoroutine(TeleportWithFade());
                }
            }
            if (isTutorialRoom)
            {
                if (Input.GetKeyDown(KeyCode.E) && tr.isDoorOpen)
                {
                    StartCoroutine(TeleportWithFade());
                }
            }
            else if (gd!=null)
            {
                if (Input.GetKeyDown(KeyCode.E) && gd.groundDoorTrigger && CanTeleport())
                {
                    StartCoroutine(TeleportWithFade());
                }

            }
            else if (Input.GetKeyDown(KeyCode.E) && CanTeleport())
            {
                StartCoroutine(TeleportWithFade());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isBossRoom)
            {
                if (EnemyHealth.isBossDead)
                {
                    isPlayerInTrigger = true;
                    playerMovement = player.GetComponent<Movement>();
                    playerRb = player.GetComponent<Rigidbody2D>();
                    animator = player.GetComponent<Animator>();
                    interactText.SetActive(true);
                    faderImage.gameObject.SetActive(true);
                }
            }
            else
            {
                isPlayerInTrigger = true;
                playerMovement = player.GetComponent<Movement>();
                playerRb = player.GetComponent<Rigidbody2D>();
                animator = player.GetComponent<Animator>();
                interactText.SetActive(true);
                faderImage.gameObject.SetActive(true);
            }
        }
    }

    private bool hasToTurnOff = false;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            isPlayerInTrigger = false;
            isFading = false;
            interactText.SetActive(false);
            faderImage.gameObject.SetActive(false);
            if (secondDoor!=null)
            {
                hasToTurnOff = true;
            }
        }
    }

    private bool CanTeleport()
    {
        return !isFading && (Time.time > lastTeleportTime + teleportCooldown);
    }

    IEnumerator TeleportWithFade()
    {
        if (isFading) yield break;
        PopUpCounter.Instance.textIndex++;

        isFading = true;
        lastTeleportTime = Time.time;

        interactText.SetActive(false);
        FreezePlayer(true);

        fader.FadeToBlack();


        yield return new WaitUntil(() => fader.IsFadingComplete());

        player.position = room.position;

        fader.FadeToClear();
        yield return new WaitUntil(() => fader.IsFadingComplete());

        if (isPlayerInTrigger) interactText.SetActive(true);
        FreezePlayer(false);
        yield return new WaitForSeconds(1f);
        isFading = false;

        if (hasToTurnOff && secondDoor != null)
        {
            yield return new WaitForSeconds(3f);
            hasToTurnOff = false;
            secondDoor.SetActive(false);
        }
        if (firstDoor != null)
        {
            firstDoor.GetComponent<Collider2D>().enabled = false;
        }
        wasCountAdded = false;
        if (gameObject.GetComponent<AllPopupController>() != null)
        {
            gameObject.GetComponent<AllPopupController>().wasSpeaking = false;
        

            gameObject.GetComponent<AllPopupController>().ClearAllBubbles();

            yield return new WaitForSeconds(0.2f);
            gameObject.GetComponent<AllPopupController>().RefreshBubbles();
        }

    }

    void FreezePlayer(bool freeze)
    {
        if (player != null && freeze)
        {
            playerMovement.enabled = false;
            playerRb.linearVelocity = Vector2.zero;
            playerRb.bodyType = RigidbodyType2D.Kinematic;
            animator.SetFloat("xVelocity", 0f);
        }
        else
        {
            playerMovement.enabled = true;
            playerRb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
