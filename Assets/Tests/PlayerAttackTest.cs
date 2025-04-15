using UnityEngine;
using NUnit.Framework;
using System.Reflection;

public class PlayerAttackTest
{
    private GameObject playerGO;
    private PlayerAttack playerAttack;

    [SetUp]
    public void Setup()
    {
        playerGO = new GameObject("Player");
        playerAttack = playerGO.AddComponent<PlayerAttack>();

        playerAttack.attackPoint = new GameObject("AttackPoint").transform;
        playerAttack.jumpAttackPoint = new GameObject("JumpAttackPoint").transform;
        playerAttack.areaAttackPoint = new GameObject("AreaAttackPoint").transform;

        playerAttack.shield = new GameObject("Shield");
        playerAttack.shield.SetActive(false);

        playerAttack.movement = playerGO.AddComponent<Movement>();
        playerAttack.movement.rb = playerGO.AddComponent<Rigidbody2D>();

        playerGO.AddComponent<Animator>();

        playerAttack.enemyLayers = LayerMask.GetMask("Enemy");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerGO);
    }

    [Test]
    public void ComboStep_IncrementsCorrectly()
    {
        
        var go = new GameObject();
        var animator = go.AddComponent<Animator>();
        playerAttack = go.AddComponent<PlayerAttack>();

        
        playerAttack.attackPoint = new GameObject("AttackPoint").transform;
        playerAttack.enemyLayers = LayerMask.GetMask("Enemy");
        playerAttack.damage = 10;
        playerAttack.attackRange = 1f;

        
        typeof(PlayerAttack)
            .GetField("animator", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(playerAttack, animator);

        
        typeof(PlayerAttack)
            .GetField("LastAttackTime", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(playerAttack, Time.time - 0.5f);

        
        playerAttack.comboStep = 0;
        playerAttack.SendMessage("HandleCombo");

        Assert.AreEqual(1, playerAttack.comboStep);

        playerAttack.SendMessage("HandleCombo");

        Assert.AreEqual(2, playerAttack.comboStep);
    }
    
    public class EnemyHealthMock : MonoBehaviour
    {
        public bool tookDamage = false;
        public int receivedAmount = 0;

        public void TakeDamage(int amount)
        {
            tookDamage = true;
            receivedAmount = amount;
        }
    }

    [Test]
    public void BlockingBullet_SetsBlockingState()
    {
        GameObject bullet = new GameObject("Bullet");
        bullet.tag = "Bullet";
        var collider = bullet.AddComponent<BoxCollider2D>();

        playerAttack.isBlocking = true;

        playerAttack.OnTriggerEnter2D(collider);

        Assert.IsTrue(playerAttack.isBlocking);
    }
}
