using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Raycast : MonoBehaviour
{

    public Animator CameraAnimator;
    
    public GameObject SupportContainer;

    public GameObject UIContainer;

    public LayerMask RailMask;

    public GameObject Crosshair;

    public bool isInfo;

    public List<InfoData> InfoItemList = new List<InfoData>();

    public InfoData tmpInfo;

    // Awake is called before the game initalizes
    void Awake()
    {
        Crosshair = GameObject.Find("CrosshairImage");
        foreach (Transform item in UIContainer.transform)
        {
            if (item.gameObject.CompareTag("InfoUI"))
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2), 0));
            if (Physics.Raycast(ray, out RaycastHit hit, RailMask.value))
            {
                if (!isInfo)
                {
                    if (hit.transform.gameObject.name == "Death")
                    {
                        SceneManager.LoadScene(1);
                    }
                    tmpInfo = InfoItemList.Find(itm => itm.Object.gameObject.name == hit.transform.gameObject.name);
                    Debug.Log($"Title Text: {tmpInfo.TitleText}, Main Text: {tmpInfo.MainText}");
                    CameraAnimator.enabled = true;
                    CameraAnimator.SetBool(tmpInfo.IDName, true);
                    GameObject.Find("CrosshairImage").SetActive(false);
                    tmpInfo.UIObject.SetActive(true);
                    tmpInfo.TitleTextUI.text = tmpInfo.TitleText;
                    tmpInfo.MainTextUI.text = tmpInfo.MainText;
                    isInfo = !isInfo;
                }
            }
        }
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isInfo)
            {
                CameraAnimator.SetBool(tmpInfo.IDName, false);
                CameraAnimator.enabled = false;
                Crosshair.SetActive(true);
                tmpInfo.UIObject.SetActive(false);
                isInfo = !isInfo;
            }
        }
    }
}
