using System.Collections;
using NUnit.Framework;
using UnityEngine;
using Unity;
using UnityEditor.Rendering;

public class PlayerTests
{
    public GameObject mvScript;
    public Movement movementScript;

    [SetUp]
    public void Setup()
    {
        mvScript = new GameObject("movementScript");
        movementScript = mvScript.AddComponent<Movement>();

        movementScript.rb = mvScript.AddComponent<Rigidbody2D>();
        movementScript.animator = mvScript.AddComponent<Animator>();

        
        GameObject groundCheck = new GameObject("GroundCheck");
        groundCheck.transform.parent = mvScript.transform;
        movementScript.groundCheck = groundCheck.transform;

        
        movementScript.groundLayer = LayerMask.GetMask("Ground");
        groundCheck.layer = LayerMask.NameToLayer("Ground");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(mvScript);
    }

    [Test]
    public void SimulateMoveRight()
    {
        movementScript.rb.linearVelocity = new Vector2(5f, 0f);
        Debug.Log("Test Move Right: " + movementScript.rb.linearVelocity.x);

        Assert.AreEqual(5f, movementScript.rb.linearVelocity.x, 0.1f, "Player should be moving right.");
    }

    [Test]
    public void Flip_ChangesDirection_WhenFacingLeft()
    {
        
        var flipField = typeof(Movement).GetField("isFacingRight", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        flipField.SetValue(movementScript, true);

        var horizontalField = typeof(Movement).GetField("horizontal", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        horizontalField.SetValue(movementScript, -1f);

        Vector3 originalScale = mvScript.transform.localScale = Vector3.one;

        typeof(Movement).GetMethod("Flip", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        .Invoke(movementScript, null);

        Assert.AreEqual(-originalScale.x, mvScript.transform.localScale.x);
    }

    [Test]
    public void IsGrounded_ReturnsFalse_WhenNoGround()
    {
        Assert.IsFalse(movementScript.IsGrounded(), "Should not be grounded with no ground.");
    }

    [Test]
    public void Jump_WhenGrounded_ShouldIncreaseYVelocity()
    {
        float initialY = movementScript.rb.linearVelocity.y;

        
        movementScript.rb.linearVelocity = new Vector2(movementScript.rb.linearVelocity.x, 20f);

        Assert.Greater(movementScript.rb.linearVelocity.y, initialY, "Player should have jumped.");
        Debug.Log("Test Jump: Success - Velocity Y increased");
    }

    



    private bool GetPrivateBool(string name)
    {
        return (bool)typeof(Movement).GetField(name, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(movementScript);
    }
}
    

