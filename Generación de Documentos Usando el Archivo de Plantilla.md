Generación de Documentos
=======================

El motor de generación de documentos  expone un Servicio Web tipo SOAP (“Distribuidor de Carga del Coordinador”) que puede ser accedido en:


	http://<servername>:<port>/<virtualdirectory>/DocumentGenerationService.asmx 

Donde `servername` es el nombre del servidor donde esta instalado el Distribuidor de Carga del Coordinador (`CoordinatorFront`) y `port` es el puerto donde  está escuchando el IIS (que normalmente sería 80).  El `virtualdirectory` es el directorio virtual bajo el cual está instalado el servicio, si es que está instalado en uno.

Este es el servicio con el que la línea de negocios estará interactuando, es decir, enviando las peticiones (`Request`) y recibiendo las respuestas (`Response`). 

Generación de Documentos usando el archivo de la plantilla
---

El método que se expone para la generación de documentos usando el archivo de la plantilla y que a su vez recibe el “Request” lleva por nombre `GenerateDocument`.

La definición del servicio (WSDL) se puede obtener en:

	http://<servername>:<port>/<virtualdirectory>/DocumentGenerationService.asmx?WSDL

La petición (`Request`) está conformada por varias partes, de las cuales algunas son opcionales y otras obligatorias.  A continuación citaremos las partes que debe contener la petición, específicamente en el `Body`, que se debe enviar al Servicio Web controlador de carga y coordinador de peticiones:

###Conectores de Datos (Data Connectors)
Representados por el elemento XML `<Connector>`. Sirven para definir un conector de datos utilizable por una plantilla.  Los conectores no son obligatorios, pueden existir peticiones con o sin conectores. Los conectores de datos se utilizan para extraer datos desde una fuente externa.  

A continuación la estructura que debe contener el elemento XML `<Connector>`:

	<Connectors>
	          <Connector>
	            <Key>string</Key>
	            <ClassName>string</ClassName>
	            <Parameters xsi:nil="true" />
	          </Connector>
	          <Connector>
	            <Key>string</Key>
	            <ClassName>string</ClassName>
	            <Parameters xsi:nil="true" />
	          </Connector>
	</Connectors>

En la estructura que acabamos de mostrar existen dos conectores de datos, se pueden colocar hasta n conectores de datos.  El elemento `<Parameters>` debe contener los parámetros específicos inherentes al conector, es decir, si es una extracción de datos desde una tabla en particular, este elemento XML contendría elementos XML con la información de la conexión (DSN, ConnString, etc).  A continuación un ejemplo del elemento `<Connector>` con datos:

	<Connectors>
	  <Connector>
	    <Key>ClientesDBDataConnector1</Key>
	    <ClassName>StandardDataConnectorLib.ODBCDataConnector, StandardDataConnectorLib</ClassName>
	    <Parameterss>
	      <ConnectionString>DSN=pruebadocgen;Uid=operador;Pwd=1234</ConnectionString>
	    </Parameters>
	 </Connector>
	</Connectors>

###Complementos (Plugins)
Representados por el elemento XML `<Plugin>`. Sirven para definir un complemento (“plugin”) utilizable por una plantilla. Los complementos no son obligatorios, pueden existir peticiones con o sin complementos. Los complementos (“plugins”) se utilizan para modificar o utilizar el documento generado ejecutando código antes de ser despachado según la disposición. Ejemplos de uso de un complemento (“plugin”) puede ser un módulo que calcule algo dentro del documento y/o lo modifique.

A continuación la estructura que debe contener el elemento XML `<Plugin>`:

	<Plugins>
	          <Plugin>
	            <Key>string</Key>
	            <ClassName>string</ClassName>
	            <Params xsi:nil="true" />
	          </Plugin>
	          <Plugin>
	            <Key>string</Key>
	            <ClassName>string</ClassName>
	            <Params xsi:nil="true" />
	          </Plugin>
	</Plugins>

En la estructura que acabamos de mostrar existen dos complementos (“plugin”), se pueden colocar hasta n complementos.  El elemento `<Parameters>` debe contener los parámetros específicos inherentes al complemento, es decir, si es una conversión de formato, este elemento XML contendría elementos XML con la información del formato destino (Pdf, XPS, etc).  A continuación un ejemplo del elemento `<Plugin>` con datos:

	<Plugins>
	  <Plugin>
	    <Key>Test1</Key>
	    <ClassName>CustomPluginLib.CustomPlugin, CustomPluginLib</ClassName>
	    <Parameters>
	     <Parameter1>Test</Parameter1>
	     <Parameter2>Test</Parameter2>
	    </Parameters>
	  </Plugin>
	</Plugins>

### Tipo de Resultado

Representado por el elemento XML `<ResultType>`, contiene el tipo de resultado esperado en la respuesta (“Response”).  Los tipos de resultados que maneja el motor de generación son:

`Inline`: retorna un documento (de formato Word) en la respuesta (“response”) dentro del elemento XML `<Data>` en formato base64Binary.  Este tipo de resultado solo puede ser usado cuando en la petición (“Request”) se envía a generar un solo documento.

