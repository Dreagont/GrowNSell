using System.Collections;
using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float lifetime = 1f;
    public int goldPopup = 10;
    public bool isAdd;
    public TextMeshProUGUI goldText;
    public bool isGold;
    void Start()
    {
        if (isGold)
        {
            if (isAdd)
            {
                goldText.text = "+" + goldPopup.ToString();
            }
            else
            {
                goldText.color = Color.red;
                goldText.text = "-" + goldPopup.ToString();
            }
        } else
        {
            goldText.text = "+" + goldPopup.ToString() + "XP";
        }
        

        transform.position = Input.mousePosition;

        StartCoroutine(DestroyAfterTime());
    }

    void Update()
    {
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
