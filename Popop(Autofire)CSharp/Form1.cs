
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;



namespace Popop_Autofire_CSharp
{
    
    

    public partial class Form1 : Form
    {
///////////////*Dllimport*/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]//PostMessage
        static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("gdi32.dll")]
        static extern int GetPixel(IntPtr hdc, int nXPos, int nYPos);
        [DllImport("kernel32.dll")]
        static extern void Sleep(uint dwMilliseconds);
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey); 
///////////////*Dllimport(END)*////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
   



        public Form1()
        {
            InitializeComponent();
        }

        ///////////////*Variables*/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint WM_LBUTTONDOWN = 0x201;
        const uint WM_LBUTTONUP = 0x202;
        IntPtr hWindowHandle = IntPtr.Zero;
        uint iShotDelay = 20;
        int iKeyStatus = 0;
        int iKeyStatus2 = 0;
        int iKeyStatusC = 0;
        int i = 0;
        bool combo1 = false, combo2 = false, combo3 = false;
        IntPtr hdc;
        int _color;
        int red, green, blue;
        Color colorRGB;
        //////////////////////////*Variables(END)*//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void timer1_Tick(object sender, EventArgs e)
        {
            if(checkBox2.Checked==false){

			
			   hdc =GetDC(IntPtr.Zero);
                if(checkBox3.Checked==false)
         _color = GetPixel(hdc, Convert.ToInt32(textBox4.Text), Convert.ToInt32(textBox5.Text));//960 540
                else _color = GetPixel(hdc, Convert.ToInt32(Cursor.Position.X), Convert.ToInt32(Cursor.Position.Y));
         ReleaseDC(IntPtr.Zero, hdc);
         colorRGB = Color.FromArgb((_color & 0x000000FF), (_color & 0x0000FF00) >> 8, (_color & 0x00FF0000) >> 16);
         red   = colorRGB.R;
         green = colorRGB.G;
         blue  = colorRGB.B;


        if(Convert.ToString(comboBox1.SelectedItem) == "="){ if (red == Convert.ToInt32(textBox1.Text)) combo1 = true; else combo1 = false; }
		if(Convert.ToString(comboBox1.SelectedItem) == ">"){  if(red>Convert.ToInt32(textBox1.Text)) combo1=true; else combo1=false;  }
		if(Convert.ToString(comboBox1.SelectedItem) == "<"){  if(red<Convert.ToInt32(textBox1.Text)) combo1=true; else combo1=false;  }

		if(Convert.ToString(comboBox2.SelectedItem) == "="){	 if(green==Convert.ToInt32(textBox2.Text)) combo2=true; else combo2=false; }
		if(Convert.ToString(comboBox2.SelectedItem) == ">"){  if(green>Convert.ToInt32(textBox2.Text)) combo2=true; else combo2=false;  }
		if(Convert.ToString(comboBox2.SelectedItem) == "<"){  if(green<Convert.ToInt32(textBox2.Text)) combo2=true; else combo2=false;  }

		if(Convert.ToString(comboBox3.SelectedItem) == "="){	 if(blue==Convert.ToInt32(textBox3.Text)) combo3=true; else combo3=false; }
		if(Convert.ToString(comboBox3.SelectedItem) == ">"){  if(blue>Convert.ToInt32(textBox3.Text)) combo3=true; else combo3=false;  }
		if(Convert.ToString(comboBox3.SelectedItem) == "<"){  if(blue<Convert.ToInt32(textBox3.Text)) combo3=true; else combo3=false;  }


             

		 if (combo1==true && combo2==true && combo3==true)
		 {
			 
			 if( checkBox1.Checked==false){
			 mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
			 Sleep(iShotDelay);
			 mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
			 //Sleep(iShotDelay);
			 }
			 
			 else{
             PostMessage(hWindowHandle, WM_LBUTTONDOWN, IntPtr.Zero, IntPtr.Zero);
			 Sleep(iShotDelay);
             PostMessage(hWindowHandle, WM_LBUTTONUP, IntPtr.Zero, IntPtr.Zero);
			 // Sleep(iShotDelay);	 
			 }
		 }
		}
		else
		{
		if( checkBox1.Checked==false){
			 mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
			 Sleep(iShotDelay);
			 mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
		    //Sleep(iShotDelay);
		}
			 
			 else{
             PostMessage(hWindowHandle, WM_LBUTTONDOWN, IntPtr.Zero, IntPtr.Zero);
			 Sleep(iShotDelay);
             PostMessage(hWindowHandle, WM_LBUTTONUP, IntPtr.Zero, IntPtr.Zero); 
			   //Sleep(iShotDelay);
			 }
		}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
			 if(this.textBox1.ReadOnly == true&& this.textBox2.ReadOnly == true && this.textBox3.ReadOnly ==true && this.textBox4.ReadOnly == true && this.textBox5.ReadOnly ==true && this.textBox6.ReadOnly == true &&  this.textBox7.ReadOnly == true &&	 this.textBox8.ReadOnly == true){
			 textBox9.Text="Stop program!";
				 
			 }
				 
				 
			 else{
				 if(checkBox1.Checked==true && textBox8.Text!=""){
				 string czWindowTitle = textBox8.Text;
                 hWindowHandle = FindWindowByCaption(IntPtr.Zero , czWindowTitle);
					if(hWindowHandle!=IntPtr.Zero)
						textBox9.Text="Success!!!";
			        else
                    { textBox9.Text = "Failure"; hWindowHandle = IntPtr.Zero; }
			                                                    }else 
																	if(checkBox1.Checked==false) 
																		textBox9.Text="Not checked!"; 
																	else textBox9.Text="Field empty!";
		 }
        
        
        
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                this.textBox8.ReadOnly = false;

            else
                this.textBox8.ReadOnly = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                this.textBox1.ReadOnly = true;
                this.textBox2.ReadOnly = true;
                this.textBox3.ReadOnly = true;
                this.textBox4.ReadOnly = true;
                this.textBox5.ReadOnly = true;
                this.comboBox1.Enabled = false;
                this.comboBox2.Enabled = false;
                this.comboBox3.Enabled = false;
            }
            else
            {
                this.textBox1.ReadOnly = false;
                this.textBox2.ReadOnly = false;
                this.textBox3.ReadOnly = false;
                this.textBox4.ReadOnly = false;
                this.textBox5.ReadOnly = false;
                this.comboBox1.Enabled = true;
                this.comboBox2.Enabled = true;
                this.comboBox3.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
             openFileDialog1.FileName =System.IO.Directory.GetCurrentDirectory();
				openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
				 if ( openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK )
      { 
				 
			 
				 String[] openarr = System.IO.File.ReadAllLines(openFileDialog1.FileName);
							
				 if(openarr.LongLength>=7 && openarr.LongLength<=8){
					textBox1.Text=openarr[0];
					textBox2.Text=openarr[1];
					textBox3.Text=openarr[2];
					textBox4.Text=openarr[3];
					textBox5.Text=openarr[4];
					textBox6.Text=openarr[5];
					textBox7.Text=openarr[6];			
				 }else MessageBox.Show("Wrong file. Try another one.","Error: bad file", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				 if(openarr.LongLength==8)
					textBox8.Text=openarr[7];

                

		 }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            iKeyStatus = GetAsyncKeyState(System.Windows.Forms.Keys.F11);
            iKeyStatus2 = GetAsyncKeyState(System.Windows.Forms.Keys.F12);
            iKeyStatusC = GetAsyncKeyState(System.Windows.Forms.Keys.ControlKey);

            if (iKeyStatus != 0 && iKeyStatusC != 0)
            {
                radioButton2.PerformClick();
            }

            if (iKeyStatus2 != 0 && iKeyStatusC != 0)
            {
                radioButton3.PerformClick();
            }

        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if(this.checkBox1.Checked==true && (hWindowHandle==IntPtr.Zero || textBox8.Text=="")){
	radioButton3.Checked=true;
	MessageBox.Show("You need to get handle of target window.","Error: Handle",MessageBoxButtons.OK, MessageBoxIcon.Hand);
	textBox9.Text="";	}
else{
	 
			 if(checkBox2.Checked==false && (textBox1.Text=="" || textBox2.Text=="" || textBox3.Text=="" || textBox4.Text=="" || textBox5.Text==""|| textBox6.Text==""|| textBox7.Text=="")){
				 radioButton2.Checked=false;
				
				 MessageBox.Show("Enter RGB values(use \"Whithout color\"), Delay and Coord.","Error",MessageBoxButtons.OK, MessageBoxIcon.Hand);
				 radioButton3.Checked=true;
				 }
				 else	{
					 if(textBox6.Text=="")  textBox6.Text="20";
					  if(textBox7.Text=="")  textBox7.Text="5";
					 this.checkBox1.Enabled=false;
					 this.checkBox2.Enabled=false;
					 this.button1.Enabled=false;
					  this.button4.Enabled=false;
					   this.button5.Enabled=false;
					   this.fileToolStripMenuItem.Enabled=false;
					 this.comboBox1.Enabled=false;
					 this.comboBox2.Enabled=false;
					 this.comboBox3.Enabled=false;
					  this.textBox1.ReadOnly = true;
					   this.textBox2.ReadOnly = true;
					    this.textBox3.ReadOnly = true;
						 this.textBox4.ReadOnly = true;
						  this.textBox5.ReadOnly = true;
						   this.textBox6.ReadOnly = true;
						    this.textBox7.ReadOnly = true;
							 this.textBox8.ReadOnly = true;
					  textBox9.Text="";
				 timer1.Interval = Convert.ToInt32(textBox6.Text);
				  iShotDelay=Convert.ToUInt32(textBox7.Text);
				 timer1.Start();
				 System.Media.SystemSounds.Asterisk.Play();}
			 
		 }
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
              
					  timer1.Stop();

			 System.Media.SystemSounds.Hand.Play();
			  this.checkBox1.Enabled=true;
			  this.checkBox2.Enabled=true;
			  this.button1.Enabled=true;
			  this.button4.Enabled=true;
			  this.button5.Enabled=true;
			  this.fileToolStripMenuItem.Enabled=true;
			  
			  this.textBox6.ReadOnly = false;
						    this.textBox7.ReadOnly = false;

							if(checkBox2.Checked==false){
								this.comboBox1.Enabled=true;
								this.comboBox2.Enabled=true;
								this.comboBox3.Enabled=true;
								this.textBox1.ReadOnly = false;
								this.textBox2.ReadOnly = false;
								this.textBox3.ReadOnly = false;
								this.textBox4.ReadOnly = false;
								this.textBox5.ReadOnly = false;	
							
							}
			 
			 
							 if(checkBox1.Checked==true)			 					  
								this.textBox8.ReadOnly = false;
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("RGB - pixel color when you need to click\n(example: sight turns red when the aim at the enemy).\n\nX Y - the coordinates of the pixel\n(example: the central point of sight).\n\nTimer - delay between iterations.\n\nShot - delay before releasing LMB.\n\nIf the program is not working in the game, you need to use PostMessage.\nEnter title of window and press \"Get Handle\".\n\nWhen all fields are filled in, click Start(Ctrl+F11). To stop, press the Stop(Ctrl+F12).\n\nIf you want to load settings from a *.txt file, it should look:\n\nR color(0-255)\nG color(0-255)\nB color(0-255)\nX coord(0-max screen width)\nY coord(0-max screen height)\nTimer delay(mc)\nShot delay(mc)\nTitle of window(not necessary)", "About Popop(Autofire)", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Popop(Autofire) v3.0 C#\n\nCopyright© 2015 by Untiy16 Productions.\nWritten by M. Glushchenko.\n\ne-mail: untiy16@gmail.com", "About Popop(Autofire)", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            textBox4.Text = (SystemInformation.PrimaryMonitorSize.Width/2).ToString();
            textBox5.Text = (SystemInformation.PrimaryMonitorSize.Height/2).ToString();

            timer1.Enabled = false;
    timer2.Enabled = false;
		

 timer2.Interval =1000;
 timer2.Start();			
			 comboBox1.SelectedItem = "=";
			 comboBox2.SelectedItem = "=";
			 comboBox3.SelectedItem = "=";
			  String[] args= Environment.GetCommandLineArgs();	         
		 
		
			if(args.LongLength>=8){			 
			 textBox1.Text=args[1];
			 textBox2.Text=args[2];
			 textBox3.Text=args[3];
			 textBox4.Text=args[4];
			 textBox5.Text=args[5];
			 textBox6.Text=args[6];
			 textBox7.Text=args[7];
				if(args.LongLength>9){	
				 textBox8.Text=args[8];
				 checkBox1.Checked=true;
				 button1.PerformClick();

				 if (args[9]=="=")  comboBox1.SelectedItem = "=";
				 if (args[9]==">")  comboBox1.SelectedItem = ">";
				 if (args[9]=="<")  comboBox1.SelectedItem = "<";
				 
				 if (args[10]=="=")  comboBox2.SelectedItem = "=";
				 if (args[10]==">")  comboBox2.SelectedItem = ">";
				 if (args[10]=="<")  comboBox2.SelectedItem = "<";

				 if (args[11]=="=")  comboBox3.SelectedItem = "=";
				 if (args[11]==">")  comboBox3.SelectedItem = ">";
				 if (args[11]=="<")  comboBox3.SelectedItem = "<";
				  
			}
			}



				if(args.LongLength==13)	
					if(args[12]=="Start")
						radioButton2.PerformClick();

						
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4.PerformClick();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button5.PerformClick();
        }

        private void button5_Click(object sender, EventArgs e)
        {
             		
			 String[] savearr = { textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text};
			
			 saveFileDialog1.FileName =System.IO.Directory.GetCurrentDirectory();
				saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
				 if ( saveFileDialog1.ShowDialog() == DialogResult.OK )			 
					System.IO.File.WriteAllLines(saveFileDialog1.FileName, savearr);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                this.textBox4.ReadOnly = true;
                this.textBox5.ReadOnly = true;
            }
            else
            {
                this.textBox4.ReadOnly = false;
                this.textBox5.ReadOnly = false;
            }
        }
    
    
    
    
    
    
    }
}
