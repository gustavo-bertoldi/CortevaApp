CREATE TABLE machine_component (
    [id] int primary key,
    [name] varchar(50) not null,
    [machineName] varchar(100) not null,
    [other_machine] int not null
);

INSERT INTO [machine_component] VALUES
(1, 'downstreamSaturation', 'filler', 1),
(2, 'dosingTurret', 'filler', 0),
(3, 'bowlStopper', 'filler', 0),
(4, 'screwingTurret', 'filler', 0),
(5, 'missingBottle', 'filler', 1),
(6, 'other', 'filler', 0);
