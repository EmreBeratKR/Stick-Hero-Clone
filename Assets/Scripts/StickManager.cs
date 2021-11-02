using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManager : MonoBehaviour
{
    [SerializeField] private GameObject stickPrefab;
    [SerializeField] private float stickOffsetX;
    [SerializeField] private float growSpeed;
    [SerializeField] private float fallDuration;
    private bool isGrowing = false;
    [System.NonSerialized] public bool canGrow = true;
    [System.NonSerialized] public bool success;
    [System.NonSerialized] public GameObject stick;
    private PillarManager pillarManager;


    void Awake()
    {
        pillarManager = GetComponent<PillarManager>();
    }

    void Update()
    {
        grow();
    }

    public void spawnStick()
    {
        Transform crnt = pillarManager.currentPillar.transform;
        stick = Instantiate(stickPrefab, new Vector3(crnt.localScale.x - stickOffsetX, 0f), Quaternion.identity);
    }

    public void userInput(bool isClick)
    {
        if (canGrow)
        {
            if (!isClick && isGrowing)
            {
                canGrow = false;
                StartCoroutine(fallStick());
            }
            isGrowing = isClick;
        }
    }

    void grow()
    {
        if (isGrowing && canGrow)
        {
            stick.transform.localScale += Vector3.up * Time.deltaTime * growSpeed;
        }
    }

    IEnumerator fallStick()
    {
        success = false;
        stick.LeanRotateZ(-90f, fallDuration).setEaseInQuint();

        yield return new WaitForSeconds(fallDuration + 0.5f);
        
        if (success)
        {
            StartCoroutine(pillarManager.moveNext());
        }
        else
        {
            Debug.Log("Failed!");
        }
    }
}
