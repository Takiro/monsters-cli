using System;
using System.Collections.Generic;

namespace Monsters
{
	public class Game
	{
		private Random rnd = new Random ();
		private List<Monster> monsters = new List<Monster> ();
		// npc
		private Player rival;
		private Player user;

		/// <summary>
		/// Initializes a new instance of the <see cref="Monsters.Game"/> class.
		/// Sets the default values for Monsters, Attacks and Elements
		/// </summary>
		public Game ()
		{
			//Elements
			var water = new Element ("Water");
			var fire = new Element ("Fire");
			var plant = new Element ("Plant");
			var stone = new Element ("Stone");
			var electric = new Element ("Electric");

			water.Weakness = electric;
			fire.Weakness = water;
			plant.Weakness = fire;
			stone.Weakness = plant;
			electric.Weakness = stone;

			water.Strength = fire;
			fire.Strength = plant;
			plant.Strength = stone;
			stone.Strength = electric;
			electric.Strength = water;

			// Attacks
			var tackle = new Attack ("Tackle", 40, false, null, 5);
			var scratch = new Attack ("Scratch", 20, false, null, 5);
			var push = new Attack ("Push", 40, false, null, 2);
			var headbut = new Attack ("Head But", 60, false, null, 3);

			var watergun = new Attack ("Water Gun", 60, true, water, 3);
			var flame = new Attack ("Flame", 40, true, fire, 4);
			var stonecrush = new Attack ("Stone Crush", 80, true, stone, 1);
			var bogaloo = new Attack ("Bogaloo", 60, true, electric, 3);
			var vinewip = new Attack ("vinewip", 50, false, plant, 4);

			// Monsters
			monsters.Add (new Monster {
				Name = "Firething",
				Level = 1, 
				MaximumHitPoints = 30,
				AttackStrength = 50,
				SpecialAttackStrength = 70,
				Defense = 25,
				SpecialDefense = 25,
				Speed = 10,
				Element = fire,
				Attacks = new List<Attack> {
					tackle,
					flame,
					scratch
				}
			});	

			monsters.Add (new Monster {
				Name = "Waterwhatever",
				Level = 1, 
				MaximumHitPoints = 30,
				AttackStrength = 50,
				SpecialAttackStrength = 50,
				Defense = 25,
				SpecialDefense = 25,
				Speed = 15,
				Element = water,
				Attacks = new List<Attack> {
					scratch,
					watergun
				}
			});	

			monsters.Add (new Monster {
				Name = "Lemonimal",
				Level = 1, 
				MaximumHitPoints = 50,
				AttackStrength = 50,
				SpecialAttackStrength = 50,
				Defense = 25,
				SpecialDefense = 25,
				Speed = 12,
				Element = plant,
				Attacks = new List<Attack> {
					vinewip,
					tackle
				}
			});

			monsters.Add (new Monster {
				Name = "Stoner",
				Level = 1, 
				MaximumHitPoints = 50,
				AttackStrength = 50,
				SpecialAttackStrength = 10,
				Defense = 50,
				SpecialDefense = 10,
				Speed = 5,
				Element = stone,
				Attacks = new List<Attack> {
					stonecrush,
					push
				}
			});	

			monsters.Add (new Monster {
				Name = "Jollyton",
				Level = 1, 
				MaximumHitPoints = 25,
				AttackStrength = 30,
				SpecialAttackStrength = 70,
				Defense = 30,
				SpecialDefense = 50,
				Speed = 20,
				Element = electric,
				Attacks = new List<Attack> {
					bogaloo,
					scratch
				}
			});	

			monsters.Add (new Monster {
				Name = "Basion",
				Level = 1, 
				MaximumHitPoints = 30,
				AttackStrength = 30,
				SpecialAttackStrength = 30,
				Defense = 30,
				SpecialDefense = 30,
				Speed = 20,
				Element = null,
				Attacks = new List<Attack> {
					scratch,
					tackle,
					push,
					headbut
				}
			});	


			rival = new Player ();
			rival.Name = "Jerry";

			// Random monster for rival.
			var random_monster = monsters [rnd.Next (0, monsters.Count)];
			// Clone monster so every player gets his own version.
			rival.Monster = random_monster.Clone ();
		}

		/// <summary>
		/// Run this instance.
		/// </summary>
		public void Run ()
		{
			intro ();
			monster_selection ();
			outro (battle ());
		}


		/// <summary>
		/// Short introduction. Asks for player name.
		/// </summary>
		private void intro ()
		{
			Console.WriteLine ("Welcome to Little Monster Game. An example Programm.");
			Console.Write ("Enter your Name: ");
			var name = Console.ReadLine ();

			user = new Player {
				Name = name
			};
		}

		/// <summary>
		/// Battle until someone is defeated.
		/// Returns the winner as integer.
		/// 1: Player
		/// 2: Rival
		/// 3: Draw
		/// </summary>
		private int battle ()
		{
			Console.Clear ();
			Console.WriteLine ("The battle begins.\nReady?");
			wait_for_key ();
		
			int winner = 0;
			// Check who starts
			var is_user_round = user.Monster.Speed >= rival.Monster.Speed;

			// Repeat until someone was defeated
			do {
				Console.Clear ();
				print_health ();
				winner = turn (is_user_round);
				wait_for_key ();
				// Change player
				is_user_round = !is_user_round;
			} while(winner < 1);

			return winner;
		}

		/// <summary>
		/// Outputs win or defeat messages.
		/// </summary>
		/// <param name="winner">Winner.</param>
		private void outro (int winner)
		{
			Console.Clear ();
			if (winner == 1) {
				Console.WriteLine ($"Congratulations, you beat {rival.Name}'s {rival.Monster.Name}.");
				Console.WriteLine ($"Good work, {user.Name}");
			} else {
				if (winner == 2) {
					Console.WriteLine ($"Sorry, {rival.Name}'s {rival.Monster.Name} was stronger.");
					Console.WriteLine ("Try harder next time.");
				} else {
					Console.Write ("No winner.");
				}
			}
			wait_for_key ();
		}

