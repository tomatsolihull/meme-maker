﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Imaging;

namespace Meme_Maker
{
    public partial class Meme_Maker : Form
    {
        OpenFileDialog OpenImageFile;
        FontDialog FontPicker;
        Font LabelFont = new Font("Impact", 18);

        public Meme_Maker()
        {
            InitializeComponent();

            FunnyMeme.Controls.Add(TopLabel);
            FunnyMeme.Controls.Add(BottomLabel);

            TopLabel.Location = new Point(0, this.MenuStrip.Height * 2);
            BottomLabel.Location = new Point(0, this.Height - this.MenuStrip.Height);

            TopLabel.Font = BottomLabel.Font = LabelFont;

            FunnyMeme.SendToBack();
        }

        private void ChangeTopText(object sender, EventArgs e)
        {
            TopLabel.Text = TopTextInput.Text;
        }

        private void ChangeBottomText(object sender, EventArgs e)
        {
            BottomLabel.Text = BottomTextInput.Text;
        }

        private void OpenImagePrompt(object sender, EventArgs e)
        {
            OpenImageFile = new OpenFileDialog();
            OpenImageFile.Filter = "Common Image Files | *.jpg; *.jpeg; *.png; *.gif";
            OpenImageFile.Title = "Select a funny meme.";

            if(OpenImageFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FunnyMeme.Image = Image.FromFile(OpenImageFile.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem opening that funny meme :(", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void SaveImagePrompt(object sender, EventArgs e)
        {
            this.Height = this.Width = 1080;

            int OldLabelHeights = TopLabel.Height;
            float OldLabelSize = TopLabel.Font.SizeInPoints;
            TopLabel.Font = BottomLabel.Font = new Font("Impact", 100);
            TopLabel.Height = BottomLabel.Height = this.Height / 2;

            SaveFileDialog SaveDialog = new SaveFileDialog();
            SaveDialog.FileName = $"{TopLabel.Text} {BottomLabel.Text}";
            SaveDialog.DefaultExt = "png";
            SaveDialog.Filter = "PNGs | *.png";
            SaveDialog.ValidateNames = true;

            if(SaveDialog.ShowDialog() == DialogResult.OK)
            {
                int w = Convert.ToInt32(FunnyMeme.Width);
                int h = Convert.ToInt32(FunnyMeme.Height);
                Bitmap bmp = new Bitmap(w, h);

                FunnyMeme.DrawToBitmap(bmp, new Rectangle(0, 0, w, h));
                
                try
                {
                    bmp.Save(SaveDialog.FileName, ImageFormat.Png);
                } catch (Exception ex)
                {
                    MessageBox.Show("Problem saving your masterpiece :(",ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else { 
               // user cancelled save :(
            }

            this.Height = this.Width = 350;
            TopLabel.Font = BottomLabel.Font = LabelFont;
            TopLabel.Height = BottomLabel.Height = OldLabelHeights;
        }

        private void ColorPickerPrompt(object sender, EventArgs e)
        {
            if (ColourDialog.ShowDialog() == DialogResult.OK)
            {
                TopLabel.ForeColor = BottomLabel.ForeColor = ColourDialog.Color;
            }
        }

        private void ImageClick(object sender, EventArgs e)
        {

        }

        private void FontPickerDialog(object sender, EventArgs e)
        {
            FontPicker.ShowDialog();
            TopLabel.Font = BottomLabel.Font = FontPicker.font;
        }
    }
}
