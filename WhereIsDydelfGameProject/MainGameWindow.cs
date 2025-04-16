using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereIsDydelfGameProject
{
    //Most Important Form, it handles the rendering the window, settin the buttons, randomizing the dydelfs on the board, same with crocs, also creates timer for it.
    public partial class MainGameWindow : Form
    {
        //Fetches Info about Settings for later usage
        private GameSet Settings;
        //Gets the button form for later usage of the grid creation
        private Button[,] Button;
        //Check Time Being left on the main window, default is 30 seconds, counts it down and check if time is up
        private int TimeLeft;
        //Creates a Timer, works along with TimeLeft to ensure time works properly and is intigrated with the game logic
        private System.Windows.Forms.Timer Timer;
        //Creates a label for time that updates itself every tick
        private Label TimeLabel;
        //Creates a HashSet, holding the position of dydelfs around the board, can only hold one dydelf on one position
        private HashSet<Point> Dydelfs = new HashSet<Point>();
        //Creates a single point of board ( or no point at all when Croc=0 in Settings ) that stores a Crocodile, we dont need HashSet here, since its just one Croc or Zero
        private Point? Crocs = null;
        //Boolean function that checks if the crocodile is revealed or nah
        private bool IsRevealed = false;
        //Esentially a special Timer that starts when the Croc is revealed , gives 3 seconds to user before ending the game
        private System.Windows.Forms.Timer CrocTimer;
        //A special button being the one which holds the croc
        private Button CrocButton;
        //A counter of Found Dydelfs
        private int DydelfFoundCounter = 0;

        public MainGameWindow(GameSet Set)
        {
            InitializeComponent();
            Settings = Set; // Gettin info from Settings
            Start(); // Starts the game after Window creation ( from First window )
        }

        private void Start()
        {

            //Settin up some stuff for later usage like width/height, window size and activates creation of Grid/Timer/Entities Creation and Timer start.
            int Width = Settings.Width;
            int Height = Settings.Height;
            this.Text = "WHERE IS DYDELF?";
            //We kinda wants it to be auto,since the buttons are usually not very cooperative with flat values, and will do really nasty things if they are not updated constantly
            this.AutoSize = true;  
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            //CreatinGrid, aka. The main map with all buttons etc.

            CreateGrid(Width, Height);
            
            //Creatin Timer that will control if the game is over or nah

            CreateTimer();

            //Settin entities around the board

            SetEntities();

            //Startin the timer

            StartTimer();

        }

        //MAP_GENERATION_METHOD
        private void CreateGrid(int Width, int Height)
        {
            //Creatin the grid the map will be based on, has the num of columns/rows and the size bein auto

            TableLayoutPanel MapGrid = new TableLayoutPanel()
            {
                ColumnCount = Width,
                RowCount = Height,
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            //Settin the size of Cols/Rows to equel size based on percentage

            for(int i = 0; i < Width; i++)
            {
                MapGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100f/Width));
            }
            for(int j = 0; j < Height; j++)
            {
                MapGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / Height));
            }

            Button = new Button[Width, Height]; // Creatin a button later used as a base for grid

            //Time for Button Placement/Creation , so this whole thing below basically makes sure to properly place buttons around the board automatically

            for(int y = 0; y < Height;  y++)
            {
                for(int x = 0; x < Width; x++)
                {
                    Button Btn = new Button
                    {
                        Dock = DockStyle.Top,
                        BackColor = Color.Gray,
                        Tag = new Point(x, y)
                    };
                    Btn.Click += RevealButton;

                    Button[x,y] = Btn;
                    MapGrid.Controls.Add(Btn,x,y);
                }
            }
            this.Controls.Add(MapGrid);
        }

        //TIMER_GENERATION_METHOD

        private void CreateTimer()
        {
            TimeLabel = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,  
            };
            this.Controls.Add(TimeLabel);
        }

        //START_TIMER_METHOD

        private void StartTimer()
        {
            TimeLeft = Settings.Time;
            Timer = new System.Windows.Forms.Timer
            {
                Interval = 1000
            };
            Timer.Tick += timer_Tick;
            Timer.Start();
        }
        //TIMER_1_SECOND_TICK
        private void timer_Tick(object sender, EventArgs e)
        {
            TimeLeft--;

            TimeLabel.Text = $"TimeLeft : {TimeLeft}s ";
            if (TimeLeft <= 0)
            {
                Timer.Stop();
                EndGame(false,"Time's Up! Game Over.");
            }
        }

        //ENTITY_CREATOR_METHOD
        private void SetEntities()
        {
            Random rand = new Random();

            while(Dydelfs.Count < Settings.Dydelfs)
            {
                Point p = new Point(rand.Next(Settings.Width),rand.Next(Settings.Height));
                Dydelfs.Add(p);
            }

            if(Settings.Crocs > 0)
            {
                Point Croc;
                do
                {
                    Croc = new Point(rand.Next(Settings.Width), rand.Next(Settings.Height));
                } while (Dydelfs.Contains(Croc));

                Crocs = Croc;
            }
        }

        //BUTTON_REVEAL_MECHANIC

        private void RevealButton(object sender,EventArgs e)
        {
            Button ClickButton = sender as Button;

            Point ClickPoint = (Point)ClickButton.Tag;

            if(IsRevealed && CrocButton == ClickButton && ClickButton.Text == "Crocodile!")
            {
                CrocTimer.Stop();
                IsRevealed = false;
                ClickButton.Text = "";
                return;
            }
            ClickButton.Enabled = false;

            if (Dydelfs.Contains(ClickPoint))
            {
                ClickButton.BackColor = Color.Blue;
                ClickButton.Text = "Dydelf!";
                DydelfFoundCounter++;
                if(DydelfFoundCounter == Settings.Dydelfs) 
                {
                    Timer.Stop();
                    EndGame(true, "All Dydelfs Found!");
                }
            }
            else if(Crocs.HasValue && Crocs.Value == ClickPoint && !IsRevealed) 
            {
            
                ClickButton.BackColor = Color.Red;
                ClickButton.Text = "Crocodile!";
                IsRevealed = true;
                CrocButton = ClickButton;
                CrocButton.Enabled = true;
                CrocTimer = new System.Windows.Forms.Timer();
                CrocTimer.Interval = 3000;
                CrocTimer.Tick += CrocTimer_Tick;
                CrocTimer.Start();
            
            }
            else
            {
                ClickButton.BackColor = Color.LightGray;
            }
        }

        //CROC_TIMER_METHOD

        private void CrocTimer_Tick(object sender,EventArgs e)
        {
            CrocTimer.Stop();
            if (IsRevealed)
            {
                EndGame(false, "You Failed to unclick in time!");
            }
        }

        //END_GAME_LOOP_METHOD

        private void EndGame(bool End,string Message)
        {
            Timer?.Stop();
            CrocTimer?.Stop();

            foreach(Button btn in Button)
            {
                btn.Enabled = false;
            }
            //CrocRev = false;
            string Title = End ? "You Won!" : "You Lost!";
            MessageBox.Show(Message, Title);
            this.Close();
        }

        private void MainGameWindow_Load(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if(Timer != null)
            {
                Timer.Stop();
            }
            if(CrocTimer != null)
            {
                CrocTimer.Stop();
            }
        }
    }
}
