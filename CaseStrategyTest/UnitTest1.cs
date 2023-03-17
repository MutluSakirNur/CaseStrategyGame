using CaseStrategy;

namespace CaseStrategyTest
{
    public class UnitTest1
    {
        [Fact]
        public void MenuFilledUpCorrect()
        {
            List<MoveMenu> moveMenus = new List<MoveMenu>();
            List<MoveMenu> moveMenustest = new List<MoveMenu>();
            moveMenustest.Add(new MoveMenu()
            {
                Description = "Attack",
                No = 1
            });
            moveMenustest.Add(new MoveMenu()
            {
                Description = "Defense",
                No = 2
            });
            moveMenustest.Add(new MoveMenu()
            {
                Description = "Add health",
                No = 3
            });

            Program.FillMenu(moveMenus);

            Assert.Equal(moveMenustest[0].Description, moveMenus[0].Description);
            Assert.Equal(moveMenustest[1].Description, moveMenus[1].Description);
            Assert.Equal(moveMenustest[2].Description, moveMenus[2].Description);
        }

        [Fact]
        public void ComputerMovesFilledUpCorrect()
        {
            Player computer = new();

            Program.FillComputerMoves(computer);

            Assert.Equal(3, computer.CharacterMoves.Count);
            Assert.Contains(PlayerMoves.Attack, computer.CharacterMoves);
            Assert.Contains(PlayerMoves.Defense, computer.CharacterMoves);
            Assert.Contains(PlayerMoves.AddHealth, computer.CharacterMoves);
        }

        [Fact]
        public void MakeMovesCorrect()
        {
            Player player = new();
            int oldHealth = 100;
            Player computer = new();
            Program.FillComputerMoves(player);
            Program.FillComputerMoves(computer);

            Program.MakeMoves(player, computer, "Player", "Computer");
            Program.MakeMoves(computer, player, "Computer", "Player");

            Assert.Equal(3, computer.CharacterMoves.Count);
            Assert.Equal(3, player.CharacterMoves.Count);
            Assert.True(player.TotalHealth != oldHealth);
        }
    }
}