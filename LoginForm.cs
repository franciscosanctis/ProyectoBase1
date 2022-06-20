using ProyectoBase1.AccesoADatos;
using ProyectoBase1.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBase1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            //Usuario usu = new Usuario(txtUsuario.Text, txtPassword.Text);
            if (txtUsuario.Text.Equals("") || txtPassword.Text.Equals(""))
            {
                MessageBox.Show("Ingrese usuario y password");
            }
            else
            {
                string nombreDeUsuario = txtUsuario.Text;
                string password = txtPassword.Text;

                bool resultado = false;
                try
                {
                    resultado = AD_Usuarios.ValidarUsuario(nombreDeUsuario, password);
                    if (resultado)
                    {
                        Usuario usu = new Usuario(nombreDeUsuario, password);
                        PrincipalForm ventana = new PrincipalForm(usu);
                        ventana.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Usuario inexistente");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al consultar el usuario");
                }
            }



            //string usuCorrecto = "mateo";
            //string passwordCorrecto = "123456";

            //if (txtUsuario.Text.Equals(usuCorrecto) && txtPassword.Text.Equals(passwordCorrecto))
            //{
            //    MessageBox.Show("Datos correctos");
            //    PrincipalForm ventana = new PrincipalForm(usu);
            //    ventana.Show();
            //    this.Hide();
            //}
            //else
            //{
            //    MessageBox.Show("Ingrese valores correctos");
            //}
        }

        //private bool ValidarUsuario(string nombreDeUsuario, string password)
        //{
        //    string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
        //    SqlConnection cn = new SqlConnection(cadenaConexion);
        //    try
        //    {
        //        bool resultado = false;
        //        SqlCommand cmd = new SqlCommand();


        //        string consulta = "SELECT * FROM usuarios WHERE NombreDeUsuario LIKE @nombreUsu AND password LIKE @pass";

        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@nombreUsu", nombreDeUsuario);
        //        cmd.Parameters.AddWithValue("@pass", password);
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = consulta;

        //        cn.Open();
        //        cmd.Connection = cn;

        //        DataTable tabla = new DataTable();

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(tabla);

        //        if (tabla.Rows.Count == 1)
        //        {
        //            resultado = true;
        //        }
        //        else
        //        {
        //            resultado = false;
        //        }
        //        return resultado;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //    finally
        //    {
        //        cn.Close();
        //    }
            
        //}
    }
}
