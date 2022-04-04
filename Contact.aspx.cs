using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace controlJugadoresWeb
{
    public partial class Contact : Page
    {
        static List<Jugador> jugadores = new List<Jugador>();
        static List<Anotacion> anotaciones = new List<Anotacion>();
        static List<Mostrar> mostrar = new List<Mostrar>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void leerJugador()
        {
            string fileName = Server.MapPath("~/jugador.txt");
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Jugador jugadorTemp = new Jugador();
                jugadorTemp.noIdentificacion = reader.ReadLine();
                jugadorTemp.nombre = reader.ReadLine();
                jugadorTemp.equipo = reader.ReadLine();

                jugadores.Add(jugadorTemp);
            }
            reader.Close();
        }

        private void leerAnotacion()
        {
            string fileName = Server.MapPath("~/anotacion.txt");
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Anotacion anotacionTemp = new Anotacion();
                anotacionTemp.noIdentificacion = reader.ReadLine();
                anotacionTemp.fecha = Convert.ToDateTime(reader.ReadLine());
                anotacionTemp.golEquipo = reader.ReadLine();
                anotacionTemp.golAnotados = Convert.ToInt16(reader.ReadLine());
                
                anotaciones.Add(anotacionTemp);                               
            }
            reader.Close();
        }

        private void cargarListMostrar()
        {
            foreach (var i in jugadores)
            {
                foreach (var j in anotaciones)
                {
                    if (i.noIdentificacion == j.noIdentificacion)
                    {
                        Mostrar mostrarTemp = new Mostrar();
                        mostrarTemp.noIdentificacion = j.noIdentificacion;
                        mostrarTemp.nombre = i.nombre;
                        mostrarTemp.golAnotados = j.golAnotados;
                        mostrar.Add(mostrarTemp);
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            leerJugador();
            leerAnotacion();
            cargarListMostrar();
            cargarVista();

            double promedio = mostrar.Average(p => p.golAnotados);
            Label1.Text = promedio.ToString();
        }

        private void cargarVista()
        {
            GridView1.DataSource = mostrar;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            mostrar = mostrar.OrderBy(g => g.golAnotados).ToList();
            cargarVista();
        }
    }
}