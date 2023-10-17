namespace Monsters
{
	public class Element
	{
		public string Name { get; }

		public Element Weakness { get; set; }

		public Element Strength { get; set; }

		public Element (string name)
		{
			Name = name;
		}


		public static double GetModifier (Element attacker, Element defender)
		{
			// check for elementless defender first
			// to prevent a null reference exception
			if (defender == null) {
				return 1;	
			}
			
			if (attacker == defender.Weakness) {
				return 2;
			}
			if (attacker == defender.Strength) {
				return 0.5;
			}
			return 1;
		}
	}
}

