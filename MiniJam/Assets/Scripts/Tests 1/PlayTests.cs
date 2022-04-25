using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayTests
{
    //a class created to simulate FrogMovement but without outside classes or need of input
    public class FakeFrogMovement
    {
        public bool isClicked;
        public float angle;
        public bool grounded;
        public bool lastGrounded;
        public int movement = 3;

        public bool isLeft;
        public bool isRight;
        public bool isForward;

        public bool gameOver = false;

        public bool lookingForInput = true;

        public GameObject gameOverScreen;

        public void FakeUpdate(Boolean click, float an, bool ground)
        {
            isLeft = false;
            isRight = false;
            isForward = false;

            grounded = ground;
            isClicked = click;
            angle = an;

            if (grounded && isClicked)
            {
                if (movement > 0)
                {

                    // Determine facing direction

                    if (angle > 100) // left
                    {
                        isLeft = true;
                        isRight = false;
                        isForward = false;
                    }
                    else if (angle < 80) // right
                    {
                        isLeft = false;
                        isRight = true;
                        isForward = false;
                    }
                    else // forward
                    {
                        isLeft = false;
                        isRight = false;
                        isForward = true;

                    }

                    movement--;
                    grounded = false;
                }
                else
                {
                    gameOver = true;
                }

            }


            if (gameOver)
            {
                gameOverScreen = Instantiate(new GameObject(), new Vector3(0, 0, 0), Quaternion.identity);
            }

            lastGrounded = grounded;
        }

        private GameObject Instantiate(GameObject gameObject, Vector3 position, Quaternion identity)
        {
            return gameObject;
        }
    }

    //Generate trash at the start, erase trash, set game over to true and check if trash generator stops generating trash
    [UnityTest]
    public IEnumerator TestForTrashGenerator()
    {
        //Set up
        GameObject trashTester = new GameObject();
        TrashGenerator trash = trashTester.AddComponent<TrashGenerator>();

        GameObject gameObject = new GameObject();


        List<Transform> points = new List<Transform>();
        points.Add(gameObject.transform);
        points.Add(gameObject.transform);
        points.Add(gameObject.transform);
        points.Add(gameObject.transform);
        points.Add(gameObject.transform);

        trash.points = points;

        List<GameObject> trashes = new List<GameObject>();
        trashes.Add(gameObject);
        trashes.Add(gameObject);
        trashes.Add(gameObject);
        trashes.Add(gameObject);

        trash.trashes = trashes;
        trash.time = 3;



        //Act
        trash.StartTrash();

        yield return new WaitForSeconds(1);

        //Asserts
        Assert.NotNull(trash.trash);

        //act
        trash.trash = null;

        trash.gameover = true;

        //assert
        Assert.Null(trash.trash);
    }

    //when player with  player tag collides with the death trigger collider game over 
    [UnityTest]
    public IEnumerator TestForDeathPlane()
    {
        //setup
        GameObject testing = new GameObject();
        DeathPlane death = testing.AddComponent<DeathPlane>();

        testing.transform.position = new Vector3(0, 0, 0);
        var tCol = testing.AddComponent(typeof(Collider2D)) as Collider2D;// refuses to cooperate
        tCol.isTrigger = true;

        GameObject a = new GameObject();
        var aCol = a.AddComponent(typeof(Collider2D)) as Collider2D;
        a.transform.position = new Vector3(10, 10, 10);
        a.tag = "A";
        GameObject player = new GameObject();
        var pCol = player.AddComponent(typeof(Collider2D)) as Collider2D;
        player.transform.position = new Vector3(-10, -10, -10);
        player.tag = "Player";

        FrogMovement notPlayer = a.AddComponent<FrogMovement>();
        FrogMovement isPlayer = player.AddComponent<FrogMovement>();

        //act
        a.transform.position = tCol.transform.position;

        yield return new WaitForSeconds(.1f);

        //assert
        Assert.False(notPlayer.gameOver);

        //act
        a.transform.position = new Vector3(10, 10, 10);

        player.transform.position = tCol.transform.position;

        yield return new WaitForSeconds(.1f);

        //assert
        Assert.True(isPlayer.gameOver);
    }

    //Testing the buttons for StartGame
    [UnityTest]
    public IEnumerator TestForStartGame()
    {
        GameObject gameObject = new GameObject();

        GameObject credits = new GameObject();
        StartGame start = gameObject.AddComponent<StartGame>();
        start.credits = credits;


        GameObject frog = new GameObject();

        start.credits = credits;
        start.frog = frog;

        //Awake is called when the script is loaded
        //this yield is meant to give unity time to setup
        yield return new WaitForSeconds(.5f);

        Assert.True(start.awakePassed);
        Assert.False(start.closePassed);
        Assert.False(start.openPassed);
        Assert.False(start.quitPassed);
        Assert.False(start.loadPassed);

        start.OpenCredits();

        //openCredits passed
        Assert.False(start.awakePassed);
        Assert.False(start.closePassed);
        Assert.True(start.openPassed);
        Assert.False(start.quitPassed);
        Assert.False(start.loadPassed);

        start.CloseCredits();

        //closeCreditsPass
        Assert.False(start.awakePassed);
        Assert.True(start.closePassed);
        Assert.False(start.openPassed);
        Assert.False(start.quitPassed);
        Assert.False(start.loadPassed);

        start.QuitGame();

        //QuitGame passed
        Assert.False(start.awakePassed);
        Assert.False(start.closePassed);
        Assert.False(start.openPassed);
        Assert.True(start.quitPassed);
        Assert.False(start.loadPassed);

        start.LoadGame();
        //LoadGame passed
        Assert.False(start.awakePassed);
        Assert.False(start.closePassed);
        Assert.False(start.openPassed);
        Assert.False(start.quitPassed);
        Assert.True(start.loadPassed);

    }

    //Testing the buttons for GameOverButtons
    [UnityTest]
    public IEnumerator TestForBGMController()
    {
        GameObject gameObject = new GameObject();
        GameOverButtons buttonMethods = gameObject.AddComponent<GameOverButtons>();

        buttonMethods.Restart();

        yield return new WaitForSeconds(.5f);


        Assert.True(buttonMethods.restartPassed);
        Assert.False(buttonMethods.quitPassed);

        buttonMethods.Quit();

        yield return new WaitForSeconds(.5f);


        Assert.False(buttonMethods.restartPassed);
        Assert.True(buttonMethods.quitPassed);
    }

    [UnityTest]
    public IEnumerator TestForAutoScrollController()
    {
        BoxCollider2D left, right, top, deathPlaneColl2D = new BoxCollider2D();
        GameObject BGMController = new GameObject();
        GameObject AmbientController = new GameObject();
        GameObject gameObject = new GameObject();
        gameObject.transform.position = new Vector3(0, 0, 0);

        AudioSource a1 = AmbientController.AddComponent<AudioSource>();
        AudioSource a2 = AmbientController.AddComponent<AudioSource>();

        BGMController.AddComponent<BGMController>();

        a1.volume = 0;
        a2.volume = .5f;


        AutoscrollController autoscroll = gameObject.AddComponent<AutoscrollController>();

        autoscroll.setIsClicked(true);

        yield return new WaitForSeconds(.1f);

        Assert.True(autoscroll.cameraActivated);
        Assert.True(a1.CompareTag("not in loop")); // used to check that it definitely did not enter the loop

        yield return new WaitForSeconds(.1f);
        Assert.True(a2.volume < .5f);// planned to assert with tag again but after seeing that the volume was lowered it felt redundant

        Assert.True(gameObject.transform.position.y > 0);//Assert that the object is moving along the y axis

        

        Assert.True(autoscroll.drawGizmosPassed);

    }

    //An attempt to test FrogMovement using a FakeFrogMovement class
    //can only go through the if statements surrounding the animations
    [UnityTest]
    public IEnumerator TestForFrogMovement()
    {


        FakeFrogMovement frog = new FakeFrogMovement();

        frog.FakeUpdate(true, 110, true);
        Assert.True(frog.isLeft);
        Assert.False(frog.isRight);
        Assert.False(frog.isForward);
         
        frog.FakeUpdate(true, 70, true);
        Assert.False(frog.isLeft);
        Assert.True(frog.isRight);
        Assert.False(frog.isForward);

        frog.FakeUpdate(true, 90, true);
        Assert.False(frog.isLeft);
        Assert.False(frog.isRight);
        Assert.True(frog.isForward);

        frog.FakeUpdate(false, 90, true);
        Assert.False(frog.isLeft);
        Assert.False(frog.isRight);
        Assert.False(frog.isForward);
        Assert.False(frog.gameOver);
        Assert.Zero(frog.movement);

        frog.FakeUpdate(true, 90, true);
        Assert.False(frog.isLeft);
        Assert.False(frog.isRight);
        Assert.False(frog.isForward);
        Assert.True(frog.gameOver);
        Assert.NotNull(frog.gameOverScreen);



        yield return null;
    }




}




