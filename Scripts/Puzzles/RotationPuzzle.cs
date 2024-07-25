using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class RotationPuzzle : MonoBehaviour
{
    [SerializeField] Sprite[] puzzleSprites;
    [SerializeField] Image[] puzzleObjImg;
     
    [SerializeField] Sprite[] finalSpriteArr;

    int[] initArr = new int[] { 0, 1, 4, 3, 0, 1, 2, 4, 4, 1, 2, 3, 0, 4, 2, 3 };
    int[] finalArr = new int[] { 0, 0, 4, 0, 1, 1, 1, 4, 3, 4, 3, 3, 4, 2, 2, 2 };
    int[] currentRotArr;
    Vector3[] defaultPos;
    bool isPlaying;

    private void OnEnable()
    {
        OnInitPuzzle();
    }
    
    public void OnInitPuzzle()
    {
        defaultPos=new Vector3[puzzleObjImg.Length];
        finalSpriteArr = new Sprite[puzzleObjImg.Length];
        for (int i=0;i< puzzleObjImg.Length;i++) 
        {
            puzzleObjImg[i].sprite = puzzleSprites[initArr[i]];
            defaultPos[i] = puzzleObjImg[i].transform.localPosition;
            finalSpriteArr[i] = puzzleSprites[finalArr[i]];
        }

    }
    public List<int[]> rotationWheelArr = new List<int[]>
    {
        new int[] {0,1,2,3},
        new int[] {4,5,6,7},
        new int[] {8,9,10,11},
        new int[] {12,13,14,15},
        new int[] {2,7,12,9},
    };
    public void OnClick_RotationButton(int index)
    {
        if (isPlaying) return;

        isPlaying = true;
        int firstElement = rotationWheelArr[index][0];
        int[] setArray=new int[rotationWheelArr[index].Length];
        for (int i = 0; i < rotationWheelArr[index].Length - 1; i++)
        {
            setArray[i] = rotationWheelArr[index][i + 1];
            
        }

        setArray[setArray.Length - 1] = firstElement;
        Image[] toMovPos = new Image[setArray.Length];
        Sprite[] changeSprite = new Sprite[setArray.Length];
        Vector3[] currentPos = new Vector3[setArray.Length];
        for (int i = 0; i < setArray.Length; i++)
        {
            toMovPos[i] = puzzleObjImg[setArray[i]];
            changeSprite[i] = puzzleObjImg[rotationWheelArr[index][i]].sprite;
            currentPos[i] = puzzleObjImg[rotationWheelArr[index][i]].transform.position;

        }
        int count = 0; 
        for (int i=0;i< rotationWheelArr[index].Length;i++)
        {
            puzzleObjImg[rotationWheelArr[index][i]].transform.DOLocalMove(toMovPos[i].transform.localPosition,0.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                count++;
                if (count >= 4)
                {
                    Debug.Log("Rotation complete");
                    for (int i = 0; i < rotationWheelArr[index].Length; i++)
                    {
                        puzzleObjImg[setArray[i]].sprite= changeSprite[i];
                    }
                    ResetPos();
                    Invoke(nameof(CheckWin),0.2f);
                }
            });
        }
    }
    void CheckWin()
    {
        isPlaying = false;
        Sprite[] currentSprite= new Sprite[puzzleObjImg.Length];
        for (int i = 0; i < puzzleObjImg.Length; i++)
            currentSprite[i]= puzzleObjImg[i].sprite;

        if(Puzzle.ArraysAreEqual(currentSprite, finalSpriteArr))
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
