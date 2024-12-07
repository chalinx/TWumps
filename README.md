# Wunpus

Wunpus es un juego basado en el clásico "Hunt the Wumpus", desarrollado en C# utilizando el entorno de Visual Studio.

## Requisitos del sistema

Antes de instalar y ejecutar el proyecto, asegúrate de cumplir con los siguientes requisitos:

- **Sistema Operativo**: Windows (recomendado).
- **.NET Framework**: Versión 4.7.2 o superior.
- **Visual Studio**: Versión 2019 o superior, con soporte para aplicaciones de consola en C#.
- **Git**: Para clonar el repositorio (opcional).

## Instalación y Ejecución directa

Sigue estos pasos para instalar y ejecutar Wunpus en tu máquina local:

1. **Clonar el repositorio**

   Si tienes instalado Git, abre una terminal o línea de comandos y ejecuta:

   ```bash
   git clone https://github.com/chalinx/TWumps.git

2. **Ejecutar directamente con el archivo precompilado**

Si deseas acceder rápidamente al juego sin necesidad de compilar el código, usa el archivo ejecutor incluido:

1. Abre la carpeta del proyecto y localiza el archivo:
2. Haz doble clic en este archivo para iniciar el juego directamente desde tu sistema.


## Uso de AOP (Programación Orientada a Aspectos)

En este proyecto, se implementó **Programación Orientada a Aspectos (AOP)** para modularizar características transversales del juego. Los aspectos destacados incluyen:

- **Registro y monitoreo**: Se usa AOP para registrar eventos importantes del juego, como colisiones, interacciones con agentes y actualizaciones de puntuación.
- **Validación de condiciones**: AOP gestiona las verificaciones previas, como comprobar si el personaje puede recoger oro o flechas.
- **Decoradores**: Se usaron decoradores para agregar dinámicamente comportamientos adicionales a los agentes, como resaltar visualmente ciertos elementos o mostrar iconos especiales.

