﻿using System;
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
    class Comandos
    {
        MySqlConnection con = new MySqlConnection("server=;Uid=;password=;database=;port=3306;persistsecurityinfo=True;");

        DateTime hoy = DateTime.Now;

        public void Accesso_Aplicacion()
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand("control_acceso_aplicaciones", con);
            MySqlTransaction myTrans; // Iniciar una transacción local 
            myTrans = con.BeginTransaction(); // Debe asignar tanto el objeto de transacción como la conexión // al objeto de Comando para una transacción local pendiente
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Usuario", usuario.Nombre);
                cmd.Parameters.AddWithValue("@_Aplicacion", "Giros");
                cmd.ExecuteNonQuery();
                myTrans.Commit();
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void guardar_Asignacion_cheques(TextBox TxtRadicado, DateTimePicker dtpFecha_Ingreso, DateTimePicker dtpHora_Ingreso, DateTimePicker dtpHora_Asignacion,
                                             ComboBox cmbGirador, DateTimePicker dtpHora_Alistamiento, DateTimePicker dtpEntrega_Banco, DateTimePicker dtpHora_Entrega_Banco,
                                             TextBox txtFirma_Banco1, TextBox txtFirma_Banco2 , TextBox txtN_Cheques, DateTimePicker dtpEntrega_Front, ComboBox cmbEstado_Operacion,
                                             DateTimePicker dtp_Hora_Custodio, TextBox txtObservaciones)
        {

            con.Open();
            MySqlCommand cmd = new MySqlCommand("guardar_Asignacion_cheques", con);
            MySqlTransaction myTrans; // Iniciar una transacción local 
            myTrans = con.BeginTransaction(); // Debe asignar tanto el objeto de transacción como la conexión // al objeto de Comando para una transacción local pendiente 
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Radicado", TxtRadicado.Text);
                cmd.Parameters.AddWithValue("@_Fecha_Ingreso", dtpFecha_Ingreso.Text);
                cmd.Parameters.AddWithValue("@_Hora_Ingreso", dtpHora_Ingreso.Text);
                cmd.Parameters.AddWithValue("@_Hora_Asignacion", dtpHora_Asignacion.Text);
                cmd.Parameters.AddWithValue("@_Girador", cmbGirador.Text);
                cmd.Parameters.AddWithValue("@_Hora_Alistamiento", dtpHora_Alistamiento.Text);
                cmd.Parameters.AddWithValue("@_Entrega_Banco", dtpEntrega_Banco.Text);
                cmd.Parameters.AddWithValue("@_Hora_Entrega_Banco", dtpHora_Entrega_Banco);
                cmd.Parameters.AddWithValue("@_Firma_Banco1", txtFirma_Banco1.Text);
                cmd.Parameters.AddWithValue("@_Firma_Banco2", txtFirma_Banco2.Text);
                cmd.Parameters.AddWithValue("@_N_Cheques", txtN_Cheques.Text);
                cmd.Parameters.AddWithValue("@_Entrega_Front", dtpEntrega_Front.Text);
                cmd.Parameters.AddWithValue("@_Estado_Operacion", cmbEstado_Operacion.Text);
                cmd.Parameters.AddWithValue("@_Hora_Custodio", dtp_Hora_Custodio.Text);
                cmd.Parameters.AddWithValue("@_Observaciones", txtObservaciones.Text);
                cmd.ExecuteNonQuery();
                myTrans.Commit();
                MessageBox.Show("Información Registrada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                con.Close();
                MessageBox.Show("Conexion cerrada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Buscar_giro(TextBox TxtRadicado, TextBox Txtcedula, TextBox Txtnombre, TextBox Txtcuenta, TextBox Txtscoring, TextBox TxtCedula_Gestor, TextBox Txtnom_gestor,
           TextBox Txtcoordinador, TextBox Txtcod_oficina, TextBox Txtnom_oficina, TextBox Txtciudad, TextBox Txtobligacion1, TextBox TxtNom_entidad1, TextBox TxtNit1, TextBox TxtValor1,
           TextBox Txtobligacion2, TextBox TxtNom_entidad2, TextBox TxtNit2, TextBox TxtValor2, TextBox Txtobligacion3, TextBox TxtNom_entidad3, TextBox TxtNit3, TextBox TxtValor3,
           TextBox Txtobligacion4, TextBox TxtNom_entidad4, TextBox TxtNit4, TextBox TxtValor4, TextBox Txtobligacion5, TextBox TxtNom_entidad5, TextBox TxtNit5, TextBox TxtValor5,
           TextBox Txtobligacion6, TextBox TxtNom_entidad6, TextBox TxtNit6, TextBox TxtValor6, TextBox Txtobligacion7, TextBox TxtNom_entidad7, TextBox TxtNit7, TextBox TxtValor7,
           TextBox Txtobligacion8, TextBox TxtNom_entidad8, TextBox TxtNit8, TextBox TxtValor8)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("buscar_giros", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_Radicado", TxtRadicado.Text);
                MySqlDataReader registro;
                registro = cmd.ExecuteReader();
                if (registro.Read())
                {
                    Txtcedula.Text = registro["cedula"].ToString();
                    Txtnombre.Text = registro["nombre"].ToString();
                    Txtcuenta.Text = registro["cuenta"].ToString();
                    Txtscoring.Text = registro["scoring"].ToString();
                    TxtCedula_Gestor.Text = registro["cedula_gestor"].ToString();              
                    Txtcoordinador.Text = registro["nombre_coordinador"].ToString();
                    Txtcod_oficina.Text = registro["codigo_oficina"].ToString();
                    Txtnom_oficina.Text = registro["sucursal"].ToString();
                    Txtciudad.Text = registro["ciudad"].ToString();
                    Txtobligacion1.Text = registro["numero_obligacion1"].ToString();
                    TxtNom_entidad1.Text = registro["nombre_entidad1"].ToString();
                    TxtNit1.Text = registro["nit_entidad1"].ToString();
                    TxtValor1.Text = registro["valor_cartera1"].ToString();
                    Txtobligacion2.Text = registro["numero_obligacion2"].ToString();
                    TxtNom_entidad2.Text = registro["nombre_entidad2"].ToString();
                    TxtNit2.Text = registro["nit_entidad2"].ToString();
                    TxtValor2.Text = registro["valor_cartera2"].ToString();
                    Txtobligacion3.Text = registro["numero_obligacion3"].ToString();
                    TxtNom_entidad3.Text = registro["nombre_entidad3"].ToString();
                    TxtNit3.Text = registro["nit_entidad3"].ToString();
                    TxtValor3.Text = registro["valor_cartera3"].ToString();
                    Txtobligacion4.Text = registro["numero_obligacion4"].ToString();
                    TxtNom_entidad4.Text = registro["nombre_entidad4"].ToString();
                    TxtNit4.Text = registro["nit_entidad4"].ToString();
                    TxtValor4.Text = registro["valor_cartera4"].ToString();
                    Txtobligacion5.Text = registro["numero_obligacion5"].ToString();
                    TxtNom_entidad5.Text = registro["nombre_entidad5"].ToString();
                    TxtNit5.Text = registro["nit_entidad5"].ToString();
                    TxtValor5.Text = registro["valor_cartera5"].ToString();
                    Txtobligacion6.Text = registro["numero_obligacion6"].ToString();
                    TxtNom_entidad6.Text = registro["nombre_entidad6"].ToString();
                    TxtNit6.Text = registro["nit_entidad6"].ToString();
                    TxtValor6.Text = registro["valor_cartera6"].ToString();
                    Txtobligacion7.Text = registro["numero_obligacion7"].ToString();
                    TxtNom_entidad7.Text = registro["nombre_entidad7"].ToString();
                    TxtNit7.Text = registro["nit_entidad7"].ToString();
                    TxtValor7.Text = registro["valor_cartera7"].ToString();
                    Txtobligacion8.Text = registro["numero_obligacion8"].ToString();
                    TxtNom_entidad8.Text = registro["nombre_entidad8"].ToString();
                    TxtNit8.Text = registro["nit_entidad8"].ToString();
                    TxtValor8.Text = registro["valor_cartera8"].ToString();
                    datos_correo.oficina = registro["codigo_oficina"].ToString();
                    con.Close();
                }
                else
                {
                    //MessageBox.Show("Caso no existe", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Caso no existe", ex.ToString());
                con.Close();
                MessageBox.Show("Conexion cerrada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Insertar_cartera1(TextBox TxtRadicado, TextBox Txtcedula, TextBox Txtnombre,TextBox Txtcod_oficina, 
                                      TextBox Txtnom_oficina, TextBox Txtciudad, TextBox Txtscoring, TextBox Txtcuenta, 
                                      TextBox Txtobligacion1, TextBox TxtNit1, TextBox TxtNom_entidad1, TextBox TxtValor1,
                                      TextBox TxtCedula_Gestor, TextBox Txtnom_gestor, TextBox Txtcoordinador)
        {
            try
            {
                con.Open();                
                MySqlCommand cmd = new MySqlCommand("insertar_giro", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_radicado", TxtRadicado.Text);
                cmd.Parameters.AddWithValue("@_cedula", Txtcedula.Text);
                cmd.Parameters.AddWithValue("@_nombre", Txtnombre.Text);
                cmd.Parameters.AddWithValue("@_codigo_oficina", Txtcod_oficina.Text);
                cmd.Parameters.AddWithValue("@_sucursal", Txtnom_oficina.Text);
                cmd.Parameters.AddWithValue("@_ciudad", Txtciudad.Text);
                cmd.Parameters.AddWithValue("@_scoring", Txtscoring.Text);
                cmd.Parameters.AddWithValue("@_cuenta", Txtcuenta.Text);
                cmd.Parameters.AddWithValue("@_numero_obligacion1", Txtobligacion1.Text);
                cmd.Parameters.AddWithValue("@_nit_entidad1", TxtNit1.Text);
                cmd.Parameters.AddWithValue("@_nombre_entidad1", TxtNom_entidad1.Text);                
                if (TxtValor1.Text != "")
                {
                    cmd.Parameters.AddWithValue("@_valor_cartera1", string.Format("{0:#}", double.Parse(TxtValor1.Text)));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@_valor_cartera1", TxtValor1.Text);
                }
                cmd.Parameters.AddWithValue("@_cedula_gestor", TxtCedula_Gestor.Text);
                cmd.Parameters.AddWithValue("@_nombre_gestor", Txtnom_gestor.Text);
                cmd.Parameters.AddWithValue("@_nombre_coordinador", Txtcoordinador.Text);
                cmd.Parameters.AddWithValue("@_fecha_giro", hoy.ToShortDateString());
                cmd.ExecuteNonQuery();                             
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Error al insertar los datos", ex.ToString());
                con.Close();
                MessageBox.Show("Conexion cerrada");
            }
        }
        public void Insertar_cartera2(TextBox TxtRadicado, TextBox Txtcedula, TextBox Txtnombre, TextBox Txtcod_oficina,
                                     TextBox Txtnom_oficina, TextBox Txtciudad, TextBox Txtscoring, TextBox Txtcuenta,
                                     TextBox Txtobligacion2, TextBox TxtNit2, TextBox TxtNom_entidad2, TextBox TxtValor2,
                                     TextBox TxtCedula_Gestor, TextBox Txtnom_gestor, TextBox Txtcoordinador)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("insertar_giro", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_radicado", TxtRadicado.Text);
                cmd.Parameters.AddWithValue("@_cedula", Txtcedula.Text);
                cmd.Parameters.AddWithValue("@_nombre", Txtnombre.Text);
                cmd.Parameters.AddWithValue("@_codigo_oficina", Txtcod_oficina.Text);
                cmd.Parameters.AddWithValue("@_sucursal", Txtnom_oficina.Text);
                cmd.Parameters.AddWithValue("@_ciudad", Txtciudad.Text);
                cmd.Parameters.AddWithValue("@_scoring", Txtscoring.Text);
                cmd.Parameters.AddWithValue("@_cuenta", Txtcuenta.Text);
                cmd.Parameters.AddWithValue("@_numero_obligacion1", Txtobligacion2.Text);
                cmd.Parameters.AddWithValue("@_nit_entidad1", TxtNit2.Text);
                cmd.Parameters.AddWithValue("@_nombre_entidad1", TxtNom_entidad2.Text);
                if (TxtValor2.Text != "")
                {
                    cmd.Parameters.AddWithValue("@_valor_cartera1", string.Format("{0:#}", double.Parse(TxtValor2.Text)));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@_valor_cartera1", TxtValor2.Text);
                }
                cmd.Parameters.AddWithValue("@_cedula_gestor", TxtCedula_Gestor.Text);
                cmd.Parameters.AddWithValue("@_nombre_gestor", Txtnom_gestor.Text);
                cmd.Parameters.AddWithValue("@_nombre_coordinador", Txtcoordinador.Text);
                cmd.Parameters.AddWithValue("@_fecha_giro", hoy.ToShortDateString());
                cmd.ExecuteNonQuery();                
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Error al insertar los datos", ex.ToString());
                con.Close();
                MessageBox.Show("Conexion cerrada");
            }
        }
        public void Insertar_cartera3(TextBox TxtRadicado, TextBox Txtcedula, TextBox Txtnombre, TextBox Txtcod_oficina,
                                     TextBox Txtnom_oficina, TextBox Txtciudad, TextBox Txtscoring, TextBox Txtcuenta,
                                     TextBox Txtobligacion3, TextBox TxtNit3, TextBox TxtNom_entidad3, TextBox TxtValor3,
                                     TextBox TxtCedula_Gestor, TextBox Txtnom_gestor, TextBox Txtcoordinador)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("insertar_giro", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_radicado", TxtRadicado.Text);
                cmd.Parameters.AddWithValue("@_cedula", Txtcedula.Text);
                cmd.Parameters.AddWithValue("@_nombre", Txtnombre.Text);
                cmd.Parameters.AddWithValue("@_codigo_oficina", Txtcod_oficina.Text);
                cmd.Parameters.AddWithValue("@_sucursal", Txtnom_oficina.Text);
                cmd.Parameters.AddWithValue("@_ciudad", Txtciudad.Text);
                cmd.Parameters.AddWithValue("@_scoring", Txtscoring.Text);
                cmd.Parameters.AddWithValue("@_cuenta", Txtcuenta.Text);
                cmd.Parameters.AddWithValue("@_numero_obligacion1", Txtobligacion3.Text);
                cmd.Parameters.AddWithValue("@_nit_entidad1", TxtNit3.Text);
                cmd.Parameters.AddWithValue("@_nombre_entidad1", TxtNom_entidad3.Text);
                if (TxtValor3.Text != "")
                {
                    cmd.Parameters.AddWithValue("@_valor_cartera1", string.Format("{0:#}", double.Parse(TxtValor3.Text)));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@_valor_cartera1", TxtValor3.Text);
                }
                cmd.Parameters.AddWithValue("@_cedula_gestor", TxtCedula_Gestor.Text);
                cmd.Parameters.AddWithValue("@_nombre_gestor", Txtnom_gestor.Text);
                cmd.Parameters.AddWithValue("@_nombre_coordinador", Txtcoordinador.Text);
                cmd.Parameters.AddWithValue("@_fecha_giro", hoy.ToShortDateString());
                cmd.ExecuteNonQuery();                
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Error al insertar los datos", ex.ToString());
                con.Close();
                MessageBox.Show("Conexion cerrada");
            }
        }
        public void Insertar_cartera4(TextBox TxtRadicado, TextBox Txtcedula, TextBox Txtnombre, TextBox Txtcod_oficina,
                                     TextBox Txtnom_oficina, TextBox Txtciudad, TextBox Txtscoring, TextBox Txtcuenta,
                                     TextBox Txtobligacion4, TextBox TxtNit4, TextBox TxtNom_entidad4, TextBox TxtValor4,
                                     TextBox TxtCedula_Gestor, TextBox Txtnom_gestor, TextBox Txtcoordinador)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("insertar_giro", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_radicado", TxtRadicado.Text);
                cmd.Parameters.AddWithValue("@_cedula", Txtcedula.Text);
                cmd.Parameters.AddWithValue("@_nombre", Txtnombre.Text);
                cmd.Parameters.AddWithValue("@_codigo_oficina", Txtcod_oficina.Text);
                cmd.Parameters.AddWithValue("@_sucursal", Txtnom_oficina.Text);
                cmd.Parameters.AddWithValue("@_ciudad", Txtciudad.Text);
                cmd.Parameters.AddWithValue("@_scoring", Txtscoring.Text);
                cmd.Parameters.AddWithValue("@_cuenta", Txtcuenta.Text);
                cmd.Parameters.AddWithValue("@_numero_obligacion1", Txtobligacion4.Text);
                cmd.Parameters.AddWithValue("@_nit_entidad1", TxtNit4.Text);
                cmd.Parameters.AddWithValue("@_nombre_entidad1", TxtNom_entidad4.Text);
                if (TxtValor4.Text != "")
                {
                    cmd.Parameters.AddWithValue("@_valor_cartera1", string.Format("{0:#}", double.Parse(TxtValor4.Text)));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@_valor_cartera1", TxtValor4.Text);
                }
                cmd.Parameters.AddWithValue("@_cedula_gestor", TxtCedula_Gestor.Text);
                cmd.Parameters.AddWithValue("@_nombre_gestor", Txtnom_gestor.Text);
                cmd.Parameters.AddWithValue("@_nombre_coordinador", Txtcoordinador.Text);
                cmd.Parameters.AddWithValue("@_fecha_giro", hoy.ToShortDateString());
                cmd.ExecuteNonQuery();                
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Error al insertar los datos", ex.ToString());
                con.Close();
                MessageBox.Show("Conexion cerrada");
            }
        }
        public void Insertar_cartera5(TextBox TxtRadicado, TextBox Txtcedula, TextBox Txtnombre, TextBox Txtcod_oficina,
                                     TextBox Txtnom_oficina, TextBox Txtciudad, TextBox Txtscoring, TextBox Txtcuenta,
                                     TextBox Txtobligacion5, TextBox TxtNit5, TextBox TxtNom_entidad5, TextBox TxtValor5,
                                     TextBox TxtCedula_Gestor, TextBox Txtnom_gestor, TextBox Txtcoordinador)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("insertar_giro", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_radicado", TxtRadicado.Text);
                cmd.Parameters.AddWithValue("@_cedula", Txtcedula.Text);
                cmd.Parameters.AddWithValue("@_nombre", Txtnombre.Text);
                cmd.Parameters.AddWithValue("@_codigo_oficina", Txtcod_oficina.Text);
                cmd.Parameters.AddWithValue("@_sucursal", Txtnom_oficina.Text);
                cmd.Parameters.AddWithValue("@_ciudad", Txtciudad.Text);
                cmd.Parameters.AddWithValue("@_scoring", Txtscoring.Text);
                cmd.Parameters.AddWithValue("@_cuenta", Txtcuenta.Text);
                cmd.Parameters.AddWithValue("@_numero_obligacion1", Txtobligacion5.Text);
                cmd.Parameters.AddWithValue("@_nit_entidad1", TxtNit5.Text);
                cmd.Parameters.AddWithValue("@_nombre_entidad1", TxtNom_entidad5.Text);
                if (TxtValor5.Text != "")
                {
                    cmd.Parameters.AddWithValue("@_valor_cartera1", string.Format("{0:#}", double.Parse(TxtValor5.Text)));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@_valor_cartera1", TxtValor5.Text);
                }
                cmd.Parameters.AddWithValue("@_cedula_gestor", TxtCedula_Gestor.Text);
                cmd.Parameters.AddWithValue("@_nombre_gestor", Txtnom_gestor.Text);
                cmd.Parameters.AddWithValue("@_nombre_coordinador", Txtcoordinador.Text);
                cmd.Parameters.AddWithValue("@_fecha_giro", hoy.ToShortDateString());
                cmd.ExecuteNonQuery();                
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Error al insertar los datos", ex.ToString());
                con.Close();
                MessageBox.Show("Conexion cerrada");
            }
        }
        public void Insertar_cartera6(TextBox TxtRadicado, TextBox Txtcedula, TextBox Txtnombre, TextBox Txtcod_oficina,
                                     TextBox Txtnom_oficina, TextBox Txtciudad, TextBox Txtscoring, TextBox Txtcuenta,
                                     TextBox Txtobligacion6, TextBox TxtNit6, TextBox TxtNom_entidad6, TextBox TxtValor6,
                                     TextBox TxtCedula_Gestor, TextBox Txtnom_gestor, TextBox Txtcoordinador)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("insertar_giro", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_radicado", TxtRadicado.Text);
                cmd.Parameters.AddWithValue("@_cedula", Txtcedula.Text);
                cmd.Parameters.AddWithValue("@_nombre", Txtnombre.Text);
                cmd.Parameters.AddWithValue("@_codigo_oficina", Txtcod_oficina.Text);
                cmd.Parameters.AddWithValue("@_sucursal", Txtnom_oficina.Text);
                cmd.Parameters.AddWithValue("@_ciudad", Txtciudad.Text);
                cmd.Parameters.AddWithValue("@_scoring", Txtscoring.Text);
                cmd.Parameters.AddWithValue("@_cuenta", Txtcuenta.Text);
                cmd.Parameters.AddWithValue("@_numero_obligacion1", Txtobligacion6.Text);
                cmd.Parameters.AddWithValue("@_nit_entidad1", TxtNit6.Text);
                cmd.Parameters.AddWithValue("@_nombre_entidad1", TxtNom_entidad6.Text);
                if (TxtValor6.Text != "")
                {
                    cmd.Parameters.AddWithValue("@_valor_cartera1", string.Format("{0:#}", double.Parse(TxtValor6.Text)));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@_valor_cartera1", TxtValor6.Text);
                }
                cmd.Parameters.AddWithValue("@_cedula_gestor", TxtCedula_Gestor.Text);
                cmd.Parameters.AddWithValue("@_nombre_gestor", Txtnom_gestor.Text);
                cmd.Parameters.AddWithValue("@_nombre_coordinador", Txtcoordinador.Text);
                cmd.Parameters.AddWithValue("@_fecha_giro", hoy.ToShortDateString());
                cmd.ExecuteNonQuery();                
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Error al insertar los datos", ex.ToString());
                con.Close();
                MessageBox.Show("Conexion cerrada");
            }
        }
        public void Insertar_cartera7(TextBox TxtRadicado, TextBox Txtcedula, TextBox Txtnombre, TextBox Txtcod_oficina,
                                     TextBox Txtnom_oficina, TextBox Txtciudad, TextBox Txtscoring, TextBox Txtcuenta,
                                     TextBox Txtobligacion7, TextBox TxtNit7, TextBox TxtNom_entidad7, TextBox TxtValor7,
                                     TextBox TxtCedula_Gestor, TextBox Txtnom_gestor, TextBox Txtcoordinador)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("insertar_giro", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_radicado", TxtRadicado.Text);
                cmd.Parameters.AddWithValue("@_cedula", Txtcedula.Text);
                cmd.Parameters.AddWithValue("@_nombre", Txtnombre.Text);
                cmd.Parameters.AddWithValue("@_codigo_oficina", Txtcod_oficina.Text);
                cmd.Parameters.AddWithValue("@_sucursal", Txtnom_oficina.Text);
                cmd.Parameters.AddWithValue("@_ciudad", Txtciudad.Text);
                cmd.Parameters.AddWithValue("@_scoring", Txtscoring.Text);
                cmd.Parameters.AddWithValue("@_cuenta", Txtcuenta.Text);
                cmd.Parameters.AddWithValue("@_numero_obligacion1", Txtobligacion7.Text);
                cmd.Parameters.AddWithValue("@_nit_entidad1", TxtNit7.Text);
                cmd.Parameters.AddWithValue("@_nombre_entidad1", TxtNom_entidad7.Text);
                if (TxtValor7.Text != "")
                {
                    cmd.Parameters.AddWithValue("@_valor_cartera1", string.Format("{0:#}", double.Parse(TxtValor7.Text)));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@_valor_cartera1", TxtValor7.Text);
                }
                cmd.Parameters.AddWithValue("@_cedula_gestor", TxtCedula_Gestor.Text);
                cmd.Parameters.AddWithValue("@_nombre_gestor", Txtnom_gestor.Text);
                cmd.Parameters.AddWithValue("@_nombre_coordinador", Txtcoordinador.Text);
                cmd.Parameters.AddWithValue("@_fecha_giro", hoy.ToShortDateString());
                cmd.ExecuteNonQuery();                
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Error al insertar los datos", ex.ToString());
                con.Close();
                MessageBox.Show("Conexion cerrada");
            }
        }
        public void Insertar_cartera8(TextBox TxtRadicado, TextBox Txtcedula, TextBox Txtnombre, TextBox Txtcod_oficina,
                                     TextBox Txtnom_oficina, TextBox Txtciudad, TextBox Txtscoring, TextBox Txtcuenta,
                                     TextBox Txtobligacion8, TextBox TxtNit8, TextBox TxtNom_entidad8, TextBox TxtValor8,
                                     TextBox TxtCedula_Gestor, TextBox Txtnom_gestor, TextBox Txtcoordinador)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("insertar_giro", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_radicado", TxtRadicado.Text);
                cmd.Parameters.AddWithValue("@_cedula", Txtcedula.Text);
                cmd.Parameters.AddWithValue("@_nombre", Txtnombre.Text);
                cmd.Parameters.AddWithValue("@_codigo_oficina", Txtcod_oficina.Text);
                cmd.Parameters.AddWithValue("@_sucursal", Txtnom_oficina.Text);
                cmd.Parameters.AddWithValue("@_ciudad", Txtciudad.Text);
                cmd.Parameters.AddWithValue("@_scoring", Txtscoring.Text);
                cmd.Parameters.AddWithValue("@_cuenta", Txtcuenta.Text);
                cmd.Parameters.AddWithValue("@_numero_obligacion1", Txtobligacion8.Text);
                cmd.Parameters.AddWithValue("@_nit_entidad1", TxtNit8.Text);
                cmd.Parameters.AddWithValue("@_nombre_entidad1", TxtNom_entidad8.Text);
                if (TxtValor8.Text != "")
                {
                    cmd.Parameters.AddWithValue("@_valor_cartera1", string.Format("{0:#}", double.Parse(TxtValor8.Text)));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@_valor_cartera1", TxtValor8.Text);
                }
                cmd.Parameters.AddWithValue("@_cedula_gestor", TxtCedula_Gestor.Text);
                cmd.Parameters.AddWithValue("@_nombre_gestor", Txtnom_gestor.Text);
                cmd.Parameters.AddWithValue("@_nombre_coordinador", Txtcoordinador.Text);
                cmd.Parameters.AddWithValue("@_fecha_giro", hoy.ToShortDateString());
                cmd.ExecuteNonQuery();               
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Error al insertar los datos", ex.ToString());
                con.Close();
                MessageBox.Show("Conexion cerrada");
            }
        }

        public void Base_punto(DateTimePicker dtpFecha_Punto, DataGridView dgvDatos_Punto)
        {
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                MySqlCommand cmd = new MySqlCommand("base_punto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_fecha_giro", dtpFecha_Punto.Text);                
                MySqlDataAdapter registro = new MySqlDataAdapter(cmd);
                registro.Fill(dt);
                dgvDatos_Punto.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("", ex.ToString());
                con.Close();
                MessageBox.Show("Conexion cerrada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Pendiente_envio(TextBox TxtRadicado)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("pendiente_envio", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_estado_correo", "Enviar");
                MySqlDataReader registro;
                registro = cmd.ExecuteReader();
                if (registro.Read())
                {
                   TxtRadicado.Text = registro["radicado"].ToString();                   
                   con.Close();
                }
                else
                {
                    //MessageBox.Show("Caso no existe", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Caso no existe", ex.ToString());
                con.Close();
                MessageBox.Show("Conexion cerrada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Actualizar_envio(TextBox TxtRadicado)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("actualizar_envio", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_radicado", TxtRadicado.Text);                
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Error al insertar los datos", ex.ToString());
                con.Close();
                MessageBox.Show("Conexion cerrada");
            }
        }
        public void Añadir_envio_correo(TextBox TxtRadicado)
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("añadir_envio_correo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_radicado", TxtRadicado.Text);
                cmd.Parameters.AddWithValue("@_estado_correo", "Enviar");                
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ok se agrego caso para remitir correo","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Error al insertar los datos", ex.ToString());
                con.Close();
                MessageBox.Show("Conexion cerrada");
            }
        }
    }
}
