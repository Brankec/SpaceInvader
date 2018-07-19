namespace SpaceInvader
{
    class Program
    {
        static void Main(string[] args)
        {
            Barrier[] barriers = new Barrier[4]; // The original had 4 barriers
            InitializeBarriers(ref barriers); 
            Invader[,] invaders = new Invader[5, 11]; // The original had a grid of 5 x 11 invaders
            InitializeInvaders(ref invaders);

            Player player = Player.getInstance();

            Display display = Display.getInstance();
            display.init();
            while (display.isOpen())
            {
                display.clear(); // Clears the window from the previous display
                display.checkForEvents(); // Checks for events such as closing the window

                player.PlayerControls(); // Player controls

                display.drawPlayer(ref player); // Player rectangle being passed to draw
                display.update(); // Draws on the window from the buffer
            }
        }

        static void InitializeInvaders(ref Invader[,] invaders) // Inistializing all the invaders
        {
            for(int i = 0; i < invaders.GetLength(0); i++)
            {
                for (int j = 0; j < invaders.GetLength(1); j++)
                {
                    invaders[i, j] = new Invader();
                }
            }
        }
        static void InitializeBarriers(ref Barrier[] barriers) // Inistializing all the barriers
        {
            for (int i = 0; i < barriers.Length; i++)
            {
                barriers[i] = new Barrier();
            }
        }
    }
}
