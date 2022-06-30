using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Singleton;

public class LocalizeSetting : MonoBehaviour
{
    Dropdown dropdown;

    private void Start()
    {
        dropdown = GetComponent<Dropdown>();
        if (dropdown.options.Count != S.Langs.Count)     //����ٿ��� �ɼ� ���� ���� �����Ϳ� �ִ� ����� ���� �ٸ��ٸ� 
            SetLangOption();
        dropdown.onValueChanged.AddListener((d) => S.SetLangIndex(dropdown.value));  //����ٿ��� value�� ����Ǹ� LanguageSingleton���� ���� ��� �ε��� ���� �� ����

    }

    private void OnDestroy()
    {
        S.LocalizeSettingChanged -= LocalizeSettingChanged;
    }

    void SetLangOption()
    {
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();


        //����ٿ��� �ɼǿ� �� ����Ʈ�� ����� ����ȭ �̸� �߰�
        for (int i = 0; i < S.Langs.Count; i++)
        {
            optionDatas.Add(new Dropdown.OptionData() { text = S.Langs[i].langLocalize });
        }

        dropdown.options = optionDatas;
    }

    void LocalizeSettingChanged()
    {
        dropdown.value = S.curLangIndex; //����ٿ��� ���� value�� ���� ���� �ٲ��ش�. 
    }
}