using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tin.CharacterStats;

public class StatPanel : MonoBehaviour
{
    [SerializeField]
    StatDisplay[] _statDisplays; 
    [SerializeField]
    string[] _statNames;


    private CharacterStat[] _stats;

    private void OnValidate()
    {
        _statDisplays = GetComponentsInChildren<StatDisplay>();
        UpdateStatNames();
    }


    public void SetStats(params CharacterStat[] charStats)
    {
        _stats = charStats;

        if(_stats.Length > _statDisplays.Length)
        {
            Debug.LogError("nedovoljno statova");
            return;
        }

        //ako imamo viška sam ugasimo ostale
        for (int i = 0; i < _statDisplays.Length; i++)
        {
            _statDisplays[i].gameObject.SetActive(i < _stats.Length);
        }
    }


    public void UpdateStatValues()
    {
        for (int i = 0; i < _stats.Length; i++)
        {
            _statDisplays[i].ValueText.text = _stats[i].Value.ToString();
        }
    }

    public void UpdateStatNames()
    {
        for (int i = 0; i < _statNames.Length; i++)
        {
            _statDisplays[i].NameText.text = _statNames[i];
        }
    }
}
