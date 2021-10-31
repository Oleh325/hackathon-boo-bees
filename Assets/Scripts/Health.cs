using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField] private int _health;
    [SerializeField] private int _numOfHearts;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    // Update is called once per frame
    private void Update()
    {
        UpdateHeartStatusBar();
        if (_numOfHearts == 0)
        {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }

    private void UpdateHeartStatusBar()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < _health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < _numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.tag.Equals("Skeleton"))
        {
            RemoveLife();
        }
       else if(other.gameObject.tag.Equals("Bat"))
        {
            Destroy(other.gameObject);
            AddLife();
        }
    }

    public void RemoveLife()
    {
        if(_health==1)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            _health--;
        }
    }

    public void AddLife()
    {
        if (_health < 3)
        {
            _health++;
        }
    }

}
