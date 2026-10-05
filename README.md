# 🃏 Sharpatro

**Sharpatro** es una implementación orientada a objetos en **C#** inspirada en las mecánicas de *Balatro*. El proyecto combina la evaluación de manos de póker tradicionales con un bucle de juego basado en la acumulación de Fichas (*Chips*) y Multiplicadores (*Mult*) para superar objetivos de puntuación (*Ciegas*).

Diseñado con un enfoque modular y arquitectura limpia en consola, el objetivo principal del proyecto es aplicar buenas prácticas de diseño en C#, manipulación de colecciones con LINQ y gestión de estado.

---

## 🚀 Características Principales

- **Mazo completo de 52 cartas:** Generación, barajado dinámico y robo mediante `Deck.cs`.
- **Gestión de mano:** Límite configurable de cartas en mano, adición y descarte de naipes con `Hand.cs`.
- **Evaluador de Póker robusto:** Identificación automática de las 9 combinaciones estándar (desde *Carta Alta* hasta *Escalera de Color*), incluyendo la regla especial del As bajo ($A-2-3-4-5$).
- **Cálculo de puntuación:** Suma de Fichas base + Fichas individuales de cada carta jugada, escaladas por el Multiplicador de la jugada.
- **Bucle de juego (`GameEngine`):** Control interactivo por terminal para seleccionar cartas a jugar o descartar, límite de manos/descartes por ronda y gestión de la ciega objetivo.

---

## ⚙️ Arquitectura del Proyecto

El código está estructurado de manera modular dentro del espacio de nombres `Sharpatro`:

| Clase / Elemento | Descripción |
| :--- | :--- |
| **`Card`** | Representa una carta individual con su Palo (`Suit`), Rango (`Rank`) y Fichas base. |
| **`Deck`** | Administra la colección de 52 cartas, el barajado (algoritmo Fisher-Yates) y el reinicio de ronda. |
| **`Hand`** | Colección dinámica que gestiona las cartas disponibles para el jugador en la ronda. |
| **`PokerHandEvaluator`** | Clase estática con la lógica necesaria para clasificar jugadas usando LINQ y calcular la puntuación final (`HandScore`). |
| **`GameEngine`** | Orquestador principal del estado de la partida, interfaz por consola y flujo de victoria/derrota. |
| **`Program`** | Punto de entrada del programa. Configura la consola e inicia la ejecución de la partida. |

---

## 🛠️ Requisitos e Instalación

### Requisitos previos
- [.NET SDK 8.0](https://dotnet.microsoft.com/) o superior.
- Editor de código recomendado: [VS Code](https://code.visualstudio.com/) o Visual Studio.