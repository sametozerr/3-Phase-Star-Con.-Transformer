using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transformer
{

    public partial class Form1 : Form
    {

        public readonly double sqrtOfThree = Math.Sqrt(3);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {


                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || comboBox1.Text == "" ||
                    Convert.ToDouble(textBox1.Text) <= 0 || Convert.ToDouble(textBox2.Text) <= 0 || Convert.ToDouble(textBox3.Text) <= 0 || Convert.ToDouble(textBox4.Text) <= 0 || Convert.ToDouble(textBox5.Text) <= 0 || Convert.ToDouble(textBox6.Text) <= 0 || Convert.ToDouble(textBox7.Text) <= 0)
                {


                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || comboBox1.Text == "") 
                {
                    MessageBox.Show("Lütfen bütün değerleri giriniz!");
                }

                else
                {

                    if (Convert.ToDouble(textBox6.Text) > 100 || Convert.ToDouble(textBox6.Text) <= 0)
                        MessageBox.Show("%Uk 1-100 arasında olmalıdır.");
                    if (Convert.ToDouble(textBox1.Text) <= 0)
                        MessageBox.Show("Sn değeri 0 veya negatif olamaz.");
                    if (Convert.ToDouble(textBox2.Text) <= 0)
                        MessageBox.Show("Vp değeri 0 veya negatif olamaz.");
                    if (Convert.ToDouble(textBox3.Text) <= 0)
                        MessageBox.Show("Kazan kaybı 0 veya negatif olamaz.");
                    if (Convert.ToDouble(textBox4.Text) <= 0)
                        MessageBox.Show("Pk değeri 0 veya negatif olamaz.");
                    if (Convert.ToDouble(textBox5.Text) <= 0)
                        MessageBox.Show("Io değeri 0 veya negatif olamaz.");
                    if (Convert.ToDouble(textBox6.Text) <= 0)
                        MessageBox.Show("%Uk değeri 0 veya negatif olamaz.");
                    if (Convert.ToDouble(textBox7.Text) <= 0)
                        MessageBox.Show("Po değeri 0 veya negatif olamaz.");
                 }
                 }

            else
            {

                dataGridView1.Visible = true;

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                dataGridView1.Columns.Add("Column1", "     ");
                dataGridView1.Columns.Add("Column2", "%100 Yük");
                dataGridView1.Columns.Add("Column3", "%75 Yük");
                dataGridView1.Columns.Add("Column4", "%50 Yük");
                dataGridView1.Columns.Add("Column5", "%25 Yük");

                /* Önekler */

                double M = Math.Pow(10.0, 6.0);
                double K = Math.Pow(10.0, 3.0);

                /* Verilen Parametreler */

                double Sn = Convert.ToDouble(textBox1.Text) * M;
                double Vp = Convert.ToDouble(textBox2.Text) * K; // 0 olamaz.
                string primWinding = comboBox1.Text;
                double Po = Convert.ToDouble(textBox7.Text) * K;
                double Pk = Convert.ToDouble(textBox4.Text) * K;
                double Qb = Convert.ToDouble(textBox3.Text) * K;
                double Uk = Convert.ToDouble(textBox6.Text);
                double Io = Convert.ToDouble(textBox5.Text);

                /* Hesaplanacak Parametreler */

                double Ip = Sn / (sqrtOfThree * Vp);
                double Vk = (Uk * Vp) / 100;
                double cosO = Po / (sqrtOfThree * Vp * Io);
                double thetaO = (Math.Acos(cosO) * 180) / 3.14;
                double Qo = Po * Math.Tan((thetaO / 180) * 3.14);
                double cosK = Pk / (sqrtOfThree * Vk * Ip);
                double thetaK = (Math.Acos(cosK) * 180) / 3.14;

                /* Bütün yükler için Pk */

                double Pk75 = sqrtOfThree * (Vk * 0.75) * (Ip * 0.75) * cosK;
                double Pk50 = sqrtOfThree * (Vk * 0.50) * (Ip * 0.50) * cosK;
                double Pk25 = sqrtOfThree * (Vk * 0.25) * (Ip * 0.25) * cosK;

                /* Bütün yükler için Qk */

                double Qk = Math.Sqrt(Math.Pow(sqrtOfThree * Vk * Ip, 2) - Math.Pow(Pk, 2));
                double Qk75 = Math.Sqrt(Math.Pow(sqrtOfThree * (Vk * 0.75) * (Ip * 0.75), 2) - Math.Pow(Pk75, 2));
                double Qk50 = Math.Sqrt(Math.Pow(sqrtOfThree * (Vk * 0.5) * (Ip * 0.5), 2) - Math.Pow(Pk50, 2));
                double Qk25 = Math.Sqrt(Math.Pow(sqrtOfThree * (Vk * 0.25) * (Ip * 0.25), 2) - Math.Pow(Pk25, 2));

                double Rk = Pk / (3 * Math.Pow(Ip, 2));
                double Xk = Qk / (3 * Math.Pow(Ip, 2));
                double Zk = Math.Sqrt(Math.Pow(Rk, 2) + Math.Pow(Xk, 2));

                double If = Io * cosO;
                double Im = Io * Math.Sin((thetaO / 180) * 3.14);

                dataGridView1.Rows.Add("Nominal Güç (MVA)", (Sn / M), (Sn / M) * 0.75, (Sn / M) * 0.5, (Sn / M) * 0.25);
                dataGridView1.Rows.Add("Primer Gerilimi (kV)", Vp / K, Vp / K, Vp / K, Vp / K);
                dataGridView1.Rows.Add("Primer Bağlantısı", primWinding, primWinding, primWinding, primWinding);
                dataGridView1.Rows.Add("Po (kW)", Po / K, Po / K, Po / K, Po / K);
                dataGridView1.Rows.Add("Pk (kW)", String.Format("{0:F2}", Pk / K), String.Format("{0:F2}", Pk75 / K), String.Format("{0:F2}", Pk50 / K), String.Format("{0:F2}", Pk25 / K));
                dataGridView1.Rows.Add("Kazan Kaybı (kW)", Qb, Qb, Qb, Qb);
                dataGridView1.Rows.Add("Kısa Devre Empedansı(%)", Uk, Uk * 0.75, Uk * 0.5, Uk * 0.25);
                dataGridView1.Rows.Add("Boşta Akım (A)", Io, Io, Io, Io);
                dataGridView1.Rows.Add("Primer Akımı (A)", String.Format("{0:F2}", Ip), String.Format("{0:F2}", Ip * 0.75), String.Format("{0:F2}", Ip * 0.5), String.Format("{0:F2}", Ip * 0.25));
                dataGridView1.Rows.Add("Primer Kısa Devre Gerilimi (kV)", String.Format("{0:F2}", Vk / K), String.Format("{0:F2}", (Vk / K) * 0.75), String.Format("{0:F2}", (Vk / K) * 0.5), String.Format("{0:F2}", (Vk / K) * 0.25));
                dataGridView1.Rows.Add("cos(φo)", String.Format("{0:F2}", cosO), String.Format("{0:F2}", cosO), String.Format("{0:F2}", cosO), String.Format("{0:F2}", cosO));
                dataGridView1.Rows.Add("φo (Degree)", String.Format("{0:F2}", thetaO), String.Format("{0:F2}", thetaO), String.Format("{0:F2}", thetaO), String.Format("{0:F2}", thetaO));
                dataGridView1.Rows.Add("Boşta Reaktif Kayıp Qo (kVAr)", String.Format("{0:F2}", Qo / K), String.Format("{0:F2}", Qo / K), String.Format("{0:F2}", Qo / K), String.Format("{0:F2}", Qo / K));
                dataGridView1.Rows.Add("cos(φk)", String.Format("{0:F2}", cosK), String.Format("{0:F2}", cosK), String.Format("{0:F2}", cosK), String.Format("{0:F2}", cosK));
                dataGridView1.Rows.Add("φk (Degree)", String.Format("{0:F2}", thetaK), String.Format("{0:F2}", thetaK), String.Format("{0:F2}", thetaK), String.Format("{0:F2}", thetaK));
                dataGridView1.Rows.Add("Kısa Devre Reaktif Kayıp Qk (kVAr)", String.Format("{0:F2}", Qk / K), String.Format("{0:F2}", Qk75 / K), String.Format("{0:F2}", Qk50 / K), String.Format("{0:F2}", Qk25 / K));
                dataGridView1.Rows.Add("Rk (Ω)", String.Format("{0:F2}", Rk), String.Format("{0:F2}", Rk), String.Format("{0:F2}", Rk), String.Format("{0:F2}", Rk));
                dataGridView1.Rows.Add("Xk (Ω)", String.Format("{0:F2}", Xk), String.Format("{0:F2}", Xk), String.Format("{0:F2}", Xk), String.Format("{0:F2}", Xk));
                dataGridView1.Rows.Add("Kısa Devre Empedansı Zk (Ω)", String.Format("{0:F2}", Zk), String.Format("{0:F2}", Zk), String.Format("{0:F2}", Zk), String.Format("{0:F2}", Zk));
                dataGridView1.Rows.Add("X1=X2=Xk/2 (Ω)", String.Format("{0:F2}", Xk / 2), String.Format("{0:F2}", Xk / 2), String.Format("{0:F2}", Xk / 2), String.Format("{0:F2}", Xk / 2));
                dataGridView1.Rows.Add("R1=R2=Rk/2 (Ω)", String.Format("{0:F2}", Rk / 2), String.Format("{0:F2}", Rk / 2), String.Format("{0:F2}", Rk / 2), String.Format("{0:F2}", Rk / 2));
                dataGridView1.Rows.Add("Demir Akımı (If)", String.Format("{0:F2}", If), String.Format("{0:F2}", If), String.Format("{0:F2}", If), String.Format("{0:F2}", If));
                dataGridView1.Rows.Add("Mıknatıslanma Akımı (Im)", String.Format("{0:F2}", Im), String.Format("{0:F2}", Im), String.Format("{0:F2}", Im), String.Format("{0:F2}", Im));

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox1.Text = string.Empty;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)8 && textBox1.Text.Length != 0)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                textBox1.SelectionStart = textBox1.Text.Length;
                e.Handled = true;
            }

            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8 && textBox2.Text.Length != 0)
            {
                textBox2.Text = textBox2.Text.Substring(0, textBox2.Text.Length - 1);
                textBox2.SelectionStart = textBox2.Text.Length;
                e.Handled = true;
            }

            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8 && textBox7.Text.Length != 0)
            {
                textBox7.Text = textBox7.Text.Substring(0, textBox7.Text.Length - 1);
                textBox7.SelectionStart = textBox7.Text.Length;
                e.Handled = true;
            }

            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8 && textBox4.Text.Length != 0)
            {
                textBox4.Text = textBox4.Text.Substring(0, textBox4.Text.Length - 1);
                textBox4.SelectionStart = textBox4.Text.Length;
                e.Handled = true;
            }

            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)8 && textBox3.Text.Length != 0)
            {
                textBox3.Text = textBox3.Text.Substring(0, textBox3.Text.Length - 1);
                textBox3.SelectionStart = textBox3.Text.Length;
                e.Handled = true;
            }

            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8 && textBox5.Text.Length != 0)
            {
                textBox5.Text = textBox5.Text.Substring(0, textBox5.Text.Length - 1);
                textBox5.SelectionStart = textBox5.Text.Length;
                e.Handled = true;
            }

            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
                e.Handled = false;
            else
                e.Handled = true;

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {

                if (e.KeyChar == (char)8 && textBox6.Text.Length != 0)
                {
                    textBox6.Text = textBox6.Text.Substring(0, textBox6.Text.Length - 1);
                    textBox6.SelectionStart = textBox6.Text.Length;
                    e.Handled = true;
                }

                if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
                    e.Handled = false;
                else
                    e.Handled = true;
        }


    }
}
