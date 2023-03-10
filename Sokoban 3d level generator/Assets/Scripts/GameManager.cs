using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int[,] Level1 = { { 4, 4, 4, 4, 4, 4, 4, 4 }, { 4, 4, 4, 1, 4, 4, 4, 4 }, { 4, 4, 4, 2, 0, 2, 1, 4 }, { 4, 1, 0, 2, 3, 4, 4, 4 }, { 4, 4, 4, 4, 2, 4, 4, 4 }, { 4, 4, 4, 4, 1, 4, 4, 4 }, { 4, 4, 4, 4, 4, 4, 4, 4 } };
    private int[,] Level2 = { { 4, 4, 4, 4, 4, 4, 4, 4 }, { 4, 4, 4, 1, 1, 4, 4, 4 }, { 4, 4, 4, 0, 1, 4, 4, 4 }, { 4, 4, 0, 0, 2, 1, 4, 4 }, { 4, 4, 0, 2, 0, 0, 4, 4 }, { 4, 0, 0, 3, 2, 2, 0, 4 }, { 4, 0, 0, 0, 0, 0, 0,  4 }, { 4, 4, 4, 4, 4, 4,  4, 4 } };
    private List<int[,]> Levels = new List<int[,]>();

    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ArrivalPoint;
    [SerializeField] private GameObject ArrivalTrigger;
    [SerializeField] private GameObject youWin;
    private int level = 0;
    // Start is called before the first frame update
    void Start()
    {
        Levels.Add(Level1);
        Levels.Add(Level2);
        CreateLevel(Levels[level]);
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckWin())
        {
            youWin.SetActive(true);
        }

    }

    void CreateLevel(int[,] level)
    {
        int i = 0,j=0;
        for (i = 0; i <= level.GetUpperBound(0); i++)
        {
            for (j = 0; j <= level.GetUpperBound(1); j++)
            {
                GetBlock(level[i, j], i ,j);
            }
        }
    }
    void DestroyLevel(int[,] level)
    {
        GameObject[] Boxes = GameObject.FindGameObjectsWithTag("Box");
        GameObject[] Walls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject[] Blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach(GameObject Block in Blocks)
        {
            Destroy(Block);
        }
        foreach (GameObject Wall in Walls)
        {
            Destroy(Wall);
        }
        foreach (GameObject Box in Boxes)
        {
            Destroy(Box);
        }

    }
    void GetBlock(int block, int x, int z)
    {
        switch (block)
        {
            case 4:
                Instantiate(wall, new Vector3(x, 1f, z), wall.transform.rotation);
                Instantiate(floor, new Vector3(x, 0f, z), floor.transform.rotation);
                break;
            case 2:
                Instantiate(box, new Vector3(x, 1f, z), wall.transform.rotation);
                Instantiate(floor, new Vector3(x, 0f, z), floor.transform.rotation);
                break;
            case 0:
                Instantiate(floor, new Vector3(x, 0f, z), floor.transform.rotation);
                break;
            case 1:
                Instantiate(ArrivalTrigger, new Vector3(x, 1f, z), floor.transform.rotation);
                Instantiate(ArrivalPoint, new Vector3(x, 0f, z), floor.transform.rotation);
                break;
            case 3:
                Instantiate(player, new Vector3(x, 1f, z), wall.transform.rotation);
                Instantiate(floor, new Vector3(x, 0f, z), floor.transform.rotation);
                break;
        }
    }
    bool CheckWin()
    {
        WinningTriiger[] WinningScript = FindObjectsOfType<WinningTriiger>();

        int len = WinningScript.Length, i = 0, checkedTrigger = 0;
        for (i = 0; i < len; i++)
        {
            if (WinningScript[i].triggerChecked == true) checkedTrigger++;
        }
        if (checkedTrigger == len) return true;
        return false;
    }
    public void RestartLevel()
    {
        DestroyLevel(Levels[level]);
        youWin.SetActive(false);
        CreateLevel(Levels[level]);
    }
    public void NextLevel()
    {

        DestroyLevel(Levels[level]);
        youWin.SetActive(false);
        level++;
        CreateLevel(Levels[level]);
    }
}
