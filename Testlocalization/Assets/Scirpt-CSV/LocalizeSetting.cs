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
        if (dropdown.options.Count != S.Langs.Count)     //드랍다운의 옵션 수와 현재 데이터에 있는 언어의 수가 다르다면 
            SetLangOption();
        dropdown.onValueChanged.AddListener((d) => S.SetLangIndex(dropdown.value));  //드랍다운의 value가 변경되면 LanguageSingleton에서 현재 언어 인덱스 변경 및 저장

    }

    private void OnDestroy()
    {
        S.LocalizeSettingChanged -= LocalizeSettingChanged;
    }

    void SetLangOption()
    {
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();


        //드랍다운의 옵션에 들어갈 리스트에 언어의 현지화 이름 추가
        for (int i = 0; i < S.Langs.Count; i++)
        {
            optionDatas.Add(new Dropdown.OptionData() { text = S.Langs[i].langLocalize });
        }

        dropdown.options = optionDatas;
    }

    void LocalizeSettingChanged()
    {
        dropdown.value = S.curLangIndex; //드랍다운의 현재 value를 현재 언어로 바꿔준다. 
    }
}