﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace clear_pictures
{
    public partial class Form1 : Form
    {
       

        bool i = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;

        }


        public void OrganizeFiles(int comboBoxIndex, bool shouldDelete)
        {
            
                string user = Environment.UserName;
                string path = $@"C:\\Users\\{user}\\Downloads";
                string pathPng = $@"C:\\Users\\{user}\\Dokumente\\PNG-Ordner";
                string pathJpg = $@"C:\\Users\\{user}\\Dokumente\\JPG-Ordner";
                string pathMp4 = $@"C:\\Users\\{user}\\Dokumente\\MP4-Ordner";

                string[] extensions = { ".jpg", ".png", ".mp4" };
                string selectedExtension = extensions[comboBoxIndex];

                var imageFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(selectedExtension));

                string selectedPath = string.Empty;

                if (!shouldDelete)
                {
                    if (comboBoxIndex == 0)
                    {
                        selectedPath = pathJpg;
                    }
                    else if (comboBoxIndex == 1)
                    {
                        selectedPath = pathPng;
                    }
                    else if (comboBoxIndex == 2)
                    {
                        selectedPath = pathMp4;
                    }

                    if (!Directory.Exists(selectedPath))
                    {
                        Directory.CreateDirectory(selectedPath);
                    }

                    foreach (var imageFile in imageFiles)
                    {
                        var destinationPath = Path.Combine(selectedPath, Path.GetFileName(imageFile));
                        File.Move(imageFile, destinationPath);
                    }
                }
                else
                {
                    if (comboBoxIndex == 0)
                    {
                        selectedPath = pathJpg;
                    }
                    else if (comboBoxIndex == 1)
                    {
                        selectedPath = pathPng;
                    }
                    else if (comboBoxIndex == 2)
                    {
                        selectedPath = pathMp4;
                    }

                    if (Directory.Exists(selectedPath))
                    {
                        DirectoryInfo di = new DirectoryInfo(selectedPath);

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir in di.GetDirectories())
                        {
                            dir.Delete(true);
                        }
                    }
                }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            i = false;
            OrganizeFiles(comboBox1.SelectedIndex,i);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            i  = true;
            OrganizeFiles(comboBox1.SelectedIndex, i);
        }
    }
}
