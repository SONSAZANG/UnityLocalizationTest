using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Lang
{
    public string lang, langLocalize;
    public List<string> value = new List<string>();
}

public class Singleton : MonoBehaviour
{
    #region CSV
    const string langURL = "https://docs.google.com/spreadsheets/d/1aqRJDOChemD72n2JUB3KDcXuZipfYqdHi0P3uu37vTg/export?format=tsv";
    public List<Lang> Langs;
    #endregion

    public static Singleton S;
    private void Awake()
    {
        if (null == S)
        {
            S = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }

    [ContextMenu("언어 가져오기-CSV")]
    void GetLang()
    {
        StartCoroutine(GetLangCo());
    }

    IEnumerator GetLangCo()
    {
        UnityWebRequest www = UnityWebRequest.Get(langURL);
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
        SetLangList(www.downloadHandler.text);
    }

    void SetLangList(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;
        int columnSize = row[0].Split('\t').Length;
        string[,] Sentence = new string[rowSize, columnSize];

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            for (int j = 0; j < columnSize; j++) Sentence[i, j] = column[j];
        }

        Langs = new List<Lang>();
        for (int i = 0; i < columnSize; i++)
        {
            Lang lang = new Lang();
            lang.lang = Sentence[0, i];
            lang.langLocalize = Sentence[1, i];

            for (int j = 2; j < rowSize; j++) lang.value.Add(Sentence[j, i]);
            Langs.Add(lang);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) SceneManager.LoadScene(0);
        else if (Input.GetKey(KeyCode.Alpha2)) SceneManager.LoadScene(1);
    }
}
