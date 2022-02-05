using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


//https://assetstore.unity.com/packages/tools/integration/character-stats-106351
namespace Tin.CharacterStats
{
	[Serializable]
	public class CharacterStat
	{
		public float BaseValue;

		protected bool _isDirty = true;
		protected float _lastBaseValue;

		protected float _value;
		public virtual float Value {
			get {
				if(_isDirty || _lastBaseValue != BaseValue) {
					_lastBaseValue = BaseValue;
					_value = CalculateFinalValue();
					_isDirty = false;
				}
				return _value;
			}
		}

		protected readonly List<StatModifier> statModifiers;
		public readonly ReadOnlyCollection<StatModifier> StatModifiers;

		public CharacterStat()
		{
			statModifiers = new List<StatModifier>();
			StatModifiers = statModifiers.AsReadOnly();
		}

		public CharacterStat(float baseValue) : this()
		{
			BaseValue = baseValue;
		}

		public virtual void AddModifier(StatModifier mod)
		{
			_isDirty = true;
			statModifiers.Add(mod);
			statModifiers.Sort(CompareModifierOrder);
		}

		public virtual bool RemoveModifier(StatModifier mod)
		{
			if (statModifiers.Remove(mod))
			{
				_isDirty = true;
				return true;
			}
			return false;
		}

		public virtual bool RemoveAllModifiersFromSource(object source)
		{
			bool didRemove = false;

			for (int i = statModifiers.Count - 1; i >= 0; i--)
			{
				if (statModifiers[i].Source == source)
				{
					_isDirty = true;
					didRemove = true;
					statModifiers.RemoveAt(i);
				}
			}
			return didRemove;
		}

		protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
		{
			if (a.Order < b.Order)
				return -1;
			else if (a.Order > b.Order)
				return 1;
			return 0; 
		}
		
		protected virtual float CalculateFinalValue()
		{
			float finalValue = BaseValue;
			float sumPercentAdd = 0;

			for (int i = 0; i < statModifiers.Count; i++)
			{
				StatModifier mod = statModifiers[i];

				if (mod.Type == StatModType.Flat)
				{
					finalValue += mod.Value;
				}
				else if (mod.Type == StatModType.PercentAdd)
				{
					sumPercentAdd += mod.Value;

					if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
					{
						finalValue *= 1 + sumPercentAdd;
						sumPercentAdd = 0;
					}
				}
				else if (mod.Type == StatModType.PercentMult)
				{
					finalValue *= 1 + mod.Value;
				}
			}

			return (float)Math.Round(finalValue, 4);
		}
	}
}