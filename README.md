# Sistema de Mesa de Partes Digital (Prototipo)

## 1. Descripción del proyecto

Proyecto académico desarrollado para el curso **Fundamentos de Programación**.

El sistema propone una solución informática básica para apoyar la gestión de documentos de la Municipalidad de San Juan de Lurigancho. Busca organizar el registro, la digitalización simulada, la clasificación, la consulta y el seguimiento de expedientes.

El prototipo permite gestionar expedientes mediante un menú de opciones y almacenar la información en archivos JSON.

## 2. Objetivo

Desarrollar un prototipo de mesa de partes digital aplicando los fundamentos de programación, mediante el uso de estructuras de control, funciones, arreglos, cadenas de texto, validaciones y almacenamiento de información en archivos.

## 3. Tecnologías utilizadas

- **Lenguaje de programación:** C#
- **Entorno de desarrollo:** Visual Studio Code
- **Plataforma:** .NET 9
- **Pseudocódigo:** PSeInt
- **Almacenamiento:** archivos JSON
- **Control de versiones:** Git y GitHub

## 4. Funcionalidades implementadas

El sistema cuenta con las siguientes opciones:

1. **Registrar nuevo documento:** permite ingresar los datos del solicitante, documento, asunto y archivo asociado. Valida los datos y genera un código de expediente.
2. **Digitalizar y validar documento:** comprueba la existencia, extensión y tamaño del archivo PDF o imagen. Incluye una simulación del proceso OCR.
3. **Clasificar y derivar expediente:** permite asignar un trámite, categoría, área responsable y estado al expediente.
4. **Consultar expediente:** permite buscar un expediente y revisar su estado, área, historial de movimientos o información completa.
5. **Mostrar expedientes registrados:** presenta los expedientes almacenados.
6. **Ordenar expedientes por código:** permite visualizar los expedientes en orden ascendente o descendente sin modificar el orden original de los datos.
7. **Salir:** solicita confirmación antes de cerrar el sistema.

## 5. Validaciones y almacenamiento

- Validación del DNI o RUC del solicitante.
- Validación de los datos ingresados.
- Validación de la existencia y extensión del archivo.
- Límite de tamaño de archivo de 5 MB.
- Prevención de la digitalización repetida de un mismo expediente.
- Búsqueda mediante el código completo o su número.
- Registro del historial de movimientos del expediente.
- Almacenamiento de la información en un archivo JSON para conservar los registros entre ejecuciones.

El archivo `expedientes.json` se genera junto al ejecutable y está excluido del repositorio Git.

## 6. Ejecución del sistema

**Requisito:** tener instalado el SDK de .NET 9.

Desde la carpeta raíz del repositorio, ejecutar:

```text
dotnet run --project Mesa_de_partes/Mesa_de_partes.csproj
```

## 7. Archivos del proyecto

- `Mesa_de_partes/`: contiene el código fuente del sistema.
- `docs/SistemaMesaDePartes.psc`: contiene el pseudocódigo desarrollado en PSeInt.
- `README.md`: contiene la descripción y las instrucciones del proyecto.

## 8. Alcance y limitaciones

Este proyecto es un prototipo académico y no un sistema municipal de producción.

La función OCR es simulada: no realiza reconocimiento ni extracción real de texto de los archivos PDF o imágenes.

La validación comprueba características del archivo, pero no certifica la autenticidad ni el contenido del documento. La derivación registra el área y el estado del expediente, pero no realiza envíos ni notificaciones electrónicas.

La implementación de OCR real, las notificaciones electrónicas y otras integraciones quedan como posibles mejoras futuras.

El informe, la presentación, la observación de campo y la declaración de uso de inteligencia artificial son entregables complementarios del proyecto.

## 9. Integrantes del equipo

**1. Tatiana Yajaira Macalopu Coronado**

Responsabilidad: Registro de documentos.

**2. Harold Jair Calderon Chavez**

Responsabilidad: Digitalización y OCR simulado.

**3. Jhandy Rafael Pacherres Flores**

Responsabilidad: Clasificación y derivación.

**4. Fanny Zulema Quiñones Custodio**

Responsabilidad: Menú principal e integración.

**5. Miler Duvan Ortiz Quispe**

Responsabilidad: Consulta de expedientes.