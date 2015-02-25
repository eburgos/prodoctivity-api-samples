Consultando Información de Documentos
=======================

El sistema de gestión de documentos expone un Servicio Web tipo SOAP (“Distribuidor de Carga del Coordinador”) que puede ser accedido en:

	http://<servername>:<port>/<virtualdirectory>/ProdoctivityService.asmx 

Donde `servername` es el nombre del servidor donde esta instalado el Distribuidor de Carga del Coordinador (`CoordinatorFront`) y `port` es el puerto donde  está escuchando el IIS (que normalmente sería 80).  El `virtualdirectory` es el directorio virtual bajo el cual está instalado el servicio, si es que está instalado en uno.

La definición del servicio (WSDL) se puede obtener en:

	http://<servername>:<port>/<virtualdirectory>/ProdoctivityService.asmx?WSDL

Este es el servicio con el que la línea de negocios estará interactuando, es decir, enviando las peticiones (`Request`) y recibiendo las respuestas (`Response`). 

Recuperando el listado de las versiones de un documento.
---
Para retornar el listado de versiones asociadas a un documento dado, utilizamos el método llamado `GetDocumentVersionHistory`.  Este método recibe como parámetros el usuario y la contraseña de prodoctivity y el identificador del documento o `DocumentHandle` del documento sobre el cual deseamos obtener el listado de versiones.

* `Username`, tipo string: Usuario para autenticarse en ProDoctivity.
* `Password`, tipo string: contraseña del usuario para acceder a ProDoctivity.
* `Agent`, tipo string: es el nombre de la aplicación que está realizando la petición.
* `documentHandle`, tipo integer: número de identificador del documento sobre el cual deseamos obtener el listado de versiones.

Este método retorna un listado de objetos con la estructura `ProdoctivityDocumentVersion` citada a continuación:

	class ProdoctivityDocumentVersion
	{
		int DocumentHandle;
		datetime DocumentDate;
		int Version;
		decimal Size;
		string DocType;
		string CreatorName;
		bool Published;
		string CreatorLongName;
	}

Dónde:

* `DocumentHandle`: Es el código del documento.
* `DocumentDate`: Es la fecha de creación del esa versión del documento.
* `Version`: Es el número de versión del documento.
* `Size`: Es el tamaño del documento (en kilobytes).
* `DocType`: Es el Tipo del Documento.
* `CreatorName`: Es en usuario creador de esta versión del documento.
* `Published`: Indica si esta versión esta publicada o no.
* `CreatorLongName`: Es la concatenación de nombres y apellidos del usuario creador de esta versión del documento.

Recuperando la Información de un Documento 
--
Para retornar la información de un documento en particular (y no recuperar su archivo), utilizamos el método llamado `GetDocumentInfo`.  Este método recibe como parámetros el usuario y la contraseña de prodoctivity y el identificador del documento o `DocumentHandle` del documento sobre el cual deseamos obtener su información.

* `Username`, tipo string: Usuario para autenticarse en ProDoctivity.
* `Password`, tipo string: contraseña del usuario para acceder a ProDoctivity.
* `Agent`, tipo string: es el nombre de la aplicación que está realizando la petición.
* `documentHandle`, tipo integer: número de identificador del documento sobre el cual deseamos obtener el listado de versiones.

Este método retorna un listado de objetos con la estructura `ProdoctivityDocument` pero sin el binario asociado al documento ni información relacionada con dicho archivo:

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
		DocumentKeywordData KeywordData;
	}

Dónde:

* `DocumentHandle`: Es el código del documento.
* `DocumentTypeHandle`: Es el código del Tipo de Documento.
* `LastUpdated`: Es la última fecha de actualización del documento.
* `Version`: Es la versión del documento.
* `CreatedBy`: Es el código del usuario creador del documento.
* `CreationDate`: Es la fecha de creación del documento.
* `VersionDate`: Es la fecha de creación de la versión del documento que estamos recibiendo.
* `DocumentType`: Es el nombre del Tipo de Documento.
* `Reference`: Es la referencia del documento o nombre descriptivo que permite identificarlo.
* `KeywordData`: Es el detalle de las llaves del documento. (Explicado en el glosario de objetos).
