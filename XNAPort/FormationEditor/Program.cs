using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AnimationEngine;
using DisplayEngine;

namespace FormationEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MainEditorWindow mainWindow = new MainEditorWindow();
            mainWindow.Show();

            BaseGame.DrawSurface = mainWindow.GetGameWindowHandle();
            AnimationGame game = new AnimationGame();
            ((AnimationGame)game).Render = mainWindow.Render;
            game.Run();
        }
    }
}
