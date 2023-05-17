using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class frmCompilador : Form
    {
        int[,] Matriz;
        string id, usr;
        string lenguaje, idLenguaje;
        string[] lst_reservadas;

        public frmCompilador(string id,string usr)
        {
            this.id = id;
            this.usr = usr;

            InitializeComponent();
            
        }

        private int Existe(string cadena, ListBox lista)
        {

            for (int i = 0; i <= lista.Items.Count - 1; i++)
            {
                lista.SelectedIndex = i;
                if (cadena == lista.Text)
                    return i;
            }

            return -1;
        }

        private int Existe(string cadena, string[] reservadas)
        {

            for(int i = 0; i < reservadas.Length; i++)
            {
                if (reservadas[i] == cadena)
                    return i;
            }

            return -1;
        }

        private void ReconoceIdentifcador(string cadena)
        {
            int exIden = Existe(cadena, lbIdentificadores);
            int exRe = Existe(cadena, lst_reservadas);

            if (exRe == -1 && exIden == -1) {
                lbIdentificadores.Items.Add(cadena);
                lbSalida.Items.Add(cadena + " Iden " + " " + (lbIdentificadores.Items.Count - 1).ToString());
            } else if (exIden != -1 && exRe == -1) {
                lbSalida.Items.Add(cadena + " Iden " + " " + exIden.ToString());
            } else if (exRe != -1) {
                lbSalida.Items.Add(cadena + " Reser " + " " + exRe.ToString());
            }
        }

        private void ReconoceEntera(string cadena) {
            int exEnt = Existe(cadena, lbEnteras);

            if (exEnt == -1) {
                lbEnteras.Items.Add(cadena);
                lbSalida.Items.Add(cadena + " CteEnt " + " " + (lbEnteras.Items.Count - 1).ToString());
            }
            else {
                lbSalida.Items.Add(cadena + " CteEnt " + " " + exEnt.ToString());
            }
        }

        private void ReconoceReal(string cadena) {
            int exReal = Existe(cadena, lbReales);

            if (exReal == -1) {
                lbReales.Items.Add(cadena);
                lbSalida.Items.Add(cadena + " CteReal " + " " + (lbReales.Items.Count - 1).ToString());
            }
            else {
                lbSalida.Items.Add(cadena + " CteReal " + " " + exReal.ToString());
            }
        }

        private void ReconoceChar(string cadena) {
            cadena = cadena.Substring(1, 1);
            int exChar = Existe(cadena, lbChar);

            if (exChar == -1) {
                lbChar.Items.Add(cadena);
                lbSalida.Items.Add(cadena + " CteChar " + " " + (lbChar.Items.Count - 1).ToString());
            }
            else {
                lbSalida.Items.Add(cadena + " CteChar " + " " + exChar.ToString());
            }
        }

        private void ReconoceString(string cadena) {
            cadena = cadena.Substring(1);
            int exStr = Existe(cadena, lbString);

            if (exStr == -1) {
                lbString.Items.Add(cadena);
                lbSalida.Items.Add(cadena + " CteStr " + " " + (lbString.Items.Count - 1).ToString());
            }
            else {
                lbSalida.Items.Add(cadena + " CteStr " + " " + exStr.ToString());
            }
        }

        private int ObtenerIndice(char car) {
            if ((Char.GetNumericValue(car) >= 65 && Char.GetNumericValue(car) <= 90) || (Char.GetNumericValue(car) >= 97 && Char.GetNumericValue(car) <= 122)) {
                return 0;
            } else {
                switch (car) {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        return 1;
                    case '_':
                        return 2;
                    case '.':
                        return 3;
                    case (char)39:
                        return 4;
                    case '"':
                        return 5;
                    case '/':
                        return 6;
                    case '*':
                        return 7;
                    case '+':
                        return 8;
                    case '-':
                        return 9;
                    case '%':
                        return 10;
                    case '!':
                        return 11;
                    case '=':
                        return 12;
                    case '<':
                        return 13;
                    case '>':
                        return 14;
                    case '&':
                        return 15;
                    case '(':
                        return 16;
                    case ')':
                        return 17;
                    case '{':
                        return 18;
                    case '}':
                        return 19;
                    case '[':
                        return 20;
                    case ']':
                        return 21;
                    case ';':
                        return 22;
                    case ' ':
                        return 23;
                    default: return 0; 
                }
            }
        }


        private int ReconoceToken(int est, string cad) {
            string error;
            switch (est) {
                case 100:
                    ReconoceIdentifcador(cad);
                    return -1;
                case 101:
                    ReconoceEntera(cad);
                    return -1;
                case 102:
                    ReconoceReal(cad);
                    return -1;
                case 103:
                    ReconoceChar(cad);
                    return 0;
                case 104:
                    ReconoceString(cad);
                    return 0;
                case 105:
                    lbSalida.Items.Add(cad.Substring( 2, cad.Length - 3) + " Coment");
                    return 0;
                case 106:
                    lbSalida.Items.Add("/ CarEsp");
                    return 0;
                case 107:
                    lbSalida.Items.Add("+ CarEsp");
                    return 0;
                case 108:
                    lbSalida.Items.Add("- CarEsp");
                    return 0;
                case 109:
                    lbSalida.Items.Add("* CarEsp");
                    return 0;
                case 110:
                    lbSalida.Items.Add("% CarEsp");
                    return 0;
                case 111:
                    lbSalida.Items.Add("! CarEsp");
                    return 0;
                case 112:
                    lbSalida.Items.Add("= CarEsp");
                    return 0;
                case 113:
                    lbSalida.Items.Add("< CarEsp");
                    return 0;
                case 114:
                    lbSalida.Items.Add("> CarEsp");
                    return 0;
                case 115:
                    lbSalida.Items.Add("& CarEsp");
                    return 0;
                case 116:
                    lbSalida.Items.Add("( CarEsp");
                    return 0;
                case 117:
                    lbSalida.Items.Add(") CarEsp");
                    return 0;
                case 118:
                    lbSalida.Items.Add("{ CarEsp");
                    return 0;
                case 119:
                    lbSalida.Items.Add("} CarEsp");
                    return 0;
                case 120:
                    lbSalida.Items.Add("[ CarEsp");
                    return 0;
                case 121:
                    lbSalida.Items.Add("] CarEsp");
                    return 0;
                case 122:
                    lbSalida.Items.Add("; CarEsp");
                    return 0;
                case 302:
                    error = "Error de lexico 302 al tratar de construir una Cte. Real";
                    lbSalida.Items.Add(error);
                    MessageBox.Show(error);
                    return 0;
                case 303:
                    error = "Error de lexico 303 al tratar de construir una Cte. Char";
                    lbSalida.Items.Add(error);
                    MessageBox.Show(error);
                    return 0;
                case 304:
                    error = "Error de lexico 304 al tratar de construir una Cte. String";
                    lbSalida.Items.Add(error);
                    MessageBox.Show(error);
                    return 0;
                default:
                    return 0;
            }
        }

        private void Compilar() {
            string cadena = tbEntrada.Text.ToString().Replace("\n", " ");
            cadena = cadena.Replace("\r", "");
            cadena = cadena.Replace("\t","");
            int posicion = 0, estado = 0;
            char caracter;
            string token = "";
            lbIdentificadores.Items.Clear();
            lbSalida.Items.Clear();
            lbEnteras.Items.Clear();
            lbReales.Items.Clear();
            lbChar.Items.Clear();
            lbString.Items.Clear();

            do {
                caracter = Convert.ToChar(cadena.Substring(posicion, 1));
                estado = Matriz[estado, ObtenerIndice(caracter)];

                if (estado >= 300) {
                    ReconoceToken(estado, token);
                    break;
                } else if (estado >= 100) {
                    posicion += ReconoceToken(estado, token);
                    estado = 0;
                    token = "";
                } else if (estado != 0) {
                    token += caracter;
                }

                posicion += 1;
            } while (posicion < cadena.Length);

            DateTime fecha = DateTime.Now;
            string f = fecha.ToString("yyyy/MM/dd HH:mm:ss");
            string salida = f.Replace("/", "_");
            salida = salida.Replace(" ", "_");
            salida = salida.Replace(":", "_");
            salida = "salida/Output" + lenguaje + usr + salida + ".txt";

            StreamWriter sw = new StreamWriter(salida);
            foreach (string linea in lbSalida.Items)
            {
                sw.WriteLine(linea);
            }
            sw.Close();
            Conexion.Query("insert into log(Id_usuario,Id_Lenguaje,fecha,archivo) values (" + id + "," + idLenguaje + ",'" + f + "','" + salida + "')");
        }


        private void frmCompilador_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ptbCargar_Click(object sender, EventArgs e)
        {
            try
            {
                ofdEntrada.ShowDialog();
                string archivo = ofdEntrada.FileName;
                tbEntrada.Text = "";

                StreamReader sr = new StreamReader(archivo);


                tbEntrada.Text = sr.ReadToEnd() + " ";

                sr.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void ptbCompilar_Click(object sender, EventArgs e)
        {
            Compilar();
        }

        private void pbRegistros_Click(object sender, EventArgs e)
        {
            log reg = new log();
            reg.Visible = true;
        }

        private void frmCompilador_Load(object sender, EventArgs e)
        {
            cmb_len.Items.Add("Java");
            
            cmb_len.Items.Add("Basic");
            
            cmb_len.Items.Add("Clipper");
          
            cmb_len.Items.Add("Cobol");
       
            cmb_len.Items.Add("DBase");
           
            cmb_len.Items.Add("Fortran");
         
            cmb_len.Items.Add("Pascal");
            
            cmb_len.Items.Add("FoxPro");
         
            cmb_len.Items.Add("Python");
           
            cmb_len.Items.Add("VB");
     
        }

        private void btn_cargar_Click(object sender, EventArgs e)
        {
            lbReservadas.Items.Clear();

            lenguaje = cmb_len.Text;
            idLenguaje = (cmb_len.SelectedIndex + 1).ToString();
            StreamReader sr = new StreamReader("matriz"+lenguaje+".csv");
            string[] arreglo = sr.ReadLine().Split(',');
            int a = Convert.ToInt32(arreglo[0]), b = Convert.ToInt32(arreglo[1]);
            Matriz = new int[a, b];
            int i = 0;

            while (!sr.EndOfStream)
            {
                arreglo = sr.ReadLine().Split(',');
                for (int j = 0; j < b; j++)
                {
                    Matriz[i, j] = Convert.ToInt32(arreglo[j]);
                }
                i++;
            }
            sr.Close();

            sr = new StreamReader("pr" + lenguaje + ".csv");
            lst_reservadas = sr.ReadLine().Split(',');

            for(int j = 0; j< lst_reservadas.Length; j++)
            {
                lbReservadas.Items.Add(lst_reservadas[j]);
            }
            sr.Close();
            panel1.Enabled = true;
        }
    }
}
