INSERT INTO `owners` (`id`,`username`,`password`,`firstName`,`lastName`,`email`,`dni`,`ruc`,`address`,`phoneNumber`,`role`,`status`,`createdAt`,`updatedAt`,`createdBy`,`updatedBy`) VALUES (
	'7b775e32-8b78-4352-ba0e-b13983ec69a0',
	'k4lfer',
	'$2a$12$jOvClFASO8j5krWw.bBThufnWf9s8FSL2OlatZS8ZGhCDTqAG/l2C',
	'Kaled', 
	'Dropi Inventory', 
	'LYpTtANcdrLVtWVtknc0ALjFNFP9UGlWGcO8J4BxOQk=', 
	'm4hb4F+X8tNHgrJc0nEe2A==', 
	'zRhL4jFlWiAXF1dbsdEk6w==',
	'Abancay-Apurímac',
	'cwwyWVKxtTegG1PZAWcPYg==', 
	'Admin', 1, '2024-10-17 18:17:28.000', '2024-10-17 18:17:28.000', NULL, NULL);

-- CREDENCIALS ADMIN
  -- username: k4lfer
  -- password: 1001inventory
  /*
  {

    "body": {
        "inputDto": {

        "username": "k4lfer",
        "password": "1001inventory"
        }
    }
   
  }

  */

INSERT INTO `customers` (`id`, `firstName`, `lastName`, `documentType`, `documentNumber`, `email`, `phoneNumber`, `createdAt`, `updatedAt`)
VALUES
(UUID(), 'Juan', 'Pérez', 'DNI', '87654321', 'juan@example.com', '999888777', NOW(), NOW()),
(UUID(), 'María', 'Gómez', 'DNI', '12345678', 'maria@example.com', '999888666', NOW(), NOW()),
(UUID(), 'Carlos', 'López', 'DNI', '56781234', 'carlos@example.com', '999888555', NOW(), NOW()),
(UUID(), 'Miguel', 'Campos', 'DNI', '73023676', 'miguel.campos0@example.com', '988254650', '2025-04-08 21:52:05', '2025-04-08 21:52:05'),
(UUID(), 'Luis', 'Ramírez', 'DNI', '87422428', 'luis.ramirez1@example.com', '920164215', '2025-04-08 21:52:05', '2025-04-08 21:52:05'),
(UUID(), 'Miguel', 'Castro', 'DNI', '84501956', 'miguel.castro2@example.com', '985312801', '2025-04-08 21:52:05', '2025-04-08 21:52:05');


