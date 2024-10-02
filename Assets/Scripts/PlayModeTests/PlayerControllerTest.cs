using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerControllerPlayModeTest
{
    private GameObject playerObject;
    private PlayerController playerController;
    private Animator animator;

    // Setup: Runs before every test
    [SetUp]
    public void Setup()
    {
        playerObject = new GameObject("TestPlayer");
        
        playerController = playerObject.AddComponent<PlayerController>();
        playerController.IsTesting = true;
        playerObject.AddComponent<BoxCollider2D>(); 
        animator = playerObject.AddComponent<Animator>();

        var runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Dragon");
        animator.runtimeAnimatorController = runtimeAnimatorController;
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(playerObject);
    }

    [UnityTest]
    public IEnumerator PlayerMovesRightOnInput()
    {
        Vector3 initialPosition = playerObject.transform.position;

        MockInput.SetAxis("Horizontal", 1);
        MockInput.SetAxis("Vertical", 0);

        yield return new WaitForFixedUpdate();

        Assert.Greater(playerObject.transform.position.x, initialPosition.x, "Player did not move to the right.");
    }

    [UnityTest]
    public IEnumerator PlayerStartsWalkingAnimationOnInput()
    {
        MockInput.SetAxis("Horizontal", 1);
        MockInput.SetAxis("Vertical", 0);

        yield return new WaitForFixedUpdate();

        Assert.IsTrue(animator.GetBool("isWalking"), "Player is not walking despite movement input.");
    }

    [UnityTest]
    public IEnumerator PlayerStopsWalkingAnimationOnNoInput()
    {
        MockInput.SetAxis("Horizontal", 0);
        MockInput.SetAxis("Vertical", 0);

        yield return new WaitForFixedUpdate();

        Assert.IsFalse(animator.GetBool("isWalking"), "Player is walking despite no input.");
    }
}
