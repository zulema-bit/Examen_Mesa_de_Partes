Algoritmo SistemaMesaDePartes
    // Prototipo de simulacion en PSeInt. El guardado en archivo se implementa en C#.
    Definir entrada, documento, nombre, tipo, asunto, codigoBuscar, confirma Como Caracter
    Definir codigos, documentos, solicitantes, tipos, asuntos, fechas, estados, areas Como Caracter
    Definir cantidad, siguiente, seleccion, i, j, k, pos Como Entero
    Definir valido, salir Como Logico
    Definir auxiliar Como Caracter
    Dimension codigos[100], documentos[100], solicitantes[100], tipos[100]
    Dimension asuntos[100], fechas[100], estados[100], areas[100]
    cantidad <- 0
    siguiente <- 1001
    salir <- Falso

    Repetir
        Escribir "===== MESA DE PARTES ====="
        Escribir "1. Registrar nuevo documento"
        Escribir "2. Digitalizar y validar documento"
        Escribir "3. Clasificar y derivar expediente"
        Escribir "4. Consultar expediente"
        Escribir "5. Mostrar expedientes registrados"
        Escribir "6. Ordenar expedientes por codigo"
        Escribir "7. Salir"
        Escribir "Seleccione una opcion:"
        Leer entrada
        seleccion <- 0
        Si Longitud(entrada) = 1 Entonces
            Si entrada = "1" O entrada = "2" O entrada = "3" O entrada = "4" O entrada = "5" O entrada = "6" O entrada = "7" Entonces
                seleccion <- ConvertirANumero(entrada)
            FinSi
        FinSi

        Segun seleccion Hacer
            1:
                Si cantidad = 100 Entonces
                    Escribir "Limite de 100 expedientes alcanzado."
                SiNo
                    Repetir
                        Escribir "DNI (8 digitos) o RUC (11 digitos):"
                        Leer documento
                        valido <- Longitud(documento) = 8 O Longitud(documento) = 11
                        Si valido Entonces
                            Para k <- 1 Hasta Longitud(documento) Hacer
                                Si Subcadena(documento,k,k) <> "0" Y Subcadena(documento,k,k) <> "1" Y Subcadena(documento,k,k) <> "2" Y Subcadena(documento,k,k) <> "3" Y Subcadena(documento,k,k) <> "4" Y Subcadena(documento,k,k) <> "5" Y Subcadena(documento,k,k) <> "6" Y Subcadena(documento,k,k) <> "7" Y Subcadena(documento,k,k) <> "8" Y Subcadena(documento,k,k) <> "9" Entonces
                                    valido <- Falso
                                FinSi
                            FinPara
                        FinSi
                        Si No valido Entonces
                            Escribir "Documento invalido."
                        FinSi
                    Hasta Que valido
                    Repetir
                        Escribir "Nombre o razon social:"
                        Leer nombre
                    Hasta Que Longitud(nombre) > 0
                    Repetir
                        Escribir "Tipo de documento:"
                        Leer tipo
                    Hasta Que Longitud(tipo) > 0
                    Repetir
                        Escribir "Asunto:"
                        Leer asunto
                    Hasta Que Longitud(asunto) > 0
                    Escribir "Confirma el registro? (S/N):"
                    Leer confirma
                    Si Mayusculas(confirma) = "S" Entonces
                        pos <- cantidad + 1
                        codigos[pos] <- "EXP-" + ConvertirATexto(siguiente)
                        documentos[pos] <- documento
                        solicitantes[pos] <- nombre
                        tipos[pos] <- tipo
                        asuntos[pos] <- asunto
                        Escribir "Fecha de registro (DD/MM/AAAA):"
                        Leer fechas[pos]
                        estados[pos] <- "Registrado"
                        areas[pos] <- "Sin derivar"
                        cantidad <- cantidad + 1
                        siguiente <- siguiente + 1
                        Escribir "Registrado: ", codigos[pos]
                    SiNo
                        Escribir "Registro cancelado."
                    FinSi
                FinSi
            2:
                Escribir "Codigo del expediente a validar:"
                Leer codigoBuscar
                pos <- -1
                Para i <- 1 Hasta cantidad Hacer
                    Si codigos[i] = codigoBuscar O codigos[i] = "EXP-" + codigoBuscar Entonces
                        pos <- i
                    FinSi
                FinPara
                Si pos = -1 Entonces
                    Escribir "Expediente no encontrado."
                SiNo
                    Escribir "Simulacion: confirme que el documento fisico fue digitalizado y validado (S/N):"
                    Leer confirma
                    Si Mayusculas(confirma) = "S" Entonces
                        estados[pos] <- "En revision"
                        Escribir "Estado actualizado: ", estados[pos]
                    SiNo
                        Escribir "No se cambio el estado."
                    FinSi
                FinSi
            3:
                Escribir "Codigo del expediente a derivar:"
                Leer codigoBuscar
                pos <- -1
                Para i <- 1 Hasta cantidad Hacer
                    Si codigos[i] = codigoBuscar O codigos[i] = "EXP-" + codigoBuscar Entonces
                        pos <- i
                    FinSi
                FinPara
                Si pos = -1 Entonces
                    Escribir "Expediente no encontrado."
                SiNo
                    Escribir "1. Obras; 2. Desarrollo Urbano; 3. Otra area"
                    Leer entrada
                    Segun entrada Hacer
                        "1": areas[pos] <- "Subgerencia de Obras"
                        "2": areas[pos] <- "Desarrollo Urbano"
                        "3": areas[pos] <- "Area correspondiente"
                        De Otro Modo: areas[pos] <- "Sin derivar"
                    FinSegun
                    Si areas[pos] = "Sin derivar" Entonces
                        Escribir "Seleccion invalida."
                    SiNo
                        estados[pos] <- "En proceso"
                        Escribir "Derivado a: ", areas[pos]
                    FinSi
                FinSi
            4:
                Escribir "Codigo a consultar:"
                Leer codigoBuscar
                pos <- -1
                Para i <- 1 Hasta cantidad Hacer
                    Si codigos[i] = codigoBuscar O codigos[i] = "EXP-" + codigoBuscar Entonces
                        pos <- i
                    FinSi
                FinPara
                Si pos = -1 Entonces
                    Escribir "Expediente no encontrado."
                SiNo
                    Escribir "Codigo: ", codigos[pos]
                    Escribir "DNI/RUC: ", documentos[pos]
                    Escribir "Solicitante: ", solicitantes[pos]
                    Escribir "Tipo: ", tipos[pos]
                    Escribir "Asunto: ", asuntos[pos]
                    Escribir "Fecha: ", fechas[pos]
                    Escribir "Estado: ", estados[pos]
                    Escribir "Area: ", areas[pos]
                FinSi
            5:
                Si cantidad = 0 Entonces
                    Escribir "No hay expedientes registrados."
                SiNo
                    Para i <- 1 Hasta cantidad Hacer
                        Escribir codigos[i], " | ", solicitantes[i], " | ", estados[i], " | ", areas[i]
                    FinPara
                FinSi
            6:
                Si cantidad < 2 Entonces
                    Escribir "No hay suficientes expedientes para ordenar."
                SiNo
                    Para i <- 1 Hasta cantidad - 1 Hacer
                        Para j <- 1 Hasta cantidad - i Hacer
                            Si codigos[j] > codigos[j+1] Entonces
                                auxiliar <- codigos[j]
                                codigos[j] <- codigos[j+1]
                                codigos[j+1] <- auxiliar
                                auxiliar <- documentos[j]
                                documentos[j] <- documentos[j+1]
                                documentos[j+1] <- auxiliar
                                auxiliar <- solicitantes[j]
                                solicitantes[j] <- solicitantes[j+1]
                                solicitantes[j+1] <- auxiliar
                                auxiliar <- tipos[j]
                                tipos[j] <- tipos[j+1]
                                tipos[j+1] <- auxiliar
                                auxiliar <- asuntos[j]
                                asuntos[j] <- asuntos[j+1]
                                asuntos[j+1] <- auxiliar
                                auxiliar <- fechas[j]
                                fechas[j] <- fechas[j+1]
                                fechas[j+1] <- auxiliar
                                auxiliar <- estados[j]
                                estados[j] <- estados[j+1]
                                estados[j+1] <- auxiliar
                                auxiliar <- areas[j]
                                areas[j] <- areas[j+1]
                                areas[j+1] <- auxiliar
                            FinSi
                        FinPara
                    FinPara
                    Escribir "Expedientes ordenados por codigo."
                    Para i <- 1 Hasta cantidad Hacer
                        Escribir codigos[i], " | ", solicitantes[i]
                    FinPara
                FinSi
            7:
                Escribir "Confirma que desea salir? (S/N):"
                Leer confirma
                Si Mayusculas(confirma) = "S" Entonces
                    salir <- Verdadero
                FinSi
            De Otro Modo:
                Escribir "Opcion no valida. Ingrese un numero del 1 al 7."
        FinSegun
    Hasta Que salir
    Escribir "Saliendo del sistema."
FinAlgoritmo
