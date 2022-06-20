using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SDD_Major_Project___Liam_Vancew
{
    public partial class Form1 : Form
    {
        bool MacroButtonsVisible = new bool();
        int[,] MacroDiceCount = new int[2, 5];
        int[] MacroBonus = new int[5];
        int[,] MacroDiceMax = new int[2, 5];
        int RenamedMacro = new int();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load Macros
            //Creating Temp Variables for loading
            //Code Is Incomplete, Cannot Finish On Time. Commented Out For Efficiency.

            // string Macro1FileName = @"C:\Users\liam\Documents\Visual Studio Files\SDD Major Project - Liam Vancew\Macro1.txt";
            // string Macro2FileName = @"C:\Users\liam\Documents\Visual Studio Files\SDD Major Project - Liam Vancew\Macro2.txt";
            // string Macro3FileName = @"C:\Users\liam\Documents\Visual Studio Files\SDD Major Project - Liam Vancew\Macro3.txt";
            // string Macro4FileName = @"C:\Users\liam\Documents\Visual Studio Files\SDD Major Project - Liam Vancew\Macro4.txt";
            // string Macro5FileName = @"C:\Users\liam\Documents\Visual Studio Files\SDD Major Project - Liam Vancew\Macro5.txt";

            //Load Text Values for Macro Demonstration
            Macro1.Text = "Macro1";
            Macro2.Text = "Test Macro";
            Macro3.Text = "Final Macro";

            //Hide Macro Screen
            Size = new Size(816, 489);
            //Init Buttons for showing/hiding
            var MacroButtons = new[] { Macro1, Macro2, Macro3, Macro4, Macro5, RenameMacro1, RenameMacro2, RenameMacro3, RenameMacro4, RenameMacro5, ModifyMacro1, ModifyMacro2, ModifyMacro3, ModifyMacro4, ModifyMacro5, CloseMacrosButton, CancelRenamingButton, RenameMacroButton };
            //Hide MacroButtons
            for (int i = 0; i < MacroButtons.Length; i++)
            {
                MacroButtons[i].Visible = false;
            }
            MacroButtonsVisible = false;
            RenameInputField.Visible = false;
            RenameMacroLabel.Visible = false;
            //Load Images
            D4PictureBox.Image = Image.FromFile(@"C:\Users\liam\Documents\Visual Studio Files\SDD Major Project - Liam Vancew\D4.png");
            D6PictureBox.Image = Image.FromFile(@"C:\Users\liam\Documents\Visual Studio Files\SDD Major Project - Liam Vancew\D6.png");
            D8PictureBox.Image = Image.FromFile(@"C:\Users\liam\Documents\Visual Studio Files\SDD Major Project - Liam Vancew\D8.png");
            D10PictureBox.Image = Image.FromFile(@"C:\Users\liam\Documents\Visual Studio Files\SDD Major Project - Liam Vancew\D10.png");
            D12PictureBox.Image = Image.FromFile(@"C:\Users\liam\Documents\Visual Studio Files\SDD Major Project - Liam Vancew\D12.png");
            D20PictureBox.Image = Image.FromFile(@"C:\Users\liam\Documents\Visual Studio Files\SDD Major Project - Liam Vancew\D20.png");
        }

        private void DisplayRolledNumber(int BonusLocal, int RolledDice, int UsedButtonMax, int EndValue, int[] RolledNumbersArray)
        {
            //Displays final result
            ResultsListBox.Items.Add("You rolled " + RolledDice + "d" + UsedButtonMax + " + " + BonusLocal);
            //Displays the individual numbers rolled.
            int TotalShown = 0;
            string DisplayNumbers = "";
            //Causes the text to not display if there is only one dice with 0 bonus rolled.
            if (RolledDice > 1 || BonusLocal != 0)
            {
                while (TotalShown < RolledNumbersArray.Length)
                {
                    //Displays the correct text, preventing the system from ending with ", "
                    if (TotalShown != RolledNumbersArray.Length - 1)
                    {
                        DisplayNumbers = DisplayNumbers + RolledNumbersArray[TotalShown] + ", ";
                    }
                    else
                    {
                        DisplayNumbers = DisplayNumbers + RolledNumbersArray[TotalShown];
                    }
                    TotalShown += 1;
                }
                ResultsListBox.Items.Add(DisplayNumbers);
            }
            ResultsListBox.Items.Add("For a total of: " + EndValue);
        }

        public int RollDice(int BonusLocal, int RolledDice, int UsedButtonMax, bool IsPositiveOrNegative)
        {
            //Init Variables
            int EndValue = 0;
            int TotalRolledDice = 0;
            int[] RolledNumbers = new int[RolledDice];
            int TempIntStore = 0;
            //Sample Code for Testing
            //Rolls a number of random numbers equal to RolledDice
            //Uses TempIntStore to keep the current rolled number until the next loop.
            while (TotalRolledDice < RolledDice)
            {
                TempIntStore = RandomNumber(UsedButtonMax);
                EndValue += TempIntStore;
                RolledNumbers[TotalRolledDice] = TempIntStore;
                TotalRolledDice += 1;
            }
            //Adds the user input bonus
            if (IsPositiveOrNegative == true)
            {
                EndValue += BonusLocal;
            }
            else
            {
                EndValue -= BonusLocal;
            }
            DisplayRolledNumber(BonusLocal, RolledDice, UsedButtonMax, EndValue, RolledNumbers);
            return EndValue;
        }

        private void DisplayRolledNumberTemp(int RolledNumber, int CurrentMax)
        {
            //Temporary Testing Code for Random Numbers
            //Shows the invidual number rolled as well as the current max of each step.
            ResultsListBox.Items.Add("You rolled a " + RolledNumber);
            ResultsListBox.Items.Add("Your current result is: " + CurrentMax);
        }
        public int RandomNumber(int UsedButtonMaxLocal)
        {
            //Rolls a random number between max and min
            int Output = 0;
            //Generates a random hashmap for the seed.
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            //Outputs the final number
            //Max is exclusive, used +1 for max.
            Output = rand.Next(1, (UsedButtonMaxLocal + 1));
            return Output;
        }
        //The following sets of code are individual lines to run each button's appropriate RollDice function.
        private void RollD4Button_Click(object sender, EventArgs e)
        {
            if (D4IsPositive.Checked == true)
            {
                RollDice(Convert.ToInt32(D4ModifierField.Value), Convert.ToInt32(D4RollCount.Value), 4, true);
            }
            else
            {
                RollDice(Convert.ToInt32(D4ModifierField.Value), Convert.ToInt32(D4RollCount.Value), 4, false);
            }

        }

        private void RollD6Button_Click(object sender, EventArgs e)
        {
            if (D6IsPositive.Checked == true)
            {
                RollDice(Convert.ToInt32(D6ModifierField.Value), Convert.ToInt32(D6RollCount.Value), 6, true);
            }
            else
            {
                RollDice(Convert.ToInt32(D6ModifierField.Value), Convert.ToInt32(D6RollCount.Value), 6, false);
            }
        }

        private void RollD8Button_Click(object sender, EventArgs e)
        {
            if (D8IsPositive.Checked == true)
            {
                RollDice(Convert.ToInt32(D8ModifierField.Value), Convert.ToInt32(D8RollCount.Value), 8, true);
            }
            else
            {
                RollDice(Convert.ToInt32(D8ModifierField.Value), Convert.ToInt32(D8RollCount.Value), 8, false);
            }
        }

        private void RollD10Button_Click(object sender, EventArgs e)
        {
            if (D10IsPositive.Checked == true)
            {
                RollDice(Convert.ToInt32(D10ModifierField.Value), Convert.ToInt32(D10RollCount.Value), 10, true);
            }
            else
            {
                RollDice(Convert.ToInt32(D10ModifierField.Value), Convert.ToInt32(D10RollCount.Value), 10, false);
            }
        }

        private void RollD12Button_Click(object sender, EventArgs e)
        {
            if (D12IsPositive.Checked == true)
            {
                RollDice(Convert.ToInt32(D12ModifierField.Value), Convert.ToInt32(D12RollCount.Value), 12, true);
            }
            else
            {
                RollDice(Convert.ToInt32(D12ModifierField.Value), Convert.ToInt32(D12RollCount.Value), 12, false);
            }
        }

        private void RollD20Button_Click(object sender, EventArgs e)
        {
            if (D20IsPositive.Checked == true)
            {
                RollDice(Convert.ToInt32(D20ModifierField.Value), Convert.ToInt32(D20RollCount.Value), 20, true);
            }
            else
            {
                RollDice(Convert.ToInt32(D20ModifierField.Value), Convert.ToInt32(D20RollCount.Value), 20, false);
            }
        }

        private void RollCustomDiceButton_Click(object sender, EventArgs e)
        {
            //Checks if the user has set the max to greater than 1
            if (CustomDiceMaxValueInput.Value >= 1)
            {
                if (CustomDiceIsPositive.Checked == true)
                {
                    RollDice(Convert.ToInt32(CustomDiceModifierField.Value), Convert.ToInt32(CustomDiceRollCount.Value), Convert.ToInt32(CustomDiceMaxValueInput.Value), true);
                }
                else
                {
                    RollDice(Convert.ToInt32(CustomDiceModifierField.Value), Convert.ToInt32(CustomDiceRollCount.Value), Convert.ToInt32(CustomDiceMaxValueInput.Value), false);
                }
            }
            else
            //Prevents the user from running d0s, which will return a value of 1.
            {
                MessageBox.Show("You cannot roll a dice with a max number of 0.");
            }
        }

        private void ResetListBoxButton_Click(object sender, EventArgs e)
        {
            //Clears the listbox
            ResultsListBox.Items.Clear();
        }

        private void OpenMacrosButton_Click(object sender, EventArgs e)
        {
            //Init Buttons for showing/hiding
            var MacroButtons = new[] { Macro1, Macro2, Macro3, Macro4, Macro5, RenameMacro1, RenameMacro2, RenameMacro3, RenameMacro4, RenameMacro5, ModifyMacro1, ModifyMacro2, ModifyMacro3, ModifyMacro4, ModifyMacro5, CloseMacrosButton };
            if (MacroButtonsVisible == false)
            {
                for (int i = 0; i < MacroButtons.Length; i++)
                {
                    MacroButtons[i].Visible = true;
                    Size = new Size(1353, 489);
                    MacroButtonsVisible = true;
                }
            }
        }

        private void CloseMacrosButton_Click(object sender, EventArgs e)
        {
            //Init Buttons for showing/hiding
            var MacroButtons = new[] { Macro1, Macro2, Macro3, Macro4, Macro5, RenameMacro1, RenameMacro2, RenameMacro3, RenameMacro4, RenameMacro5, ModifyMacro1, ModifyMacro2, ModifyMacro3, ModifyMacro4, ModifyMacro5, CloseMacrosButton, CancelRenamingButton, RenameMacroButton };
            for (int i = 0; i < MacroButtons.Length; i++)
            {
                MacroButtons[i].Visible = false;
                Size = new Size(816, 489);
                MacroButtonsVisible = false;
            }
            RenameInputField.Visible = false;
            RenameMacroLabel.Visible = false;
        }

        private void OpenRenameMacro(string MacroLabelText, int NewMacro)
        {
            //Shows all dialogs required
            CancelRenamingButton.Visible = true;
            RenameMacroButton.Visible = true;
            RenameInputField.Visible = true;
            RenameMacroLabel.Visible = true;
            //Modifies the text field and global variable to match.
            RenameMacroLabel.Text = MacroLabelText;
            RenameInputField.Text = "";
            RenamedMacro = NewMacro;
        }

        private void RenameMacro1_Click(object sender, EventArgs e)
        {
            OpenRenameMacro("Rename Macro 1", 1);
        }

        private void RenameMacro2_Click(object sender, EventArgs e)
        {
            OpenRenameMacro("Rename Macro 2", 2);
        }

        private void RenameMacro3_Click(object sender, EventArgs e)
        {
            OpenRenameMacro("Rename Macro 3", 3);
        }

        private void RenameMacro4_Click(object sender, EventArgs e)
        {
            OpenRenameMacro("Rename Macro 4", 4);
        }

        private void RenameMacro5_Click(object sender, EventArgs e)
        {
            OpenRenameMacro("Rename Macro 5", 5);
        }

        private void CancelRenamingButton_Click(object sender, EventArgs e)
        {
            //Hides all renaming dialogs
            CancelRenamingButton.Visible = false;
            RenameMacroButton.Visible = false;
            RenameInputField.Visible = false;
            RenameMacroLabel.Visible = false;
            //Resets text to be safe
            RenameInputField.Text = "";
        }

        private void Macro1_Click(object sender, EventArgs e)
        {
            //Sample Creation For Demonstration
            MacroDiceCount[0, 1] = 5;
            MacroBonus[0] = 5;
            MacroDiceMax[0, 1] = 10;
            MacroDiceCount[1, 1] = 10;
            MacroBonus[1] = 7;
            MacroDiceMax[1, 1] = 20;
            int MacroTotal = new int();
            //Displays each dice separately, then displays a total
            for (int i = 0; i < MacroDiceCount.GetLength(0); i++)
            {
                MacroTotal += RollDice(MacroBonus[i], MacroDiceCount[i, 1], MacroDiceMax[i, 1], true);
            }
            ResultsListBox.Items.Add("Macro 1 Total: " + MacroTotal);
        }

        private void Macro2_Click(object sender, EventArgs e)
        {
            //Sample Creation For Demonstration
            MacroDiceCount[0, 2] = 10;
            MacroBonus[0] = 2;
            MacroDiceMax[0, 2] = 4;
            MacroDiceCount[1, 2] = 6;
            MacroBonus[1] = 0;
            MacroDiceMax[1, 2] = 10;
            int MacroTotal = new int();
            //Displays each dice separately, then displays a total
            for (int i = 0; i < MacroDiceCount.GetLength(0); i++)
            {
                MacroTotal += RollDice(MacroBonus[i], MacroDiceCount[i, 2], MacroDiceMax[i, 2], true);
            }
            ResultsListBox.Items.Add("Macro 2 Total: " + MacroTotal);
        }

        private void Macro3_Click(object sender, EventArgs e)
        {
            //Sample Creation For Demonstration
            MacroDiceCount[0, 3] = 8;
            MacroBonus[0] = 0;
            MacroDiceMax[0, 3] = 6;
            MacroDiceCount[1, 3] = 10;
            MacroBonus[1] = 7;
            MacroDiceMax[1, 3] = 20;
            int MacroTotal = new int();
            //Displays each dice separately, then displays a total
            for (int i = 0; i < MacroDiceCount.GetLength(0); i++)
            {
                MacroTotal += RollDice(MacroBonus[i], MacroDiceCount[i, 3], MacroDiceMax[i, 3], true);
            }
            ResultsListBox.Items.Add("Macro 3 Total: " + MacroTotal);
        }

        private void Macro4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You cannot run an empty macro.");
        }

        private void RenameMacroButton_Click(object sender, EventArgs e)
        {
            //Prevents empty dialogs
            if (RenameInputField.Text != "")
            {
                //Switch Statement for each macro
                if (RenamedMacro == 1)
                {
                    Macro1.Text = RenameInputField.Text;
                    CancelRenamingButton_Click(sender, e);
                }
                else if (RenamedMacro == 2)
                {
                    Macro2.Text = RenameInputField.Text;
                    CancelRenamingButton_Click(sender, e);
                }
                else if (RenamedMacro == 3)
                {
                    Macro3.Text = RenameInputField.Text;
                    CancelRenamingButton_Click(sender, e);
                }
                else if(RenamedMacro == 4)
                {
                    Macro4.Text = RenameInputField.Text;
                    CancelRenamingButton_Click(sender, e);
                }
                else if(RenamedMacro == 5)
                {
                    Macro5.Text = RenameInputField.Text;
                    CancelRenamingButton_Click(sender, e);
                }
            }
            else
            {
                MessageBox.Show("You cannot rename a macro to an empty macro");
            }
        }

        private void Macro5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You cannot run an empty macro.");
        }

        //Sample Code For Changing Macros
        //MacroDiceCount[x, y] = 10;
        //MacroBonus[x] = 2;
        //MacroDiceMax[x, y] = 4;
        //Changing the x value changes which dice is rolled.
        //Changing the y value changes which macro is rolled.
        //Currently not setup to automatically change, dimensions only use x = 2.
    }
}