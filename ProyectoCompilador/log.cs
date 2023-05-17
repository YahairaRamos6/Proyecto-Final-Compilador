using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProyectoFinal
{
    public partial class log : Form
    {
        public log()
        {
            InitializeComponent();
            dataGridView1.DataSource = Conexion.Query("select U.nombre as Usuario,L.nombre as Lenguaje,log.fecha as Fecha,log.archivo as Archivo_de_salida from log,usuarios as U,lenguajes as L where log.Id_usuario = U.Id_usuario and L.Id_Lenguaje = log.Id_Lenguaje");
        }

        private void exportar(string extencion,string separa)
        {
            SaveFileDialog exportar = new SaveFileDialog();
            exportar.DefaultExt = extencion;
            exportar.ShowDialog();

            try
            {
                StreamWriter sw = new StreamWriter(exportar.FileName);
                foreach (DataGridViewRow linea in dataGridView1.Rows)
                {
                    sw.WriteLine(linea.Cells["Usuario"].Value + separa + linea.Cells["Lenguaje"].Value + separa+ linea.Cells["Fecha"].Value + separa + linea.Cells["Archivo_de_salida"].Value);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void ExportarDataGridViewExcel(DataGridView grd)
        {
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xls)|*.xls";
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                Microsoft.Office.Interop.Excel.Application aplicacion;
                Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();
                hoja_trabajo =
                    (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);
                
                for (int i = 0; i < grd.Rows.Count; i++)
                {
                    for (int j = 0; j < grd.Columns.Count; j++)
                    {
                        hoja_trabajo.Cells[i + 1, j + 1] = grd.Rows[i].Cells[j].Value.ToString();
                    }
                }
                libros_trabajo.SaveAs(fichero.FileName,
                    Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                libros_trabajo.Close(true);
                aplicacion.Quit();
            }
        }

        private void btnXlsx_Click(object sender, EventArgs e)
        {
            ExportarDataGridViewExcel(dataGridView1);
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string query = "select U.nombre as Usuario,L.nombre as Lenguaje,log.fecha as Fecha,log.archivo as Archivo_de_salida from log,usuarios as U,lenguajes as L where log.Id_usuario = U.Id_usuario and L.Id_Lenguaje = log.Id_Lenguaje";

            if (cbUsuario.Checked) query += " and U.Nombre='" + tbUsuario.Text + "'";
            if (cbLenguaje.Checked) query += " and L.Nombre='" + tbLenguaje.Text + "'";
            if (cbFecha.Checked) query += " and fecha between '" + dtpInicio.Value.Date.ToString("yyyy/MM/dd") + "' and '" + dtpFinal.Value.Date.ToString("yyyy/MM/dd") + "'";
            dataGridView1.DataSource = Conexion.Query(query);
            
        }

        private void btnTxt_Click(object sender, EventArgs e)
        {
            exportar("txt", " ");
        }

        private void btnCsv_Click(object sender, EventArgs e)
        {
            exportar("csv", ",");
        }

        private void log_Load(object sender, EventArgs e)
        {

        }
    }
}
