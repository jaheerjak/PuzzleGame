using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidePuzzle : MonoBehaviour
{
    [SerializeField] Sprite[] puzzleSprites;
    [SerializeField] Image[] puzzleObjImg;

    [SerializeField] Sprite[] finalSpriteArr;
    [SerializeField] int startHiddenTile=15;


    int[] initArr = new int[] { 15, 0, 12, 10, 2, 9, 7, 13, 5, 4, 14, 8, 3, 11, 6, 1 };

    Vector3[] defaultPos;
    bool isPlaying;
    void OnEnable()
    {
        OnInitSlidePuzzle();
    }
    void OnInitSlidePuzzle()
    {
        defaultPos = new Vector3[puzzleObjImg.Length];
        for (int i = 0; i < puzzleObjImg.Length; i++)
        {
            puzzleObjImg[i].sprite = puzzleSprites[initArr[i]];
            defaultPos[i] = puzzleObjImg[i].transform.localPosition;
        }
        puzzleObjImg[startHiddenTile].enabled= false;
    }
    GameObject firstTile;
    GameObject secondTile;
    Sprite firstTileSprite;
    Sprite secondTileSprite;

    public List<int[]> CheckAdjArr = new List<int[]>
    {
        new int[] {1,4},
        new int[] {0,2,5},
        new int[] {1,3,6},
        new int[] {2,7},
        new int[] {0,5,8},
        new int[] {1,4,6,9},
        new int[] {2,5,7,10},
        new int[] {3,6,11},
        new int[] {4,9,12},
        new int[] {5,8,10,13},
        new int[] {6,9,11,14},
        new int[] {7,10,15},
        new int[] {8,13},
        new int[] {9,12,14},
        new int[] {10,13,15},
        new int[] {11,14}
    };
    public void OnClickSwapTile(int index)
    {
        if (isPlaying) return;
        int movetoIndex;
        for (int i = 0; i < CheckAdjArr[index].Length; i++)
        {
           
            if (puzzleObjImg[CheckAdjArr[index][i]].enabled == false)
            {
                movetoIndex = i;
                firstTile = puzzleObjImg[index].gameObject;
                secondTile = puzzleObjImg[CheckAdjArr[index][i]].gameObject;
                firstTileSprite = firstTile.GetComponent<Image>().sprite ;
                secondTileSprite=secondTile.GetComponent<Image>().sprite ;
                break;
            }
        }
        if(firstTile!= null)
        {
            firstTile.transform.DOLocalMove(secondTile.transform.localPosition, 0.5f)
                .SetEase(Ease.OutBounce)
                .OnComplete(() =>
                {
                    SwapTweenCompleted();
                });
        }
    }
    void SwapTweenCompleted()
    {
        
        firstTile.GetComponent<Image>().sprite = secondTileSprite;
        secondTile.GetComponent<Image>().sprite = firstTileSprite;
        firstTile.GetComponent<Image>().enabled= false;
        secondTile.GetComponent<Image>().enabled= true;

        firstTile = null;
        secondTile = null;
        ResetPos();
        Invoke(nameof(CheckWin), 0.2f);
        
    }
    void CheckWin()
    {
        isPlaying = false;
        Sprite[] currentSprite = new Sprite[puzzleObjImg.Length];
        for (int i = 0; i < puzzleObjImg.Length; i++)
            currentSprite[i] = puzzleObjImg[i].sprite;

        if (Puzzle.ArraysAreEqual(currentSprite, puzzleSprites))
        {
            Debug.Log("Win.....");
            GameManager.Instance.ShowLevelCompleted();
        }
    }
    void ResetPos()
    {
        for (int i = 0; i < puzzleObjImg.Length; i++)
        {
            puzzleObjImg[i].transform.localPosition = defaultPos[i];
        }
    }
}
