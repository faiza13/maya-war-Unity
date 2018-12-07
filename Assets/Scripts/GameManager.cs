using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts;
using UnityEngine.SceneManagement;


using System.Linq;
public class GameManager : MonoBehaviour
{

    public CameraFollow cameraFollow;
    int currentBirdIndex;

    public SlingShot slingshot;
      private const int COIN_SCORE = 100;
            private const int HEALTH = 100;


    private int _score;
    private int _balls;
    private int _health;

    public GameInfoPanel  GameInfo;
    public PauseMenuPanel PauseMenu;
    public GoalMenuPanel  GoalMenuPanel ;
    public WonMenuPanel  WonMenuPanel ;
     public  LostMenuPanel LostMenuPanel;
 






    [HideInInspector]
    public static GameState CurrentGameState = GameState.Start;
    private List<GameObject> Bricks;
    private List<GameObject> Birds;
    private List<GameObject> Pigs;
  




    // Use this for initialization
    void Start()
    {


        CurrentGameState = GameState.Start;
        slingshot.enabled = false;
        //find all relevant game objects
        Bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
        Birds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bird"));
        Pigs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pig"));
        //unsubscribe and resubscribe from the event
        //this ensures that we subscribe only once
        slingshot.BirdThrown -= Slingshot_BirdThrown; slingshot.BirdThrown += Slingshot_BirdThrown;
        InteractiveCoinController.StartInteractionEvent += new InteractiveCoinController.StartInteractionDelegate(CollectCoin);
        Bird.StartInteractionEvent += new Bird.StartInteractionDelegate(ReduceBalls);
        ennemymaya.StartInteractionEvent += new ennemymaya.StartInteractionDelegate(ReduceHealth);

        

        _score = 0;
        _balls = 2;
        _health = 100;


    }
private void CollectCoin () {
            _score += COIN_SCORE;
        }
private void ReduceBalls () {
            _balls -= 1;
        }
        private void ReduceHealth () {
            _health -= 50;
        }


    // Update is called once per frame
    void Update()
    {

                    GameInfo.SetScore(_score);

                    GameInfo.SetLives(_balls);
                                        GameInfo.SetHealth(_health);



        switch (CurrentGameState)
        {
            case GameState.Start:
                //if player taps, begin animating the bird 
                //to the slingshot
                if (Input.GetMouseButtonUp(0))
                {
                    AnimateBirdToSlingshot();
                }
                break;
            case GameState.BirdMovingToSlingshot:
                //do nothing
                break;
            case GameState.Playing:
                //if we have thrown a bird
                //and either everything has stopped moving
                //or there has been 5 seconds since we threw the bird
                //animate the camera to the start position
                if (slingshot.slingshotState == SlingshotState.BirdFlying &&
                    (BricksBirdsPigsStoppedMoving() || Time.time - slingshot.TimeSinceThrown > 5f))
                {
                    slingshot.enabled = false;
                    AnimateCameraToStartPosition();
                    CurrentGameState = GameState.BirdMovingToSlingshot;
                }
                break;
            //if we have won or lost, we will restart the level
            //in a normal game, we would show the "Won" screen 
            //and on tap the user would go to the next level
            case GameState.Won:
                                          WonMenuPanel.SetScore(_score);


            WonMenuPanel.gameObject.SetActive(true);
            GameInfo.gameObject.SetActive(false);
            break;
            case GameState.Lost:
                                          LostMenuPanel.SetScore(_score);

            LostMenuPanel.gameObject.SetActive(true);
            GameInfo.gameObject.SetActive(false);
               
                break;
            default:
                break;
        }
    }



    /// <summary>
    /// A check whether all Pigs are null
    /// i.e. they have been destroyed
    /// </summary>
    /// <returns></returns>
    private bool AllPigsDestroyed()
    {
        return Pigs.All(x => x == null);
    }

    /// <summary>
    /// Animates the camera to the original location
    /// When it finishes, it checks if we have lost, won or we have other birds
    /// available to throw
    /// </summary>
    private void AnimateCameraToStartPosition()
    {
Animator	animator = GameObject.FindGameObjectWithTag("maya").GetComponent<Animator> ();
	    	animator.SetInteger ("state",0);
        float duration = Vector2.Distance(Camera.main.transform.position, cameraFollow.StartingPosition) / 10f;
        if (duration == 0.0f) duration = 0.1f;
        //animate the camera to start
        Camera.main.transform.positionTo
            (duration,
            cameraFollow.StartingPosition). //end position
            setOnCompleteHandler((x) =>
                        {
                            cameraFollow.IsFollowing = false;
                            if (_health==0)
                            {
                                CurrentGameState = GameState.Won;
                            }
                            //animate the next bird, if available
                            else if (currentBirdIndex == Birds.Count - 1 )
                            {
                                //no more birds, go to finished
                                if (_score >= 300) {

           
                              Time.timeScale = 0;
                              GoalMenuPanel.SetScore(_score);
                              GoalMenuPanel.gameObject.SetActive(true);

                              GameInfo.gameObject.SetActive(false);

                                 


                                }
                                else{
                                CurrentGameState = GameState.Lost; }
                            }
                            else
                            {
                                slingshot.slingshotState = SlingshotState.Idle;
                                //bird to throw is the next on the list
                                currentBirdIndex++;
                                AnimateBirdToSlingshot();
                            }
                        });
    }

