

El error 


```sh
GLib-GObject-CRITICAL **: g_object_remove_toggle_ref: assertion 'G_IS_OBJECT (object)' failed indica que hay 
un problema con la gestión de referencias de 
objetos en GTK

```

## POsibles causas 
- acceder a objetos GTK despues de su destrucciòn

```sh
if (myWidget != null && myWidget.IsRealized)
{
    myWidget.Hide();
}

```
- destruir manualmente con un 


destroy()
Hide()

- problemas con application.quit
    Cierr de la aplicacion incorrectamente.
Application.Quit()


### Comentarios

1. No  destruir los widgets de forma manual esto si GTK Lo  manipula
2. Verificar que la aplicacion se cierre de forma correcta
3. Verificar los accesos despues de la destrucion


