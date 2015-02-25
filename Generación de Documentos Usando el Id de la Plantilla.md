Generación de Documentos
=======================

El motor de generación de documentos  expone un Servicio Web tipo SOAP (“Distribuidor de Carga del Coordinador”) que puede ser accedido en:


	http://<servername>:<port>/<virtualdirectory>/ProdoctivityService.asmx 

Donde `servername` es el nombre del servidor donde esta instalado el Distribuidor de Carga del Coordinador (`CoordinatorFront`) y `port` es el puerto donde  está escuchando el IIS (que normalmente sería 80).  El `virtualdirectory` es el directorio virtual bajo el cual está instalado el servicio, si es que está instalado en uno.

Este es el servicio con el que la línea de negocios estará interactuando, es decir, enviando las peticiones (`Request`) y recibiendo las respuestas (`Response`). 

Generación de Documentos usando el Id de la plantilla
---

El método que se expone para la generación de documentos usando el Id de la plantilla y que a su vez recibe el “Request” lleva por nombre `GenerateDocumentFromProDoctivityDocumentHandle`.

La definición del servicio (WSDL) se puede obtener en:

	http://<servername>:<port>/<virtualdirectory>/ProdoctivityService.asmx?WSDL

La petición (`Request`) está conformada por varias partes, de las cuales algunas son opcionales y otras obligatorias.  A continuación citaremos las partes que debe contener la petición, específicamente en el `Body`, que se debe enviar al Servicio Web controlador de carga y coordinador de peticiones:

###Contextos
Representados por el elemento XML `<GenerationContext>`. Sirven para definir un contexto de generación. En el motor de generación los contextos son los que dictan las acciones a ejecutar por plantillas y también son los que llevan los datos de cada una.  Básicamente abarca cuales son los datos a utilizar para la generación de los documentos y cuales disposiciones deben aplicarse a cada documento.

A continuación la estructura que debe contener un elemento XML `<GenerationContext>`: 

	<Contexts>
	          <GenerationContext>
	              <Data xsi:nil="true" />
	              <Documents xsi:nil="true" />
	              <Id>string<Id />
	          </GenerationContext>
	</Contexts>

En la estructura que acabamos de mostrar existe un Contexto de Generación (“GenerationContext”), se pueden colocar hasta n Contextos de Generación. El elemento XML `<Data>` debe contener un XML conteniendo los datos a utilizar por los Documentos que se pretenden generar, un set de datos puede ser utilizado por múltiples documentos dentro del mismo contexto, es decir, se puede enviar un set de datos e incluir diferentes documentos, cada uno con una plantilla diferente y disposiciones, conectores y complementos diferentes.  El elemento XML `<Documents>` debe contener el listado de documentos que se desean generar y dentro de este elemento se deben colocar los siguientes datos:

* ID del Documento (único por petición).
* Disposiciones a utilizar.

El elemento `<Id>` debe contener un identificador (colocado por usted) para identificar un contexto en particular en caso de que remita múltiples contextos en una sola llamada.  Con este identificador es que podrá distinguir los diferentes contextos una vez reciba la respuesta. A continuación un ejemplo del elemento `<GenerationContext>` con datos:

	<GenerationContext>
	    <Data>
	        <NombreEmpleado>Juan</NombreEmpleado>
	        <Cedula>001-1357832-9</Cedula>
	    </Data>
	    <Documents>
	        <Document>
	            <DocumentKey>Contrato-Juan001</DocumentKey>
	            <Dispositions>
	                <DocumentDisposition>
	                    <DispositionKey>ProdoctivityDisposition1</DispositionKey>
	                    <Parameters>
	                        <DocumentKey>24</DocumentKey>
	                        <ProdoctivityUsername>user</ProdoctivityUsername>
	                        <ProdoctivityPassword>pass</ProdoctivityPassword>
	                        <KeywordMap>
	                            <Keyword id="22" source="Name" />
	                            <Keyword id="23" source="IdentificationNumber" />
	                        </KeywordMap>
	                    </Parameters>
	                </DocumentDisposition>
	            </Dispositions>
	        </Document>
	    </Documents>
	    <Id>MainContext</Id>
	</GenerationContext>