    /// <summary>
    /// Animates the bird from the waiting position to the slingshot
    /// </summary>
    void AnimateBirdToSlingshot()
    {
    	Animator	animator = GameObject.FindGameObjectWithTag("ennemi").GetComponent<Animator> ();
					animator.SetInteger ("state",0);
        CurrentGameState = GameState.BirdMovingToSlingshot;
        Birds[currentBirdIndex].transform.positionTo
            (Vector2.Distance(Birds[currentBirdIndex].transform.position / 10,
            slingshot.BirdWaitPosition.transform.position) / 10, //duration
            slingshot.BirdWaitPosition.transform.position). //final position
                setOnCompleteHandler((x) =>
                        {
                            x.complete();
                            x.destroy(); //destroy the animation
                            CurrentGameState = GameState.Playing;
                            slingshot.enabled = true; //enable slingshot
                            //current bird is the current in the list
                            slingshot.BirdToThrow = Birds[currentBirdIndex];
                        });
    }

    /// <summary>
    /// Event handler, when the bird is thrown, camera starts following it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Slingshot_BirdThrown(object sender, System.EventArgs e)
    {
        cameraFollow.BirdToFollow = Birds[currentBirdIndex].transform;
        cameraFollow.IsFollowing = true;
    }

    /// <summary>
    /// Check if all birds, pigs and bricks have stopped moving
    /// </summary>
    /// <returns></returns>
    bool BricksBirdsPigsStoppedMoving()
    {
        foreach (var item in Bricks.Union(Birds).Union(Pigs))
        {
            if (item != null && item.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > Constants.MinVelocity)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Found here
    /// http://www.bensilvis.com/?p=500
    /// </summary>
    /// <param name="screenWidth"></param>
    /// <param name="screenHeight"></param>
    public static void AutoResize(int screenWidth, int screenHeight)
    {
        Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
    }

    /// <summary>
    /// Shows relevant GUI depending on the current game state
    /// </summary>

    public void PauseButtonOnClick() {
            Debug.Log("PauseButtonOnClick called...");
            //_gameState = GameState.Paused;
            Time.timeScale = 0;

            PauseMenu.gameObject.SetActive(true);
            GameInfo.gameObject.SetActive(false);
            //VirtualInput.gameObject.SetActive(false);
        }
         public void PlayButtonOnClick() {
            Debug.Log("PlayButtonOnClick called...");
            //_gameState = GameState.Paused;
            Time.timeScale = 1;
            PauseMenu.gameObject.SetActive(false);
            GameInfo.gameObject.SetActive(true);
            //VirtualInput.gameObject.SetActive(false);
        }
        public void QuitButtonOnClick() {
            Debug.Log("QuitButtonOnClick called...");
            Time.timeScale = 1;
            SceneManager.LoadScene ("Title"); 

        }
        public void ReloadLevelButtonOnClick() {
            Time.timeScale = 1;

            Debug.Log("reloadButtonOnClick called...");
            SceneManager.LoadScene ("game3"); 

                               


        }
          public void ReloadLevel4ButtonOnClick() {
            Time.timeScale = 1;

            Debug.Log("reloadButtonOnClick called...");
            SceneManager.LoadScene ("game4"); 

                               


        }
         public void ExtraBallButtonOnClick() {
            

                                _score -= 300;
                                 _balls += 1;
                                 GameObject sphere = (GameObject)Instantiate(Resources.Load("Sphere")) as GameObject;
                                 sphere.transform.position = new Vector3(-9,-1,1);
                                 slingshot.slingshotState = SlingshotState.Idle;


                                //bird to throw is the next on the list
                                Birds.Add(sphere);
                                currentBirdIndex++;
                                AnimateBirdToSlingshot();
                                 Time.timeScale = 1;
            GoalMenuPanel.gameObject.SetActive(false);
            GameInfo.gameObject.SetActive(true);
                            }

    void OnGUI()
    {
        AutoResize(800, 480);
        switch (CurrentGameState)
        {
            case GameState.Start:
                GUI.Label(new Rect(0, 150, 200, 100), "Tap the screen to start");
                break;
            case GameState.Won:

                GUI.Label(new Rect(0, 150, 200, 100), "You won! Tap the screen to restart");
                break;
            case GameState.Lost:
                GUI.Label(new Rect(0, 150, 200, 100), "You lost! Tap the screen to restart");
                break;
            default:
                break;
        }
    }


}
