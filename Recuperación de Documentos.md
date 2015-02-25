Recuperación de Documentos
=======================

El sistema de gestión de documentos expone un Servicio Web tipo SOAP (“Distribuidor de Carga del Coordinador”) que puede ser accedido en:

	http://<servername>:<port>/<virtualdirectory>/ProdoctivityService.asmx 

Donde `servername` es el nombre del servidor donde esta instalado el Distribuidor de Carga del Coordinador (`CoordinatorFront`) y `port` es el puerto donde  está escuchando el IIS (que normalmente sería 80).  El `virtualdirectory` es el directorio virtual bajo el cual está instalado el servicio, si es que está instalado en uno.

La definición del servicio (WSDL) se puede obtener en:

	http://<servername>:<port>/<virtualdirectory>/ProdoctivityService.asmx?WSDL

Este es el servicio con el que la línea de negocios estará interactuando, es decir, enviando las peticiones (`Request`) y recibiendo las respuestas (`Response`). 

Retornando el documento mediante invocación SOAP
---
Este servicio posee un método llamado `GetDocument` el cual recibe como parámetros el usuario y contraseña de ProDoctivity, el identificador del documento o `DocumentHandle` y un indicador para especificar si requerimos la última versión del documento o es el documento específico que fue enviado en el parámetro `DocumentHandle`.  

* `Username`, tipo string: Usuario para autenticarse en ProDoctivity.
* `Password`, tipo string: contraseña del usuario para acceder a ProDoctivity.
* `Agent`, tipo string: es el nombre de la aplicación que está realizando la petición.
* `docHandle`, tipo integer: número de identificador del documento que deseamos recuperar.
* `getLatestVersion`, tipo bool: indicador que especifica si debe retornar la última versión del documento o el documento específico que le fue indicado.

Este método retorna un objeto con la estructura `ProdotivityDocument` citada a continuación

	class ProdoctivityDocument
	{
		string ContentType;
		int DocumentHandle;
		int DocumentTypeHandle;
		datetime LastUpdated;
		int Version;
		int CreatedBy;
		datetime CreationDate;
		string CreatedByName;
		datetime VersionDate;
		string DocumentType;
		List<byte[]> Pages;
		string Reference;
		DocumentKeywordData KeywordData;
		string GenerationToken;
	}

Dónde:

* `ContentType`: Es el formato del documento (Word, Tiff, Pdf, etc.).
* `DocumentHandle`: Es el código del documento.
* `DocumentTypeHandle`: Es el código del Tipo de Documento.
* `LastUpdated`: Es la última fecha de actualización del documento.
* `Version`: Es la versión del documento.
* `CreatedBy`: Es el código del usuario creador del documento.
* `CreationDate`: Es la fecha de creación del documento.
* `VersionDate`: Es la fecha de creación de la versión del documento que estamos recibiendo.
* `DocumentType`: Es el nombre del Tipo de Documento.
* `Pages`: Son los arreglos de bypes correspondiente a cada una de las páginas del documento.  Si es un documento Word, Excel o PDF, el documento completo viene en el único elemento que traerá el arreglo.
* `Reference`: Es la referencia del documento o nombre descriptivo que permite identificarlo.
* `KeywordData`: Es el detalle de las llaves del documento. (Explicado en el glosario de objetos).
* `GenerationToken`: Es el token de generación interno asignado al documento.

Retornando el Archivo mediante invocación SOAP y convirtiendo el resultado a PDF
---

Para retornar un documento transformado a formato PDF, utilizaremos el método `GetDocumentToPDF`, el cual recibe los mismos parámetros que el método citado en el tópico anterior.  

* `Username`, tipo string: Usuario para autenticarse en ProDoctivity.
* `Password`, tipo string: contraseña del usuario para acceder a ProDoctivity.
* `Agent`, tipo string: es el nombre de la aplicación que está realizando la petición.
* `documentHandle`, tipo integer: número de identificador del documento que deseamos recuperar.
* `getLatestVersion`, tipo bool: indicador que especifica si debe retornar la última versión del documento o el documento específico que le fue indicado.

Este método retorna un objeto con la estructura `ProdotivityDocument` el cual citamos en el tópico anterior.

Retornando el listado de documentos generados utilizando el Token de Generación
---
Para retornar el listado de códigos de documentos que fueron generados mediante una llamada al motor de generación a través de un token de generacion, utilizaremos el método `GetDocumentHandleListFromGenerationToken`, el cual recibe los siguientes parámetros:

* `User`, tipo string: Usuario para autenticarse en ProDoctivity.
* `Password`, tipo string: contraseña del usuario para acceder a ProDoctivity.
* `Agent`, tipo string: es el nombre de la aplicación que está realizando la petición.
* `generationToken`, tipo string: es el token de generación sobre el cual queremos saber los códigos de documentos generados con este.

Este método retorna un listado de códigos de documentos de tipo `integer`.

Retornando el Archivo por Descarga Directa Http
---
El servicio para recuperación de documentos almacenados en Prodoctivity se encuentra en: 

	http://<servername>:<port>/<virtualdirectory>/Services/GetImage.ashx

Donde `servername` es el nombre del servidor donde esta instalado el Distribuidor de Carga del Coordinador (`CoordinatorFront`) y `port` es el puerto donde  está escuchando el IIS (que normalmente sería 80).  El `virtualdirectory` es el directorio virtual bajo el cual está instalado el servicio, si es que está instalado en uno.

Se puede obtener un documento original desde Prodoctivity al invocar vía HTTP el siguiente URL:

	http://<servername>:<port>/<virtualdirectory>/Services/GetImage.ashx?DocumentHandle=1234&Page=1

Dónde el parámetro “Page” es opcional, al no enviar el parámetro Page se asume por defecto la página 1. Los documentos en formato Word se consideran de una sola página y su primera página es todo el documento .docx.

Un documento Prodoctivity puede ser convertido a PDF automáticamente al invocar el servicio de la siguiente manera:

	http://<servername>:<port>/<virtualdirectory>/Services/GetImage.ashx?DocumentHandle=1234&ConvertToPDF=true

