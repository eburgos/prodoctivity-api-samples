Seguridad y Autenticación
=======================

Servicios de Recuperación y Almacenamiento de Documentos
--
La seguridad del servicio de recuperación de documentos es **BASIC Authentication** cuyo usuario y password corresponden a los de Prodoctivity y el usuario que envíe sus credenciales debe tener permisos para poder ver el documento.

El motor de generación de documentos tiene un esquema de seguridad diferente al de Prodoctivity por lo cual los "requests" de generación deben ir con el usuario y password del motor y no de Prodoctivity. En el request el usuario se define en `request.UserName` y el password en `request.Password`.

Integración de autenticación con el Web Site
--
ProDoctivity permite que sistemas de terceros puedan redireccionar a sus usuarios hacia algún módulo de ProDoctivity y que estos puedan quedar autenticados y en el contexto de trabajo de ProDoctivity.  Esto se logra usando uno de tres mecanismos que citamos a continuación.

###Autenticación Active Directory (Recomendada)
Este mecanismo de autenticación consiste en integrar la seguridad de ProDoctivity con **Active Directory**.  Al realizar esta integración, los usuarios que intentan acceder a ProDoctivity son validados contra los usuarios con acceso permitido usando sus credenciales de Windows.  No requiere enviar datos de usuarios vía el URL y no le requerirá colocar sus credenciales a los usuarios en ningún momento.

Si el usuario que esta autenticado en Windows no posee permisos sobre el modulo o página que está intentando visualizar, el sistema re direccionara al usuario hacia la página de autenticación de ProDoctivity.

###Autenticación Básica (vía URL)
Este mecanismo de autenticación consiste en colocar las credenciales del usuario en la URL que se usara para invocar ProDoctivity®.  La forma de colocar las credenciales en la URL es la siguiente:

	http://<Servername>:<Port>/<virtualdirectory>/Login.aspx?u=<usuario>&p=<contraseña>&ReturnUrl=<Pagina o Modulo destino>

Dónde:

* `Usuario`: es el nombre de usuario que se usara para autenticarse en ProDoctivity.
	* Si este parámetro no es suministrado y el sistema no está configurado para acceder vía Active Directory o la autenticación falla, el sistema re direccionará al usuario hacia el módulo de autenticación.
* `Contraseña`: es la contraseña que se usara para autenticarse en ProDoctivity.
	* Si este parámetro no es suministrado y el sistema no está configurado para acceder vía Active Directory o la autenticación falla, el sistema re direccionará al usuario hacia el módulo de autenticación.
* `Página o Modulo Destino`: es la ruta de la página o modulo hacia donde desea que el usuario sea redireccionado, para acceder al “home” de ProDoctivity coloque “ProDoc/Default.aspx”

###Autenticación usando el Login
Este mecanismo de autenticación consiste en redireccionar al usuario al módulo de autenticación de ProDoctivity para que este a su vez coloque sus credenciales.  Una vez las credenciales fueron validadas, el sistema desplegara la página o dirección a la que originalmente fue enviado el usuario.
