using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarManager : MonoBehaviour
{
    [SerializeField] private GameObject pillarPrefab;
    [SerializeField] private float min_ScaleX;
    [SerializeField] private float max_ScaleX;
    [SerializeField] private float min_PosX;
    [SerializeField] private float max_PosX;
    [SerializeField, Range(0.5f, 2f)] float moveDuration;
    [SerializeField, Range(10f, 15f)] float spawnOffsetX;
    [System.NonSerialized] public GameObject currentPillar;
    private GameObject targetPillar;
    private StickManager stickManager;


    void Start()
    {
        stickManager = GetComponent<StickManager>();

        currentPillar = spawnPillar(true);
        targetPillar = spawnPillar();
        stickManager.spawnStick();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(moveNext());
        }
    }

    GameObject spawnPillar(bool isFirst = false)
    {
        float scaleX = Random.Range(min_ScaleX, max_ScaleX);
        float posX = isFirst ? 0f : Random.Range(min_PosX, max_PosX);
        GameObject newPillar = Instantiate(pillarPrefab, new Vector3(spawnOffsetX, 0f), Quaternion.identity);
        newPillar.transform.localScale = new Vector3(scaleX, newPillar.transform.localScale.y);
        newPillar.LeanMoveX(posX, moveDuration).setEaseOutQuint();
        return newPillar;
    }

    public IEnumerator moveNext()
    {
        stickManager.stick.LeanMoveX(-stickManager.stick.transform.localScale.y, moveDuration).setEaseOutQuint();
        currentPillar.LeanMoveX(-2f, moveDuration).setEaseOutQuint();
        targetPillar.LeanMoveX(0f, moveDuration).setEaseOutQuint();
        GameObject newPillar = spawnPillar();

        yield return new WaitForSeconds(moveDuration);

        Destroy(stickManager.stick);
        Destroy(currentPillar);
        currentPillar = targetPillar;
        targetPillar = newPillar;
        stickManager.spawnStick();

        stickManager.canGrow = true;
    }
}