INSERT INTO `products` (`id`, `name`, `description`, `stock`, `price`, `createdAt`, `updatedAt`) VALUES
(UUID(), 'Laptop Gamer', 'Laptop con procesador Ryzen 7 y 16GB RAM', 10, 1299.99, NOW(), NOW()),
(UUID(), 'Mouse Inalámbrico', 'Mouse óptico inalámbrico recargable', 50, 29.99, NOW(), NOW()),
(UUID(), 'Teclado Mecánico', 'Teclado mecánico RGB con switches azules', 30, 89.99, NOW(), NOW()),
(UUID(), 'Monitor 27 Pulgadas', 'Monitor IPS 144Hz con resolución 2K', 15, 249.99, NOW(), NOW()),
(UUID(), 'Disco SSD 1TB', 'Unidad de estado sólido NVMe de 1TB', 25, 119.99, NOW(), NOW()),
(UUID(), 'Silla Gamer', 'Silla ergonómica con soporte lumbar', 12, 199.99, NOW(), NOW()),
(UUID(), 'Procesador Ryzen 7', 'AMD Ryzen 7 5700X 8 núcleos 16 hilos', 20, 299.99, NOW(), NOW()),
(UUID(), 'Tarjeta Gráfica RTX 3060', 'Tarjeta gráfica NVIDIA RTX 3060 12GB', 8, 399.99, NOW(), NOW()),
(UUID(), 'Fuente de Poder 750W', 'Fuente de poder certificada 80 Plus Gold', 18, 129.99, NOW(), NOW()),
(UUID(), 'Memoria RAM 16GB', 'Memoria RAM DDR4 3200MHz', 40, 69.99, NOW(), NOW()),
(UUID(), 'Monitor UltraWide 34"', 'Monitor curvo 34" con resolución 3440x1440', 10, 499.99, NOW(), NOW()),
(UUID(), 'Teclado Compacto 60%', 'Teclado mecánico RGB formato 60% con switches rojos', 35, 79.99, NOW(), NOW()),
(UUID(), 'Auriculares Inalámbricos', 'Auriculares Bluetooth con cancelación de ruido', 20, 159.99, NOW(), NOW()),
(UUID(), 'Mousepad RGB XL', 'Mousepad extendido con iluminación RGB personalizable', 50, 39.99, NOW(), NOW()),
(UUID(), 'Gabinete ATX Gaming', 'Gabinete con panel lateral de vidrio templado y RGB', 15, 109.99, NOW(), NOW()),
(UUID(), 'Tarjeta Madre B550', 'Placa base compatible con Ryzen, chipset B550', 18, 149.99, NOW(), NOW()),
(UUID(), 'Webcam 1080p', 'Cámara web Full HD con micrófono integrado', 22, 69.99, NOW(), NOW()),
(UUID(), 'Micrófono Condensador', 'Micrófono profesional para streaming y podcast', 12, 99.99, NOW(), NOW()),
(UUID(), 'Disco HDD 2TB', 'Disco duro mecánico de 2TB para almacenamiento', 30, 79.99, NOW(), NOW()),
(UUID(), 'Controlador USB para Juegos', 'Gamepad compatible con PC y consolas', 25, 49.99, NOW(), NOW()),
(UUID(), 'Router WiFi 6', 'Router de alta velocidad con tecnología WiFi 6', 14, 199.99, NOW(), NOW()),
(UUID(), 'Laptop Ultrabook', 'Ultrabook ligera con Intel i5 y 8GB RAM', 8, 899.99, NOW(), NOW()),
(UUID(), 'Kit Ventiladores RGB', 'Pack de 3 ventiladores RGB con controlador', 40, 59.99, NOW(), NOW()),
(UUID(), 'Cargador USB-C 65W', 'Cargador rápido compatible con múltiples dispositivos', 35, 29.99, NOW(), NOW()),
(UUID(), 'Tablet Android 10"', 'Tablet de 10" con Android 12 y 128GB de almacenamiento', 12, 299.99, NOW(), NOW()),
(UUID(), 'SSD Externo 500GB', 'Unidad de estado sólido portátil de 500GB', 20, 89.99, NOW(), NOW()),
(UUID(), 'Cable HDMI 4K', 'Cable HDMI 2.1 de 2 metros compatible con 4K 120Hz', 50, 19.99, NOW(), NOW()),
(UUID(), 'Lámpara LED RGB', 'Lámpara de escritorio con iluminación RGB táctil', 30, 34.99, NOW(), NOW()),
(UUID(), 'Smartwatch Deportivo', 'Reloj inteligente con monitoreo de salud', 15, 129.99, NOW(), NOW()),
(UUID(), 'Altavoces Bluetooth', 'Parlantes portátiles con sonido envolvente', 20, 79.99, NOW(), NOW()),
(UUID(), 'Cámara de Seguridad WiFi', 'Cámara IP con visión nocturna y detección de movimiento', 18, 99.99, NOW(), NOW()),
(UUID(), 'Consola Retro', 'Consola portátil con 500 juegos clásicos', 10, 59.99, NOW(), NOW()),
(UUID(), 'Tarjeta de Captura 4K', 'Capturadora de video para streaming en 4K', 10, 229.99, NOW(), NOW()),
(UUID(), 'Hub USB 3.0', 'Concentrador USB con 4 puertos de alta velocidad', 30, 24.99, NOW(), NOW()),
(UUID(), 'Mochila para Laptop', 'Mochila resistente al agua con compartimento acolchado', 25, 49.99, NOW(), NOW()),
(UUID(), 'Cinta LED RGB', 'Tira LED RGB de 5 metros con control remoto', 40, 25.99, NOW(), NOW()),
(UUID(), 'Batería Externa 20000mAh', 'Power bank con carga rápida y doble puerto USB', 20, 44.99, NOW(), NOW()),
(UUID(), 'Estación de Carga para Móvil', 'Base de carga inalámbrica 3 en 1 para dispositivos Apple', 18, 39.99, NOW(), NOW()),
(UUID(), 'Control Remoto Universal', 'Mando a distancia programable para múltiples dispositivos', 30, 19.99, NOW(), NOW()),
(UUID(), 'Cable USB-C', 'Nuevo modelo con mejor rendimiento', 42, 436.84, '2025-04-08 21:52:05', '2025-04-08 21:52:05'),
(UUID(), 'Soporte para Laptop', 'Nuevo modelo con mejor rendimiento', 10, 843.85, '2025-04-08 21:52:05', '2025-04-08 21:52:05'),
(UUID(), 'Cable USB-C', 'Con garantía extendida', 43, 210.15, '2025-04-08 21:52:05', '2025-04-08 21:52:05'),
(UUID(), 'Parlantes Bluetooth', 'Parlantes inalámbricos con sonido estéreo', 30, 79.99, NOW(), NOW()),
(UUID(), 'Power Bank 10000mAh', 'Batería externa compacta con carga rápida', 40, 39.99, NOW(), NOW()),
(UUID(), 'Cable de Carga Rápida', 'Cable USB-A a USB-C de alta velocidad', 50, 9.99, NOW(), NOW()),
(UUID(), 'Monitor 32" Curvo', 'Monitor 4K con soporte ajustable', 15, 349.99, NOW(), NOW()),
(UUID(), 'Teclado Inalámbrico', 'Teclado compacto sin cables', 20, 59.99, NOW(), NOW()),
(UUID(), 'Disco SSD 500GB', 'Unidad de estado sólido portátil', 30, 49.99, NOW(), NOW()),
(UUID(), 'Mochila Gamer', 'Mochila de alta capacidad con compartimentos', 25, 59.99, NOW(), NOW()),
(UUID(), 'Lámpara LED Táctil', 'Lámpara LED de escritorio con control táctil', 40, 19.99, NOW(), NOW()),
(UUID(), 'Cargador Inalámbrico', 'Base de carga inalámbrica para smartphones', 20, 29.99, NOW(), NOW()),
(UUID(), 'Reloj Inteligente', 'Smartwatch con monitorización de actividad física', 15, 99.99, NOW(), NOW()),
(UUID(), 'Silla de Oficina Ergonómica', 'Silla cómoda y ajustable para oficina', 10, 129.99, NOW(), NOW()),
(UUID(), 'Router WiFi 5', 'Router con soporte para WiFi 5 de alta velocidad', 50, 69.99, NOW(), NOW()),
(UUID(), 'Cámara de Seguridad HD', 'Cámara de vigilancia con visión nocturna y audio', 12, 79.99, NOW(), NOW()),
(UUID(), 'Teclado Gaming', 'Teclado mecánico RGB para jugadores', 30, 89.99, NOW(), NOW()),
(UUID(), 'Cinta LED RGB', 'Tira LED para personalizar tu escritorio', 60, 15.99, NOW(), NOW()),
(UUID(), 'Monitor 24" Full HD', 'Monitor con resolución 1080p', 20, 139.99, NOW(), NOW()),
(UUID(), 'Auriculares con Micrófono', 'Auriculares de diadema con micrófono incorporado', 25, 49.99, NOW(), NOW()),
(UUID(), 'Estación de Carga USB', 'Estación de carga para hasta 6 dispositivos USB', 18, 89.99, NOW(), NOW()),
(UUID(), 'Gamepad para PC', 'Control USB para juegos en PC', 50, 29.99, NOW(), NOW()),
(UUID(), 'Batería Externa 20000mAh', 'Power bank con dos puertos USB y carga rápida', 15, 49.99, NOW(), NOW()),
(UUID(), 'Altavoces Portátiles', 'Altavoces Bluetooth resistentes al agua', 25, 69.99, NOW(), NOW()),
(UUID(), 'Cámara de Seguridad 4K', 'Cámara de vigilancia con grabación 4K y audio bidireccional', 8, 199.99, NOW(), NOW()),
(UUID(), 'Lámpara LED Inteligente', 'Lámpara LED controlada por app con múltiples colores', 50, 39.99, NOW(), NOW()),
(UUID(), 'Cargador Solar', 'Cargador solar portátil para dispositivos móviles', 30, 59.99, NOW(), NOW()),
(UUID(), 'Monitor 27" 144Hz', 'Monitor con panel IPS y tasa de refresco de 144Hz', 10, 329.99, NOW(), NOW()),
(UUID(), 'Teclado Mecánico RGB', 'Teclado mecánico con retroiluminación RGB y teclas programables', 20, 109.99, NOW(), NOW()),
(UUID(), 'Bocina Bluetooth 5.0', 'Bocina portátil con sonido estéreo y conectividad Bluetooth 5.0', 40, 49.99, NOW(), NOW()),
(UUID(), 'Cargador Rápido 65W', 'Cargador USB-C rápido con soporte para carga de alta velocidad', 25, 39.99, NOW(), NOW()),
(UUID(), 'Silla Gaming con Reposapiés', 'Silla de juego ergonómica con soporte lumbar y reposapiés', 10, 249.99, NOW(), NOW()),
(UUID(), 'Cable de Carga USB-C', 'Cable USB-C a USB-C de 1 metro para carga rápida', 60, 12.99, NOW(), NOW()),
(UUID(), 'Smartwatch Fitness', 'Reloj inteligente con GPS y monitorización de actividad', 18, 149.99, NOW(), NOW()),
(UUID(), 'Teclado 60% Mecánico', 'Teclado compacto 60% con switches mecánicos y retroiluminación RGB', 35, 79.99, NOW(), NOW()),
(UUID(), 'Disco HDD 4TB', 'Disco duro mecánico de 4TB para almacenamiento masivo', 12, 149.99, NOW(), NOW()),
(UUID(), 'Estación de Carga Inalámbrica', 'Base de carga inalámbrica 3 en 1 para Apple', 22, 59.99, NOW(), NOW()),
(UUID(), 'Micrófono USB para Streaming', 'Micrófono condensador USB para grabación y transmisión en vivo', 20, 129.99, NOW(), NOW()),
(UUID(), 'Controlador Bluetooth para PC', 'Gamepad inalámbrico para PC y consolas con Bluetooth', 30, 39.99, NOW(), NOW()),
(UUID(), 'Auriculares Gaming con Micrófono', 'Auriculares con cancelación de ruido y micrófono para juegos', 40, 79.99, NOW(), NOW()),
(UUID(), 'Disco SSD 2TB', 'Unidad SSD externa de 2TB de alta velocidad', 8, 249.99, NOW(), NOW()),
(UUID(), 'Monitor 34" Curvo', 'Monitor ultrawide curvo con resolución 1440p', 10, 499.99, NOW(), NOW()),
(UUID(), 'Teclado Inalámbrico Compacto', 'Teclado sin cables de perfil bajo y diseño compacto', 50, 39.99, NOW(), NOW()),
(UUID(), 'Bocina para Fiesta', 'Altavoces Bluetooth con sonido envolvente y luces LED', 25, 99.99, NOW(), NOW()),
(UUID(), 'Disco Externo 1TB', 'Disco duro portátil con 1TB de capacidad y transferencia USB 3.0', 18, 79.99, NOW(), NOW());
