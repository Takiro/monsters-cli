namespace Monsters
{
	public class Attack
	{
		public string Name { get; }

		public int Strength { get; }

		public bool SpecialAttack { get; }

		public Element Element { get; }

		public int AttackPoints { get; private set; }

		public Attack (string name, int strength, bool special, Element element, int ap)
		{
			Name = name;
			Strength = Functions.LimitValue (strength, 0, 150);
			SpecialAttack = special;
			Element = element;
			AttackPoints = ap;
		}

		/// <summary>
		/// Clone this instance.
		/// </summary>
		public Attack Clone ()
		{
			return new Attack (Name, Strength, SpecialAttack, Element, AttackPoints);
		}

		public void Use ()
		{
			AttackPoints--;
		}
	}
}

