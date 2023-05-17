using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class signin : Form
    {
        public signin()
        {
            InitializeComponent();
        }

        private void btn_signin_Click(object sender, EventArgs e)
        {
            try
            { 
                string query = "insert into usuarios(Nombre,Contrasena,correo,Telefono) values ('" + txt_nombre.Text + "','" + Conexion.Encriptar(txt_pswd.Text) + "','" + txt_correo.Text + "','" + txt_num.Text + "')";
                DataTable usuario = Conexion.Query("select * from usuarios where nombre='" +txt_nombre.Text + "'");

                if (usuario.Rows.Count != 0)
                    MessageBox.Show("Ya existe ese usuario");
                else
                {
                    Conexion.Query(query);
                    MessageBox.Show("Usuario registrado");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

      
    }
}
