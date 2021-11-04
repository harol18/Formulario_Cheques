using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using MySql.Data.MySqlClient;
using SpreadsheetLight;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Configuration;




namespace Usuarios_planta.Formularios
{
    public partial class FormGiros : Form
    {

        MySqlConnection con = new MySqlConnection("server=;Uid=;password=;database=;port=3306;persistsecurityinfo=True;");
        Comandos cmds = new Comandos();
        
        public FormGiros()
        {
            InitializeComponent();
        }       

        DateTime hoy = DateTime.Today;
        private Timer timer;

        private void TxtCod_oficina_TextChanged(object sender, EventArgs e)
        {
            MySqlCommand comando = new MySqlCommand("SELECT * FROM tf_oficinas WHERE codigo_oficina = @codigo ", con);
            comando.Parameters.AddWithValue("@codigo", Txtcod_oficina.Text);
            con.Open();
            MySqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                datos_correo.correo_gerente= registro["correo_gerente"].ToString();
                datos_correo.correo_subgerente = registro["correo_subgerente"].ToString();                
            }
            con.Close();
        }

        private void TxtCoordinador_TextChanged(object sender, EventArgs e)
        {
            MySqlCommand comando = new MySqlCommand("SELECT * FROM tf_coordinador WHERE nombre_coordinador = @coordinador ", con);
            comando.Parameters.AddWithValue("@coordinador", Txtcoordinador.Text);
            con.Open();
            MySqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                datos_correo.correo_coordinador = registro["correo_coordinador"].ToString();
                datos_correo.correo_apoyo = registro["correo_apoyo"].ToString();
            }
            con.Close();
        }

        public static string Endoso(DataGridView grid)
        {
            try
            {
                string messageBody = "<font><br><br>Endosar:</font><br><br>";
                if (grid.RowCount == 0) return messageBody;
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style=\"background-color:#004254; color:#FFFFFF;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style=\"color:#000000;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#000000; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";
                string htmlTdparrafo = "<font><br><br><br>BBVA - INDRA.<br> Centro de formalización.<br>Calle 75a # 27a - 28.<br>cheques.libranza@bbva.com.co</font>";
                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;                
                messageBody += htmlTdStart + "Entidad" + htmlTdEnd;
                messageBody += htmlTdStart + "Valor" + htmlTdEnd;
                messageBody += htmlTdStart + "Obligacion" + htmlTdEnd;                
                messageBody += htmlHeaderRowEnd;

                //Loop all the rows from grid vew and added to html td  
                for (int i = 0; i <= grid.RowCount - 1; i++)
                {
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[0].Value; //Entidad
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[1].Value; //Valor
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[2].Value; //Obligacion                    
                    messageBody = messageBody + htmlTrEnd;
                }
                messageBody = messageBody + htmlTableEnd;
                messageBody = messageBody + htmlTdparrafo;
                return messageBody; // devuelve la tabla HTML como cadena de esta función  
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string Formato1(DataGridView grid)
        {
            try
            {
                string messageBody = "<font>Señores: </font><br><br><br>Oficina  " + datos_correo.oficina + "<br><br><br>Buen Día,<br><br>Por motivo del desembolso de la compra de cartera del cliente en referencia, se generó a su oficina el(los) Giro(s) de Cheque(s) de acuerdo con la información adjunta, para su respectiva impresión, custodia y contacto a cliente para su entrega. <br><br>" +
                    "La operatoria que se debe realizar:   Operatoria 2 / Operatoria activos / Prestamos / Formalización / Imprimir Cheques - Desembolso Crédito<br><br>Tener en cuenta:<br><br>1.    Realizar giro de cheque de forma inmediata, ya que la partida quedará pendiente en la cuenta 259595201 de su centro de costos y será monitoreada por CONTROL CONTABLE.<br>" +
                    "           2.    Una vez realizada la impresión del cheque se solicita <i><b>realizar el endoso de cada una de las obligaciones</b><i> correspondientes según la información suministrada; igualmente de requerirse esta información se podrá consultar en Bonita.<br>" +
                    "           3.    <i>Si presenta ERROR</i> al realizar la impresión, remitir pantallas paso a paso de toda la información ingresada con copia a TODOS los BUZONES de este correo. Vale aclarar que <i><b>se debe ingresar el valor informado en el correo y NO el valor de la partida Contable.</i><b><br>" +
                    "           4.    Para los casos cuando el cliente NO va utilizar el cheque o por Desistimiento del crédito se adjunta formato para que sea diligenciado por el cliente y remitir posteriormente por esta vía para la instrucción correspondiente. <u><i><b>FORMATO DEV CHEQ</i></b></u><br><br>";
                if (grid.RowCount == 0) return messageBody;
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style=\"background-color:#004254; color:#FFFFFF;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style=\"color:#000000;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#000000; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";
                string htmlTdparrafo = "<font><br><br><br>BBVA - INDRA.<br> Centro de formalización.<br>Calle 75a # 27a - 28.<br>cheques.libranza@bbva.com.co</font>";
                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "Radicado" + htmlTdEnd;
                messageBody += htmlTdStart + "Codigo" + htmlTdEnd;
                messageBody += htmlTdStart + "Fecha" + htmlTdEnd;
                messageBody += htmlTdStart + "Oficina" + htmlTdEnd;
                messageBody += htmlTdStart + "Ciudad" + htmlTdEnd;
                messageBody += htmlTdStart + "Cedula" + htmlTdEnd;
                messageBody += htmlTdStart + "Nombre" + htmlTdEnd;
                messageBody += htmlTdStart + "Nit" + htmlTdEnd;
                messageBody += htmlTdStart + "Entidad" + htmlTdEnd;
                messageBody += htmlTdStart + "Valor" + htmlTdEnd;
                messageBody += htmlTdStart + "Obligacion" + htmlTdEnd;
                messageBody += htmlTdStart + "Scoring" + htmlTdEnd;
                messageBody += htmlTdStart + "Gestor" + htmlTdEnd;
                messageBody += htmlTdStart + "Coordinador" + htmlTdEnd;
                messageBody += htmlTdStart + "Cuenta" + htmlTdEnd;
                messageBody += htmlTdStart + "Ref" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;

                //Loop all the rows from grid vew and added to html td  
                for (int i = 0; i <= grid.RowCount - 1; i++)
                {
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[0].Value; //Radicado
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[1].Value; //Codigo
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[2].Value; //Fecha  
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[3].Value; //Oficina
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[4].Value; //Ciudad 
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[5].Value; //Cedula 
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[6].Value; //Nombre 
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[7].Value; //Nit
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[8].Value; //Entidad
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[9].Value; //Valor
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[10].Value; //Obligacion 
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[11].Value; //Scoring 
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[12].Value; //Gestor
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[13].Value; //Coordinador 
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[14].Value; //Cuenta
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[15].Value; //Ref
                    messageBody = messageBody + htmlTrEnd;
                }
                messageBody = messageBody + htmlTableEnd;
                messageBody = messageBody + htmlTdparrafo;
                return messageBody; // devuelve la tabla HTML como cadena de esta función  
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string Formato(DataGridView grid)
        {
            try
            {
                string messageBody = "<font>Señores: </font><br><br><br>Oficina  " + datos_correo.oficina + "<br><br><br>Buen Día,<br><br>Por motivo del desembolso de la compra de cartera del cliente en referencia, se generó a su oficina el(los) Giro(s) de Cheque(s) de acuerdo con la información adjunta, para su respectiva impresión, custodia y contacto a cliente para su entrega. <br><br>" +
                    "La operatoria que se debe realizar:   Operatoria 2 / Operatoria activos / Prestamos / Formalización / Imprimir Cheques - Desembolso Crédito<br><br>Tener en cuenta:<br><br>1.    Realizar giro de cheque de forma inmediata, ya que la partida quedará pendiente en la cuenta 259595201 de su centro de costos y será monitoreada por CONTROL CONTABLE.<br>" +
                    "           2.    Una vez realizada la impresión del cheque se solicita <i><b>realizar el endoso de cada una de las obligaciones</b><i> correspondientes según la información suministrada; igualmente de requerirse esta información se podrá consultar en Bonita.<br>" +
                    "           3.    <i>Si presenta ERROR</i> al realizar la impresión, remitir pantallas paso a paso de toda la información ingresada con copia a TODOS los BUZONES de este correo. Vale aclarar que <i><b>se debe ingresar el valor informado en el correo y NO el valor de la partida Contable.</i><b><br>" +
                    "           4.    Para los casos cuando el cliente NO va utilizar el cheque o por Desistimiento del crédito se adjunta formato para que sea diligenciado por el cliente y remitir posteriormente por esta vía para la instrucción correspondiente. <u><i><b>FORMATO DEV CHEQ</i></b></u><br><br>";
                if (grid.RowCount == 0) return messageBody;
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style=\"background-color:#004254; color:#FFFFFF;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style=\"color:#000000;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#000000; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";
                //string htmlTdparrafo = "<font><br><br><br>BBVA - INDRA.<br> Centro de formalización.<br>Calle 75a # 27a - 28.<br>cheques.libranza@bbva.com.co</font>";
                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "Radicado" + htmlTdEnd;
                messageBody += htmlTdStart + "Codigo" + htmlTdEnd;
                messageBody += htmlTdStart + "Fecha" + htmlTdEnd;
                messageBody += htmlTdStart + "Oficina" + htmlTdEnd;
                messageBody += htmlTdStart + "Ciudad" + htmlTdEnd;
                messageBody += htmlTdStart + "Cedula" + htmlTdEnd;
                messageBody += htmlTdStart + "Nombre" + htmlTdEnd;
                messageBody += htmlTdStart + "Nit" + htmlTdEnd;
                messageBody += htmlTdStart + "Entidad" + htmlTdEnd;
                messageBody += htmlTdStart + "Valor" + htmlTdEnd;
                messageBody += htmlTdStart + "Obligacion" + htmlTdEnd;
                messageBody += htmlTdStart + "Scoring" + htmlTdEnd;
                messageBody += htmlTdStart + "Gestor" + htmlTdEnd;
                messageBody += htmlTdStart + "Coordinador" + htmlTdEnd;
                messageBody += htmlTdStart + "Cuenta" + htmlTdEnd;
                messageBody += htmlTdStart + "Ref" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;

                //Loop all the rows from grid vew and added to html td  
                for (int i = 0; i <= grid.RowCount - 1; i++)
                {
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[0].Value; //Radicado
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[1].Value; //Codigo
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[2].Value; //Fecha  
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[3].Value; //Oficina
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[4].Value; //Ciudad 
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[5].Value; //Cedula 
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[6].Value; //Nombre 
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[7].Value; //Nit
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[8].Value; //Entidad
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[9].Value; //Valor
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[10].Value; //Obligacion 
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[11].Value; //Scoring 
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[12].Value; //Gestor
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[13].Value; //Coordinador 
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[14].Value; //Cuenta
                    messageBody = messageBody + htmlTdStart + grid.Rows[i].Cells[15].Value; //Ref
                    messageBody = messageBody + htmlTrEnd;
                }
                messageBody = messageBody + htmlTableEnd;
                //messageBody = messageBody + htmlTdparrafo;
                return messageBody; // devuelve la tabla HTML como cadena de esta función  
            }
            catch (Exception)
            {
                return null;
            }
        }
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            cmds.Buscar_giro(TxtRadicado, Txtcedula, Txtnombre, Txtcuenta, Txtscoring, TxtCedula_Gestor, Txtnom_gestor,
           Txtcoordinador, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtobligacion1, TxtNom_entidad1, TxtNit1, TxtValor1,
           Txtobligacion2, TxtNom_entidad2, TxtNit2, TxtValor2, Txtobligacion3, TxtNom_entidad3, TxtNit3, TxtValor3,
           Txtobligacion4, TxtNom_entidad4, TxtNit4, TxtValor4, Txtobligacion5, TxtNom_entidad5, TxtNit5, TxtValor5,
           Txtobligacion6, TxtNom_entidad6, TxtNit6, TxtValor6, Txtobligacion7, TxtNom_entidad7, TxtNit7, TxtValor7,
           Txtobligacion8, TxtNom_entidad8, TxtNit8, TxtValor8);

           

            if (TxtValor1.Text != "")
            {
                TxtValor1.Text = string.Format("{0:#,##0.##}", double.Parse(TxtValor1.Text));
            }
            if (TxtValor2.Text != "")
            {
                TxtValor2.Text = string.Format("{0:#,##0.##}", double.Parse(TxtValor2.Text));
            }
            if (TxtValor3.Text != "")
            {
                TxtValor3.Text = string.Format("{0:#,##0.##}", double.Parse(TxtValor3.Text));
            }
            if (TxtValor4.Text != "")
            {
                TxtValor4.Text = string.Format("{0:#,##0.##}", double.Parse(TxtValor4.Text));
            }
            if (TxtValor5.Text != "")
            {
                TxtValor5.Text = string.Format("{0:#,##0.##}", double.Parse(TxtValor5.Text));
            }
            if (TxtValor6.Text != "")
            {
                TxtValor6.Text = string.Format("{0:#,##0.##}", double.Parse(TxtValor6.Text));
            }
            if (TxtValor7.Text != "")
            {
                TxtValor7.Text = string.Format("{0:#,##0.##}", double.Parse(TxtValor7.Text));
            }
            if (TxtValor8.Text != "")
            {
                TxtValor8.Text = string.Format("{0:#,##0.##}", double.Parse(TxtValor8.Text));
            }            
        }

        private void Txtnombre_TextChanged(object sender, EventArgs e)
        {
            TxtAsunto.Text = "GIRO CHEQUE CPK " + Txtnombre.Text + " CC " + Txtcedula.Text;
        }

        private void TxtCedula_Gestor_TextChanged(object sender, EventArgs e)
        {
            MySqlCommand comando = new MySqlCommand("SELECT * FROM gestores WHERE Cedula_Gestor = @Cedula_Gestor ", con);
            comando.Parameters.AddWithValue("@Cedula_Gestor", TxtCedula_Gestor.Text);
            con.Open();
            MySqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                datos_correo.correo_gestor = registro["Correo_Gestor"].ToString();
                Txtnom_gestor.Text = registro["nombre_gestor"].ToString();
            }
            else
            {                
                con.Close();
            }
            con.Close();
        }

        private void btnEnviar_Correo_Click(object sender, EventArgs e)
        {
            string correo_oficina = datos_correo.correo_gerente + " ; " + datos_correo.correo_subgerente + " ; ";
            string correo_F_comercial = datos_correo.correo_coordinador + " ; " + datos_correo.correo_apoyo + " ; " + datos_correo.correo_gestor + " ; ";
            string destinatarios = correo_oficina + correo_F_comercial;
            string correo_copia = datos_correo.copia_correo + TxtCopia_Correo.Text;
            string htmlString = Formato(dataGridView1);
            string htmlString1 = Endoso(dgvEndoso);
            string htmlString2 = Formato1(dataGridView1);


            try
            {
                Outlook.Application outlookApp = new Outlook.Application();
                Outlook._MailItem oMailItem = (Outlook._MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                Outlook.Inspector oInspector = oMailItem.GetInspector;


                oMailItem.Subject = TxtAsunto.Text;
                oMailItem.To = destinatarios;
                oMailItem.CC = correo_copia;                
                if (dgvEndoso.Rows.Count >1)
                {
                    oMailItem.HTMLBody = htmlString + htmlString1;
                }
                else
                {
                    oMailItem.HTMLBody = htmlString2;
                }
                
                oMailItem.Attachments.Add(@"D:\Guia_Rapida.pdf");
                oMailItem.Attachments.Add(@"D:\FORMATO DEVOLUCION CHEQUE.pdf");
                //oMailItem.BCC = "hsmartinez@indracompany.com";//Copia oculta
                oMailItem.Importance = Outlook.OlImportance.olImportanceNormal;//Asignar Importancia del correo
                //oMailItem.Display(true);
                oMailItem.Display(false);
                oMailItem.Send();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text == "" && TxtNom_entidad3.Text == "" && TxtNom_entidad4.Text == "" && TxtNom_entidad5.Text == "" && TxtNom_entidad6.Text == "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                cmds.Insertar_cartera1(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                      Txtobligacion1, TxtNit1, TxtNom_entidad1, TxtValor1, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text == "" && TxtNom_entidad4.Text == "" && TxtNom_entidad5.Text == "" && TxtNom_entidad6.Text == "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                cmds.Insertar_cartera1(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                      Txtobligacion1, TxtNit1, TxtNom_entidad1, TxtValor1, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera2(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion2, TxtNit2, TxtNom_entidad2, TxtValor2, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text == "" && TxtNom_entidad5.Text == "" && TxtNom_entidad6.Text == "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                cmds.Insertar_cartera1(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                      Txtobligacion1, TxtNit1, TxtNom_entidad1, TxtValor1, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera2(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion2, TxtNit2, TxtNom_entidad2, TxtValor2, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera3(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion3, TxtNit3, TxtNom_entidad3, TxtValor3, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text != "" && TxtNom_entidad5.Text == "" && TxtNom_entidad6.Text == "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                cmds.Insertar_cartera1(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                      Txtobligacion1, TxtNit1, TxtNom_entidad1, TxtValor1, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera2(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion2, TxtNit2, TxtNom_entidad2, TxtValor2, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera3(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion3, TxtNit3, TxtNom_entidad3, TxtValor3, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera4(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion4, TxtNit4, TxtNom_entidad4, TxtValor4, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text != "" && TxtNom_entidad5.Text != "" && TxtNom_entidad6.Text == "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                cmds.Insertar_cartera1(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                      Txtobligacion1, TxtNit1, TxtNom_entidad1, TxtValor1, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera2(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion2, TxtNit2, TxtNom_entidad2, TxtValor2, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera3(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion3, TxtNit3, TxtNom_entidad3, TxtValor3, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera4(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion4, TxtNit4, TxtNom_entidad4, TxtValor4, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera5(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion5, TxtNit5, TxtNom_entidad5, TxtValor5, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
              
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text != "" && TxtNom_entidad5.Text != "" && TxtNom_entidad6.Text != "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                cmds.Insertar_cartera1(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                      Txtobligacion1, TxtNit1, TxtNom_entidad1, TxtValor1, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera2(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion2, TxtNit2, TxtNom_entidad2, TxtValor2, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera3(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion3, TxtNit3, TxtNom_entidad3, TxtValor3, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera4(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion4, TxtNit4, TxtNom_entidad4, TxtValor4, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera5(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion5, TxtNit5, TxtNom_entidad5, TxtValor5, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera6(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion6, TxtNit6, TxtNom_entidad6, TxtValor6, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text != "" && TxtNom_entidad5.Text != "" && TxtNom_entidad6.Text != "" && TxtNom_entidad7.Text != "" && TxtNom_entidad8.Text == "")
            {
                cmds.Insertar_cartera1(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                    Txtobligacion1, TxtNit1, TxtNom_entidad1, TxtValor1, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera2(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion2, TxtNit2, TxtNom_entidad2, TxtValor2, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera3(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion3, TxtNit3, TxtNom_entidad3, TxtValor3, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera4(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion4, TxtNit4, TxtNom_entidad4, TxtValor4, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera5(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion5, TxtNit5, TxtNom_entidad5, TxtValor5, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera6(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion6, TxtNit6, TxtNom_entidad6, TxtValor6, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera7(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion7, TxtNit7, TxtNom_entidad7, TxtValor7, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text != "" && TxtNom_entidad5.Text != "" && TxtNom_entidad6.Text != "" && TxtNom_entidad7.Text != "" && TxtNom_entidad8.Text != "")
            {
                cmds.Insertar_cartera1(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                    Txtobligacion1, TxtNit1, TxtNom_entidad1, TxtValor1, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera2(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion2, TxtNit2, TxtNom_entidad2, TxtValor2, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera3(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion3, TxtNit3, TxtNom_entidad3, TxtValor3, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera4(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion4, TxtNit4, TxtNom_entidad4, TxtValor4, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera5(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion5, TxtNit5, TxtNom_entidad5, TxtValor5, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera6(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion6, TxtNit6, TxtNom_entidad6, TxtValor6, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera7(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion7, TxtNit7, TxtNom_entidad7, TxtValor7, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
                cmds.Insertar_cartera8(TxtRadicado, Txtcedula, Txtnombre, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtscoring, Txtcuenta,
                                       Txtobligacion8, TxtNit8, TxtNom_entidad8, TxtValor8, TxtCedula_Gestor, Txtnom_gestor, Txtcoordinador);
               
            }            
            else
            {
                MessageBox.Show("No hay carteras para almacenar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }           
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(datos_correo.correo_gerente + datos_correo.correo_subgerente);            
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            TxtRadicado.Text = ""; 
            Txtcedula.Text = "";
            Txtnombre.Text = "";
            Txtcuenta.Text = "";
            Txtscoring.Text = "";
            TxtCedula_Gestor.Text = "";
            Txtnom_gestor.Text = "";
            Txtcoordinador.Text = "";
            Txtcod_oficina.Text = "";
            Txtnom_oficina.Text = "";
            Txtciudad.Text = "";
            Txtobligacion1.Text = "";
            TxtNom_entidad1.Text = "";
            TxtNit1.Text = "";
            TxtValor1.Text = "";
            Txtobligacion2.Text = "";
            TxtNom_entidad2.Text = "";
            TxtNit2.Text = "";
            TxtValor2.Text = "";
            Txtobligacion3.Text = "";
            TxtNom_entidad3.Text = "";
            TxtNit3.Text = "";
            TxtValor3.Text = "";
            Txtobligacion4.Text = "";
            TxtNom_entidad4.Text = "";
            TxtNit4.Text = "";
            TxtValor4.Text = "";
            Txtobligacion5.Text = "";
            TxtNom_entidad5.Text = "";
            TxtNit5.Text = "";
            TxtValor5.Text = "";
            Txtobligacion6.Text = "";
            TxtNom_entidad6.Text = "";
            TxtNit6.Text = "";
            TxtValor6.Text = "";
            Txtobligacion7.Text = "";
            TxtNom_entidad7.Text = "";
            TxtNit7.Text = "";
            TxtValor7.Text = "";
            Txtobligacion8.Text = "";
            TxtNom_entidad8.Text = "";
            TxtNit8.Text = "";
            TxtValor8.Text = "";
            dataGridView1.Rows.Clear();
            dgvEndoso.Rows.Clear();
            cbCartera1.Checked = false;
            cbCartera2.Checked = false;
            cbCartera3.Checked = false;
            cbCartera4.Checked = false;
            cbCartera5.Checked = false;
            cbCartera6.Checked = false;
            cbCartera7.Checked = false;
            cbCartera8.Checked = false;
        }

        private void FormGiros_Load(object sender, EventArgs e)  
        {
            datos_correo.copia_correo = "luis.zarate@bbva.com ; CUENTAS-PAGARCF@BBVA.COM.CO ; DESGLOSESCF@bbva.com.co ; brianduvan.garzon@bbva.com ; controldecambiosfabrica.co@bbva.com";
            //datos_correo.copia_correo = "hsmartinez@indracompany.com";
        }

        private void btnAgregar_Carteras_Click(object sender, EventArgs e)
        {
            if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text == "" && TxtNom_entidad3.Text == "" && TxtNom_entidad4.Text == "" && TxtNom_entidad5.Text == "" && TxtNom_entidad6.Text == "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                   TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text == "" && TxtNom_entidad4.Text == "" && TxtNom_entidad5.Text == "" && TxtNom_entidad6.Text == "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                   TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit2.Text,
                                   TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text == "" && TxtNom_entidad5.Text == "" && TxtNom_entidad6.Text == "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                   TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit2.Text,
                                   TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit3.Text,
                                  TxtNom_entidad3.Text, TxtValor3.Text, Txtobligacion3.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text != "" && TxtNom_entidad5.Text == "" && TxtNom_entidad6.Text == "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                      TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit2.Text,
                                   TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit3.Text,
                                  TxtNom_entidad3.Text, TxtValor3.Text, Txtobligacion3.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit4.Text,
                                  TxtNom_entidad4.Text, TxtValor4.Text, Txtobligacion4.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text != "" && TxtNom_entidad5.Text != "" && TxtNom_entidad6.Text == "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                      TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit2.Text,
                                   TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit3.Text,
                                  TxtNom_entidad3.Text, TxtValor3.Text, Txtobligacion3.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit4.Text,
                                  TxtNom_entidad4.Text, TxtValor4.Text, Txtobligacion4.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit5.Text,
                                  TxtNom_entidad5.Text, TxtValor5.Text, Txtobligacion5.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text != "" && TxtNom_entidad5.Text != "" && TxtNom_entidad6.Text != "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                     TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit2.Text,
                                   TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit3.Text,
                                  TxtNom_entidad3.Text, TxtValor3.Text, Txtobligacion3.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit4.Text,
                                  TxtNom_entidad4.Text, TxtValor4.Text, Txtobligacion4.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit5.Text,
                                  TxtNom_entidad5.Text, TxtValor5.Text, Txtobligacion5.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit6.Text,
                                  TxtNom_entidad6.Text, TxtValor6.Text, Txtobligacion6.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text != "" && TxtNom_entidad5.Text != "" && TxtNom_entidad6.Text != "" && TxtNom_entidad7.Text != "" && TxtNom_entidad8.Text == "")
            {
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                     TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit2.Text,
                                   TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit3.Text,
                                  TxtNom_entidad3.Text, TxtValor3.Text, Txtobligacion3.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit4.Text,
                                  TxtNom_entidad4.Text, TxtValor4.Text, Txtobligacion4.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit5.Text,
                                  TxtNom_entidad5.Text, TxtValor5.Text, Txtobligacion5.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit6.Text,
                                  TxtNom_entidad6.Text, TxtValor6.Text, Txtobligacion6.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit7.Text,
                                  TxtNom_entidad7.Text, TxtValor7.Text, Txtobligacion7.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text != "" && TxtNom_entidad5.Text != "" && TxtNom_entidad6.Text != "" && TxtNom_entidad7.Text != "" && TxtNom_entidad8.Text != "")
            {
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                     TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit2.Text,
                                   TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit3.Text,
                                  TxtNom_entidad3.Text, TxtValor3.Text, Txtobligacion3.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit4.Text,
                                  TxtNom_entidad4.Text, TxtValor4.Text, Txtobligacion4.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit5.Text,
                                  TxtNom_entidad5.Text, TxtValor5.Text, Txtobligacion5.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit6.Text,
                                  TxtNom_entidad6.Text, TxtValor6.Text, Txtobligacion6.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit7.Text,
                                  TxtNom_entidad7.Text, TxtValor7.Text, Txtobligacion7.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit8.Text,
                                  TxtNom_entidad8.Text, TxtValor8.Text, Txtobligacion8.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
            else
            {
                MessageBox.Show("No hay carteras para remitir", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TeclaEnter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // Si es un enter
            {
                e.Handled = true; //Interceptamos la pulsación
                SendKeys.Send("{TAB}"); //Pulsamos la tecla Tabulador por código
            }
            cmds.Buscar_giro(TxtRadicado, Txtcedula, Txtnombre, Txtcuenta, Txtscoring, TxtCedula_Gestor, Txtnom_gestor,
           Txtcoordinador, Txtcod_oficina, Txtnom_oficina, Txtciudad, Txtobligacion1, TxtNom_entidad1, TxtNit1, TxtValor1,
           Txtobligacion2, TxtNom_entidad2, TxtNit2, TxtValor2, Txtobligacion3, TxtNom_entidad3, TxtNit3, TxtValor3,
           Txtobligacion4, TxtNom_entidad4, TxtNit4, TxtValor4, Txtobligacion5, TxtNom_entidad5, TxtNit5, TxtValor5,
           Txtobligacion6, TxtNom_entidad6, TxtNit6, TxtValor6, Txtobligacion7, TxtNom_entidad7, TxtNit7, TxtValor7,
           Txtobligacion8, TxtNom_entidad8, TxtNit8, TxtValor8);
        }       

        private void btnInicio_Click(object sender, EventArgs e)
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
                cmds.Pendiente_envio(TxtRadicado);
                if (TxtRadicado.Text!="")
                {
                    BtnBuscar.PerformClick();
                    if (Txtnombre.Text != "")
                    {
                        btnEnviar_Correo.PerformClick();
                        cmds.Actualizar_envio(TxtRadicado);
                        TxtRadicado.Text = "";
                        dataGridView1.Rows.Clear();
                        dataGridView1.Refresh();
                    }
                    else
                    {
                        timer.Enabled = false;
                        timer.Stop();
                        MessageBox.Show("Radicado no se encuentra en la base, se procede a detener el servicio de envio automatico de correos, una vez agregado el radicado proceder a activar nuevamente el servicio");                        
                    }                   
                }
                else
                {
                    timer.Enabled = false;
                    timer.Stop();
                    MessageBox.Show("No hay correos para remitir");                    
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDetenerServicio_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timer.Stop();
        }

        private void btn_Añadir_EnvioMAIL_Click(object sender, EventArgs e)
        {
            cmds.Añadir_envio_correo(TxtRadicado);
        }

        private void Limpiar_Checkbox()
        {
            cbCartera1.Checked = false;
            cbCartera2.Checked = false;
            cbCartera3.Checked = false;
            cbCartera4.Checked = false;
            cbCartera5.Checked = false;
            cbCartera6.Checked = false;
            cbCartera7.Checked = false;
            cbCartera8.Checked = false;
        }      

        private void BtnUnificar_Carteras_Click(object sender, EventArgs e)
        {
            double Total = 0;
            string Nit ="";
            string Entidad = "";            
            foreach (DataGridViewRow row in dgvEndoso2.Rows)
            {
                Total += Convert.ToDouble(row.Cells[9].Value);
                Nit =  row.Cells[7].Value.ToString();
                Entidad = row.Cells[8].Value.ToString();                
            }
            dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, Nit,
                                                  Entidad, Total.ToString("C"), "1", Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            Limpiar_Checkbox();
            dgvEndoso2.Rows.Clear();
        }

        private void Add1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                   TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
        }

        private void Add2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, 
                                   TxtNit2.Text,TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
        }

        private void Add3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text,
                                   TxtNit3.Text, TxtNom_entidad3.Text, TxtValor3.Text, Txtobligacion3.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
        }

        private void Add4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text,
                                   TxtNit4.Text, TxtNom_entidad4.Text, TxtValor4.Text, Txtobligacion4.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
        }

        private void Add5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text,
                                   TxtNit5.Text, TxtNom_entidad5.Text, TxtValor5.Text, Txtobligacion5.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
        }

        private void Add6_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text,
                                   TxtNit6.Text, TxtNom_entidad6.Text, TxtValor6.Text, Txtobligacion6.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
        }

        private void Add7_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text,
                                   TxtNit7.Text, TxtNom_entidad7.Text, TxtValor7.Text, Txtobligacion7.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
        }

        private void Add8_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text,
                                   TxtNit8.Text, TxtNom_entidad8.Text, TxtValor8.Text, Txtobligacion8.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
        }

        private void BtnLimpiarEndoso_Click(object sender, EventArgs e)
        {
            dgvEndoso.Rows.Clear();
        }

        private void cbCartera1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCartera1.Checked==true)
            {
                dgvEndoso.Rows.Add(TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text);
                dgvEndoso2.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                      TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }            
        }

        private void cbCartera2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCartera2.Checked == true)
            {
                dgvEndoso.Rows.Add(TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text);
                dgvEndoso2.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit2.Text,
                                      TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }            
        }

        private void cbCartera3_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCartera3.Checked == true)
            {
                dgvEndoso.Rows.Add(TxtNom_entidad3.Text, TxtValor3.Text, Txtobligacion3.Text);
                dgvEndoso2.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit3.Text,
                                      TxtNom_entidad3.Text, TxtValor3.Text, Txtobligacion3.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
        }

        private void cbCartera4_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCartera4.Checked == true)
            {
                dgvEndoso.Rows.Add(TxtNom_entidad4.Text, TxtValor4.Text, Txtobligacion4.Text);
                dgvEndoso2.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit4.Text,
                                      TxtNom_entidad4.Text, TxtValor4.Text, Txtobligacion4.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
        }

        private void cbCartera5_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCartera5.Checked == true)
            {
                dgvEndoso.Rows.Add(TxtNom_entidad5.Text, TxtValor5.Text, Txtobligacion5.Text);
                dgvEndoso2.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit5.Text,
                                      TxtNom_entidad5.Text, TxtValor5.Text, Txtobligacion5.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
        }

        private void cbCartera6_CheckedChanged_1(object sender, EventArgs e)
        {
            if (cbCartera6.Checked == true)
            {
                dgvEndoso.Rows.Add(TxtNom_entidad6.Text, TxtValor6.Text, Txtobligacion6.Text);
                dgvEndoso2.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit6.Text,
                                      TxtNom_entidad6.Text, TxtValor6.Text, Txtobligacion6.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
        }

        private void cbCartera7_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCartera7.Checked == true)
            {
                dgvEndoso.Rows.Add(TxtNom_entidad7.Text, TxtValor7.Text, Txtobligacion7.Text);
                dgvEndoso2.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit7.Text,
                                      TxtNom_entidad7.Text, TxtValor7.Text, Txtobligacion7.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
        }

        private void cbCartera8_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCartera8.Checked == true)
            {
                dgvEndoso.Rows.Add(TxtNom_entidad8.Text, TxtValor8.Text, Txtobligacion8.Text);
                dgvEndoso2.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit8.Text,
                                      TxtNom_entidad8.Text, TxtValor8.Text, Txtobligacion8.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
        }

        private void btnAgregar_Carteras_Click_1(object sender, EventArgs e)
        {

            if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text == "" && TxtNom_entidad3.Text == "" && TxtNom_entidad4.Text == "" && TxtNom_entidad5.Text == "" && TxtNom_entidad6.Text == "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                   TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text == "" && TxtNom_entidad4.Text == "" && TxtNom_entidad5.Text == "" && TxtNom_entidad6.Text == "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                   TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit2.Text,
                                   TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text == "" && TxtNom_entidad5.Text == "" && TxtNom_entidad6.Text == "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                   TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit2.Text,
                                   TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit3.Text,
                                  TxtNom_entidad3.Text, TxtValor3.Text, Txtobligacion3.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text != "" && TxtNom_entidad5.Text == "" && TxtNom_entidad6.Text == "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                      TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit2.Text,
                                   TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit3.Text,
                                  TxtNom_entidad3.Text, TxtValor3.Text, Txtobligacion3.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit4.Text,
                                  TxtNom_entidad4.Text, TxtValor4.Text, Txtobligacion4.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text != "" && TxtNom_entidad5.Text != "" && TxtNom_entidad6.Text == "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                      TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit2.Text,
                                   TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit3.Text,
                                  TxtNom_entidad3.Text, TxtValor3.Text, Txtobligacion3.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit4.Text,
                                  TxtNom_entidad4.Text, TxtValor4.Text, Txtobligacion4.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit5.Text,
                                  TxtNom_entidad5.Text, TxtValor5.Text, Txtobligacion5.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text != "" && TxtNom_entidad5.Text != "" && TxtNom_entidad6.Text != "" && TxtNom_entidad7.Text == "" && TxtNom_entidad8.Text == "")
            {
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                     TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit2.Text,
                                   TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit3.Text,
                                  TxtNom_entidad3.Text, TxtValor3.Text, Txtobligacion3.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit4.Text,
                                  TxtNom_entidad4.Text, TxtValor4.Text, Txtobligacion4.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit5.Text,
                                  TxtNom_entidad5.Text, TxtValor5.Text, Txtobligacion5.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit6.Text,
                                  TxtNom_entidad6.Text, TxtValor6.Text, Txtobligacion6.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text != "" && TxtNom_entidad5.Text != "" && TxtNom_entidad6.Text != "" && TxtNom_entidad7.Text != "" && TxtNom_entidad8.Text == "")
            {
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                     TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit2.Text,
                                   TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit3.Text,
                                  TxtNom_entidad3.Text, TxtValor3.Text, Txtobligacion3.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit4.Text,
                                  TxtNom_entidad4.Text, TxtValor4.Text, Txtobligacion4.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit5.Text,
                                  TxtNom_entidad5.Text, TxtValor5.Text, Txtobligacion5.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit6.Text,
                                  TxtNom_entidad6.Text, TxtValor6.Text, Txtobligacion6.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit7.Text,
                                  TxtNom_entidad7.Text, TxtValor7.Text, Txtobligacion7.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
            else if (TxtNom_entidad1.Text != "" && TxtNom_entidad2.Text != "" && TxtNom_entidad3.Text != "" && TxtNom_entidad4.Text != "" && TxtNom_entidad5.Text != "" && TxtNom_entidad6.Text != "" && TxtNom_entidad7.Text != "" && TxtNom_entidad8.Text != "")
            {
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit1.Text,
                                     TxtNom_entidad1.Text, TxtValor1.Text, Txtobligacion1.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit2.Text,
                                   TxtNom_entidad2.Text, TxtValor2.Text, Txtobligacion2.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit3.Text,
                                  TxtNom_entidad3.Text, TxtValor3.Text, Txtobligacion3.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit4.Text,
                                  TxtNom_entidad4.Text, TxtValor4.Text, Txtobligacion4.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit5.Text,
                                  TxtNom_entidad5.Text, TxtValor5.Text, Txtobligacion5.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit6.Text,
                                  TxtNom_entidad6.Text, TxtValor6.Text, Txtobligacion6.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit7.Text,
                                  TxtNom_entidad7.Text, TxtValor7.Text, Txtobligacion7.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
                dataGridView1.Rows.Add(TxtRadicado.Text, Txtcod_oficina.Text, hoy.ToShortDateString(), Txtnom_oficina.Text, Txtciudad.Text, Txtcedula.Text, Txtnombre.Text, TxtNit8.Text,
                                  TxtNom_entidad8.Text, TxtValor8.Text, Txtobligacion8.Text, Txtscoring.Text, Txtnom_gestor.Text, Txtcoordinador.Text, Txtcuenta.Text, "LIBRANZA");
            }
            else
            {
                MessageBox.Show("No hay carteras para remitir", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
