using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApi
{
    public partial class Form1 : Form
    {
        JobJson jobJson;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            jobJson = new JobJson();

            textBoxShouGetJson.Text = jobJson.GetDataJson(textBox1.Text);
            jobJson.SaveLinkJson(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            jobJson = new JobJson();
           label3.Text = jobJson.SaveChanges(textBoxShouGetJson.Text);
        }
    }
}