		/// <summary>
		/// Waits for key.
		/// </summary>
		private void wait_for_key ()
		{
			Console.Write ("Press any key ...");
			Console.ReadKey ();
		}

		/// <summary>
		/// Prints the health of both player's monster.
		/// </summary>
		private void print_health ()
		{
			Console.WriteLine ($"{rival.Name}: {rival.Monster.Name} {rival.Monster.HitPoints}/{rival.Monster.MaximumHitPoints} HP");
			Console.WriteLine ($"{user.Name}: {user.Monster.Name} {user.Monster.HitPoints}/{user.Monster.MaximumHitPoints} HP");
		}

		/// <summary>
		/// Checks if a player won the game.
		/// Returns 1 if the player won, 2 if the rival won
		/// or 3 if both opponents lost.
		/// </summary>
		/// <returns>The winner</returns>
		private int check_for_winner ()
		{
			
			int winner = 0; // no result
			if (rival.Monster.HitPoints <= 0 || attacks_left (rival.Monster) <= 0) {
				winner += 1;
			}

			if (user.Monster.HitPoints <= 0 || attacks_left (user.Monster) <= 0) {
				winner += 2;
			}

			return winner;
		}

		/// <summary>
		/// Checks how many APs the monster has.
		/// </summary>
		/// <returns>The left.</returns>
		/// <param name="mon">Mon.</param>
		private int attacks_left (Monster mon)
		{
			var atk_count = 0;
			foreach (var atk in mon.Attacks) {
				atk_count += atk.AttackPoints;
			}
			return atk_count;
		}

		/// <summary>
		/// Starts the player's turns.
		/// </summary>
		/// <param name="user_round">If set to <c>true</c> user round.</param>
		private int turn (bool user_round)
		{

			if (user_round) {
				player_turn ();
			} else {
				rival_turn ();
			}

			return check_for_winner ();
		}


		/// <summary>
		/// Rivals turn.
		/// </summary>
		private void rival_turn ()
		{
			Console.WriteLine ($"{rival.Name}'s turn.");
			// chose a random attack with ap left
			Attack attack;
			do {
				var rand_int = rnd.Next (0, rival.Monster.Attacks.Count);
				attack = rival.Monster.Attacks [rand_int];
			} while(attack.AttackPoints <= 0);
			Console.WriteLine ($"{rival.Name}'s {rival.Monster.Name} uses {attack.Name}.");

			// Attack users Monster
			var damage = rival.Monster.AttackEnemy (user.Monster, attack);

			Console.WriteLine ($"Your {user.Monster.Name} takes {damage} points of damage.");
		}

		/// <summary>
		/// Players turn.
		/// </summary>
		private void player_turn ()
		{
			Console.WriteLine ($"It's your turn, {user.Name}.");
			Console.WriteLine ($"Choose one of {user.Monster.Name}'s attacks.");

			// Print attack list
			for (int i = 0; i < user.Monster.Attacks.Count; i++) {
				Console.WriteLine ($"{i+1}. {user.Monster.Attacks[i].Name} {user.Monster.Attacks[i].AttackPoints} AP left");
			}

			Attack attack;
			do {
				// Get Choice
				var choice = prompt_for_range (1, user.Monster.Attacks.Count, "Select an attack: ");
				attack = user.Monster.Attacks [choice - 1];
			} while(attack.AttackPoints <= 0);
			Console.WriteLine ($"Your {user.Monster.Name} uses {attack.Name}.");

			// Attack rival
			var damage = user.Monster.AttackEnemy (rival.Monster, attack);

			Console.WriteLine ($"{rival.Name}'s {rival.Monster.Name} takes {damage} points of damage.");
		}

		/// <summary>
		/// Asks the user to select a Monster from a list.
		/// </summary>
		private void monster_selection ()
		{
			// List Monsters with a Number to enter
			Console.WriteLine ("Choose your Monster.");

			for (int i = 0; i < monsters.Count; i++) {
				Console.WriteLine ($"{i+1}. {monsters[i].Name}");
			}
			// prompt user to select monster
			var input = prompt_for_range (1, monsters.Count, "Type in the Monster's number: ");
			user.Monster = monsters [input - 1].Clone ();
			Console.WriteLine ($"You selected {user.Monster.Name}");
			Console.WriteLine ($"Your rival {rival.Name} selected {rival.Monster.Name}");
			wait_for_key ();
		}

		/// <summary>
		/// Prompts to enter a number between min and max (inclusive).
		/// </summary>
		/// <returns>Entered number in range.</returns>
		/// <param name="min">Minimum.</param>
		/// <param name="max">Max.</param>
		private int prompt_for_range (int min, int max, string message = "")
		{
			int number;
			var success = false;
			do {
				number = prompt_for_int (message);
				if (number < min || number > max) {
					Console.WriteLine ($"Entered value must be in range of {min}-{max}.");
				} else {
					success = true;
				}
			} while (!success);

			return number;
		}

		/// <summary>
		/// Prompts the user to enter a valid number.
		/// </summary>
		/// <returns>The entered number.</returns>
		/// <param name="message">Message.</param>
		private int prompt_for_int (string message = "")
		{
			// In case of null was passed by mistake
			if (message == null) {
				message = "";
			}

			int number;
			var success = false;

			// Repeat prompt until a valid number was entered
			do {
				Console.Write (message);
				var inpunt = Console.ReadLine ();
				success = int.TryParse (inpunt, out number);
			} while(!success);
			return number;
		}
	}
}

