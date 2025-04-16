namespace WhereIsDydelfGameProject
{
    public partial class Form1 : Form
    {
        //Gettin Settings
        GameSet Set = new GameSet();
        public Form1()
        {
            InitializeComponent();
        }
        
        //Start Button, Creates the window from the other Form that has all the renderin goin on in the background ( Entity Creation, Grid Creation etc. )
        private void button1_Click(object sender, EventArgs e)
        {
            MainGameWindow newWindow = new MainGameWindow(Set);

            newWindow.Show();
        }
        //Settings Button, Allows the User to change settings, default Settings are always in use if not changed by the user

        private void button2_Click(object sender, EventArgs e)
        {
            SettingsControlPanel NewSet = new SettingsControlPanel(Set);
            NewSet.Show();
        }

        //Exit Button, Ends the program, simple

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
