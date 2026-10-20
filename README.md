# Mesa de partes digital (prototipo)

Proyecto del curso Fundamentos de Programacion. La version integrada une el
menu, registro, validacion de archivo, clasificacion, consulta, listado y
ordenamiento de expedientes. Los registros se guardan en JSON para recuperarlos
al reiniciar el programa.

## Ejecutar

Requiere .NET 9 SDK. Desde la raiz del repositorio:

```text
dotnet run --project Mesa_de_partes/Mesa_de_partes.csproj
```

El archivo `expedientes.json` se crea junto al ejecutable y esta excluido de Git.
El pseudocodigo correspondiente esta en `docs/SistemaMesaDePartes.psc`.
En la opcion 1 se indica el archivo PDF o imagen y su ruta queda asociada al
expediente. La opcion 2 vuelve a comprobar ese mismo archivo y actualiza su
estado, sin pedir la ruta otra vez. Para expedientes antiguos sin archivo, la
opcion 2 permite indicarlo una sola vez. El archivo original permanece en su
ubicacion; si se mueve o borra, la validacion posterior fallara.
Al consultar, validar o derivar un expediente se puede escribir el codigo
completo (`EXP-1001`) o solo su numero (`1001`).

## Alcance

El registro y la digitalizacion comprueban que exista un archivo PDF o imagen,
su extension y su tamano (hasta 5 MB). No realiza OCR. La derivacion registra un
area y un estado, pero no envia notificaciones electronicas. El informe,
presentacion, observacion de campo y declaracion de uso de IA son entregables
separados del codigo.
