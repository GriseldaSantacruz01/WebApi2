
## Instalación 
### 1. Clonar Repositorio

```sh
git clone https://github.com/GriseldaSantacruz01/WebApi2 cd WebApi2
```
### 2. Crear contenedor de Docker
```ps1
docker container run ^
-dp 5435:5432 ^
--name posgres-db ^
-e POSTGRES_PASSWORD=123456 ^
-v postgres-db:/var/lib/postgresql/data ^
postgres:15.1
```
### 3. Actualizar la Base de Datos
```sh
dotnet ef database update -p Infrastructure -s WebApi2
```
## Requerimientos del Sistema
### 1. Creación de la Entidad "Plazo y Tasa de Interés".
### 2. Simulador de Cuota.
`[POST]` `/Installment/api/Simulator`
Recibe `Months: int, Amount: Decimal
**Descripción:** 
-Months: es el plazo total del préstamo al que le esta simulando la creación de cuota
-Amount: el monto solicitado del préstamo
Simula la creación de una cuota dado los datos solicitados, realiza el calculo necesario y devuelve el monto a pagar del préstamo con el interés asociado al plazo que eligió.

**Devuelve:

```json
{
  "installmentAmount": 204514,
  "totalAmount": 2454168
}
```

### 3. Solicitud de Préstamo.
`[POST]` `/LoanRequest/api/CreateLoanRequest`
Recibe `customerId: int, amount: decimal, months: int, type: string`
**Descripción:**
Crea una solicitud de préstamo de acuerdo a un id de Cliente que automáticamente se genera con la propiedad de estado en pendiente, dicho estado esta sujeto a cambios.
**Devuelve:**

```
La solicitud de préstamo está siendo procesada. El Id de la solicitud es 1
```
El 1 representaría el id de la solicitud del préstamo.

### 4. Aprobación/Rechazo de Solicitudes.
- *Rechazo*
`[POST]` `/LoanRequest/api/Rejected/{loanId}`
Recibe `loanId: int, reason: string`
**Descripción:**
Selecciona la solicitud con el loanId, que es el id de la solicitud hecha en el punto anterior y solicita un motivo de rechazo de manera obligatoria. Actualiza el estado de solicitud a rechazado y guarda el motivo de rechazo en MotivoRechazo en la tabla de Solicitud de Préstamo.
**Devuelve:**

```
La solicitud fue rechaza por el siguiente motivo: Cliente sin credito
```
El motivo del rechazo ingresado al realizar el rechazo es lo que se muestra.

- *Aprobación*
`[POST]` `/LoanRequest/api/Approval/{loanId}`
Recibe `loanId: int`
**Descripción:**
Selecciona la solicitud con el id de solicitud ingresado, para luego general automáticamente un registro en la tabla de Prestamos aprobados, mientras que también crea las cuotas asociadas al id del registro del préstamo aprobado, guarda las cuotas en una tabla Cuotas. 
**Devuelve:**
```
La solicitud fue aprobada correctamente. El id del prestamo aprobado es: 1
```
Devuelve el id del préstamo aprobado que es generado automáticamente.

### 5. Consulta de Detalles de un Préstamo Aprobado.
`[GET]` `ApprovedLoan/api/GetApprovedLoanDetails/{approvedLoanId}`
Recibe: `approvedLoanId: int`
**Descripción:**
Selecciona el presamo aprovado de acuerdo al id de un prestamo aprobado. Consulta a las tablas necesarias y trae un registro detallado de un préstamo aprobado.
**Devuelve:**
```json
{
  "customerId": 2,
  "customerName": "Alphonso Bahde",
  "amount": 6000000,
  "totalAmount": 6217079,
  "profit": 217079,
  "pendingAmount": 4144719,
  "months": 6,
  "type": "Hipotecario",
  "interestRate": 12.3,
  "paidInstallments": 2,
  "pendingInstallments": 4,
  "nextDueDate": "2025-02-01"
}
```
Donde `customerName` es extraido de otra tabla apartir de `customerId`.  `amount`, `months`, `type` y `customerId` son los datos ingresados al crear la solicitud de prestamo, `interestRate` es traido de acuerdo al  `months`.  `totalAmount` es la cantidad total a pagar con el interes total, `profit` es la ganancia obtenida, `pendingAmount` es el monto debido del cliente, se inicializa igual al `totalAmount`, el cual disminuye cuando se paga alguna cuota, `paidInstallments` es la cantidad de cuotas pagadas, `pendingInstallments` las cuotas pendientes del cliente y `nextDueDate` es la fecha del vencimiento segun la fecha de vencimiento de la ultima cuota pagada.
### 6. Pago de cuotas.
`[POST]` `PaymentInstallments/api/Pay`
Recibe: `approvedLoanId: in, installmentIds: int[]`
**Descripción:**
De acuerdo al id de un prestamo aprobado y los ids de las cuotas que desea pagar de ese prestamo, realiza un registro de las cuotas pagadas y descontando el valor de las cuotas pagadas en el pendingAmount de los prestamos aprobados, a su vez aumenta las cuotas pagadas y disminuye las cuotas pendientes de prestamos aprobados.
**Devuelve:**
```
1 cuota(s) pagada(s) exitosamente.
```

El valor 1 depende de la cantidad de cuotas ingresadas, siendo el ejemplo que solo desea pagar una cuota de cierto prestamo aprobado.
### 7.  Listado de Cuotas.
`[GET]` `/Installment/api/GetInstallmentsByStatus/{approvedLoanId}`
Recibe: `approvedLoanId: int, filter: string`
**Descripción:**
De acuerdo al id del prestamo aprobado, filtra las cuotas, siendo los filtros disponibles all; es decir todas las cuotas de ese prestamo, paid; todas las cuotas pagadas de ese prestamo, unpaid: todas las cuotas pendientes de ese prestamo.
**Devuelve:**
```json
[
  {
    "installmentId": 7,
    "installmentTotal": 342936,
    "installmentStatus": "Pagada",
    "dueDate": "2024-12-01T03:00:00Z",
    "paymentDate": "2024-11-27T17:06:13.286726Z"
  },
  {
    "installmentId": 8,
    "installmentTotal": 342936,
    "installmentStatus": "Pagada",
    "dueDate": "2025-01-01T03:00:00Z",
    "paymentDate": "2024-11-27T17:06:13.314581Z"
  },
  {
    "installmentId": 9,
    "installmentTotal": 342936,
    "installmentStatus": "Pendiente",
    "dueDate": "2025-02-01T03:00:00Z",
    "paymentDate": null
  }
]
```

### 8. Listado de Cuotas Atrasadas.
`[GET]` `/Installment/api/GetDelayedInstallments`
No recibe ningun parametro.
**Descripción:**
Trae todas cuotas en las que la fecha actual pase sus fechas de vencimiento.
**Devuelve:**

```json
[
  {
    "installmentId": 3,
    "approvedLoanId": 1,
    "customerId": 2,
    "customerName": "Alphonso Bahde",
    "dueDate": "2025-02-01T03:00:00Z",
    "daysDelayed": 207,
    "pendingAmount": 4144719
  },
  {
    "installmentId": 4,
    "approvedLoanId": 1,
    "customerId": 2,
    "customerName": "Alphonso Bahde",
    "dueDate": "2025-03-01T03:00:00Z",
    "daysDelayed": 179,
    "pendingAmount": 4144719
  }
]
```
Calcula los días retrasados y muestra al cliente asociado, también muestra la deuda acumulada.
### Aclaración.
Los puntos 4 y 5 solo pueden ser accedidos por usuarios de rol admin por medio de un token generado.

























