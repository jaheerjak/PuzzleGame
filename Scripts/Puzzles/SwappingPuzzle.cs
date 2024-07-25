using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwappingPuzzle : MonoBehaviour
{
    [SerializeField] Sprite[] puzzleSprites;
    [SerializeField] Image[] puzzleObjImg;

    int[] initArr = new int[] { 15,0,12,10,2,9,7,13,5,4,14,8,3,11,6,1 };
    Vector3[] defaultPos;
    bool isPlaying;
    void OnEnable()
    {
        OnInitSwapPuzzle();
    }

    // Update is called once per frame
    void OnInitSwapPuzzle()
    {
        defaultPos = new Vector3[puzzleObjImg.Length];
        for (int i = 0; i < puzzleObjImg.Length; i++)
        {
            puzzleObjImg[i].sprite = puzzleSprites[initArr[i]];
            defaultPos[i] = puzzleObjImg[i].transform.localPosition;
        }
    }
    GameObject firstTile;
    GameObject secondTile;
    Sprite firstTileSprite;
    Sprite secondTileSprite;
    public void OnClickSwapTile(int index)
    {
        if (isPlaying) return;

        if (firstTile==null)
        {
            firstTile = puzzleObjImg[index].gameObject;
            
        }
        else
        {
            secondTile = puzzleObjImg[index].gameObject;
            if (firstTile == secondTile)
            {
                firstTile = null;
                secondTile = null;
                ResetPos();
            }
            else
            {
                isPlaying = true;
                firstTileSprite = firstTile.GetComponent<Image>().sprite;
                secondTileSprite = secondTile.GetComponent<Image>().sprite;
                firstTile.transform.DOLocalMove(secondTile.transform.localPosition, 0.5f)
                .SetEase(Ease.OutBounce)
                .OnComplete(() =>
                {
                    SwapTweenCompleted();
                });
                secondTile.transform.DOLocalMove(firstTile.transform.localPosition, 0.5f)
                .SetEase(Ease.OutBounce)
                .OnComplete(() =>
                {
                    SwapTweenCompleted();
                });
            }
        }
    }
    int moveCount = 0;
    void SwapTweenCompleted()
    {
       moveCount++;
        if (moveCount>=2)
        {
            moveCount = 0;
            firstTile.GetComponent<Image>().sprite= secondTileSprite;
            secondTile.GetComponent<Image>().sprite= firstTileSprite;


            firstTile = null;
            secondTile = null;
            ResetPos();
            Invoke(nameof(CheckWin), 0.2f);
        }
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
