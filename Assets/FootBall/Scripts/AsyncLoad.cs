using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class AsyncLoad : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI HelpText_UI;
    [SerializeField]
    [TextArea]
    string[] helpText;

    private AsyncOperation async = null;
    private bool isLoading = false;
    [SerializeField]
    string levelName = "";
    [SerializeField]
    Image lineBar;
    [SerializeField]
    TextMeshProUGUI text_load;

    private void Start()
    {
        isLoading = true;
        StartCoroutine(_Start());
        StartCoroutine(Show());
    }
    private IEnumerator Show()
    {
        while (true)
        {
            //show random helptext
            HelpText_UI.text = helpText[Random.Range(0, helpText.Length)];
            yield return new WaitForSeconds(2.0f);
        }
    }
    private IEnumerator _Start()
    {

        async = SceneManager.LoadSceneAsync(levelName);
        while (!async.isDone)
        {

            yield return null;
        }

        isLoading = false;
        yield return async;
    }
    private void Update()
    {
        if (isLoading == true)
        {
            lineBar.rectTransform.sizeDelta = new Vector2(async.progress * 722, lineBar.rectTransform.sizeDelta.y);
            float progress = async.progress;
            progress *= 100;
            text_load.text = progress.ToString() + "%";

        }
    }
}
