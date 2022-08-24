using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hudmanager : MonoBehaviour
{
    public List<Image> progressBar = new List<Image>();

    public float hungryValue, thirstValue, hitValue;
    public bool life, gameplay;
    public TextMeshProUGUI textMeshProUGUI;

    // Start is called before the first frame update
    void Start()
    {
        gameplay = true;
        life = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameplay)
        {
            if (life)
            {
                //hungry
                if (progressBar[1].fillAmount > 0)
                    progressBar[1].fillAmount -= hungryValue * Time.deltaTime;

                //thirst
                if (progressBar[2].fillAmount > 0)
                    progressBar[2].fillAmount -= hungryValue * Time.deltaTime;
                //---------------------------------------------------------------------------------------------------------------

                if (progressBar[1].fillAmount == 0 || progressBar[2].fillAmount == 0 && progressBar[0].fillAmount>0)
                    progressBar[0].fillAmount -= hitValue * Time.deltaTime;
                 else if(progressBar[0].fillAmount == 0 && gameplay)
                    life = false;
            }
            else
            {           
                    textMeshProUGUI.gameObject.SetActive(true);
                    gameplay = false;
           
            }
        }
    }



}