`InlineSoap`: retorna varios documentos en la respuesta (“response”) como un Soap Attachment.  

`Token`: este tipo de resultado solo retorna un “token” en la respuesta (“response”) dentro del elemento XML `<GenerationToken>`.  Este “token” puede ser utilizado posteriormente para consultar el estado de la petición enviada.  Este es el tipo de resultado recomendado para la generación de multipes documentos.

	<ResultType>Token|Inline|InlineSoap</ResultType>

###Disposiciones (Dispositions)
Representados por el elemento XML `<Disposition>`. Sirven para definir una disposición ejecutable por el motor. Una disposición representa una acción a ejecutar por el motor luego de haber generado un documento y haber aplicado los complementos (“Plugin), si existiesen. Ejemplos de uso de disposición: Insertar en ProDoctivity, enviar por correo, imprimir, enviar por fax, ingresar en un workflow de Sharepoint, etc.

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

En la estructura que acabamos de mostrar existen dos disposiciones `<Disposition>`, se pueden colocar hasta n disposiciones. El elemento `<GlobalParameters>` debe contener los parámetros específicos inherentes a la disposición, es decir, si es un envío del documento a ProDoctivity, este elemento XML `<GlobalParameters>` contendría elementos XML con la información de la equivalencia de llaves con marcadores, el tipo de documento destino, conexión de Base de Datos a utilizar, etc.   A continuación un ejemplo del elemento `<Disposition>` con datos:

	<Dispositions>
	  <Disposition>
	    <Key>ProdoctivityDisposition1</Key>
	    <ClassName>ProdoctivityDocumentGenerationLib.ProdoctivityInsertDisposition, ProdoctivityDocumentGenerationLib</ClassName>
	    <GlobalParameters>
	      <ConnectionString>ServerName=test</ConnectionString>
	    </GlobalParameters>
	  </Disposition>
	</Dispositions>

###Plantillas (Templates)
Representados por el elemento XML `<Template>`. Se utilizan para enviar las plantillas que serán utilizadas en la generación de los documentos. Estas son las plantillas que previamente fueron creadas y publicadas mediante el Configurador de Plantillas de ProDoctivity.

A continuación la estructura que debe contener un elemento XML `<Template>`:

	<Templates>
	          <Template>
	            <Key>string</Key>
	            <DocumentType>MSWordOpenXml|MSExcelOpenXml|MSPowerpointOpenXml</DocumentType>
	            <Content>base64Binary</Content>
	          </Template>
	          <Template>
	            <Key>string</Key>
	            <DocumentType>MSWordOpenXml|MSExcelOpenXml|MSPowerpointOpenXml</DocumentType>
	            <Content>base64Binary</Content>
	          </Template>
	</Templates>

En la estructura que acabamos de mostrar existen dos plantillas, se pueden colocar hasta n plantillas. El elemento `<Content>` debe contener la plantilla en formato base64Binary.   A continuación un ejemplo del elemento `<Template>` con datos:

	<Templates>	
	  <Template>
	    <Key>Contrato1</Key>
	     <DocumentType>MSWordOpenXml</DocumentType>
	     <Content>[Aqui la secuencia binaria del doc en base64Binary]</Content>
	  </Template>
	</Templates>

###Contextos de Generación (Generation Context)
Representados por el elemento XML `<GenerationContext>`. Sirven para definir un contexto de generación. En el motor de generación los contextos son los que dictan las acciones a ejecutar por plantillas y también son los que llevan los datos de cada una.  Básicamente abarca cuales son los datos a utilizar para la generación de los documentos, cuales complementos deben aplicarse a cada documento, cuales conectores deben usarse en cada documento y cuales disposiciones deben aplicarse a cada documento.

A continuación la estructura que debe contener un elemento XML `<GenerationContext>`: 

	<Contexts>
	          <GenerationContext>
	              <Data xsi:nil="true" />
	              <Documents xsi:nil="true" />
	              <Id>string<Id />
	          </GenerationContext>
	</Contexts>

En la estructura que acabamos de mostrar existe un Contexto de Generación, se pueden colocar hasta n Contextos de Generación. El elemento XML `<Data>` debe contener un XML conteniendo los datos a utilizar por los Documentos que se pretenden generar, un set de datos puede ser utilizado por múltiples documentos dentro del mismo contexto, es decir, se puede enviar un set de datos e incluir diferentes documentos, cada uno con una plantilla diferente y disposiciones, conectores y complementos diferentes.  El elemento XML `<Documents>` debe contener el listado de documentos que se desean generar y dentro de este elemento se deben colocar los siguientes datos:

* ID de Plantilla a utilizar (viene del listado de Plantillas).
* ID del Documento (único por petición).
* Disposiciones a utilizar.
* Complementos a utilizar.
* Conectores a utilizar.

