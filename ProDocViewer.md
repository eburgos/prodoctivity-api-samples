8.	Visor de Documentos en la Web – ProDocViewer®
=======================
ProDocViewer
--
ProDocViewer® es el visor de documentos de ProDoctivity que permite a los usuarios integrar en sus sistemas la funcionalidad de visualizar documentos que están almacenados en ProDoctivity.  Con ProDocViewer® podemos visualizar, descargar e imprimir cualquier documento almacenado en ProDoctivity y todo esto a través de un navegador de internet.

ProDocViewer® puede ser integrado en sus aplicaciones Web y Desktop y de esta forma brindar a sus usuarios la facilidad de visualizar documentos sin cambiar su ambiente habitual de trabajo.

Navegadores de Internet Soportados:

* Internet Explorer (versión 9 o superior).
* Chrome.
* Firefox.
* Safari.
* Opera.

**Nota:** Para imprimir documentos deberá disponer de un visor de PDF en el computador (ej. Acrobat Reader).  Si el navegador que utiliza es Google Chrome este último requisito no es necesario.

Visualizar Documentos
--
Para visualizar documentos mediante el ProDocViewer® solo debe indicar en el navegador de internet la ruta del Viewer incluyendo en la misma el `GenerationToken` o el `DocumentHandle` que los Web Services de Generación de Documentos le han retornado. La URL que debe utilizar corresponde al Sitio Web de ProDoctivity y debe colocarla de la forma que se detalla a continuación.

###Para desplegar documentos usando un Token de generación

	http://<Servername>:<Port>/<virtualdirectory>/Login.aspx?u=<usuario>&p=<contraseña>&ReturnUrl=<virtualdirectory>/Action.aspx?action=/view/documents/<Token>

Dónde:

* `Servername`: es el nombre o dirección IP del servidor donde está instalado el sitio web de ProDoctivity.
* `Port`: es el puerto por el cual se accede a la aplicación (si es el puerto 80 no es necesario colocarlo).
* `VirtualDirectory`: es el directorio virtual donde está siendo ejecutado el Web Site de ProDoctivity.
* `Usuario`: es el nombre de usuario que se usara para autenticarse en ProDoctivity.
	* Si el sistema está configurado para autenticarse usando Active Directory este parámetro no es necesario.
	* Si este parámetro no es suministrado y el sistema no está configurado para acceder vía Active Directory o la autenticación falla, el sistema re direccionará al usuario hacia el módulo de autenticación.
* `Contraseña`: es la contraseña que se usara para autenticarse en ProDoctivity.
	* Si el sistema está configurado para autenticarse usando Active Directory este parámetro no es necesario.
	* Si este parámetro no es suministrado y el sistema no está configurado para acceder vía Active Directory o la autenticación falla, el sistema re direccionará al usuario hacia el módulo de autenticación.
* `Token`: es el Token de Generación que obtuvo al realizar la generación de documentos vía los Web Services.

###Para desplegar un documento utilizando su “DocumentHandle”

	http://<Servername>:<Port>/<virtualdirectory>/Login.aspx?u=<usuario>&p=<contraseña>&ReturnUrl=<virtualdirectory>/Action.aspx?action=/view/documents/<DocumentHandle>

Dónde:

* `Servername`: es el nombre o dirección IP del servidor donde está instalado el sitio web de ProDoctivity.
* `Port`: es el puerto por el cual se accede a la aplicación (si es el puerto 80 no es necesario colocarlo).
* `VirtualDirectory`: es el directorio virtual donde está siendo ejecutado el Web Site de ProDoctivity.
* `Usuario`: es el nombre de usuario que se usara para autenticarse en ProDoctivity.
	* Si el sistema está configurado para autenticarse usando Active Directory este parámetro no es necesario.
	* Si este parámetro no es suministrado y el sistema no está configurado para acceder vía Active Directory o la autenticación falla, el sistema re direccionará al usuario hacia el módulo de autenticación.
* `Contraseña`: es la contraseña que se usara para autenticarse en ProDoctivity.
	* Si el sistema está configurado para autenticarse usando Active Directory este parámetro no es necesario.
	* Si este parámetro no es suministrado y el sistema no está configurado para acceder vía Active Directory o la autenticación falla, el sistema re direccionará al usuario hacia el módulo de autenticación.
* `DocumentHandle`: es el DocumentHandle correspondiente al documento que desea recuperar.