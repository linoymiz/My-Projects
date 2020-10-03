using System;

// $G$ SFN-999 (-5) the board buttons should be disabled

// $G$ SFN-012 (+7) Bonus: Events in the Logic layer are handled by the UI.

// $G$ CSS-999 (-3) namespace name should be the same as the project name
namespace C18_Ex05
{
    public class Program
    {
        public static void Main()
        {
            SettingForm settingForm = new SettingForm();
            settingForm.ShowDialog();

            Game game = new Game(settingForm.getParameters());
            GameInterface gameInterface = new GameInterface(settingForm.getParameters(),game);

            gameInterface.ShowDialog();
        }

    }
}
