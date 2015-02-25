Almacenamiento de Documentos
=======================

El sistema de gestión de documentos expone un Servicio Web tipo SOAP (“Distribuidor de Carga del Coordinador”) que puede ser accedido en:

	http://<servername>:<port>/<virtualdirectory>/ProdoctivityService.asmx 

Donde `servername` es el nombre del servidor donde esta instalado el Distribuidor de Carga del Coordinador (`CoordinatorFront`) y `port` es el puerto donde  está escuchando el IIS (que normalmente sería 80).  El `virtualdirectory` es el directorio virtual bajo el cual está instalado el servicio, si es que está instalado en uno.

La definición del servicio (WSDL) se puede obtener en:

	http://<servername>:<port>/<virtualdirectory>/ProdoctivityService.asmx?WSDL

Ejemplo:

	http://test.prodoctivity.com:80/CoordinatorFront/ProdoctivityService.asmx?WSDL

Este es el servicio con el que la línea de negocios estará interactuando, es decir, enviando las peticiones (`Request`) y recibiendo las respuestas (`Response`). 

Almacenando un documento nuevo
---
Para almacenar un nuevo documento en ProDoctivity utilizamos el método llamado `SaveDocumentWithKeywords` el cual recibe los siguientes parámetros:

* `Username`, tipo string: Usuario para autenticarse en ProDoctivity.
* `Password`, tipo string: contraseña del usuario para acceder a ProDoctivity.
* `Agent`, tipo string: es el nombre de la aplicación que está realizando la petición.
* `documentType`, tipo integer: código del Tipo de Documento en ProDoctivity.
* `DocumentData`, tipo arreglo de bytes (`byte[]`): arreglo de bytes del documento que se desea almacenar.
* `lastVersionDocHandle`, tipo integer: el identificador del documento al cual le estaremos creando la nueva versión en caso de que sea una versión nueva que estemos enviando (se envía 0 cuando el documento es nuevo).
* `mimeType`, tipo string: Aquí debemos indicar el formato del archivo o tipo de contenido que estamos enviando.
* `keywordsData`, tipo estructura de keywords: aquí  debemos enviar los datos de las llaves que deseamos almacenar.

Este método retorna un objeto con la estructura `ProdoctivityDocument` pero sin el binario asociado al documento ni información relacionada con dicho archivo:

	class ProdoctivityDocument
	{
		int DocumentHandle;
		int DocumentTypeHandle;
		datetime LastUpdated;
		int Version;
		int CreatedBy;
		datetime CreationDate;
		string CreatedByName;
		datetime VersionDate;
		string DocumentType;
		string Reference;
	}

Dónde:

* `DocumentHandle`: Es el código del documento que acaba de almacenarse.
* `DocumentTypeHandle`: Es el código del Tipo de Documento.

Almacenando nuevas versiones de documentos existentes
--
Para almacenar una nueva versión de un documento existente en ProDoctivity utilizamos el método llamado `SaveDocument` el cual recibe los siguientes parámetros:

* `Username`, tipo string: Usuario para autenticarse en ProDoctivity.
* `Password`, tipo string: contraseña del usuario para acceder a ProDoctivity.
* `Agent`, tipo string: es el nombre de la aplicación que está realizando la petición.
* `documentType`, tipo integer: código del Tipo de Documento en ProDoctivity.
* `DocumentData`, tipo arreglo de bytes (byte[]): arreglo de bytes del documento que se desea almacenar.
* `lastVersionDocHandle`, tipo integer: el identificador del documento al cual le estaremos creando la nueva versión.
* `mimeType`, tipo string: Aquí debemos indicar el formato del archivo o tipo de contenido que estamos enviando.

Este método retorna un objeto con la estructura `ProdoctivityDocument` pero sin el binario asociado al documento ni información relacionada con dicho archivo:

	class ProdoctivityDocument
	{
		int DocumentHandle;
		int DocumentTypeHandle;
		datetime LastUpdated;
		int Version;
		int CreatedBy;
		datetime CreationDate;
		string CreatedByName;
		datetime VersionDate;
		string DocumentType;
		string Reference;
	}

Dónde:

* `DocumentHandle`: Es el código del documento que acaba de almacenarse.
* `DocumentTypeHandle`: Es el código del Tipo de Documento.