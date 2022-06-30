using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Singleton;

public class LocalizeSrcript : MonoBehaviour
{
    public string textKey;      //0��° ��(���� ������)�� �������� key�� ���� ���ڿ� ���� 
    public string[] dropdownKey;    //���� text�� �ƴ� ����ٿ��� ���

    private void Start()
    {
        LocalizeChanged();
        S.LocalizeChanged += LocalizeChanged;
    }

    private void OnDestroy()
    {
        S.LocalizeChanged -= LocalizeChanged;
    }

    string Localize(string key) //� ���ڿ��� �ʿ����� key�� �Ű������� �޴´� 
    {
        int keyIndex = S.Langs[0].value.FindIndex(x => x.ToLower() == key.ToLower()); //������ �Ǵ� 0�� �������� value Ž�� �� keyIndex�� ���ڿ� ����
        return S.Langs[S.curLangIndex].value[keyIndex];      //���� ��� �ε���, value�� key�� �������� ���ڿ��� ��ȯ�Ѵ�. 
    }

    void LocalizeChanged()
    {
        if (GetComponent<Text>() != null)
        {
            GetComponent<Text>().text = Localize(textKey);  // Localize�� ��ȯ ������ �ؽ�Ʈ�� �ٲ��ش�.
        }

        else if (GetComponent<Dropdown>() != null)
        {
            Dropdown dropdown = GetComponent<Dropdown>();
            dropdown.captionText.text = Localize(dropdownKey[dropdown.value]);

            for (int i = 0; i < dropdown.options.Count; i++)
            {
                dropdown.options[i].text = Localize(dropdownKey[i]);
            }
        }
    }
}