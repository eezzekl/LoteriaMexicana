CREATE DATABASE loteria;

use loteria;

CREATE TABLE cartas (
    id_carta INT PRIMARY KEY,
    nombre_carta VARCHAR(50),
    imagen_carta VARCHAR(100)
);

CREATE TABLE tablas (
    id_tabla INT PRIMARY KEY,
    nombre_tabla VARCHAR(50),
    creada_en DATETIME
);

CREATE TABLE cartas_tabla (
    id_carta INT,
    id_tabla INT,
    posicion INT,
    PRIMARY KEY (id_carta, id_tabla),
    FOREIGN KEY (id_carta) REFERENCES cartas(id_carta),
    FOREIGN KEY (id_tabla) REFERENCES tablas(id_tabla)
);

