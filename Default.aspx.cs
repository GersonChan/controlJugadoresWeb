using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace controlJugadoresWeb
{
    public partial class _Default : Page
    {
        List<Jugador> jugadores = new List<Jugador>();
        static List<Anotacion> anotaciones = new List<Anotacion>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Leer();
                DropDownList1.DataValueField = "noIdentificacion";
                DropDownList1.DataTextField = "nombre";
                DropDownList1.DataSource = jugadores;
                DropDownList1.DataBind();
            }            
        }

        public void Leer()
        {
            string fileName = Server.MapPath("~/jugador.txt");
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Jugador jugadroTemp = new Jugador();
                jugadroTemp.noIdentificacion = reader.ReadLine();
                jugadroTemp.nombre = reader.ReadLine();
                jugadroTemp.equipo = reader.ReadLine();

                jugadores.Add(jugadroTemp);
            }

            reader.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Anotacion anotacionTemp = new Anotacion();
            anotacionTemp.noIdentificacion = DropDownList1.SelectedValue;
            anotacionTemp.fecha = Calendar1.SelectedDate;
            anotacionTemp.golEquipo = TextBox1.Text;
            anotacionTemp.golAnotados = Convert.ToInt16(TextBox2.Text);

            anotaciones.Add(anotacionTemp);
            guardar();

            TextBox1.Text = "";
            TextBox2.Text = "";
        }

        private void guardar()
        {
            string fileNombre = Server.MapPath("~/anotacion.txt");
            FileStream stream = new FileStream(fileNombre, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            foreach (var anotacionTemp in anotaciones)
            {
                writer.WriteLine(anotacionTemp.noIdentificacion);
                writer.WriteLine(anotacionTemp.fecha);
                writer.WriteLine(anotacionTemp.golEquipo);
                writer.WriteLine(anotacionTemp.golAnotados);
            }
            writer.Close();
        }
    }
}