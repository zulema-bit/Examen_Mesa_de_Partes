Algoritmo SistemaMesaDePartes
    // Prototipo de simulacion en PSeInt. El guardado en archivo se implementa en C#.
    Definir entrada, documento, nombre, tipo, asunto, ruta, codigoBuscar, confirma Como Caracter
    Definir tramite, categoria, area Como Caracter
    Definir codigos, documentos, solicitantes, tipos, asuntos, rutas, fechas, estados, areas, tramites, categorias Como Caracter
    Definir cantidad, siguiente, seleccion, i, j, k, pos Como Entero
    Definir valido, salir, intercambiar Como Logico
    Definir auxiliar Como Caracter
    Dimension codigos[100], documentos[100], solicitantes[100], tipos[100]
    Dimension asuntos[100], rutas[100], fechas[100], estados[100], areas[100]
    Dimension tramites[100], categorias[100]
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
                    Repetir
                        Escribir "Ruta del archivo PDF o imagen (simulacion):"
                        Leer ruta
                    Hasta Que Longitud(ruta) > 0
                    Escribir "Confirma el registro? (S/N):"
                    Leer confirma
                    Si Mayusculas(confirma) = "S" Entonces
                        pos <- cantidad + 1
                        codigos[pos] <- "EXP-" + ConvertirATexto(siguiente)
                        documentos[pos] <- documento
                        solicitantes[pos] <- nombre
                        tipos[pos] <- tipo
                        asuntos[pos] <- asunto
                        rutas[pos] <- ruta
                        Escribir "Fecha de registro (DD/MM/AAAA):"
                        Leer fechas[pos]
                        estados[pos] <- "Registrado"
                        areas[pos] <- "Sin derivar"
                        tramites[pos] <- ""
                        categorias[pos] <- ""
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
                codigoBuscar <- Mayusculas(codigoBuscar)
                pos <- -1
                Para i <- 1 Hasta cantidad Hacer
                    Si codigos[i] = codigoBuscar O codigos[i] = "EXP-" + codigoBuscar Entonces
                        pos <- i
                    FinSi
                FinPara
                Si pos = -1 Entonces
                    Escribir "Expediente no encontrado."
                SiNo
                    Escribir "Archivo asociado: ", rutas[pos]
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
                Escribir "Codigo del expediente a clasificar:"
                Leer codigoBuscar
                codigoBuscar <- Mayusculas(codigoBuscar)
                pos <- -1

                Para i <- 1 Hasta cantidad Hacer
                    Si codigos[i] = codigoBuscar O codigos[i] = "EXP-" + codigoBuscar Entonces
                        pos <- i
                    FinSi
                FinPara

                Si pos = -1 Entonces
                    Escribir "Expediente no encontrado."
                SiNo
                    Escribir "===== SELECCIONE EL ASUNTO ====="
                    Escribir "1. Reparacion de pistas y veredas"
                    Escribir "2. Licencia de edificacion"
                    Escribir "3. Habilitacion urbana"
                    Escribir "4. Zonificacion o catastro"
                    Escribir "5. Impuesto predial"
                    Escribir "6. Limpieza publica"
                    Escribir "7. Mantenimiento de parques y jardines"
                    Escribir "8. Licencia de funcionamiento de negocio"
                    Escribir "9. Senalizacion vial"
                    Escribir "10. Escribir asunto manualmente"
                    Escribir "Seleccione una opcion (1-10):"
                    Leer entrada

                    valido <- Verdadero
                    asunto <- ""
                    tramite <- ""
                    categoria <- ""
                    area <- ""


                    Segun entrada Hacer
                        "1":
                            asunto <- "Reparacion de pistas y veredas"
                            tramite <- "Obras y pistas"
                            categoria <- "Obras publicas"
                            area <- "Gerencia de Desarrollo Urbano"
                        "2":
                            asunto <- "Licencia de edificacion"
                            tramite <- "Licencias de edificacion"
                            categoria <- "Licencias y edificaciones"
                            area <- "Subgerencia de Obras Privadas"
                        "3":
                            asunto <- "Habilitacion urbana"
                            tramite <- "Habilitacion urbana"
                            categoria <- "Habilitacion urbana"
                            area <- "Subgerencia de Planeamiento Urbano"
                        "4":
                            asunto <- "Zonificacion o catastro"
                            tramite <- "Zonificacion y catastro"
                            categoria <- "Zonificacion y catastro"
                            area <- "Subgerencia de Catastro"
                        "5":
                            asunto <- "Impuesto predial"
                            tramite <- "Asuntos tributarios"
                            categoria <- "Asuntos tributarios"
                            area <- "Gerencia de Administracion Tributaria"
                        "6":
                            asunto <- "Limpieza publica"
                            tramite <- "Limpieza publica"
                            categoria <- "Limpieza publica"
                            area <- "Gerencia de Servicios Publicos"
                        "7":
                            asunto <- "Mantenimiento de parques y jardines"
                            tramite <- "Parques y jardines"
                            categoria <- "Parques y jardines"
                            area <- "Gerencia de Servicios Publicos"
                        "8":
                            asunto <- "Licencia de funcionamiento de negocio"
                            tramite <- "Formalizacion de negocios"
                            categoria <- "Formalizacion de negocios"
                            area <- "Subgerencia de Comercializacion"
                        "9":
                            asunto <- "Senalizacion vial"
                            tramite <- "Transito y senalizacion"
                            categoria <- "Transito y senalizacion vial"
                            area <- "Gerencia de Transporte"
                        "10":
                            Escribir "Escriba el asunto:"
                            Leer asunto
                            Escribir "Escriba el tipo de tramite:"
                            Leer tramite

                            Si asunto = "" O tramite = "" Entonces
                                valido <- Falso
                            SiNo
                                categoria <- "Pendiente de clasificacion"
                                area <- "Mesa de Partes"
                                estados[pos] <- "Pendiente de evaluacion y derivacion"
                            FinSi
                        De Otro Modo:
                            valido <- Falso
                    FinSegun

                  Si valido Entonces
                        Si entrada <> "10" Entonces
                            estados[pos] <- "En proceso"
                        FinSi

                        asuntos[pos] <- asunto
                        tramites[pos] <- tramite
                        categorias[pos] <- categoria
                        areas[pos] <- area

                        Escribir "Expediente clasificado correctamente."
                        Escribir "Asunto: ", asuntos[pos]
                        Escribir "Tramite: ", tramites[pos]
                        Escribir "Categoria: ", categorias[pos]
                        Escribir "Area responsable: ", areas[pos]
                        Escribir "Estado: ", estados[pos]
                    SiNo
                        Escribir "Error: opcion o datos no validos."
                    FinSi
                FinSi


            4:
                Escribir "Codigo del expediente a consultar:"
                Leer codigoBuscar
                codigoBuscar <- Mayusculas(codigoBuscar)
                pos <- -1

                Para i <- 1 Hasta cantidad Hacer
                    Si codigos[i] = codigoBuscar O codigos[i] = "EXP-" + codigoBuscar Entonces
                        pos <- i
                    FinSi
                FinPara

                Si pos = -1 Entonces
                    Escribir "Expediente no encontrado."
                SiNo
                    Escribir "DNI/RUC del solicitante:"
                    Leer documento

                    Si documentos[pos] <> documento Entonces
                        Escribir "DNI/RUC no corresponde al expediente."
                    SiNo
                        Escribir "===== TIPO DE CONSULTA ====="
                        Escribir "1. Consultar estado"
                        Escribir "2. Consultar area responsable"
                        Escribir "3. Consultar historial"
                        Escribir "4. Mostrar datos completos"
                        Escribir "Seleccione una opcion:"
                        Leer entrada

                        Segun entrada Hacer
                            "1":
                                Escribir "Codigo: ", codigos[pos]
                                Escribir "Estado: ", estados[pos]
                            "2":
                                Escribir "Codigo: ", codigos[pos]
                                Escribir "Area responsable: ", areas[pos]
                            "3":
                                Escribir "El historial detallado se implementa en C#."
                            "4":
                                Escribir "Codigo: ", codigos[pos]
                                Escribir "DNI/RUC: ", documentos[pos]
                                Escribir "Solicitante: ", solicitantes[pos]
                                Escribir "Tipo de documento: ", tipos[pos]
                                Escribir "Asunto: ", asuntos[pos]
                                Escribir "Tipo de tramite: ", tramites[pos]
                                Escribir "Categoria: ", categorias[pos]
                                Escribir "Fecha de registro: ", fechas[pos]
                                Escribir "Estado: ", estados[pos]
                                Escribir "Area responsable: ", areas[pos]
                                Escribir "Archivo asociado: ", rutas[pos]
                            De Otro Modo:
                                Escribir "Opcion no valida."
                        FinSegun
                    FinSi
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
                    Escribir "Ordenar por codigo: 1. Ascendente; 2. Descendente"
                    Leer entrada
                    Si entrada = "1" O entrada = "2" Entonces
                      Para i <- 1 Hasta cantidad - 1 Hacer
                        Para j <- 1 Hasta cantidad - i Hacer
                            intercambiar <- Falso
                            Si entrada = "1" Y codigos[j] > codigos[j+1] Entonces
                                intercambiar <- Verdadero
                            FinSi
                            Si entrada = "2" Y codigos[j] < codigos[j+1] Entonces
                                intercambiar <- Verdadero
                            FinSi
                            Si intercambiar Entonces
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
                                auxiliar <- rutas[j]
                                rutas[j] <- rutas[j+1]
                                rutas[j+1] <- auxiliar
                                auxiliar <- fechas[j]
                                fechas[j] <- fechas[j+1]
                                fechas[j+1] <- auxiliar
                                auxiliar <- estados[j]
                                estados[j] <- estados[j+1]
                                estados[j+1] <- auxiliar
                                auxiliar <- areas[j]
                                areas[j] <- areas[j+1]
                                areas[j+1] <- auxiliar
                                auxiliar <- tramites[j]
                                tramites[j] <- tramites[j+1]
                                tramites[j+1] <- auxiliar
                                auxiliar <- categorias[j]
                                categorias[j] <- categorias[j+1]
                                categorias[j+1] <- auxiliar
                            FinSi
                        FinPara
                      FinPara
                      Escribir "Expedientes ordenados por codigo."
                      Para i <- 1 Hasta cantidad Hacer
                          Escribir codigos[i], " | ", solicitantes[i]
                      FinPara
                    SiNo
                      Escribir "Opcion no valida."
                    FinSi
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
