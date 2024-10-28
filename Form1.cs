using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFrmCaluladora
{
    public partial class Calculadora : Form
    {
        public Calculadora()
        {
            InitializeComponent();
        }

        private string memoria = "";
        private Double res = 0;
        private char[] delimiterChars = { '+', '-', '/', '*' };

        private void Calculadora_Load(object sender, EventArgs e)
        {

        }

        private void btn2_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            txtBox.Text = txtBox.Text + b.Text;
        }

        // Botón "=" Resultado
        private void btn16_Click(object sender, EventArgs e)
        {

            string[] num = txtBox.Text.Split(delimiterChars);

            // Control de Excepciones (txtBox vacio; falta de un operando; resultado negativo)
            if(num.Length < 2 || txtBox.Text.Length == 0 || delimiterChars.Contains(txtBox.Text.Last()) || txtBox.Text.StartsWith("-"))
            {
                MessageBox.Show("Operación no valida");
                return;
            }

            // Operadores
            if (txtBox.Text.Contains(delimiterChars[0]))
            {
                res = Convert.ToDouble(num[0]) + Convert.ToDouble(num[1]);
                txtBox.Text = "";
                txtBox.Text = txtBox.Text + res;
            }

            else if (txtBox.Text.Contains(delimiterChars[1]))
            {
                res = Convert.ToDouble(num[0]) - Convert.ToDouble(num[1]);
                txtBox.Text = "";
                txtBox.Text = txtBox.Text + res;
            }

            else if (txtBox.Text.Contains(delimiterChars[2])) 
            {
                if (num[1] == "0")
                {
                    txtBox.Text = "Infinito";
                }
                else
                {
                    res = Convert.ToDouble(num[0]) / Convert.ToDouble(num[1]);
                    txtBox.Text = "";
                    txtBox.Text = txtBox.Text + res;
                }
            }

            else if (txtBox.Text.Contains(delimiterChars[3]))
            {
                res = Convert.ToDouble(num[0]) * Convert.ToDouble(num[1]);
                txtBox.Text = "";
                txtBox.Text = txtBox.Text + res;
            }
        }

        // Botón C
        private void btn22_Click(object sender, EventArgs e)
        {
            txtBox.Text = txtBox.Text.Remove(0,txtBox.Text.Length);
        }

        // Botón CE
        private void btn21_Click(object sender, EventArgs e)
        {
            if(txtBox.Text.Length == 0)
            {
                txtBox.Text = "";
            }else
            {
                txtBox.Text = txtBox.Text.Remove(txtBox.Text.Length - 1);
            }
        }

        // Botones Memoria
        // MS
        private void btn15_Click(object sender, EventArgs e)
        {
            // Comprobación guardar solo operaciones completas
            if (txtBox.Text == "" || delimiterChars.Contains(txtBox.Text.Last()) || txtBox.Text.Split(delimiterChars).Length < 2)
            {
                MessageBox.Show("Solo guardar operaciones completas");
            }
            else
            {
                memoria = txtBox.Text;
            }
        }
        
        // MR
        private void btn10_Click(object sender, EventArgs e)
        {
            txtBox.Text = memoria;
        }

        // MC
        private void btn1_Click(object sender, EventArgs e)
        {
            memoria = "";
            txtBox.Text = memoria;
        }

        // M+
        private void btn20_Click(object sender, EventArgs e)
        {
            // Comprobación guardar solo operaciones completas || memoria vacia || copiar tosa la memoria 
            if (memoria.Length == 0)
            {
                MessageBox.Show("memoria vacia");
            }
            else if (txtBox.Text == "" || delimiterChars.Contains(txtBox.Text.Last()) || txtBox.Text.Split(delimiterChars).Length < 2)
            {
                MessageBox.Show("Solo operaciones Completas");
            }
            else if(txtBox.Text.Contains(Environment.NewLine))
            {
                MessageBox.Show("no se pueden añadir toda la memoria");
            }
            else 
            { 
                memoria += Environment.NewLine + txtBox.Text;
            }
        }

        // Botón 1/x
        private void btn18_Click(object sender, EventArgs e)
        {
            // Si texto vacio || texto contiene operador 
            if (txtBox.Text.Length == 0 || txtBox.Text.Contains(delimiterChars[0]) || txtBox.Text.Contains(delimiterChars[1]) || txtBox.Text.Contains(delimiterChars[2]) || txtBox.Text.Contains(delimiterChars[3]))
            {
                MessageBox.Show("Operacion no valida");
            }
            else 
            {
                res = 1 / Convert.ToDouble(txtBox.Text);
                txtBox.Text = "";
                txtBox.Text = txtBox.Text + res;
            }
        }

        // Método para pintar botones y ponerlos como eventos
        private void BtnColores_Paint(object sender, PaintEventArgs e) 
        {
            // Control del Botón
            Button boton = sender as Button;

            if (boton != null) 
            { 
                int midHeight = boton.Height / 2;

                Color colorTop = Color.WhiteSmoke;
                Color colorDown = Color.LightGray;

                using (SolidBrush topBrush = new SolidBrush(colorTop))
                using (SolidBrush DownBrush = new SolidBrush(colorDown))
                {
                    // mitad superior
                    e.Graphics.FillRectangle(topBrush, 0, 0, boton.Width, midHeight);
                    // mirad inferior
                    e.Graphics.FillRectangle(DownBrush, 0, midHeight, boton.Width, boton.Height - midHeight);
                }

                // Texto
                TextRenderer.DrawText(e.Graphics, boton.Text, boton.Font, boton.ClientRectangle, boton.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                // Bordes
                ControlPaint.DrawBorder(e.Graphics, boton.ClientRectangle, SystemColors.ControlDark, ButtonBorderStyle.Solid);
            }
        }
    }
}