###Disposiciones
Representados por el elemento XML `<Disposition>`. Sirven para definir una disposición ejecutable por el motor. Una disposición representa una acción a ejecutar por el motor luego de haber generado un documento. Ejemplos de uso de disposición: Insertar en ProDoctivity, enviar por correo, imprimir, enviar por fax, ingresar en un workflow de Sharepoint, etc.

A continuación la estructura que debe contener un elemento XML `<Disposition>`:

	<Dispositions>
	          <Disposition>
	            <Key>string</Key>
	            <ClassName>string</ClassName>
	            <GlobalParameters xsi:nil="true" />
	          </Disposition>
	          <Disposition>
	            <Key>string</Key>
	            <ClassName>string</ClassName>
	            <GlobalParameters xsi:nil="true" />
	          </Disposition>
	</Dispositions>

En la estructura que acabamos de mostrar existen dos disposiciones (“disposition”), se pueden colocar hasta n disposiciones. El elemento `<GlobalParameters>` debe contener los parámetros específicos inherentes a la disposición, es decir, si es un envío del documento a ProDoctivity, este elemento XML `<GlobalParameters>` contendría elementos XML con la información de la equivalencia de llaves con marcadores, el tipo de documento destino, conexión de Base de Datos a utilizar, etc.   A continuación un ejemplo del elemento `<Disposition>` con datos:

	<Dispositions>
	  <Disposition>
	    <Key>ProdoctivityDisposition1</Key>
	    <ClassName>ProdoctivityDocumentGenerationLib.ProdoctivityInsertDisposition, ProdoctivityDocumentGenerationLib</ClassName>
	    <GlobalParameters>
	      <ConnectionString>ServerName=test</ConnectionString>
	    </GlobalParameters>
	  </Disposition>
	</Dispositions>


###Otros datos
Adicional a lo citado, se deben suministrar los siguientes datos en el “request”:

* `UserName` (string): Este es el usuario utilizado para generar documentos, no tiene relación directa con el usuario de ProDoctivity, este es el que permite utilizar el motor de generación de documentos para generar documentos.
* `Password` (string): Esta es la contraseña del usuario utilizado para generar documentos.
* `ResultType` (string): contiene el tipo de resultado esperado en la respuesta (“Response”).  Los tipos de resultados que maneja el motor de generación son:
	* `Inline`: retorna un documento (de formato Word) en la respuesta (“response”) dentro del elemento XML <Data> en formato base64Binary.  Este tipo de resultado solo puede ser usado cuando en la petición (“Request”) se envía a generar un solo documento.
	* `InlineSoap`: retorna varios documentos en la respuesta (“response”) como un Soap Attachment.  
	* `Token`: este tipo de resultado solo retorna un “token” en la respuesta (“response”) dentro del elemento XML <GenerationToken>.  Este “token” puede ser utilizado posteriormente para consultar el estado de la petición enviada.  Este es el tipo de resultado recomendado para la generación de múltiples documentos.
* `DocumentHandle` (int): Aquí se debe enviar el código del documento correspondiente a la plantilla con la cual vamos a generar el documento.
* `ProdoctivityUserName` (string): Aquí se debe enviar el usuario de ProDoctivity con el que estaremos autenticándonos para realizar la generación del documento y el posterior almacenamiento en ProDoctivity.
* `ProdoctivityPassword` (string): Aquí se debe enviar la contraseña del usuario de ProDoctivity con el que estaremos autenticándonos para la realizar la generación del documento y el posterior almacenamiento en ProDoctivity.
* `ProdoctivityAgent` (string): Aquí se indica cual es la aplicación que está haciendo la llamada de generación, para fines de log y autenticación.

		<RequestorName>string</RequestorName>
		<UserName>string</UserName>
		<Password>string</Password>
		<ResultType>Token|Inline|InlineSoap</ResultType>
		<DocumentHandle>int</DocumentHandle>
		<ProdoctivityUserName>string</ProdoctivityUserName>
		<ProdoctivityPassword>string</ProdoctivityPassword>
		<ProdoctivityAgent>string</ProdoctivityAgent>

