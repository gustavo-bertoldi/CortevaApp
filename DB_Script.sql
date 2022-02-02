CREATE TABLE [machine_component] (
    [id] int identity(1,1) primary key,
    [name] nvarchar(50) not null,
    [machineName] nvarchar(100) not null,
    [other_machine] int not null
);

INSERT INTO [machine_component] VALUES
(1, 'downstreamSaturation', 'filler', 1),
(2, 'dosingTurret', 'filler', 0),
(3, 'bowlStopper', 'filler', 0),
(4, 'screwingTurret', 'filler', 0),
(5, 'missingBottle', 'filler', 1),
(6, 'other', 'filler', 0);

CREATE TABLE [users] (
  [id] int identity(1,1) primary key,
  [login] nvarchar(25) not null,
  [password] nvarchar(25) not null,
  [worksite_name] nvarchar(100) not null,
  [lastname] nvarchar(50) not null,
  [firstname] nvarchar(50) not null,
  [status] int not null
)

INSERT INTO [users] VALUES
('thotrb', '1234', 'Cernay', 'Trubert', 'Thomas', 0),
('pieria', '', 'Cernay', 'Riant', 'Pierre', 1),
('minpas', '', 'Medan', 'Minault', 'Pascal', 1),
('userMedan', '1234', 'Medan', 'lastname', 'first name', 0),
('leaderMedan', '1234', 'Medan', 'Junaidi', 'Albert', 1);


CREATE TABLE [worksite] (
  [id] int identity(1,1) primary key,
  [name] nvarchar(50) not null
);

INSERT INTO [worksite] VALUES
('Cernay'), ('Medan');

CREATE TABLE [teamInfo] (
  [id] int identity(1,1) primary key,
  [workingDebut] int not null,
  [workingEnd] int not null,
  [type] nvarchar(50) not null,
  [worksite_name] nvarchar(50) not null,
  [state] in not null default 0
);

INSERT INTO [teamInfo] VALUES
('10', '18', 'A', 'Cernay', 0),
('18', '02', 'B', 'Cernay', 0),
('8', '13', 'First', 'Medan', 0),
('13', '18', 'Second', 'Medan', 0),
('18', '00', 'Third', 'Medan', 0);

CREATE TABLE [ole_productionline] (
  [id] int primary key,
  [productionline_name] nvarchar(50) not null,
  [worksite_name] nvarchar(100) not null
);

INSERT INTO [ole_productionline] VALUES
(1, 'F52', 'Cernay'),
(5, 'F53', 'Cernay'),
(6, 'Medan1', 'Medan'),
(7, 'Medan2', 'Medan');
