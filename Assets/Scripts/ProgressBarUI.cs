using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] Image barImage;
    [SerializeField] CuttingCounter counter;

    void Start()
    {
        counter.OnProgressChanged += UpdateBar;
        barImage.fillAmount = 0f;
    }

    void UpdateBar(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
    }
}
