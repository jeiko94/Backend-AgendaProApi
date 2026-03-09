## Agendamiento de citas

Aquí ya empiezas el flujo principal de negocio.

### Entidad:

Citas

### Qué construir:

crear cita

listar citas por usuario

listar citas por fecha

listar citas por especialista

cancelar cita

consultar detalle de cita

## 📊 Modelo de Base de Datos (DER)

### 🔹 Rol
- **IdRol** (PK)
- nombre (varchar)

### 🔹 Usuario
- **idUsuario** (PK)
- idRol (FK → Rol.IdRol)
- nombre (varchar)
- email (varchar) [Único]
- passwordHash (varchar)
- fechaCreacion (datetime)
- estado (bit)

### 🔹 Especialista
- **idEspecialista** (PK)
- nombre (varchar)
- especialidad (varchar)
- email (varchar) [Único]
- estado (bit)

### 🔹 Horarios
- **idHorarios** (PK)
- idEspecialista (FK → Especialista.idEspecialista)
- diaSemana (int)
- horaInicio (time)
- horaFin (time)
- estado (bit)

### 🔹 BloquesHorario
- **idBloqueHorario** (PK)
- idHorarios (FK → Horarios.idHorarios)
- horaInicio (time)
- horaFin (time)
- disponibilidad (bit)

### 🔹 Citas
- **idCitas** (PK)
- idUsuario (FK → Usuario.idUsuario)
- idBloqueHorario (FK → BloquesHorario.idBloqueHorario)
- fecha (date)
- motivo (varchar)
- estado (bit)
- fechaCreacion (datetime)

---

## 🔗 Relaciones

- Rol (1) → (N) Usuario
- Usuario (1) → (N) Citas
- Especialista (1) → (N) Horarios
- Horarios (1) → (N) BloquesHorario
- BloquesHorario (1) → (N) Citas

------------------------------------------------------------------------------------------------------------------

Rol (1) ──── (N) Usuario
Usuario (1) ──── (N) Citas
Especialista (1) ──── (N) Horarios
Horarios (1) ──── (N) BloquesHorario
BloquesHorario (1) ──── (N) Citas

------------------------------------------------------------------------------------------------------------------
erDiagram
    ROL ||--o{ USUARIO : tiene
    USUARIO ||--o{ CITAS : agenda
    ESPECIALISTA ||--o{ HORARIOS : define
    HORARIOS ||--o{ BLOQUES_HORARIO : genera
    BLOQUES_HORARIO ||--o{ CITAS : reserva

------------------------------------------------------------------------------------------------------------------
