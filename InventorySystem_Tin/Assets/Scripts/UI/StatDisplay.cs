using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Tin.CharacterStats;

public class StatDisplay : MonoBehaviour
{
	private CharacterStat _stat;
	public CharacterStat Stat
	{
		get { return _stat; }
		set
		{
			_stat = value;
			UpdateStatValue();
		}
	}

	private string _name;
	public string Name
	{
		get { return _name; }
		set
		{
			_name = value;
			NameText.text = _name.ToLower();
		}
	}


	public 	Text NameText;
	public	Text ValueText;


	private bool showingTooltip;

	private void OnValidate()
	{
		Text[] texts = GetComponentsInChildren<Text>();
		NameText = texts[0];
		ValueText = texts[1];


	}


	public void UpdateStatValue()
	{
		ValueText.text = _stat.Value.ToString();

	}
}
