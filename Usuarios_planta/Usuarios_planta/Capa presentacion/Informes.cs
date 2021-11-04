using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Configuration;
using SpreadsheetLight;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Usuarios_planta.Formularios
{
    public partial class Informes : Form
    {
        #region DeclaracionVariables
        private System.Windows.Forms.Timer timer;
        #endregion

        Comandos cmds = new Comandos();

        public Informes()
        {
            InitializeComponent();
        }

        private void InicioServicio(object sender, EventArgs e)
        {
            try
            {
                timer = new System.Windows.Forms.Timer();
                timer.Interval = Convert.ToInt32(ConfigurationManager.AppSettings["IntervaloEjecucion"]);
                timer.Enabled = true;
                this.timer.Tick += new EventHandler(EventoTemporizador);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void EventoTemporizador(object sender, EventArgs e)
        {
            try
            {
                //Declaracion de variable para conectar la base de datos
                string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionDB"].ToString();
                MySqlConnection conexion = new MySqlConnection(cadenaConexion);
                MySqlCommand comando = new MySqlCommand("Windows_Service", conexion); //se pasa el nombre del procedimiento almacena y la conexion
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DetenerServicio(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timer.Stop();
        }

        private void btnPunto_Control_Click(object sender, EventArgs e)
        {
            cmds.Base_punto(dtpFecha_Punto, dgvDatos_Punto);
            //lbltotal.Text = dgvDatos_Punto.Rows.Count.ToString();
        }

        private void btnDescargar_Excel_Click(object sender, EventArgs e)
        {
            SLDocument sl = new SLDocument();
            SLStyle style = new SLStyle();
            style.Font.Bold = true;
            style.Font.FontSize = 11;
            style.Font.FontName = "Calibri";
            style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Lavender, System.Drawing.Color.LightGray);
            style.Alignment.Horizontal = HorizontalAlignmentValues.Center;

            int i = 1;
            foreach (DataGridViewColumn columna in dgvDatos_Punto.Columns)
            {
                sl.SetCellValue(1, i, columna.HeaderText.ToString());
                sl.SetCellStyle(1, i, style);
                i++;
            }

            int j = 2;
            foreach (DataGridViewRow row in dgvDatos_Punto.Rows)
            {
                sl.SetCellValue(j, 1, row.Cells[0].Value.ToString());
                sl.SetCellValue(j, 2, row.Cells[1].Value.ToString());
                sl.SetCellValue(j, 3, row.Cells[2].Value.ToString());
                sl.SetCellValue(j, 4, row.Cells[3].Value.ToString());
                sl.SetCellValue(j, 5, row.Cells[4].Value.ToString());
                sl.SetCellValue(j, 6, row.Cells[5].Value.ToString());
                sl.SetCellValue(j, 7, row.Cells[6].Value.ToString());
                sl.SetCellValue(j, 8, row.Cells[7].Value.ToString());
                sl.SetCellValue(j, 9, row.Cells[8].Value.ToString());
                sl.SetCellValue(j, 10, row.Cells[9].Value.ToString());
                sl.SetCellValue(j, 11, row.Cells[10].Value.ToString());
                sl.SetCellValue(j, 12, row.Cells[11].Value.ToString());
                sl.SetCellValue(j, 13, row.Cells[12].Value.ToString());
                sl.SetCellValue(j, 14, row.Cells[13].Value.ToString());
                sl.SetCellValue(j, 15, row.Cells[14].Value.ToString());
                sl.SetCellValue(j, 16, row.Cells[15].Value.ToString());
                j++;
            }
            sl.SaveAs(@"D:\punto_giros.xlsx");
            MessageBox.Show("Ok archivo creado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
