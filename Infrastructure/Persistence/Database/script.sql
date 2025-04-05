CREATE TABLE `owners` (
    `id` CHAR(36) PRIMARY KEY NOT NULL,
    `username` VARCHAR(50) UNIQUE NOT NULL,
    `password` VARCHAR(255) NOT NULL,
    `firstName` VARCHAR(255) NOT NULL,
    `lastName` VARCHAR(255) NOT NULL,
    `email` VARCHAR(255) UNIQUE NOT NULL,
    `dni` VARCHAR(255) UNIQUE NOT NULL,
    `ruc` VARCHAR(255) UNIQUE,
    `address` VARCHAR(100) NOT NULL,
    `phoneNumber` VARCHAR(30) UNIQUE NOT NULL,
    `role` ENUM('Manager', 'Admin') NOT NULL,
    `status` TINYINT(1) NOT NULL,
   -- `profilePictureUrl` TEXT NULL,
    `createdAt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updatedAt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `createdBy` CHAR(36),
    `updatedBy` CHAR(36)
) ENGINE=InnoDB;

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
        "dto": {

        "username": "k4lfer",
        "password": "1001inventory"
        }
    }
  eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI3Yjc3NWUzMi04Yjc4LTQzNTItYmEwZS1iMTM5ODNlYzY5YTAiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE3NDM4MTM1MDUsImV4cCI6MTc0MzgxNDQwNSwiaWF0IjoxNzQzODEzNTA1LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUyNjYiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjMwMDAifQ.zv_INcKOJrycX--_meVZ4rKWA1hdqKqUJKYdIzuX7q0
      
  }

  {
  "createDto": {
    "username": "k4lfer",
    "password": "1001inventory"
  }
}
  */


CREATE TABLE `customers` (
    `id` CHAR(36) PRIMARY KEY NOT NULL,
    `firstName` VARCHAR(100) NOT NULL,
    `lastName` VARCHAR(100) NOT NULL,
    `documentType` VARCHAR(30),
    `documentNumber` VARCHAR(30) UNIQUE NOT NULL,
    `email` VARCHAR(100) UNIQUE NOT NULL,
    `phoneNumber` VARCHAR(50) NULL,

    `createdAt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updatedAt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

INSERT INTO `customers` (`id`, `firstName`, `lastName`, `documentType`, `documentNumber`, `email`, `phoneNumber`, `createdAt`, `updatedAt`)
VALUES
('1a2b3c4d-0004-0000-0000-000000000001', 'Juan', 'Pérez', 'DNI', '87654321', 'juan@example.com', '999888777', NOW(), NOW()),
('1a2b3c4d-0004-0000-0000-000000000002', 'María', 'Gómez', 'DNI', '12345678', 'maria@example.com', '999888666', NOW(), NOW()),
('1a2b3c4d-0004-0000-0000-000000000003', 'Carlos', 'López', 'DNI', '56781234', 'carlos@example.com', '999888555', NOW(), NOW());


CREATE TABLE `products` (
    `id` CHAR(36) PRIMARY KEY NOT NULL,
    `name` VARCHAR(100) NOT NULL,
    `description` TEXT,
    `stock` INT NOT NULL,
    `price` DECIMAL(10,2) NOT NULL,

    `createdAt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updatedAt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

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
(UUID(), 'Control Remoto Universal', 'Mando a distancia programable para múltiples dispositivos', 30, 19.99, NOW(), NOW());


CREATE TABLE `orders` (
    `id` CHAR(36) PRIMARY KEY NOT NULL,
    `customerId` CHAR(36) NOT NULL,
    `totalPrice` DECIMAL(10,2),
    `status` ENUM('Pending', 'Processing','Completed', 'Cancelled') NOT NULL,
    
    `createdAt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `updatedAt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (`customerId`) REFERENCES `customers`(`id`) ON DELETE CASCADE,
) ENGINE=InnoDB;

CREATE TABLE `order_details` (
    `id` CHAR(36) PRIMARY KEY NOT NULL,
    `orderId` CHAR(36) NOT NULL,
    `productId` CHAR(36) NOT NULL,
    `quantity` INT NOT NULL,
    `unitPrice` DECIMAL(10,2) NOT NULL,
    `subtotal` DECIMAL(10,2) NOT NULL,

    FOREIGN KEY (`orderId`) REFERENCES `orders`(`id`) ON DELETE CASCADE,
    FOREIGN KEY (`productId`) REFERENCES `products`(`id`) ON DELETE CASCADE
) ENGINE=InnoDB;