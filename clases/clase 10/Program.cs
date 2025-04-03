using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Gtk;
using System.Text;

class SubNodo
{
    public int Valor { get; set; }
    public SubNodo Siguiente { get; set; } = null;

    public SubNodo(int val)
    {
        Valor = val;
    }
}

class Nodo
{
    public int Indice { get; set; }
    public Nodo Siguiente { get; set; } = null;
    public Nodo Anterior { get; set; } = null;
    public SubNodo Lista { get; set; } = null;

    public void Agregar(int val)
    {
        SubNodo nuevoNodo = new SubNodo(val);
        if (Lista == null)
        {
            Lista = nuevoNodo;
        }
        else 
        {
            SubNodo aux = Lista;
            while (aux.Siguiente != null)
            {
                aux = aux.Siguiente;
            }
            aux.Siguiente = nuevoNodo;
        }
    }

    public void Imprimir()
    {
        SubNodo aux = Lista;
        while (aux != null)
        {
            Console.Write($"{aux.Valor} ");
            aux = aux.Siguiente;
        }
        Console.WriteLine();
    }

    public string ObtenerCadena()
    {
        StringBuilder sb = new StringBuilder();
        SubNodo aux = Lista;
        while (aux != null)
        {
            sb.Append($"{aux.Valor} ");
            aux = aux.Siguiente;
        }
        return sb.ToString();
    }
}

class ListaDeListas
{
    public Nodo Cabecera { get; set; } = null;
    public Nodo Cola { get; set; } = null;

    public void Insertar(int indice, int valor)
    {
        Nodo nuevoNodo = new Nodo();
        nuevoNodo.Indice = indice;

        if (Cabecera == null)
        {
            Cabecera = nuevoNodo;
            Cola = nuevoNodo;
            nuevoNodo.Agregar(valor);
        }
        else
        {
            if (indice < Cabecera.Indice)
            {
                Cabecera.Anterior = nuevoNodo;
                nuevoNodo.Siguiente = Cabecera;
                Cabecera = nuevoNodo;
                nuevoNodo.Agregar(valor);
            }
            else
            {
                Nodo aux = Cabecera;
                while (aux.Siguiente != null && indice > aux.Siguiente.Indice)
                {
                    aux = aux.Siguiente;
                }
                
                if (indice == aux.Indice)
                {
                    aux.Agregar(valor);
                }
                else 
                {
                    nuevoNodo.Siguiente = aux.Siguiente;
                    nuevoNodo.Anterior = aux;

                    if (aux.Siguiente != null)
                    {
                        aux.Siguiente.Anterior = nuevoNodo;
                    }
                    else
                    {
                        Cola = nuevoNodo;
                    }
                    
                    aux.Siguiente = nuevoNodo;
                    nuevoNodo.Agregar(valor);
                }
            }
        }
    }

    public void ImprimirLista()
    {
        Nodo aux = Cabecera;
        while (aux != null)
        {
            Console.WriteLine($"Indice: {aux.Indice}");
            aux.Imprimir();
            aux = aux.Siguiente;
        }
    }
 
    public string ObtenerListaCadena()
    {
        StringBuilder sb = new StringBuilder();
        Nodo aux = Cabecera;
        while (aux != null)
        {
            sb.AppendLine($"Indice: {aux.Indice}");
            sb.AppendLine(aux.ObtenerCadena());
            aux = aux.Siguiente;
        }
        return sb.ToString();
    }

    public void GenerarGrafo()
    {
        using (StreamWriter file = new StreamWriter("graph.dot"))
        {
            file.WriteLine("digraph G {");
            
            Nodo nodoActual = Cabecera;
            while (nodoActual != null)
            {
                file.WriteLine($"{nodoActual.Indice}[label=\"{nodoActual.Indice}\"];");
                
                SubNodo subNodoActual = nodoActual.Lista;
                while (subNodoActual != null)
                {
                    file.WriteLine($"{nodoActual.Indice} -> {subNodoActual.Valor} [dir=normal];");
                    subNodoActual = subNodoActual.Siguiente;
                }
                
                nodoActual = nodoActual.Siguiente;
            }
            
            file.WriteLine("}");
        }
        
        try
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dot",
                Arguments = "-Tpng ./graph.dot -o ./graph.png",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            
            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    Console.WriteLine("Error al crear la imagen.");
                }
                else
                {
                    Console.WriteLine("Imagen creada con éxito.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al generar la imagen: {ex.Message}");
        }
    }
}

class AplicacionGrafo : Window
{
    private ListaDeListas listaAdyacencia;
    private Entry entradaNodoOrigen;
    private Entry entradaNodoDestino;
    private TextView vistaTextoLista;
    private Image imagenGrafo;
    
