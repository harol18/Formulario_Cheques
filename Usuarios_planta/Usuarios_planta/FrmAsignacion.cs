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

namespace Usuarios_planta
{
    public partial class FrmAsignacion : Form
    {
        MySqlConnection con = new MySqlConnection("server=;Uid=;password=;database=;port=3306;persistsecurityinfo=True;");

        Comandos cmds = new Comandos();

        public FrmAsignacion()
        {
            InitializeComponent();
            cargar_girador();
        }

        public void cargar_girador()
        {
            con.Open();
            string query = "SELECT nombre from tf_usuarios where Area= 'Cheques' order by nombre asc";
            MySqlCommand comando = new MySqlCommand(query, con);
            MySqlDataAdapter da1 = new MySqlDataAdapter(comando);
            DataTable dt = new DataTable();
            da1.Fill(dt);
            con.Close();
            DataRow fila = dt.NewRow();
            fila["Nombre"] = "";
            dt.Rows.InsertAt(fila, 0);
            cmbGirador.ValueMember = "Nombre";
            cmbGirador.DisplayMember = "Nombre";
            cmbGirador.DataSource = dt;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            cmds.guardar_Asignacion_cheques( TxtRadicado, dtpFecha_Ingreso, dtpHora_Ingreso, dtpHora_Asignacion,
                                             cmbGirador, dtpHora_Alistamiento, dtpEntrega_Banco, dtpHora_Entrega_Banco,
                                             txtFirma_Banco1, txtFirma_Banco2, txtN_Cheques, dtpEntrega_Front, cmbEstado_Operacion,
                                             dtp_Hora_Custodio, txtObservaciones);
        }

        private void FrmAsignacion_Load(object sender, EventArgs e)
        {

        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
