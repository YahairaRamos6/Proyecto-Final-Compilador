using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ProyectoFinal
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }


        private void btn_signin_Click(object sender, EventArgs e)
        {
            signin registrar = new signin();
            registrar.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable consulta = Conexion.Query("select Id_usuario from usuarios where nombre='" + txt_usr.Text + "' and contrasena='" + Conexion.Encriptar(txt_pswd.Text) + "'");

                if (consulta.Rows.Count != 0)
                {
                    frmCompilador principal = new frmCompilador(consulta.Rows[0]["Id_usuario"].ToString(), txt_usr.Text);
                    principal.Show();
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show("Nombre de usuario o contraseña incorrectos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
