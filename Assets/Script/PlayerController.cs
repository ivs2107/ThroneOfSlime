using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private bool isPressingKey = false;
    private float horizontalInput;
    private float verticalInput;
    public float moveDuration;
    public Ease moveEase;
    public CameraShake cameraShake;
    //public float loadLevelDelay;
    //public Sprite changeToSprite;
    // public float changeSpritePerc;
    public bool moving;
    //public ParticleSystem particleSystem;
    public GameObject playerSprite;

    private int nombrePlayers;

    public string colorPlayer;

    public string item = "";

    private string nb_str;



    //fdrfdfgdfdf


    public const float MAX_SWIPE_TIME = 0.5f;

    // Factor of the screen width that we consider a swipe
    // 0.17 works well for portrait mode 16:9 phone
    public const float MIN_SWIPE_DISTANCE = 0.17f;

    public static bool swipedRight = false;
    public static bool swipedLeft = false;
    public static bool swipedUp = false;
    public static bool swipedDown = false;

    public bool debugWithArrowKeys = true;

    Vector2 startPos;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        swipedRight = false;
        swipedLeft = false;
        swipedUp = false;
        swipedDown = false;

        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                startPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);
                startTime = Time.time;
            }
            if (t.phase == TouchPhase.Ended)
            {
                if (Time.time - startTime > MAX_SWIPE_TIME) // press too long
                    return;

                Vector2 endPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);

                Vector2 swipe = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);

                if (swipe.magnitude < MIN_SWIPE_DISTANCE) // Too short swipe
                    return;

                if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                { // Horizontal swipe
                    if (swipe.x > 0)
                    {
                        swipedRight = true;
                        horizontalInput = 1;
                    }
                    else
                    {
                        swipedLeft = true;
                        horizontalInput = -1;
                    }
                }
                else
                { // Vertical swipe
                    if (swipe.y > 0)
                    {
                        verticalInput = 1;
                        swipedUp = true;
                    }
                    else
                    {
                        verticalInput = -1;
                        swipedDown = true;
                    }
                }
            }
        }

        /* if (debugWithArrowKeys)
         {

             swipedDown = swipedDown || Input.GetKeyDown(KeyCode.DownArrow);
             swipedUp = swipedUp || Input.GetKeyDown(KeyCode.UpArrow);
             swipedRight = swipedRight || Input.GetKeyDown(KeyCode.RightArrow);
             swipedLeft = swipedLeft || Input.GetKeyDown(KeyCode.LeftArrow);
         }*/






        //ferfsdrfe



        /* if (Input.GetKey("d"))
         {
             moving = true;
             MoveRight();
         }*/

        if (horizontalInput > 0 && !moving && !isPressingKey)
        {
            isPressingKey = true;
            moving = true;
            MoveRight();
        }

        if (horizontalInput < 0 && !moving && !isPressingKey)
        {
            isPressingKey = true;
            moving = true;
            MoveLeft();
        }

        if (verticalInput > 0 && !moving && !isPressingKey)
        {
            isPressingKey = true;
            moving = true;
            MoveUp();
        }

        if (verticalInput < 0 && !moving && !isPressingKey)
        {
            isPressingKey = true;
            moving = true;
            MoveDown();
        }
        if (horizontalInput == 0 && verticalInput == 0 && isPressingKey)
        {
            isPressingKey = false;
        }
    }

    public void MoveRight()
    {

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().UpdateWall();
        Vector3 vector3 = new Vector3(this.transform.position.x + 0.6f, this.transform.position.y, this.transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(vector3, Vector2.right, 20f);

        /*if(Mathf.Abs(this.transform.localPosition.x)- Mathf.Abs(hit.point.x)<= 0.5f)
        {
            notMoving();
            return;
        }*/

        if (hit.transform.gameObject.tag == "Wall")
        {
            if (vector3.x == hit.point.x)
            {
                PlayerNotMove();
                return;
            }
           /* GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().UpdateWall();
            vector3 = new Vector3(this.transform.position.x + 0.6f, this.transform.position.y, this.transform.position.z);
            hit = Physics2D.Raycast(vector3, Vector2.right, 20f);*/
            float move = hit.point.x - 0.5f;
            this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(notMoving);
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Pique")
        {
            float move = hit.point.x - 0.3f;
            this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(Kill);
            Debug.Log(move);

        }
        if (hit.transform.gameObject.tag == "Substance")
        {
            float move = 0;
            if (vector3.x == hit.point.x)
            {
                move = hit.point.x + 0.4f;
            }
            else
            {
                move = hit.point.x + 0.5f;
            }
            Debug.Log("substance" + hit.transform.name);
            //float move = hit.point.x + 0.5f;
            this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(notMoving);
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "End")
        {
            float move = hit.point.x + 0.5f;
            this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(Fini);

            Debug.Log(move);
        }

        if (hit.transform.gameObject.tag == "Color_Green" && colorPlayer != "Green")
        {
            if (vector3.x == hit.point.x)
            {
                PlayerNotMove();
                return;
            }
            float move = hit.point.x - 0.5f;
            this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(notMoving);
            Debug.Log(move);
        }
        else if (hit.transform.gameObject.tag == "Color_Green" && colorPlayer == "Green")
        {
            hit.transform.gameObject.layer = 2;
            MoveRight();
            hit.transform.gameObject.layer = 0;
        }
        if (hit.transform.gameObject.tag == "BreakWall")
        {
            if (vector3.x == hit.point.x)
            {
                PlayerNotMove();
                return;
            }
            float move = hit.point.x - 0.5f;
            this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(() => DestroyWall(hit));
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Cle")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().UpdateWall();
            float move = hit.point.x + 0.5f;
            item = GetTileBase(hit).name.Split('_')[1];
            hit.transform.gameObject.layer = 2;
            this.playerSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Slimekey");
            DestroyWall(hit);
            MoveRight();
            hit.transform.gameObject.layer = 0;
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Porte")
        {
            float move = hit.point.x - 0.5f;
            string porteColor = GetTileBase(hit).name.Split('_')[1];
            if (item == porteColor)
            {
                if (vector3.x == hit.point.x)
                {
                    DestroyDoor(hit);
                    return;
                }
                else
                {
                    this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(() => DestroyDoor(hit));

                }
                item = null;

            }
            else
            {
                if (vector3.x == hit.point.x)
                {
                    PlayerNotMove();
                    return;
                }
                this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(notMoving);
            }
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Portal")
        {
            float move = hit.point.x + 0.5f;
            Debug.Log(hit.transform.parent.name);
            if (hit.transform.gameObject.name == "Portal_Red")
            {
                this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(() => Portal(hit, "Blue"));
            }
            else
            {
                this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(() => Portal(hit, "Red"));
            }
        }

    }

    public void MoveLeft()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().UpdateWall();
        Vector3 vector3 = new Vector3(this.transform.position.x - 0.6f, this.transform.position.y, this.transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(vector3, Vector2.left, 20f);

        if (hit.transform.gameObject.tag == "Wall")
        {
            if (vector3.x == hit.point.x)
            {
                PlayerNotMove();
                return;
            }
         /*   GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().UpdateWall();
            vector3 = new Vector3(this.transform.position.x - 0.6f, this.transform.position.y, this.transform.position.z);
            hit = Physics2D.Raycast(vector3, Vector2.left, 20f);*/
            float move = hit.point.x + 0.5f;
            this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(notMoving);
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Pique")
        {
        /*    GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().UpdateWall();
            vector3 = new Vector3(this.transform.position.x - 0.6f, this.transform.position.y, this.transform.position.z);
            hit = Physics2D.Raycast(vector3, Vector2.left, 20f);*/
            float move = hit.point.x + 0.3f;
            this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(Kill);
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Substance")
        {
          /*  GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().UpdateWall();
            vector3 = new Vector3(this.transform.position.x - 0.6f, this.transform.position.y, this.transform.position.z);
            hit = Physics2D.Raycast(vector3, Vector2.left, 20f);*/
            float move = 0;
            if (vector3.x == hit.point.x)
            {
                move = hit.point.x - 0.4f;
            }
            else
            {
                move = hit.point.x - 0.5f;
            }
            Debug.Log("substance" + hit.transform.name);
            //float move = hit.point.x - 0.5f;
            this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(notMoving);
            Debug.Log(move);
        }

        if (hit.transform.gameObject.tag == "End")
        {

            float move = hit.point.x - 0.5f;
            this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(Fini);
            Debug.Log(move);
        }

        if (hit.transform.gameObject.tag == "Color_Green" && colorPlayer != "Green")
        {
            if (vector3.x == hit.point.x)
            {
                PlayerNotMove();
                return;
            }
            float move = hit.point.x + 0.5f;
            this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(notMoving);
            Debug.Log(move);
        } else if (hit.transform.gameObject.tag == "Color_Green" && colorPlayer == "Green")
        {
            hit.transform.gameObject.layer = 2;
            MoveLeft();
            hit.transform.gameObject.layer = 0;
        }
        if (hit.transform.gameObject.tag == "BreakWall")
        {
            if (vector3.x == hit.point.x)
            {
                PlayerNotMove();
                return;
            }
            float move = hit.point.x + 0.5f;
            hit.point = new Vector2(hit.point.x - 1, hit.point.y);
            this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(() => DestroyWall(hit));
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Cle")
        {
            float move = hit.point.x - 0.5f;
            item = GetTileBase(hit).name.Split('_')[1];
            hit.transform.gameObject.layer = 2;
            this.playerSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Slimekey");
            DestroyWall(hit);
            MoveLeft();
            hit.transform.gameObject.layer = 0;
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Porte")
        {
            float move = hit.point.x + 0.5f;
            string porteColor = GetTileBase(hit).name.Split('_')[1];
            if (item == porteColor)
            {
                if (vector3.x == hit.point.x)
                {
                    DestroyDoor(hit);
                    return;
                }
                else
                {
                    this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(() => DestroyDoor(hit));

                }
                item = null;

            }
            else
            {
                if (vector3.x == hit.point.x)
                {
                    PlayerNotMove();
                    return;
                }
                this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(notMoving);
            }
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Portal")
        {
            float move = hit.point.x - 0.5f;
            Debug.Log(hit.transform.parent.name);
            if (hit.transform.gameObject.name == "Portal_Red")
            {
                this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(() => Portal(hit, "Blue"));
            }
            else
            {
                this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(() => Portal(hit, "Red"));
            }
        }
    }

    public void MoveUp()
    {
        //Vector3
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().UpdateWall();
        Vector3 vector3 = new Vector3(this.transform.position.x, this.transform.position.y + 0.6f, this.transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(vector3, Vector2.up, 20f);
        if (hit.transform.gameObject.tag == "Wall")
        {
            if (vector3.y == hit.point.y)
            {
                PlayerNotMove();
                return;
            }
          /*  GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().UpdateWall();
            vector3 = new Vector3(this.transform.position.x, this.transform.position.y + 0.6f, this.transform.position.z);
            hit = Physics2D.Raycast(vector3, Vector2.up, 20f);*/
            float move = hit.point.y - 0.5f;
            this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(notMoving);
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Pique")
        {
            Debug.Log("substance" + hit.transform.name);
            float move = hit.point.y - 0.3f;
            this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(Kill);
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Substance")
        {
            float move = 0;
            if (vector3.y == hit.point.y)
            {
                move = hit.point.y + 0.4f;
            }
            else
            {
                move = hit.point.y + 0.5f;
            }
            Debug.Log("substance" + hit.transform.name);
            //float move = hit.point.y + 0.5f;
            this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(notMoving);
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "End")
        {
            float move = hit.point.y + 0.5f;
            this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(Fini);
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Color_Green" && colorPlayer != "Green")
        {
            if (vector3.y == hit.point.y)
            {
                PlayerNotMove();
                return;
            }
            float move = hit.point.y - 0.5f;
            this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(notMoving);
            Debug.Log(move);
        }
        else if (hit.transform.gameObject.tag == "Color_Green" && colorPlayer == "Green")
        {
            hit.transform.gameObject.layer = 2;
            MoveUp();
            hit.transform.gameObject.layer = 0;
        }
        if (hit.transform.gameObject.tag == "BreakWall")
        {
            if (vector3.y == hit.point.y)
            {
                PlayerNotMove();
                return;
            }
            float move = hit.point.y - 0.5f;
            hit.point = new Vector2(hit.point.x, hit.point.y + 1);
            this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(() => DestroyWall(hit));
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Cle")
        {
            float move = hit.point.y + 0.5f;
            item = GetTileBase(hit).name.Split('_')[1];
            hit.transform.gameObject.layer = 2;
            this.playerSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Slimekey");
            DestroyWall(hit);
            MoveUp();
            hit.transform.gameObject.layer = 0;
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Porte")
        {
            float move = hit.point.y - 0.5f;
            string porteColor = GetTileBase(hit).name.Split('_')[1];
            if (item == porteColor)
            {
                if (vector3.y == hit.point.y)
                {
                    DestroyDoor(hit);
                    return;
                }
                else
                {
                    this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(() => DestroyDoor(hit));

                }
                item = null;

            }
            else
            {
                if (vector3.y == hit.point.y)
                {
                    PlayerNotMove();
                    return;
                }
                this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(notMoving);
            }
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Portal")
        {
            float move = hit.point.y + 0.5f;
            Debug.Log(hit.transform.parent.name);
            if (hit.transform.gameObject.name == "Portal_Red")
            {
                this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(() => Portal(hit, "Blue"));
            }
            else
            {
                this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(() => Portal(hit, "Red"));
            }
        }
    }

    public void MoveDown()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().UpdateWall();
        Vector3 vector3 = new Vector3(this.transform.position.x, this.transform.position.y - 0.6f, this.transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(vector3, Vector2.down, 20f);

        if (hit.transform.gameObject.tag == "Wall")
        {
            if (vector3.y == hit.point.y)
            {
                PlayerNotMove();
                return;
            }
          /*  GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().UpdateWall();
            vector3 = new Vector3(this.transform.position.x, this.transform.position.y - 0.6f, this.transform.position.z);
            hit = Physics2D.Raycast(vector3, Vector2.down, 20f);*/
            float move = hit.point.y + 0.5f;
            this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(notMoving);
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Pique")
        {
            float move = hit.point.y + 0.3f;
            this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(Kill);
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Substance")
        {
            float move = 0;
            if (vector3.y == hit.point.y)
            {
                move = hit.point.y - 0.4f;
            }
            else {
                move = hit.point.y - 0.5f;
            }
            Debug.Log("substance" + hit.transform.name);
            //float move = hit.point.y - 0.5f;
            this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(notMoving);
            Debug.Log(move);
        }

        if (hit.transform.gameObject.tag == "End")
        {
            float move = hit.point.y - 0.5f;
            this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(Fini);
            Debug.Log(move);
        }


        if (hit.transform.gameObject.tag == "Color_Green" && colorPlayer != "Green")
        {
            if (vector3.y == hit.point.y)
            {
                PlayerNotMove();
                return;
            }
            float move = hit.point.y + 0.5f;
            this.transform.DOMoveX(move, moveDuration).SetEase(moveEase).OnComplete(notMoving);
            Debug.Log(move);
        }
        else if (hit.transform.gameObject.tag == "Color_Green" && colorPlayer == "Green")
        {
            hit.transform.gameObject.layer = 2;
            MoveDown();
            hit.transform.gameObject.layer = 0;
        }

        if (hit.transform.gameObject.tag == "BreakWall")
        {
            if (vector3.y == hit.point.y)
            {
                PlayerNotMove();
                return;
            }
            float move = hit.point.y + 0.5f;
            this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(() => DestroyWall(hit));
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Cle")
        {
            float move = hit.point.y - 0.5f;
            item = GetTileBase(hit).name.Split('_')[1];
            hit.transform.gameObject.layer = 2;
            this.playerSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Slimekey");
            DestroyWall(hit);
            MoveDown();
            hit.transform.gameObject.layer = 0;
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Porte")
        {
            float move = hit.point.y + 0.5f;
            string porteColor = GetTileBase(hit).name.Split('_')[1];
            if (item == porteColor)
            {
                if (vector3.y == hit.point.y)
                {
                    DestroyDoor(hit);
                    return;
                }
                else
                {
                    this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(() => DestroyDoor(hit));

                }
                item = null;

            }
            else
            {
                if (vector3.y == hit.point.y)
                {
                    PlayerNotMove();
                    return;
                }
                this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(notMoving);
            }
            Debug.Log(move);
        }
        if (hit.transform.gameObject.tag == "Portal")
        {
            float move = hit.point.y - 0.5f;
            Debug.Log(hit.transform.parent.name);
            if (hit.transform.gameObject.name == "Portal_Red")
            {
                this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(() => Portal(hit, "Blue"));
            }
            else
            {
                this.transform.DOMoveY(move, moveDuration).SetEase(moveEase).OnComplete(() => Portal(hit, "Red"));
            }
        }
    }

    public void Fini()
    {
        GameObject Gcontroller = GameObject.FindGameObjectWithTag("GameController");
        nombrePlayers = Gcontroller.GetComponent<GameManager>().GetNombrePlayers();
        nombrePlayers -= 1;
        Gcontroller.GetComponent<GameManager>().SetNombrePlayers(nombrePlayers);
        if (nombrePlayers == 0)
        {
            //Gcontroller.GetComponent<GameManager>().ChangeScene();
            Invoke("ChangeScene", Gcontroller.GetComponent<GameManager>().ChangeScene());
        }
    }


    public void ChangeScene()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        var scene = SceneManager.GetActiveScene().buildIndex + 1;
        nb_str = PlayerPrefs.GetString("Level").Split()[1];
        if (scene > int.Parse(nb_str))
        {
            PlayerPrefs.SetString("Level", "Level " + scene.ToString());
        }
        SceneManager.LoadScene(scene);
    }

    public void notMoving()
    {
        ///a voir
       // StartCoroutine(cameraShake.Shake(0.1f, 0.05f));
        //  this.transform.Find("Particle System_Death").GetComponent<ParticleSystem>().Play();
        moving = false;
    }

    public void PlayerNotMove()
    {
        moving = false;
    }


    public void Kill()
    {
        StartCoroutine(cameraShake.Shake(0.1f, 0.15f));
        this.transform.Find("Particle System_Death").GetComponent<ParticleSystem>().Play();
        //particleSystem.Play();
        Destroy(playerSprite);
        Invoke("Reload", 1f);
    }

    public void DestroyWall(RaycastHit2D hit)
    {
        StartCoroutine(cameraShake.Shake(0.1f, 0.05f));
        Tilemap tilemap = hit.transform.gameObject.GetComponent<Tilemap>();
        Grid grid = tilemap.layoutGrid;

        
        tilemap.SetTile(grid.WorldToCell(hit.point), null);
        PlayerNotMove();
    }

    private void Portal(RaycastHit2D hit, string Color)
    {
        Debug.Log(Color);
        this.transform.position = hit.transform.parent.Find($"Portal_{Color}").position;
        notMoving();
    }
    public TileBase GetTileBase(RaycastHit2D hit)
    {
        Tilemap tilemap = hit.transform.gameObject.GetComponent<Tilemap>();
        Grid grid = tilemap.layoutGrid;

        return tilemap.GetTile(grid.WorldToCell(hit.point));
    }

    private void Reload (){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DestroyDoor(RaycastHit2D hit)
    {
        Tilemap tilemap = hit.transform.gameObject.GetComponent<Tilemap>();
        Grid grid = tilemap.layoutGrid;

        this.playerSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Slime");
        tilemap.SetTile(grid.WorldToCell(hit.point), null);
        notMoving();
    }
}