    public AplicacionGrafo() : base("Aplicación de Grafos")
    {
        // Inicializar la lista de adyacencia
        listaAdyacencia = new ListaDeListas();
        
        // Configurar la ventana principal
        SetDefaultSize(800, 600);
        SetPosition(WindowPosition.Center);
        DeleteEvent += (o, args) => Application.Quit();
        
        // Crear el diseño principal
        Box cajaMain = new Box(Orientation.Vertical, 10);
        cajaMain.BorderWidth = 10;
        
        // Panel de entrada de datos
        Frame marcoEntrada = new Frame("Insertar Nodos");
        Box cajaEntrada = new Box(Orientation.Horizontal, 5);
        cajaEntrada.BorderWidth = 10;
        
        cajaEntrada.PackStart(new Label("Nodo Origen:"), false, false, 0);
        entradaNodoOrigen = new Entry();
        cajaEntrada.PackStart(entradaNodoOrigen, false, false, 5);
        
        cajaEntrada.PackStart(new Label("Nodo Destino:"), false, false, 5);
        entradaNodoDestino = new Entry();
        cajaEntrada.PackStart(entradaNodoDestino, false, false, 5);
        
        Button botonInsertar = new Button("Insertar");
        botonInsertar.Clicked += OnBotonInsertarClick;
        cajaEntrada.PackStart(botonInsertar, false, false, 5);
        
        Button botonGrafo = new Button("Generar Grafo");
        botonGrafo.Clicked += OnBotonGrafoClick;
        cajaEntrada.PackStart(botonGrafo, false, false, 5);
        
        marcoEntrada.Add(cajaEntrada);
        cajaMain.PackStart(marcoEntrada, false, false, 0);
        
        // Panel para mostrar la lista de adyacencia
        Frame marcoLista = new Frame("Lista de Adyacencia");
        ScrolledWindow scrollLista = new ScrolledWindow();
        scrollLista.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
        vistaTextoLista = new TextView();
        vistaTextoLista.Editable = false;
        scrollLista.Add(vistaTextoLista);
        marcoLista.Add(scrollLista);
        cajaMain.PackStart(marcoLista, true, true, 0);
        
        // Panel para mostrar el grafo
        Frame marcoGrafo = new Frame("Visualización del Grafo");
        ScrolledWindow scrollGrafo = new ScrolledWindow();
        scrollGrafo.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
        imagenGrafo = new Image();
        scrollGrafo.Add(imagenGrafo);
        marcoGrafo.Add(scrollGrafo);
        cajaMain.PackStart(marcoGrafo, true, true, 0);
        
        Add(cajaMain);
        ShowAll();
    }
    
    private void OnBotonInsertarClick(object sender, EventArgs args)
    {
        try
        {
            int nodoOrigen = int.Parse(entradaNodoOrigen.Text);
            int nodoDestino = int.Parse(entradaNodoDestino.Text);
            
            listaAdyacencia.Insertar(nodoOrigen, nodoDestino);
            ActualizarVistaLista();
            
            entradaNodoOrigen.Text = "";
            entradaNodoDestino.Text = "";
        }
        catch (Exception ex)
        {
            MostrarError("Error al insertar nodo", ex.Message);
        }
    }
    
    private void OnBotonGrafoClick(object sender, EventArgs args)
    {
        try
        {
            listaAdyacencia.GenerarGrafo();
            CargarImagenGrafo();
        }
        catch (Exception ex)
        {
            MostrarError("Error al generar el grafo", ex.Message);
        }
    }
    
    private void ActualizarVistaLista()
    {
        string textoLista = listaAdyacencia.ObtenerListaCadena();
        vistaTextoLista.Buffer.Text = textoLista;
    }
    
    private void CargarImagenGrafo()
    {
        try
        {
            if (File.Exists("./graph.png"))
            {
                imagenGrafo.Pixbuf = new Gdk.Pixbuf("./graph.png");
                imagenGrafo.Show();
            }
        }
        catch (Exception ex)
        {
            MostrarError("Error al cargar la imagen", ex.Message);
        }
    }
    
    private void MostrarError(string titulo, string mensaje)
    {
        MessageDialog dialogo = new MessageDialog(
            this, 
            DialogFlags.Modal, 
            MessageType.Error, 
            ButtonsType.Ok, 
            $"{mensaje}");
        
        dialogo.Title = titulo;
        dialogo.Run();
        dialogo.Destroy();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Application.Init();
        
        new AplicacionGrafo();
        
        Application.Run();
    }
}