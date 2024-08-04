#Bank Services API - Documentación Técnica
Resumen del Proyecto
El proyecto Bank Services API se encarga de manejar la lógica de negocio para tarjetas de crédito y estados de cuenta, siguiendo una arquitectura modular y escalable basada en principios de desarrollo ágil y limpio.

Arquitectura Onion
La arquitectura Onion se implementa para asegurar una separación clara de las preocupaciones y la flexibilidad en la evolución del software. Las capas principales son:

Capa de Dominio:

Contiene las entidades fundamentales del negocio y las interfaces de repositorios.
Representa el núcleo de la aplicación, sin depender de ninguna tecnología externa.
Capa de Aplicación:

Gestiona la lógica de negocio específica, como la manipulación de datos y las reglas de negocio.
Implementa patrones como CQRS, separando comandos y consultas.
Capa de Infraestructura:

Provee implementaciones para interfaces de repositorios, utilizando tecnologías como Dapper para el acceso a datos.
Incluye servicios externos, manejadores de eventos, y otros elementos de infraestructura.
Capa de Presentación:

Expone la API a través de controladores y endpoints, proporcionando interfaces para la interacción con los clientes.
CQRS
El patrón CQRS se utiliza para separar claramente las operaciones de lectura y escritura:

Comandos: Encapsulan las operaciones que modifican el estado de la aplicación, asegurando un flujo claro y separado de las mutaciones de estado.
Consultas: Se encargan de obtener datos sin modificar el estado, optimizando la eficiencia y simplicidad del sistema.
Mapper
Se utiliza AutoMapper para transformar objetos de dominio a DTOs (Data Transfer Objects) y viceversa. Esto facilita la separación de las capas y asegura que las modificaciones en el dominio no afecten directamente a las capas externas.

Dapper
Dapper se implementa como micro ORM para realizar operaciones de acceso a datos de manera eficiente. Su uso es clave para mantener un acceso rápido y ligero a la base de datos, ejecutando consultas SQL directamente.

Buenas Prácticas
Inyección de Dependencias: Se utiliza un contenedor IoC para gestionar las dependencias, promoviendo el desacoplamiento y la facilidad de pruebas.
Separación de Responsabilidades: Cada capa y componente tiene una responsabilidad clara, siguiendo el principio de Single Responsibility.
Principio DRY (Don't Repeat Yourself): Se busca la reutilización de código, evitando la duplicación innecesaria y facilitando el mantenimiento del sistema.
