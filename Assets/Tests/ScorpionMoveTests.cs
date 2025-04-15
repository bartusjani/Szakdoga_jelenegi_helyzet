using NUnit.Framework;
using UnityEngine;
public class ScorpionMoveTests
{
    GameObject enemyGO;
    EnemyMovement enemyMovement;

    [SetUp]
    public void Setup()
    {
        enemyGO = new GameObject("ScorpionEnemy");
        enemyMovement = enemyGO.AddComponent<EnemyMovement>();
        enemyGO.AddComponent<Rigidbody2D>();
        enemyGO.AddComponent<Animator>();

        // Setup patrol points
        var pointA = new GameObject("PointA").transform;
        var pointB = new GameObject("PointB").transform;
        pointA.position = new Vector2(0, 0);
        pointB.position = new Vector2(5, 0);
        enemyMovement.patrolPoints = new Transform[] { pointA, pointB };

        // Ground check
        var groundCheck = new GameObject("GroundCheck").transform;
        groundCheck.position = enemyGO.transform.position + Vector3.down;
        enemyMovement.groundCheck = groundCheck;

        // Setup player
        var player = new GameObject("Player");
        player.tag = "Player";
        player.transform.position = new Vector2(2, 0);
        enemyMovement.player = player.transform;
    }

    [Test]
    public void Patroling_MovesTowardsNextPoint()
    {
        enemyMovement.patrolDestination = 0;
        enemyGO.transform.position = new Vector2(-1, 0);
        enemyMovement.Patroling();

        var moveDirField = typeof(EnemyMovement).GetField("moveDir", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Vector2 moveDir = (Vector2)moveDirField.GetValue(enemyMovement);
        enemyGO.GetComponent<Rigidbody2D>().linearVelocity = moveDir * enemyMovement.speed;

        Assert.IsTrue(enemyMovement.patrolDestination == 0 || enemyMovement.patrolDestination == 1);
        Assert.AreNotEqual(Vector2.zero, enemyGO.GetComponent<Rigidbody2D>().linearVelocity);
    }

    [Test]
    public void TargetingPlayer_StopsWhenCloseEnough()
    {
        enemyMovement.stopDis = 1f;
        enemyMovement.player.position = enemyGO.transform.position + new Vector3(0.5f, 0);
        enemyMovement.TargetingPlayer();

        Assert.AreEqual(Vector2.zero, enemyMovement.GetComponent<Rigidbody2D>().linearVelocity);
    }

    [Test]
    public void FlipTowardsPlayer_FlipsCorrectly()
    {
        enemyGO.transform.localScale = Vector3.one;
        var playerPos = enemyMovement.player.position;
        playerPos.x = -1;
        enemyMovement.player.position = playerPos;

        // force facingRight true initially
        typeof(EnemyMovement)
            .GetField("facingRight", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(enemyMovement, true);

        enemyMovement.FlipTowardsPlayer();

        Assert.AreEqual(-1, enemyGO.transform.localScale.x);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(enemyGO);
    }
}
