Ejemplos de Contextos
=======================
El sistema de gestión de documentos expone un Servicio Web tipo SOAP (“Distribuidor de Carga del Coordinador”) que puede ser accedido en:

	http://<servername>:<port>/<virtualdirectory>/ProdoctivityService.asmx 

Donde `servername` es el nombre del servidor donde esta instalado el Distribuidor de Carga del Coordinador (`CoordinatorFront`) y `port` es el puerto donde  está escuchando el IIS (que normalmente sería 80).  El `virtualdirectory` es el directorio virtual bajo el cual está instalado el servicio, si es que está instalado en uno.

La definición del servicio (WSDL) se puede obtener en:

	http://<servername>:<port>/<virtualdirectory>/ProdoctivityService.asmx?WSDL

Este es el servicio con el que la línea de negocios estará interactuando, es decir, enviando las peticiones (`Request`) y recibiendo las respuestas (`Response`). 

Obteniendo un ejemplo de datos del contexto para una plantilla en particular
--
El método utilizado para obtener un XML de ejemplo conteniendo el contexto de datos especifico de una plantilla es `GetTemplateSampleContextXml`.  Este método recibe como parámetros el usuario y contraseña de Prodoctivity y el identificador del documento plantilla o `DocumentHandle` de la plantilla.  Al invocar este método obtenemos un XML con el contexto de datos de la plantilla indicada conteniendo datos de ejemplo.

