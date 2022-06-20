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
    public partial class ReporteListadoCursos : Form
    {
        public ReporteListadoCursos()
        {
            InitializeComponent();
        }

        private void ReporteListadoCursos_Load(object sender, EventArgs e)
        {

            this.rptViewer.RefreshReport();
        }

        private void rptViewer_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = AD_Cursos.ObtenerListadoDeCursos();

            ReportDataSource ds = new ReportDataSource("DatosCursos", dt);

            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.DataSources.Add(ds);
            rptViewer.LocalReport.Refresh();
        }
    }
}
