﻿using ProyectoBase1.AccesoADatos;
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
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            try
            {
                grillaUsuarios.DataSource = AD_Usuarios.ObtenerListadoUsuarios();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al  hacer lo que tenia que hacer");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtUsuario.Text = "";
            txtPassword.Text = "";
            txtRepetirPassword.Text = "";
        }

        private void btnAltaUsuario_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Equals(""))
            {
                MessageBox.Show("Ingrese Nombre de usuario");
            }
            else
            {
                if (txtPassword.Text.Equals(txtRepetirPassword.Text))
                {
                    try
                    {
                        bool resultado = AD_Usuarios.InsertarUsuario(txtUsuario.Text, txtPassword.Text);
                        if (resultado)
                        {
                            MessageBox.Show("Usuario alta exito");
                            LimpiarCampos();
                            CargarGrilla();
                            txtUsuario.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Error al ingresar nuevo usuario");
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Error al sinsertar nuevo usuario " + ex.Message);
                        txtUsuario.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Los passwords no coinciden");
                }
            }
        }
    }   
}
