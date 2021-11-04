using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Usuarios_planta.Capa_presentacion
{
    public partial class Login : Form
    {
        MySqlConnection con = new MySqlConnection("server=;Uid=;password=;database=;port=3306;persistsecurityinfo=True;");
        Comandos cmds = new Comandos();

        public Login()
        {
            InitializeComponent();
        }

        public void loguear(string user, string pass)
        {
            try
            {

                string user1 = Txtuser.Text; //captura el dato registrado en el campo usuario
                string pass1 = Txtpass.Text;//captura el dato registrado en el campo contraseña
                string epass1 = Encrypt.GetSHA256(pass1);//llama la clase encrypt y encripta el valor registrado en el campo contraseña
                con.Open();
                MySqlCommand cmd = new MySqlCommand("Select Identificacion,nombre,Area,Perfil, Plataforma from tf_usuarios where Identificacion=@Identificacion and Contraseña=@Contraseña", con);
                cmd.Parameters.AddWithValue("@Identificacion", user);
                cmd.Parameters.AddWithValue("@Contraseña", epass1);
                MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    this.Hide();
                    usuario.Identificacion = dt.Rows[0][0].ToString();
                    usuario.Nombre = dt.Rows[0][1].ToString();
                    usuario.Area = dt.Rows[0][2].ToString();
                    usuario.Perfil = dt.Rows[0][3].ToString();
                    usuario.Plataforma = dt.Rows[0][4].ToString();
                    cmds.Accesso_Aplicacion();

                    if (usuario.Plataforma == "Giros" || usuario.Plataforma == "Administrador")
                    {
                        MessageBox.Show("Bienvenido !! " + dt.Rows[0][1].ToString());
                        Form formulario = new desembolso();
                        formulario.Show();
                    }
                    else
                    {
                        MessageBox.Show("Usuario no tiene permiso para ingresar a esta aplicacion");
                        Application.Exit();
                    }

                }
                else
                {
                    MessageBox.Show("Usuario y/o Contraseña incorrectos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loguear(Txtuser.Text, Txtpass.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(char)(Keys.Enter))
            {
                e.Handled = true;
                button1_Click(sender,e);
            }
        }
    }
}
