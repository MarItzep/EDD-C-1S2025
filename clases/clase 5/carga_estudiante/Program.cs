using Gtk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Estructuras;

namespace carga
{   class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            ListaSimple ListaSimple = new ListaSimple();

            // Crear una nueva ventana
            Window window = new Window("Carga de archivos");
            window.SetDefaultSize(800, 600);
            window.DeleteEvent += delegate { Application.Quit(); };

            // Crear un nuevo contenedor
            VBox vbox = new VBox(false, 8);
            window.Add(vbox);

            // Crear un nuevo botón
            Button button = new Button("Cargar archivo");
            button.Clicked += delegate {
                // Crear un nuevo diálogo de archivo
                FileChooserDialog filechooser = new FileChooserDialog("Seleccione un archivo",
                    window,
                    FileChooserAction.Open,
                    "Cancelar", ResponseType.Cancel,
                    "Abrir", ResponseType.Accept);

                // Mostrar el diálogo
                if (filechooser.Run() == (int)ResponseType.Accept)
                {
                    try
                    {
                        // Mostrar la ruta del archivo seleccionado
                        Console.WriteLine($"Archivo seleccionado: {filechooser.Filename}");

                        // Leer el contenido del archivo
                        string contenido = File.ReadAllText(filechooser.Filename);

                        // Intentar deserializar el JSON
                        // en una lista de objetos de tipo Persona
                        List<Persona> personas = JsonSerializer.Deserialize<List<Persona>>(contenido);
                        

                        // Verificar si la lista no está vacía
                        if (personas != null)
                        {
                            Console.WriteLine($"Total de personas cargadas: {personas.Count}");

                            // Imprimir los datos
                            foreach (var persona in personas)
                            {
                                Console.WriteLine($"ID: {persona.ID}, Nombres: {persona.Nombres}, Apellidos: {persona.Apellidos}, Correo: {persona.Correo}");
                                ListaSimple.Insertar(persona.ID, persona.Nombres, persona.Apellidos, persona.Correo);
                            }
                        }
                        else
                        {
                            Console.WriteLine("El archivo no contiene datos válidos.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al procesar el archivo: {ex.Message}");
                    }
                }

                // Cerrar el diálogo

                Console.WriteLine("Lista de personas cargadas");
                ListaSimple.Mostrar();
                ListaSimple.GenerarGraphviz();

                filechooser.Destroy();
            };

            vbox.PackStart(button, false, false, 0);
        // Crear un nuevo botón 
        // //agregar la imagen
            Image image = new Image("lista.png");
            vbox.PackStart(image, false, false, 0);

            // Mostrar la ventana y todos sus elementos
            window.ShowAll();
            Application.Run();
        }

        // Clase Persona con atributos ID, Nombres, Apellidos y Correo
        public class Persona
        {
            public int ID { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Correo { get; set; }
        }
    }
}
