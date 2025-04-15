using UnityEngine;
using NUnit.Framework;
public class BossMovementAndAttacksTests
{
    private GameObject bossGO;
    private MiniBossMovement bossMovement;
    private GameObject playerGO;

    [SetUp]
    public void SetUp()
    {
        
        bossGO = new GameObject();
        bossGO.AddComponent<Rigidbody2D>();
        bossMovement = bossGO.AddComponent<MiniBossMovement>();
        bossGO.AddComponent<Animator>();


        playerGO = new GameObject();
        playerGO.AddComponent<Rigidbody2D>();
        playerGO.AddComponent<PlayerHealth>();
        playerGO.tag = "Player"; 

        
        playerGO.transform.position = new Vector2(5f, 0f);
    }
    [Test]
    public void BossStopsMoving_WhenCloseToPlayer()
    {
        
        bossGO.transform.position = new Vector2(0f, 0f);
        playerGO.transform.position = new Vector2(0.5f, 0f);

        bossMovement.TargetingPlayer();

        
        var moveDirField = typeof(MiniBossMovement).GetField("moveDir", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Vector2 moveDir = (Vector2)moveDirField.GetValue(bossMovement);

        Assert.AreEqual(Vector2.zero, moveDir);
    }

    [Test]
    public void BossFlipsTowardsPlayer_WhenChangingDirection()
    {
        
        bossGO.transform.position = new Vector2(0f, 0f);
        playerGO.transform.position = new Vector2(-5f, 0f);

        bossMovement.player = playerGO.transform;

        
        Vector3 initialScale = bossGO.transform.localScale;
        bossMovement.FlipTowardsPlayer();

        Assert.AreNotEqual(initialScale.x, bossGO.transform.localScale.x);
    }
}
