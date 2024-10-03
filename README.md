# WebApiBE2022
Esta API RESTful permite la gestión de perros, perreras y administradores del sistema. Proporciona un conjunto completo de operaciones CRUD (Crear, Leer, Actualizar, Borrar) para manejar los registros relacionados con perros, perreras y administradores. La arquitectura está basada en controladores, utilizando entidades, DTOs (Data Transfer Objects) y mappers para gestionar y transformar los datos. Además, se integra con una base de datos para garantizar la persistencia de la información.

## Funcionalidades principales
* Operaciones CRUD: Se soportan las operaciones de creación, lectura, actualización y eliminación de registros para perros, perreras y administradores.
* Gestión de administradores del sistema: Administra usuarios encargados del sistema, con soporte completo para operaciones CRUD.
* Conexión a base de datos: La API se conecta a una base de datos para almacenar y gestionar los registros de rifas y participantes.
* Filtros y middleware: Incluye middleware personalizado y filtros para validar peticiones, manejar errores y asegurar la seguridad y el control de acceso.
* Mappers: Usa mappers para transformar datos entre las entidades y los DTOs de forma eficiente.
* Servicio de escritura de archivos: Permite exportar y manejar la escritura de archivos relacionados con la gestión del sistema.

### Operaciones CRUD Perros
El programa cuenta con un controlador base para las operaciones de los perros, donde se encuentran los endpoints para obtener todos los perros creados, agregar datos de un perro, modificar los datos de un perro en particular por medio de un id y eliminar el registro de un perro.

### Operaciones CRUD Perreras
Asimismo, el programa cuenta con un controlador base para las operaciones de las perreras. Se presentan endpoints para obtener datos de todas las perreras creadas, agregar una perrera, modificar una perrera en particular por medio de un id y eliminar el registro de una perrera.

### Entidades y DTOs
Puesto que es necesario el manejo de registros en Base de Datos, el programa permite la creación y manejo de las entidades de perro, perrera y la relación de estas dos, pues una perrera puede contener más de un perro. De igual forma, para ir de acuerdo con prácticas de seguridad, se hace uso de DTOs (Data Transfer Objects) para poder modificar estas entidades.

### Escritura de Archivos
La escritura de archivos es un servicio escencial de este WebApi, pues permite un manejo documental más eficiente. Para esto, es importante saber el endpoint el cual escribirá el archivo. El servicio contiene métodos para empezar el task y terminar el task de la lectura, como se puede ver a continuación:
```
public Task StartAsync(CancellationToken cancellationToken)
        {
            //Se ejecuta cuando cargamos la aplicacion 1 vez
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            Escribir("Proceso iniciado: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
            //Escribir("Proceso Iniciado");
            return Task.CompletedTask;
        }
```
Este proceso inicia el task, pero también inicia un _Timer_ para saber el tiempo que este tomó. Para usar esta funcionalidad, simplemente es necesario iniciar la aplicación e ingresar al endpoint correspondiente. Finalmente, también se tiene un método para escribir en el archivo como tal:
```
private void Escribir(string msg)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{fileName}";
            using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(msg); }
        }
```
Esta es fue una descripción sencilla de cómo utilizar esta herramienta, para visualizar el código completo, revisar carpeta de servicios.
##

Esta WebApi para perreras, desarrollada en C#, es la herramienta perfecta para administrar un sistema de refugio de animales o una base de datos de perros, asegurando que todo el proceso sea ágil, seguro y escalable. Con las operaciones CRUD completas, es posible crear, actualizar, eliminar y visualizar de manera sencilla información sobre perros, perreras y administradores del sistema. Además, el servicio de escritura de archivos permite exportar y manejar la información relacionada con el sistema de manera rápida y confiable, facilitando la gestión documental.