###Estructura de la petición ("Request") completa.

	<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
	   <soapenv:Header/>
	   <soapenv:Body>
	      <tem:GenerateDocumentFromProdoctivityDocumentHandle>
	         <tem:prodoctivityReq>
	            <tem:UserName>?</tem:UserName>
	            <tem:Password>?</tem:Password>
	            <tem:Contexts>
	               <tem:GenerationContext>
	                  <tem:Data>
	                  </tem:Data>
	                  <tem:Documents>
	                     <tem:Document>
	                        <tem:Dispositions>
	                           <tem:DocumentDisposition>
	                              <tem:DispositionKey>?</tem:DispositionKey>
	                              <tem:Parameters>
	                              </tem:Parameters>
	                           </tem:DocumentDisposition>
	                        </tem:Dispositions>
	                        <tem:DocumentKey>?</tem:DocumentKey>
	                     </tem:Document>
	                  </tem:Documents>
	                  <tem:Id>?</tem:Id>
	               </tem:GenerationContext>
	            </tem:Contexts>
	            <tem:ResultType>?</tem:ResultType>
	            <tem:Dispositions>
	               <tem:Disposition>
	                  <tem:Key>?</tem:Key>
	                  <tem:ClassName>?</tem:ClassName>
	                  <tem:GlobalParameters>
	                  </tem:GlobalParameters>
	               </tem:Disposition>
	            </tem:Dispositions>
	            <tem:DocumentHandle>?</tem:DocumentHandle>
	            <tem:ProdoctivityUsername>?</tem:ProdoctivityUsername>
	            <tem:ProdoctivityPassword>?</tem:ProdoctivityPassword>
	            <tem:ProdoctivityAgent>?</tem:ProdoctivityAgent>
	         </tem:prodoctivityReq>
	      </tem:GenerateDocumentFromProdoctivityDocumentHandle>
	   </soapenv:Body>
	</soapenv:Envelope>

Generación de Documentos usando varios Id de plantillas
---

Este método funciona exactamente igual que `GenerateDocumentFromProDoctivityDocumentHandle` con la diferencia de que en este se recibe un listado de códigos de documentos de tipo plantillas, con el objetivo de que se generen tantos documentos como plantillas se indiquen en el listado de Códigos de Documentos y esto así para cada contexto enviado.   Si enviamos tres (3) contextos de datos y dos (2) códigos de documentos de plantillas, el sistema generaría seis (6) documentos, dos documentos para cada contexto de datos.  Aquí detallamos el único elemento adicional que debe colocar al “Request”, nos referimos al elemento `documentHandleList`. Al usar este método, ya no es necesario que coloque en el “request” el dato `documentHandle`.

###Listado de Codigos de Plantillas
Representados por el elemento XML `<documentHandleList>`. Sirven para definir un listado de códigos de documentos que a su vez corresponden al listado de plantillas que deseamos sean usadas para generar los documentos. A continuación la estructura que debe contener un elemento XML `<documentHandleList>`:

	<documentHandleList >
	          <int>integer</int>
	</documentHandleList >

A continuación un ejemplo del elemento `<documentHandleList>` con datos:

	<documentHandleList>
	          <int>2589</int>
	          <int>3654</int>
	          <int>7851</int>
	</documentHandleList>

###Estructura de la Petición (“Request”) completa.

	<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
	   <soapenv:Header/>
	   <soapenv:Body>
	      <tem:GenerateDocumentFromProdoctivityDocumentHandleList>
	         <tem:prodoctivityReq>
	            <tem:UserName>?</tem:UserName>
	            <tem:Password>?</tem:Password>
	            <tem:Contexts>
	               <tem:GenerationContext>
	                  <tem:Data>
	                  </tem:Data>
	                  <tem:Documents>
	                  </tem:Documents>
	                  <tem:Id>?</tem:Id>
	               </tem:GenerationContext>
	            </tem:Contexts>
	            <tem:ResultType>?</tem:ResultType>
	            <tem:Dispositions>
	            </tem:Dispositions>
	            <tem:DocumentHandle>?</tem:DocumentHandle>
	            <tem:ProdoctivityUsername>?</tem:ProdoctivityUsername>
	            <tem:ProdoctivityPassword>?</tem:ProdoctivityPassword>
	            <tem:ProdoctivityAgent>?</tem:ProdoctivityAgent>
	         </tem:prodoctivityReq>
	         <tem:documentHandleList>
	            <tem:int>?</tem:int>
	         </tem:documentHandleList>
	      </tem:GenerateDocumentFromProdoctivityDocumentHandleList>
	   </soapenv:Body>
	</soapenv:Envelope>
