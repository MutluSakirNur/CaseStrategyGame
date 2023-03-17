using System;

namespace CaseStrategy
{
    public class Program
    {
        static void Main(string[] args)
        {
            // get username
            Console.WriteLine(" Welcome to the game. Please enter your nickname");
            string nickName = Console.ReadLine() ?? String.Empty;

            while(string.IsNullOrEmpty(nickName))
            {
                Console.WriteLine(" Welcome to the game. Please enter your nickname");
                nickName = Console.ReadLine() ?? String.Empty;
            }

            //create instances
            Player computer = new();
            Player player = new();

            //prepare menu
            List<MoveMenu> moveMenus = new List<MoveMenu>();
            while(player.TotalHealth > 0 && computer.TotalHealth > 0)
            {
                FillMenu(moveMenus);

                int characterNo = 1;
                while (moveMenus.Count > 0)
                {
                    ShowMenu(characterNo, moveMenus);
                    try
                    {
                        // fill user movements
                        int playerMove = Int32.Parse(Console.ReadLine());
                        if (moveMenus.Where(w => w.No.Equals(playerMove)).Any())
                        {
                            moveMenus.Remove(moveMenus.Find(w => w.No.Equals(playerMove)));
                            characterNo++;
                            player.CharacterMoves.Add((PlayerMoves)playerMove);
                        }
                    }
                    catch (Exception)
                    {
                        ShowMenu(characterNo, moveMenus);
                    }
                }
                FillComputerMoves(computer); // assign random movements
                MakeMoves(player, computer, "Player", "Computer"); 
                MakeMoves(computer, player, "Computer", "Player");
                ShowHealtsAndClear(player, computer);
            }
            CalculateAndShowScore(player, computer, nickName); // as a result
        }

        public static void FillMenu(List<MoveMenu> moveMenus)
        {
            MoveMenu moveMenu = new MoveMenu()
            {
                Description = "Attack",
                No = 1
            };
            moveMenus.Add(moveMenu);
            MoveMenu moveMenu2 = new MoveMenu()
            {
                Description = "Defense",
                No = 2
            };
            moveMenus.Add(moveMenu2);
            MoveMenu moveMenu3 = new MoveMenu()
            {
                Description = "Add health",
                No = 3
            };
            moveMenus.Add(moveMenu3);
        }

        public static void ShowMenu(int characterNo, List<MoveMenu> moveMenus)
        {
            Console.WriteLine();
            Console.Write(" Please select your " + characterNo + ". character's move.");
            Console.Write("Please type only ");
            foreach (MoveMenu moveMenu in moveMenus)
            {
                Console.Write(moveMenu.No + " ");
            }
            Console.WriteLine();
            foreach (MoveMenu moveMenu in moveMenus)
            {
                Console.WriteLine(" " + moveMenu.No + "-)" + moveMenu.Description);
            }
        }

        public static void FillComputerMoves(Player computer)
        {
            List<int> moves = new() { 1,2,3 };
            Random rnd = new();
            while(moves.Count > 0)
            {
                int num = rnd.Next(0, moves.Count);
                computer.CharacterMoves.Add((PlayerMoves)moves[num]);
                moves.RemoveAt(num);
            }
        }

        public static void MakeMoves(Player attacking, Player defending,string whosplay, string whostandby)
        {
            Console.WriteLine();
            Console.WriteLine(" " + whosplay + " moves");
            Console.WriteLine("------------------------------------------------------------");
            foreach (var move in attacking.CharacterMoves)
            {
                if(move == PlayerMoves.Attack)
                {
                    Random rnd = new();
                    int damage = rnd.Next(10,21); // attack between 10..20
                    int index = attacking.CharacterMoves.IndexOf(move);
                    Console.WriteLine(" " + whosplay + " performs " + damage + " damage with the " + (index+1) + ". character");
                    if (defending.CharacterMoves[index] == PlayerMoves.Defense)
                    {
                        damage = (damage / 2);
                        Console.WriteLine(" But " + whostandby + "'s " + (index+1) + ". character has selected defense");
                        Console.WriteLine(" So," + whosplay + " performs " + damage + " damage with the " + (index+1) + ". character");
                    }
                    defending.TotalHealth -= damage;
                }
                else if(move == PlayerMoves.AddHealth)
                {
                    Random rnd = new();
                    int health = rnd.Next(1, 6); // add health between 1..5
                    attacking.TotalHealth += health;
                    int index = attacking.CharacterMoves.IndexOf(move);
                    Console.WriteLine(" " + whosplay + " increases health by " + health + " points with the " + (index + 1) + ". character");
                }
            }
            Console.WriteLine();
        }

        public static void ShowHealtsAndClear(Player player, Player computer)
        {
            Console.WriteLine();
            Console.WriteLine("************************************************************");
            Console.WriteLine();
            player.TotalHealth = player.TotalHealth < 0 ? 0 : player.TotalHealth;
            computer.TotalHealth = computer.TotalHealth < 0 ? 0 : computer.TotalHealth;
            Console.WriteLine(" Player Health : " + player.TotalHealth);
            Console.WriteLine(" Computer Health : " + computer.TotalHealth);
            Console.WriteLine();
            player.CharacterMoves.Clear();
            computer.CharacterMoves.Clear();
            Console.WriteLine("************************************************************");
        }

        public static void CalculateAndShowScore(Player player, Player computer,string nickName)
        {
            int playerScore;
            playerScore = (player.TotalHealth + (100 - computer.TotalHealth)) < 0 ? 0 : player.TotalHealth + (100 - computer.TotalHealth);
            Console.WriteLine();
            Console.WriteLine(" Player name: " + nickName);
            Console.WriteLine(" Player score: " + playerScore);
            Console.Write("Press <Enter> to exit... ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }
    }
}