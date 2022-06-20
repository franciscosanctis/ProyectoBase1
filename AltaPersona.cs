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
    public partial class AltaPersona : Form
    {
        public AltaPersona()
        {
            InitializeComponent();
        }

        private void AltaPersona_Load(object sender, EventArgs e)
        {
            LimpiarCampos();
            txtCantidadHijos.Enabled = false;
            btnActualizar.Enabled = false;
            CargarComboTiposDocumentos();
            CargarComboCarreras();
            CargarGrilla();

        }

        private void CargarGrilla()
        {
            try
            {
                grdPersonas.DataSource = AD_Personas.ObtenerListadoPersonasReducido();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al obtener personas");
            }
        }

        private void CargarComboTiposDocumentos()
        {
            try
            {                
                cmbTipo.DataSource = AD_Varios.ObtenerTiposDeDocumentos();
                cmbTipo.DisplayMember = "Nombre";
                cmbTipo.ValueMember = "Id";
                cmbTipo.SelectedIndex = -1;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al cargar el combo tipo de doc");
            }
        }

        private void CargarComboCarreras()
        {
            try
            {
                cmbCarreras.DataSource = AD_Varios.ObtenerCarreras();
                cmbCarreras.DisplayMember = "Nombre";
                cmbCarreras.ValueMember = "Id";
                cmbCarreras.SelectedIndex = -1;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al obtener carreras");
            }
        }

        private Persona ObtenerDatosPersona()
        {
            Persona p = new Persona();
            p.NombrePersona = txtNombre.Text.Trim();
            p.ApellidoPersona = txtApellido.Text.Trim();
            p.FechaNacimientoPersona = DateTime.Parse(txtFechaDeNacimiento.Text);

            if (rdMasculino.Checked)
            {
                p.SexoPersona = 1;
            }
            else if (rdFemenino.Checked)
            {
                p.SexoPersona = 2;
            }
            else
            {
                p.SexoPersona = 3;
            }

            p.TipoDocumentoPersona = (int)cmbTipo.SelectedValue;
            p.DocumentoPersona = txtNroDocumento.Text.Trim();
            p.CallePersona = txtCalle.Text.Trim();
            p.NroCasaPersona = txtNroCasa.Text.Trim();

            if (chkSoltero.Checked)
            {
                p.SolteroPersona = true;
            }
            else
            {
                p.CasadoPersona = true;
            }

            if (chkHijos.Checked)
            {
                p.HijosPersona = true;
            }
            else
            {
                p.HijosPersona = false;
            }

            if (txtCantidadHijos.Text.Equals(""))
            {
                p.CantidadHijosPersona = 0;
            }
            else
            {
                p.CantidadHijosPersona = int.Parse(txtCantidadHijos.Text);
            }

            p.CarreraPersona = (int)cmbCarreras.SelectedValue;

            return p;
        }

        private void btnGuardarPersona_Click(object sender, EventArgs e)
        {
            Persona p = ObtenerDatosPersona();
            bool resultado = AD_Personas.AgregarPersonaBaseDatos(p);
            if (resultado)
            {
                MessageBox.Show("Persona agrefgada con exito");
                LimpiarCampos();
                CargarComboCarreras();
                CargarComboTiposDocumentos();
                CargarGrilla();
            }
            else
            {
                MessageBox.Show("Error al agregar la persona");
            }
        }

        private void AgregarPersona(Persona per)
        {
            DataGridViewRow fila = new DataGridViewRow();
            
            DataGridViewTextBoxCell celdaDocumento = new DataGridViewTextBoxCell();
            celdaDocumento.Value = per.DocumentoPersona;
            fila.Cells.Add(celdaDocumento);

            DataGridViewTextBoxCell celdaNombre = new DataGridViewTextBoxCell();
            celdaNombre.Value = per.NombrePersona;
            fila.Cells.Add(celdaNombre);

            DataGridViewTextBoxCell celdaApellido = new DataGridViewTextBoxCell();
            celdaApellido.Value = per.ApellidoPersona;
            fila.Cells.Add(celdaApellido);

            grdPersonas.Rows.Add(fila);

            MessageBox.Show("Persona agregada con exito");
            LimpiarCampos();
            txtNombre.Focus();

        }

        private void chkHijos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHijos.Checked)
            {
                txtCantidadHijos.Enabled = true;
            }
            else
            {
                txtCantidadHijos.Enabled=false;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtFechaDeNacimiento.Text = "";
            txtNroDocumento.Text = "";
            txtNroCasa.Text = "";
            txtCalle.Text = "";
            rdFemenino.Checked = false;
            rdMasculino.Checked = false;
            rdOtro.Checked = false;
            chkCasado.Checked = false;
            chkSoltero.Checked = false;
            chkHijos.Checked = false;

        }

        private bool ExisteEnGrilla(string criterioABuscar)
        {
            bool resultado = false;
            for (int i = 0; i < grdPersonas.Rows.Count; i++)
            {
                if (grdPersonas.Rows[i].Cells["Documento"].Value.Equals(criterioABuscar))
                {
                    resultado = true;
                    break;
                }
            }

            return resultado;
        }

        private void grdPersonas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            btnActualizar.Enabled = true;
            DataGridViewRow filaSeleccionada = grdPersonas.Rows[indice];
            string documento = filaSeleccionada.Cells["Documento"].Value.ToString();
            Persona p = ObtenerPersona(documento);
            LimpiarCampos();
            CargarCampos(p);
        }

        private void CargarCampos(Persona p)
        {
            txtNombre.Text = p.NombrePersona;
            txtApellido.Text = p.ApellidoPersona;
            txtFechaDeNacimiento.Text= p.FechaNacimientoPersona.Date.ToShortDateString();
            if(p.SexoPersona ==1)
            {
                rdMasculino.Checked = true;
            }
            else if(p.SexoPersona==2)
            {
                rdFemenino.Checked = true;
            }
            else
            {
                rdOtro.Checked = true;
            }

            cmbTipo.SelectedValue = p.TipoDocumentoPersona;

            txtNroDocumento.Text = p.DocumentoPersona;
            txtCalle.Text = p.CallePersona;
            txtNroCasa.Text = p.NroCasaPersona.ToString();
            if(p.CasadoPersona)
            {
                chkCasado.Checked = true;
            }
            else 
            { 
                chkCasado.Checked = false;
            }
            if (p.SolteroPersona)
            {
                chkSoltero.Checked = true;
            }
            else 
            { 
                chkSoltero.Checked = false; 
            }
            if (p.HijosPersona)
            {
                chkHijos.Checked = true;
            }
            else 
            { 
                chkHijos.Checked = false; 
            }
            if(p.CantidadHijosPersona > 0)
            {
                txtCantidadHijos.Text = p.CantidadHijosPersona.ToString();
            }
            else
            {
                txtCantidadHijos.Text = "";
            }

            cmbCarreras.SelectedValue = p.CarreraPersona;


        }

        private Persona ObtenerPersona(string documento)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            Persona p = new Persona();
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM personas where NroDocumento LIKE @documento";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@documento", documento);

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null && dr.Read())
                {
                    p.NombrePersona = dr["Nombre"].ToString();
                    p.ApellidoPersona = dr["Apellido"].ToString();
                    p.FechaNacimientoPersona = DateTime.Parse(dr["FechaNacimiento"].ToString());
                    p.TipoDocumentoPersona = int.Parse(dr["IdTipoDocumento"].ToString());
                    p.DocumentoPersona = dr["NroDocumento"].ToString();
                    p.CallePersona = dr["Calle"].ToString();
                    p.NroCasaPersona = dr["NroCasa"].ToString();
                    p.SolteroPersona = bool.Parse(dr["Soltero"].ToString());
                    p.CasadoPersona = bool.Parse(dr["Casado"].ToString());
                    p.HijosPersona = bool.Parse(dr["Hijos"].ToString());
                    p.CantidadHijosPersona = int.Parse(dr["CantidadHijos"].ToString());
                    p.CarreraPersona = int.Parse(dr["IdCarrera"].ToString());

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }


            return p;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Persona p = ObtenerDatosPersona();
            bool resultado = ActualizarPerona(p);
            if(resultado)
            {
                MessageBox.Show("Persona actualizada con exito");
                LimpiarCampos();
                CargarGrilla();
                CargarComboCarreras();
                CargarComboTiposDocumentos();
            }
            else
            {
                MessageBox.Show("Error al actualizar persona");

            }
        }

        private bool ActualizarPerona(Persona per)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            bool resultado = false;
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "UPDATE personas SET Nombre = @nombre, Apellido = @Apellido, FechaNAcimiento = @fechaNacimiento, IdSexo = @idSexo, IdTipoDocumento = @idTipoDocumento, NroDocumento = @nroDocumento, Calle = @calle, NroCasa = @nroCasa, Soltero = @soltero, Casado = @casado, Hijos = @hijos, CantidadHijos = @cantidadHijos, IdCarrera = @idCarrera WHERE NroDocumento LIKE @nroDocumento";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", per.NombrePersona);
                cmd.Parameters.AddWithValue("@apellido", per.ApellidoPersona);
                cmd.Parameters.AddWithValue("@fechaNacimiento", per.FechaNacimientoPersona);
                cmd.Parameters.AddWithValue("@idSexo", per.SexoPersona);
                cmd.Parameters.AddWithValue("@idTipoDocumento", per.TipoDocumentoPersona);
                cmd.Parameters.AddWithValue("@nroDocumento", per.DocumentoPersona);
                cmd.Parameters.AddWithValue("@calle", per.CallePersona);
                cmd.Parameters.AddWithValue("@nroCasa", per.NroCasaPersona);
                cmd.Parameters.AddWithValue("@soltero", per.SolteroPersona);
                cmd.Parameters.AddWithValue("@casado", per.CasadoPersona);
                cmd.Parameters.AddWithValue("@hijos", per.HijosPersona);
                cmd.Parameters.AddWithValue("@cantidadHijos", per.CantidadHijosPersona);
                cmd.Parameters.AddWithValue("@idCarrera", per.CarreraPersona);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }


            return resultado;
        }
    }
}
