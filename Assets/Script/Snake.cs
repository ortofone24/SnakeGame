using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    int[,] grid = new int[15,10];

    private int snakeScore = 5;
    private int snakeX = 0;
    private int snakeY = 4;

    private Transform snakeTransform;
    private float lastMove;
    private float timeInBetweenMoves = 0.35f;
    private Vector3 direction;

    public GameObject applePrefab;
    public Text scoreText;

    private bool hasLost;

    private void Start()
    {
        snakeTransform = transform;
        direction = Vector3.right;
        grid[snakeX, snakeY] = snakeScore;

        scoreText.text = "Score : " + snakeScore.ToString();

        grid[8, 4] = -1;
        GameObject go = Instantiate(applePrefab) as GameObject;
        go.transform.position = new Vector3(8, 4, 0);
        go.name = "Apple";

    }

    private void Update()
    {
        if (hasLost)
        {
            return;
        }
        if (Time.time - lastMove > timeInBetweenMoves)
        {
            // Every move, itterate through our whole array and reduce every tile that isnt -1 or 0
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] > 0)
                    {
                        grid[i, j]--;
                        if (grid[i, j] == 0)
                        {
                            // We have to destroy something
                            GameObject toDestroy = GameObject.Find(i.ToString() + j.ToString());
                            if (toDestroy != null)
                            {
                                Destroy(toDestroy);
                            }
                        }
                    }
                }
            }

            lastMove = Time.time;
            // Add up direction to our snakeX and snakeY

            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = new Vector3(snakeX, snakeY, 0);
            go.name = snakeX.ToString() + snakeY.ToString();

            if (direction.x == 1)
            {
                snakeX++;
            }

            if (direction.x == -1)
            {
                snakeX--;
            }

            if (direction.y == 1)
            {
                snakeY++;
            }

            if (direction.y == -1)
            {
                snakeY--;
            }
         

            //If he goes out of bound
            if (snakeX >= grid.GetLength(0) || snakeX < 0 || snakeY >= grid.GetLength(1) || snakeY < 0)
            {
                hasLost = true;
                Debug.Log("We Lost");
            }
            else
            {
                //We eat an apple
                if (grid[snakeX, snakeY] == -1)
                {
                    Debug.Log("Apple!");
                    GameObject toDestroy = GameObject.Find("Apple");
                    Destroy(toDestroy);
                    snakeScore++;
                    scoreText.text = "Score : " + snakeScore.ToString();

                    for (int i = 0; i < grid.GetLength(0); i++)
                    {
                        for (int j = 0; j < grid.GetLength(1); j++)
                        {
                            if (grid[i, j] > 0)
                            {
                                grid[i, j]++;
                            }
                        }
                    }
                    //Create new apple
                    bool appleCreated = false;

                    while (!appleCreated)
                    {
                        int x = UnityEngine.Random.Range(0, grid.GetLength(0));
                        int y = UnityEngine.Random.Range(0, grid.GetLength(1));

                        if (grid[x,y] == 0)
                        {
                            grid[x, y] = -1;
                            GameObject apple = Instantiate(applePrefab) as GameObject;
                            apple.transform.position = new Vector3(x, y, 0);
                            apple.name = "Apple";

                            appleCreated = true;
                        }
                    }
                }
                else if (grid[snakeX, snakeY] != 0)
                {
                    hasLost = true;
                    Debug.Log("We lost");
                    return;
                }

                // Move

                snakeTransform.position += direction;
                grid[snakeX, snakeY] = snakeScore;
                
            }

            

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector3.up;
            Debug.Log("Move up!");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector3.down;
            Debug.Log("Move down!");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector3.left;
            Debug.Log("Move left!");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector3.right;
            Debug.Log("Move right!");
        }

        //Debug.Log(Input.GetAxis("Horizontal"));
    }

    public void OnBackButtonClick()
    {
        SceneManager.LoadScene(0);
    }

}
