using UnityEngine;
using TMPro;

public class CatGuide : MonoBehaviour
{
    [SerializeField] private GameObject cat;
    [SerializeField] private GameObject guideText;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            cat.SetActive(false);
            guideText.SetActive(false);
        }
    }
}
