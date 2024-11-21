Entidades
-Prestamo
-Plazo y Taza de Interes (TermIR)
no se ingresa ningun dato, solo se ingresan por base de datos
-Cuota
-Solicitudes
-Cliente
-Prestamos Aprobados

Un prestamo varias cuotas, una cuota un prestamo
Una solicitud varios prestamos, un prestamo una solicitud
Un prestamo solo tiene un aprobado, un aprobado un prestamo
Un cliente varios prestamos, un prestamo un cliente 

CUOTA- FEE
-Id 
-Monto/Amount
-Plazo/Months(Lista de la entidad de TermIR)
-Interes/InterestRate (Lista de la entidad de TermIR)
-Importe total/TotalAmount 
-Importe de la Cuota/FeeAmount
-Estado de la cuota

Prestamo 
-Id
-Tipo: Personal, Hipotecario
-Plazo: (fk de TermIR)
-Monto: Cantidad solicitada
-Estado de solicitud: Pendiente de aprobacion (inicial)
-Cuota asociada a ese prestamo
-Cuotas pagadas
-Cuotas debidas

Prestamos Aprobados 
-Cliente (Id y nombre fk de clientes)
-Fecha de Aprobacion 
-Monto Solicitado
-Plazo(fk de prestamo)
-Tipo de Prestamo (fk de prestamo)
-Tasa de interes (fk de cuotas)
Generar automaticamente:
-Monto total de cuota
-Monto capital correspondiente
-Monto del interes correspondiente
-Fecha de vencimiento (el dia 1 de cada mes, comenzando desde el mes siguiente a su aprobacion)
Datos extras para el detalle 
-Fecha de Aprobacion
-Ganancia Obtenida 
-Cantidad de Cuotas Pagadas
-Cantidad de cuotas pendientes
-Proxima fecha de vencimiento o un mensaje aclaratorio si ya pago todo

USUARIO
-Id
-Rol

CLIENTE/CUSTOMER
-Id 
-UsserId
-Prestamos (fk de prestamos)
-Nombre
-Apellido


# WebApi2