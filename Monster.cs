using System;
using System.Collections.Generic;


namespace Monsters
{
	public class Monster
	{
		private int level;
		private int maximum_hit_points;
		private int attack;
		private int special_attack;
		private int defense;
		private int special_defense;
		private int speed;

		public string Name { get; set; }

		public int Level { get { return level; } set { level = Functions.LimitValue (value, max: 100); } }

		public int HitPoints { get; private set; }

		public int MaximumHitPoints { 
			get { return maximum_hit_points; } 
			set {
				maximum_hit_points = Functions.LimitValue (value, max: 999);
				HitPoints = maximum_hit_points;
			} 
		}

		public Element Element { get; set; }

		public int AttackStrength { get { return attack; } set { attack = Functions.LimitValue (value); } }

		public int SpecialAttackStrength { get { return special_attack; } set { special_attack = Functions.LimitValue (value); } }

		public int Defense { get { return defense; } set { defense = Functions.LimitValue (value); } }

		public int SpecialDefense { get { return special_defense; } set { special_defense = Functions.LimitValue (value); } }

		public int Speed { get { return speed; } set { speed = Functions.LimitValue (value); } }

		public List<Attack> Attacks { get; set; }

		/// <summary>
		/// Attacks the enemy.
		/// </summary>
		/// <returns>The enemy.</returns>
		/// <param name="monster">Monster.</param>
		/// <param name="attack">Attack.</param>
		public int AttackEnemy (Monster monster, Attack attack)
		{
			attack.Use ();
			var level_factor = (2.0 * Level + 10) / 250;

			double stat_coefficent;
			if (attack.SpecialAttack) {
				stat_coefficent = SpecialAttackStrength / monster.SpecialDefense;
			} else {
				stat_coefficent = AttackStrength / monster.Defense;
			}

			var modifier = Element.GetModifier (attack.Element, monster.Element);
			var damage = (level_factor * stat_coefficent * attack.Strength + 2) * modifier;
			// round up
			var rounded_damage = (int)Math.Ceiling(damage);

			monster.ApplyDamage(rounded_damage);
			return rounded_damage;
		}

		/// <summary>
		/// Applies the damage.
		/// </summary>
		/// <param name="damage">Damage.</param>
		public void ApplyDamage (int damage)
		{
			if (HitPoints - damage <= 0) {
				HitPoints = 0;
			} else {
				
				HitPoints -= damage;
			}

		}

		/// <summary>
		/// Clone this instance.
		/// </summary>
		public Monster Clone ()
		{
			var monster = new Monster () {
				Name = this.Name.Clone () as string,
				Level = this.Level,
				HitPoints = this.HitPoints,
				MaximumHitPoints = this.MaximumHitPoints,
				Element = this.Element,
				AttackStrength = this.AttackStrength,
				SpecialAttackStrength = this.SpecialAttackStrength,
				Defense = this.Defense,
				SpecialDefense = this.SpecialDefense,
				Speed = this.Speed,
				Attacks = new List<Attack> ()
			};

			// Also clone all the attacks or else
			// changes to AP will be applied to every
			// monster having the same attack
			foreach (var a in this.Attacks) {
				monster.Attacks.Add (a.Clone ());
			}
			return monster;
		}


	}
}