El elemento `<Id>` debe contener un identificador (colocado por usted) para identificar un contexto en particular en caso de que remita múltiples contextos en una sola llamada.  Con este identificador es que podrá distinguir los diferentes contextos una vez reciba la respuesta.A continuación un ejemplo del elemento `<GenerationContext>` con datos:

	<GenerationContext>
	    <Data>
	        <NombreEmpleado>Juan</NombreEmpleado>
	        <Cedula>001-1357832-9</Cedula>
	    </Data>
	    <Documents>
	        <Document>
	            <TemplateKey>Contrato1</TemplateKey>
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
	            <Plugins>
	                <DocumentPlugin>
	                    <PluginKey>Test1</PluginKey>
	                    <Params>
	                        <Parameter001>test</Parameter001>
	                    </Params>
	                </DocumentPlugin>
	            </Plugins>
	        </Document>
	    </Documents>
	    <Id>MainContext</Id>
	</GenerationContext>


###Otros datos a suministrar
Adicional a lo citado, se deben suministrar los siguientes datos en el “request”:

* `UserName` (string): Este es el usuario utilizado para generar documentos, no tiene relación directa con el usuario de ProDoctivity, este es el que permite utilizar el motor de generación de documentos para generar documentos.
* `Password` (string): Esta es la contraseña del usuario utilizado para generar documentos.
* `RequestorName` (string): Aquí debe colocar un nombre descriptivo que indique el servicio o aplicación que está haciendo la petición, para fines de log.

		<RequestorName>string</RequestorName>
		<UserName>string</UserName>
		<Password>string</Password>

###Estructura de la Petición (“Request”) completa

	<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:fron="http://services.prodoctivity.com/DocumentGeneration/Front/">
	   <soapenv:Header/>
	   <soapenv:Body>
	      <fron:GenerateDocument>
	         <fron:request>
	            <fron:Connectors>
	               <fron:Connector>
	                  <fron:Key>?</fron:Key>
	                  <fron:ClassName>?</fron:ClassName>
	                  <fron:Parameters>
	                  </fron:Parameters>
	               </fron:Connector>
	            </fron:Connectors>
	            <fron:Plugins>
	               <fron:Plugin>
	                  <fron:Key>?</fron:Key>
	                  <fron:ClassName>?</fron:ClassName>
	                  <fron:Parameters>
	                  </fron:Parameters>
	               </fron:Plugin>
	            </fron:Plugins>
	            <fron:ResultType>?</fron:ResultType>
	            <fron:Dispositions>
	               <fron:Disposition>
	                  <fron:Key>?</fron:Key>
	                  <fron:ClassName>?</fron:ClassName>
	                  <fron:GlobalParameters>
	                  </fron:GlobalParameters>
	               </fron:Disposition>
	            </fron:Dispositions>
	            <fron:Templates>
	               <fron:Template>
	                  <fron:Key>?</fron:Key>
	                  <fron:DocumentType>?</fron:DocumentType>
	                  <fron:Content>cid:725083832030</fron:Content>
	                  <fron:SubTemplates/>
	               </fron:Template>
	            </fron:Templates>
	            <fron:Contexts>
	               <fron:GenerationContext>
	                  <fron:Data>
	                  </fron:Data>
	                  <fron:Documents>
	                     <fron:Document>
	                        <fron:TemplateKey>?</fron:TemplateKey>
	                        <fron:Plugins>
	                           <fron:DocumentPlugin>
	                              <fron:PluginKey>?</fron:PluginKey>
	                              <fron:Parameters>
	                              </fron:Parameters>
	                           </fron:DocumentPlugin>
	                        </fron:Plugins>
	                        <fron:Dispositions>
	                           <fron:DocumentDisposition>
	                              <fron:DispositionKey>?</fron:DispositionKey>
	                              <fron:Parameters>
	                              </fron:Parameters>
	                           </fron:DocumentDisposition>
	                        </fron:Dispositions>
	                        <fron:DocumentKey>?</fron:DocumentKey>
	                     </fron:Document>
	                  </fron:Documents>
	                  <fron:Id>?</fron:Id>
	               </fron:GenerationContext>
	            </fron:Contexts>
	            <fron:RequestorName>?</fron:RequestorName>
	            <fron:UserName>?</fron:UserName>
	            <fron:Password>?</fron:Password>
	         </fron:request>
	      </fron:GenerateDocument>
	   </soapenv:Body>
	</soapenv:Envelope>

Dentro de la petición (“request”) los valores de `UserName` y `Password` son exclusivamente para el motor de generación y no están compartidos con ProDoctivity.

###Consideraciones Generales
Una sola petición (“request”) puede generar tanto 1 documento como una cantidad n de documentos.

La cantidad de conectores de datos, plantillas, complementos o documentos/contextos de una petición (“request”) puede ser mayor que 1 en cualquiera de estas.

Los complementos (“plugins”) y conectores son completamente opcionales en una petición (“request”) en el sentido de que no es obligatorio enviar un complemento o conector que no se va a utilizar. Sin embargo, si alguna plantilla requiere utilizar algún complemento o conector entonces si es mandatorio incluirlo porque de lo contrario la generación retornará con un error.

Cualquier plantilla que solo requiera simplemente completar los datos para generar no necesitará de un complemento o conector, sino simplemente enviar la plantilla en base64Binary en el elemento `<Template>` y un contexto de generación que vincule por ese TemplateKey que contenga los datos necesarios en `GenerationContext/Data`.
