using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] Image barImage;
    [SerializeField] GameObject counterObject;
    IHasProgress counter;
    void Start()
    {
        counter = counterObject.GetComponent<IHasProgress>();
        if (counter == null)
        {
            Debug.LogError("Object " + counterObject + "doesn't have component - IHasPorgress");
        }

        counter.OnProgressChanged += UpdateBar;
        barImage.fillAmount = 0f;
        Hide();
    }

    void UpdateBar(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
