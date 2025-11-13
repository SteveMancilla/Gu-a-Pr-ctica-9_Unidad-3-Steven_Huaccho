# 🧠 La Chispa de Vida  
### *Dev Kata — El Desafío del Estudio*

Bienvenido al repositorio del proyecto desarrollado para la **Guía Práctica #12** del curso *Desarrollo de Videojuegos*.  
Aquí implementamos una IA basada en el **Patrón State**, un sistema de aturdimiento (*StunState*) y un arma láser interactiva.

---

## 🎮 Nombre del Estudio:  
**CodeSquad**

---

## 👥 Miembros y Roles  

| Miembro | Rol | Descripción |
|--------|------|-------------|
| **Steven Huaccho Mancilla** | Arquitecto de IA | Implementación del Patrón State, creación del *StunState*, lógica del AIController. |
| **Andrea Rojas Mellado** | Diseñadora de Comportamiento | Configuración del enemigo, Waypoints, valores de búsqueda, velocidad y ajustes de *stunDuration*. |

---

## 🧩 Descripción del Hito  
Este hito consistió en implementar una IA completa utilizando el **Patrón State**, integrarla al entorno del jugador y añadir un sistema de aturdimiento mediante un arma láser. En esta práctica implementamos un sistema de Inteligencia Artificial completo basado en el Patrón de Diseño *State*. La IA del enemigo ahora cuenta con tres comportamientos principales: patrullar una ruta establecida, detectar y perseguir al jugador, y un nuevo estado de aturdimiento (*StunState*) que detiene temporalmente al agente mediante un arma láser creada por el equipo. Este hito integró programación y configuración en Unity para lograr una IA funcional.

Se desarrolló lo siguiente:

- ✔️ **Patrulla automática** usando `NavMeshAgent`.  
- ✔️ **Estados IA:** `PatrolState`, `ChaseState`, `StunState`.  
- ✔️ **Sistema de aturdimiento:** La IA se detiene durante un tiempo configurable.  
- ✔️ **Láser interactivo:** Implementado con **Raycast + LineRenderer**.  
- ✔️ **Configuración del diseñador:** Ajuste de radios, velocidades, rutas y temporizadores.  
- ✔️ **Integración y pruebas:** Colocación de prefabs, control de escenas y revisión en equipo.

---

## 💭 Reflexión del Estudio

### **1. Sinergia y Fricción: ¿Cuál fue el mayor beneficio de trabajar en equipo para esta tarea? ¿Y cuál fue el mayor desafío de comunicación o coordinación que enfrentaron y cómo lo resolvieron?**  
El mayor beneficio de trabajar en equipo fue la **división clara de roles**: mientras uno programaba la lógica interna, el otro configuraba y probaba en Unity.  
El principal desafío fue **mantener sincronía** entre scripts, escena y valores expuestos, ya que cualquier desajuste rompía el comportamiento del enemigo.  
La solución fue realizar **commits frecuentes**, comunicación constante y pruebas en conjunto.

### **2. El Alma de la Máquina: Más allá del código, ¿qué parámetro ([SerializeField]) descubrieron que tenía el mayor impacto en hacer que la IA se sintiera más "viva" o "inteligente"? 
**(Ejemplo: detectionRadius, chaseSpeed, la diferencia entre detectionRadius y loseSightRadius, etc.)** 

El parámetro que tuvo el mayor impacto en que la IA se sintiera realmente “viva” fue la combinación entre los radios de detección (`detectionRadius` y `loseSightRadius`) y 
las velocidades diferenciadas entre patrulla y persecución (`patrolSpeed` y `chaseSpeed`), ya que estas variables definieron con precisión cuándo el enemigo debía reaccionar al jugador y 
cuánta agresividad mostrar al entrar en modo de persecución. Además, la incorporación de `stunDuration` dentro del nuevo StunState añadió un toque de vulnerabilidad que hizo que la IA 
dejara de sentirse como un objeto automático: ahora puede ser contenida temporalmente con el láser, lo que crea una sensación de interacción dinámica. 
El ajuste cuidadoso de estos valores permitió que la IA patrullara de manera natural, respondiera rápidamente cuando el jugador entraba en su rango visual y se recuperara tras ser aturdida, 
logrando un comportamiento más realista, reactivo y coherente con el entorno del juego.

En general el ba;lance estuve entre estos elementos:

- `detectionRadius`  
- `chaseSpeed`  
- `loseSightRadius`  
- `stunDuration`  

Estos valores hicieron que la IA se comportara de manera más natural: detecta al jugador, lo persigue con agresividad, pero muestra vulnerabilidad al ser aturdida por el láser.

---

## 🛠️ Tecnologías Utilizadas  
- Unity 6000.2f1 (URP)  
- C#  
- NavMesh Agent  
- LineRenderer / Raycast  
- Git & GitHub

---

## 📂 Repositorio del Proyecto  
🔗 *https://github.com/SteveMancilla/Gu-a-Pr-ctica-9_Unidad-3-Steven_Huaccho.git*

### Commits relevantes:
- `HuacchoSteven – RojasAndrea: Dev Kata: El Desafío del Estudio`  
- `Material de enemy`  
- `Parte 2 – La Arquitectura del cerebro / Parte 3 – El alma de la máquina`  
- `Arreglo de escena`

---

## 🌟 Cierre  
Este proyecto representó un paso importante hacia la comprensión de IA modular, colaboración activa y el uso de patrones de diseño esenciales en el desarrollo de videojuegos.
