using ProyectoBase1.AccesoADatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBase1
{
    public partial class AltaCurso : Form
    {
        public AltaCurso()
        {
            InitializeComponent();
        }

        private void AltaCurso_Load(object sender, EventArgs e)
        {
            CargarFecha();
            ObtenerUltimoIdCurso();
        }

        private void CargarFecha()
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
        }

        private void ObtenerUltimoIdCurso()
        {
            int id = AD_Cursos.ObtenerUltimoIdCurso();
            txtNroCurso.Text = (id+1).ToString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tablaResultado = AD_Carreras.ObtenerCarrerasXId(int.Parse(txtNroCarrera.Text));
                if (tablaResultado.Rows.Count > 0)
                {
                    txtCarrera.Text = tablaResultado.Rows[0][1].ToString();
                }
                else
                {
                    MessageBox.Show("Carrear inexistente");
                    txtNroCarrera.Focus();
                    txtCarrera.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener Carrera");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tablaResultado = AD_Personas.ObtenerPersonaPorDocumento(txtDocumento.Text.Trim());
                if (tablaResultado.Rows.Count > 0)
                {
                    txtIdPersona.Text = tablaResultado.Rows[0][0].ToString();
                    txtNombreAlumno.Text = tablaResultado.Rows[0][1].ToString();
                    txtApellidoAlumno.Text = tablaResultado.Rows[0][2].ToString();
                    txtIdPersona.Text = tablaResultado.Rows[0][0].ToString();
                }
                else
                {
                    MessageBox.Show("Alumno no encontrado");
                    txtDocumento.Focus();
                    txtNombreAlumno.Text = "";
                    txtApellidoAlumno.Text = "";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al obtener persona");
            }
        }

        private void btnAgregarAlumno_Click(object sender, EventArgs e)
        {
            grillaAlumnos.Rows.Add(txtIdPersona.Text, txtDocumento.Text, txtNombreAlumno.Text, txtApellidoAlumno.Text);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            List<int> listaAlumnos = new List<int>();
            for (int i = 0; i < grillaAlumnos.Rows.Count; i++)
            {
                listaAlumnos.Add(int.Parse(grillaAlumnos.Rows[i].Cells[0].Value.ToString()));
            }

            bool resultado = AD_Cursos.AltaNuevoCurso(int.Parse(txtNroCurso.Text), txtNombreCurso.Text.Trim(), txtDescripcion.Text.Trim(), int.Parse(txtNroCarrera.Text), listaAlumnos);
            if (resultado)
            {
                MessageBox.Show("Curso dado de alta con exito");

            }
            else
            {
                MessageBox.Show("Erro al dar de alta curso");
            }
        }
    }
}
