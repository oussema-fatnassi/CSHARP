using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/*
    This class is responsible for testing the PlayerController class in play mode.
    It tests the movement and animation of the player character.
*/

public class PlayerControllerPlayModeTest
{
    private GameObject playerObject;
    private PlayerController playerController;
    private Animator animator;
    // Set up the test environment by creating a new PlayerController object and adding a BoxCollider2D and Animator component to it.
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
    // Tear down the test environment by destroying the PlayerController object.
    [TearDown]
    public void Teardown()
    {
        Object.Destroy(playerObject);
    }
    // Test the player character moves to the right on input.
    [UnityTest]
    public IEnumerator PlayerMovesRightOnInput()
    {
        Vector3 initialPosition = playerObject.transform.position;

        MockInput.SetAxis("Horizontal", 1);
        MockInput.SetAxis("Vertical", 0);

        yield return new WaitForFixedUpdate();

        Assert.Greater(playerObject.transform.position.x, initialPosition.x, "Player did not move to the right.");
    }
    // Test the player character moves to the left on input.
    [UnityTest]
    public IEnumerator PlayerStartsWalkingAnimationOnInput()
    {
        MockInput.SetAxis("Horizontal", 1);
        MockInput.SetAxis("Vertical", 0);

        yield return new WaitForFixedUpdate();

        Assert.IsTrue(animator.GetBool("isWalking"), "Player is not walking despite movement input.");
    }
    // Test the player character stops moving on no input.
    [UnityTest]
    public IEnumerator PlayerStopsWalkingAnimationOnNoInput()
    {
        MockInput.SetAxis("Horizontal", 0);
        MockInput.SetAxis("Vertical", 0);

        yield return new WaitForFixedUpdate();

        Assert.IsFalse(animator.GetBool("isWalking"), "Player is walking despite no input.");
    }
}
