using Microsoft.Reporting.WinForms;
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
    public partial class ReporteEstadistica : Form
    {
        public ReporteEstadistica()
        {
            InitializeComponent();
        }

        private void ReporteEstadistica_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = AD_Cursos.ObtenerEstadisticaCursos();

            ReportDataSource ds = new ReportDataSource("DatosEstadisticasCurso", dt);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(ds);
            reportViewer1.LocalReport.Refresh();
        }
    }
}
