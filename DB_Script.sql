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
  [state] int not null default 0
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

CREATE TABLE [ole_machines] (
  [id] int identity(1,1) primary key,
  [name] nvarchar(50) not null,
  [operation] nvarchar(50) not null,
  [fabricant] nvarchar(100) not null,
  [modele] nvarchar(100) not null,
  [productionline_name] nvarchar(100) not null,
  [denomination_ordre] nvarchar(75) not null,
  [ordre] int not null,
  [rejection] int not null default 0
);

INSERT INTO [ole_machines] (name, operation, fabricant, modele, productionline_name, denomination_ordre, ordre, rejection) VALUES
('Depalettiseur', 'Manual', 'NA', 'NA', 'F52', 'M1', 1, 0),
('filler', 'Automated', 'SERAC', 'R1V1', 'F52', 'Filler/Caper', 2, 1),
('EtiqueteuseBouteille', 'Automated', 'PE Labeler', 'XXX', 'F52', 'M2', 3, 1),
('Encaisseuse', 'Manual', 'NA', 'NA', 'F52', 'M4', 5, 0),
('Palettiseur', 'Manual', 'NA', 'NA', 'F52', 'M8', 9, 0),
('PeseurDeCaisse', 'Automated', 'METTLER TOLEDO', 'MT-20', 'F52', 'M7', 8, 1),
('ScelleurDeCaisse', 'Automated', 'LANTECH', 'CS-300', 'F52', 'M6', 7, 0),
('FormeurDeCaisse', 'Semi-Auto', 'LANTECH', 'C-300', 'F52', 'M5', 6, 0),
('ApplicateurBouteille', 'Automated', 'SERAC', 'RX20', 'F52', 'M3', 4, 0);

CREATE TABLE [ole_formats] (
  [id] int identity(1,1) primary key,
  [format] nvarchar(15) not null,
  [shape] nvarchar(40) not null,
  [mat1] nvarchar(15) not null,
  [mat2] nvarchar(15) not null,
  [mat3] nvarchar(15) not null,
  [design_rate] int not null,
  [productionlineName] nvarchar(100) not null
);

INSERT INTO [ole_formats] (format, shape, mat1, mat2, mat3, design_rate, productionlineName) VALUES
('1L', 'round', 'HDPE', 'FHDPE', 'PE/PA', 25, 'F52'),
('2L', 'square', 'HDPE', 'PET', 'None', 20, 'F52'),
('3L', 'square', 'HDPE', 'PET', 'None', 16, 'F52'),
('5L', 'square', 'HPDE', 'PET', 'None', 15, 'F52'),
('10L', 'square', 'HDPE', 'PET', 'None', 8, 'F52');

CREATE TABLE [ole_speed_losses] (
  [id] int identity(1,1) primary key,
  [created_at] datetime not null default CURRENT_TIMESTAMP,
  [updated_at] datetime not null default '1900-01-01 00:00:00',
  [OLE] nvarchar(50) not null,
  [productionline] nvarchar(50) not null,
  [duration] int not null,
  [reason] nvarchar(150) not null,
  [comment] text default null
);


INSERT INTO [ole_speed_losses] (created_at, updated_at, OLE, productionline, duration, reason, comment) VALUES
('2021-11-15 15:34:23', '2021-09-15 15:07:04', 'titi', 'F53', 20, 'Reduced Rate At Filler', 'zizihz'),
('2021-11-15 15:34:21', '2021-09-20 11:16:07', 'lala', 'F53', 15, 'Reduce Rate At An Other Machine', 'Commentaire'),
('2021-11-14 15:34:19', '2021-10-02 13:50:50', 'titi', 'F52', 10, 'Filler Own Stoppage', 'lalalalala');

CREATE TABLE [ole_assignement_team_pos] (
  [id] int identity(1,1) primary key,
  [created_at] datetime not null default CURRENT_TIMESTAMP,
  [updated_at] datetime not null default '1900-01-01 00:00:00',
  [username] nvarchar(25) not null,
  [productionline] int not null,
  [po] nvarchar(50) not null,
  [shift] nvarchar(50) not null,
  [worksite] int not null
);

INSERT INTO [ole_assignement_team_pos] (created_at, updated_at, username, productionline, po, shift, worksite) VALUES
('2021-09-22 07:46:09', '1900-01-01 00:00:00', 'thotrb', 1, 'titi', 'A', 1),
('2021-09-22 07:46:09', '1900-01-01 00:00:00', 'thotrb', 5, 'toto', 'A', 1),
('2021-10-26 11:49:50', '2021-10-26 11:49:50', 'userMedan', 6, 'testPOMedan1', 'First', 2),
('2021-10-26 11:50:11', '2021-10-26 11:50:11', 'userMedan', 7, 'testPOMedan2', 'First', 2),
('2021-10-26 12:58:13', '2021-10-26 12:58:13', 'userMedan', 6, 'testPOMedan2', 'First', 2),
('2021-10-26 13:00:54', '2021-10-26 13:00:54', 'userMedan', 6, 'testPOMedan3', 'Second', 2),
('2021-10-26 13:00:57', '2021-10-26 13:00:57', 'userMedan', 7, 'testPOMedan4', 'Second', 2),
('2021-10-26 13:37:21', '2021-10-26 13:37:21', 'userMedan', 6, 'testPOMedan4', 'Third', 2),
('2021-10-26 13:37:25', '2021-10-26 13:37:25', 'userMedan', 7, 'testPOMedan2', 'Third', 2),
('2021-10-26 13:38:02', '2021-10-26 13:38:02', 'userMedan', 7, 'testPOMedan3', 'Second', 2),
('2021-10-26 13:38:51', '2021-10-26 13:38:51', 'userMedan', 7, 'testPOMedan1', 'First', 2),
('2021-10-28 12:31:44', '2021-10-28 12:31:44', 'userMedan', 7, 'testPOMedan5', 'Third', 2),
('2021-10-28 12:58:34', '2021-10-28 12:58:34', 'userMedan', 6, 'testPOMedan5', 'Third', 2),
('2021-10-28 12:58:37', '2021-10-28 12:58:37', 'userMedan', 7, 'testPOMedan1', 'Third', 2),
('2021-11-03 15:05:03', '2021-11-03 15:05:03', 'userMedan', 6, 'testMedanLigne1', 'Third', 2),
('2021-11-03 15:05:06', '2021-11-03 15:05:06', 'userMedan', 7, 'testMedanLigne2', 'Third', 2),
('2021-11-04 12:06:15', '2021-11-04 12:06:15', 'thotrb', 5, 'titi', 'A', 1),
('2021-11-20 09:48:47', '2021-11-20 09:48:47', 'userMedan', 6, 'nouveauPODETest', 'Third', 2),
('2021-11-20 09:48:49', '2021-11-20 09:48:49', 'userMedan', 7, 'PO1', 'Third', 2);

CREATE TABLE [ole_pos] (
  [id] int identity(1,1) primary key,
  [created_at] datetime not null default CURRENT_TIMESTAMP,
  [updated_at] datetime not null default '1900-01-01 00:00:00',
  [number] nvarchar(50) not null,
  [GMIDCode] nvarchar(50) not null,
  [productionline_name] nvarchar(50) not null,
  [state] int not null default 1,
  [totalOperatingTime] int not null default 0,
  [totalNetOperatingTime] int not null default 0,
  [Availability] float(2) not null default 0.00,
  [Performance] float(2) not null default 0.00,
  [Quality] float(2) not null default 0.00,
  [OLE] float(2) not null default 0.00,
  [qtyProduced] int not null default 0,
  [workingDuration] int not null default 0
);

INSERT INTO [ole_pos] (created_at, updated_at, number, GMIDCode, productionline_name, state, totalOperatingTime, totalNetOperatingTime, Availability, Performance, Quality, OLE, qtyProduced, workingDuration) VALUES
('2021-11-15 16:24:12', '1900-01-01 00:00:00', 'titi', '87507', 'F52', 0, 0, 0, 72.00, 64.00, 98.00, 78.00, 200, 160),
('2021-11-15 16:24:17', '1900-01-01 00:00:00', 'toto', '87507', 'F52', 0, 0, 0, 1.00, 0.67, 1.00, 0.67, 100, 20),
('2021-10-10 15:12:56', '2021-09-16 06:37:25', 't1', '335871', 'F53', 0, 0, 0, 0.00, 0.00, 0.00, 0.00, 0, 0),
('2021-10-10 15:12:57', '2021-09-16 06:37:25', 't2', '335871', 'F53', 0, 0, 0, 0.00, 0.00, 0.00, 0.00, 0, 0),
('2021-11-15 16:24:23', '2021-09-20 11:02:38', 'lala', '335871', 'F52', 1, 0, 0, 0.00, 0.00, 0.00, 0.00, 50, 50),
('2021-10-26 16:01:15', '2021-10-26 12:22:39', 'testPOMedan1', '11111', 'Medan1', 0, 0, 0, 0.50, 1.00, 1.00, 0.50, 180, 210),
('2021-11-20 09:58:45', '2021-10-26 12:22:39', 'testPOMedan2', '11111', 'Medan1', 0, 0, 0, 0.81, 1.00, -1.11, -0.90, 120, 240),
('2021-10-26 16:55:21', '2021-10-26 13:00:58', 'testPOMedan3', '11111', 'Medan1', 0, 0, 0, 0.68, 0.51, 1.00, 0.34, 618, 480),
('2021-10-27 14:58:42', '2021-10-28 12:44:22', 'testPOMedan4', '11113', 'Medan1', 0, 0, 0, 0.64, 0.34, 1.00, 0.22, 226, 480),
('2021-10-27 15:01:50', '2021-10-28 12:58:38', 'testPOMedan5', '11114', 'Medan1', 0, 0, 0, 0.73, 0.34, 1.00, 0.25, 448, 480),
('2021-11-03 18:26:03', '2021-11-03 15:05:07', 'testMedanLigne1', '138087', 'Medan1', 0, 0, 0, 0.86, 0.00, 1.00, 0.00, 0, 720),
('2021-11-20 10:31:25', '2021-11-03 15:05:07', 'testMedanLigne2', '225830', 'Medan2', 0, 0, 0, 1.00, 1.00, 9.23, 9.23, 120, 240);

CREATE TABLE [ole_planned_events] (
  [id] int identity(1,1) primary key,
  [created_at] datetime not null default CURRENT_TIMESTAMP,
  [updated_at] datetime not null default '1900-01-01 00:00:00',
  [OLE] nvarchar(50) not null,
  [productionline] nvarchar(50) not null,
  [reason] nvarchar(100) not null,
  [duration] int not null,
  [comment] text default '\' \'',
  [kind] int not null default 0
);

INSERT INTO [ole_planned_events] (created_at, updated_at, OLE, productionline, reason, duration, comment, kind) VALUES
('2021-11-04 13:23:47', '2021-09-02 10:38:32', 'titi', 'F52', 'meeting', 15, 'lala', 0),
('2021-11-04 13:23:45', '2021-09-04 14:23:15', 'titi', 'F53', 'break', 1, 'lunch', 0),
('2021-11-04 13:23:43', '2021-09-06 13:40:47', 'titi', 'F52', 'break', 30, 'Pause déjeuner', 0),
('2021-11-04 13:23:40', '2021-09-06 13:41:21', 'titi', 'F52', 'meeting', 45, 'lala', 0),
('2021-11-04 13:23:37', '2021-10-26 12:27:39', 'testPOMedan1', 'Medan1', 'break', 120, 'Break', 0),
('2021-11-04 13:23:34', '2021-10-26 13:01:12', 'testPOMedan3', 'Medan1', 'break', 30, 'Break', 0),
('2021-11-04 13:23:32', '2021-10-26 14:10:32', 'testPOMedan2', 'Medan1', 'break', 30, 'Breaks', 0),
('2021-11-04 13:23:28', '2021-10-26 14:10:46', 'testPOMedan2', 'Medan1', 'break', 30, 'Breaks', 0),
('2021-11-04 13:23:25', '2021-10-28 12:29:32', 'testPOMedan4', 'Medan1', 'break', 30, 'Breaks', 0),
('2021-11-04 13:23:22', '2021-10-28 12:59:34', 'testPOMedan5', 'Medan1', 'break', 30, 'Breaks', 0),
('2021-11-03 16:39:09', '2021-11-03 15:21:12', 'testMedanLigne1', 'Medan1', 'break', 45, 'Lunch', 0),
('2021-11-03 15:44:51', '2021-11-03 15:44:51', 'testMedanLigne2', 'Medan2', 'break', 50, 'BREAK MEDAN2', 0),
('2021-11-03 15:46:33', '2021-11-03 15:46:33', 'testMedanLigne2', 'Medan2', 'projectImplementation', 30, 'No comments', 0),
('2021-11-20 09:49:12', '2021-11-20 09:49:12', 'nouveauPODETest', 'Medan1', 'Pause', 30, 'LUNCH', 0);

CREATE TABLE [ole_unplanned_event_changing_clients] (
  [id] int identity(1,1) primary key,
  [created_at] datetime not null default CURRENT_TIMESTAMP,
  [updated_at] datetime not null default '1900-01-01 00:00:00',
  [OLE] nvarchar(50) not null,
  [productionline] nvarchar(50) not null,
  [predicted_duration] int not null,
  [total_duration] int not null,
  [lot_number] nvarchar(50) not null,
  [comment] text default '/',
  [type] nvarchar(255) not null default 'Changement de lot',
  [kind] int not null default 1
);

INSERT INTO [ole_unplanned_event_changing_clients] (created_at, updated_at, OLE, productionline, predicted_duration, total_duration, lot_number, comment, type, kind) VALUES
('2021-11-03 16:26:21', '2021-11-03 16:26:21', 'testMedanLigne1', 'Medan1', 5, 10, 'ABCD', NULL, 'Changement de lot', 1);

CREATE TABLE [ole_unplanned_event_changing_formats] (
  [id] int identity(1,1) primary key,
  [created_at] datetime not null default CURRENT_TIMESTAMP,
  [updated_at] datetime not null default '1900-01-01 00:00:00',
  [OLE] nvarchar(50) not null,
  [productionline] nvarchar(50) not null,
  [predicted_duration] int not null,
  [total_duration] int not null,
  [comment] text default '/',
  [type] nvarchar(255) not null default 'Changement de format',
  [kind] int not null default 1
);

INSERT INTO [ole_unplanned_event_changing_formats] (created_at, updated_at, OLE, productionline, predicted_duration, total_duration, comment, type, kind) VALUES
('2021-11-03 16:38:10', '2021-11-03 16:38:10', 'testMedanLigne1', 'Medan1', 60, 10, NULL, 'Changement de format', 1);

CREATE TABLE [ole_unplanned_event_cips] (
  [id] int identity(1,1) primary key,
  [created_at] datetime not null default CURRENT_TIMESTAMP,
  [updated_at] datetime not null default '1900-01-01 00:00:00',
  [OLE] nvarchar(50) not null,
  [previous_bulk] nvarchar(255) not null,
  [predicted_duration] int not null,
  [total_duration] int not null,
  [comment] text default '/',
  [productionline] nvarchar(50) not null,
  [type] nvarchar(255) not null default 'CIP',
  [kind] int not null default 1
);

INSERT INTO [ole_unplanned_event_cips] (created_at, updated_at, OLE, previous_bulk, predicted_duration, total_duration, comment, productionline, type, kind) VALUES
('2021-11-03 15:47:20', '2021-11-03 15:47:20', 'testMedanLigne1', 'prevBulk', 5, 35, 'CIP', 'Medan1', 'CIP', 1),
('2021-11-03 16:12:48', '2021-11-03 16:12:48', 'testMedanLigne1', 'PREV', 5, 10, NULL, 'Medan1', 'CIP', 1),
('2021-11-03 16:21:41', '2021-11-03 16:21:41', 'testMedanLigne1', 'PREVIOUSBULK', 5, 30, NULL, 'Medan1', 'CIP', 1),
('2021-11-17 14:58:47', '2021-11-17 14:58:47', 'titi', 'PreviousBULKTEST', 5, 30, 'J''ai été lent.', 'F52', 'CIP', 1);


CREATE TABLE [ole_unplanned_event_unplanned_downtimes] (
  [id] int identity(1,1) primary key,
  [created_at] datetime not null default CURRENT_TIMESTAMP,
  [updated_at] datetime not null default '1900-01-01 00:00:00',
  [OLE] nvarchar(50) not null,
  [productionline] nvarchar(255) not null,
  [implicated_machine] nvarchar(255) not null,
  [component] nvarchar(255) not null,
  [total_duration] int not null,
  [comment] text DEFAULT '/',
  [type] nvarchar(255) not null default 'unplannedDowntime',
  [kind] int not null default 1
);

INSERT INTO [ole_unplanned_event_unplanned_downtimes] (created_at, updated_at, OLE, productionline, implicated_machine, component, total_duration, comment, type, kind) VALUES
('2021-11-19 15:54:29', '2021-10-26 12:28:35', 'testPOMedan1', 'Medan1', 'Autres', 'Autres', 20, 'Labelling Box Manual', 'unplannedDowntime', 1),
('2021-11-19 15:54:35', '2021-10-26 12:29:04', 'testPOMedan1', 'Medan1', 'Autres', 'Autres', 25, 'Weighing error', 'unplannedDowntime', 1),
('2021-11-19 15:54:43', '2021-10-26 13:02:11', 'testPOMedan3', 'Medan1', 'Etiqueteuse bouteille', 'Saturation aval', 70, 'Label Stuck in the Servo 3 time', 'unplannedDowntime', 1),
('2021-11-19 15:54:49', '2021-10-26 13:02:48', 'testPOMedan3', 'Medan1', 'Etiqueteuse bouteille', 'Saturation aval', 75, 'Change roll label and setting 5 time', 'unplannedDowntime', 1),
('2021-11-19 15:54:51', '2021-10-26 14:11:23', 'testPOMedan2', 'Medan1', 'Etiqueteuse bouteille', 'Saturation aval', 20, 'Label broken', 'unplannedDowntime', 1),
('2021-11-19 15:54:54', '2021-10-26 14:11:46', 'testPOMedan2', 'Medan1', 'Etiqueteuse bouteille', 'Saturation aval', 15, 'Label Zigzag', 'unplannedDowntime', 1),
('2021-11-19 15:54:41', '2021-10-28 12:30:42', 'testPOMedan4', 'Medan1', 'Bol bouchon', 'Bol bouchon', 90, 'Sensor bottle reject Failure', 'unplannedDowntime', 1),
('2021-11-19 15:55:01', '2021-10-28 12:31:35', 'testPOMedan4', 'Medan1', 'Etiqueteuse bouteille', 'Saturation aval', 72, 'Change Roll Label 12 time', 'unplannedDowntime', 1),
('2021-11-19 15:55:08', '2021-10-28 13:00:26', 'testPOMedan5', 'Medan1', 'Tourelle de vissage', 'Tourelle de vissage', 95, 'Manual failing and check capping process', 'unplannedDowntime', 1),
('2021-11-19 15:54:38', '2021-10-28 13:01:11', 'testPOMedan5', 'Medan1', 'Autres', 'Autres', 25, 'Setting capper filling machine and tooling', 'unplannedDowntime', 1),
('2021-11-19 15:55:03', '2021-11-03 17:40:16', 'testMedanLigne1', 'Medan1', 'other', 'other', 120, NULL, 'unplannedDowntime', 1),
('2021-11-19 15:55:06', '2021-11-03 17:49:16', 'testMedanLigne1', 'Medan1', 'screwingTurret', 'screwingTurret', 120, NULL, 'unplannedDowntime', 1),
('2021-11-20 09:49:58', '2021-11-20 09:49:58', 'nouveauPODETest', 'Medan1', 'other', 'other', 95, 'Manual Filling and Check Capping Process', 'unplannedDowntime', 1),
('2021-11-20 09:50:24', '2021-11-20 09:50:24', 'nouveauPODETest', 'Medan1', 'other', 'other', 25, 'Setting Capper', 'unplannedDowntime', 1),
('2021-11-20 09:50:52', '2021-11-20 09:50:52', 'nouveauPODETest', 'Medan1', 'other', 'other', 20, 'Batchnumber', 'unplannedDowntime', 1),
('2021-11-20 09:51:26', '2021-11-20 09:51:26', 'nouveauPODETest', 'Medan1', 'other', 'other', 30, 'Slip Servo', 'unplannedDowntime', 1);

CREATE TABLE [ole_downtimeReason] (
  [id] int identity(1,1) primary key,
  [reason] nvarchar(50) not null,
  [downtimeType] nvarchar(50) not null
);

INSERT INTO [ole_downtimeReason] (reason, downtimeType) VALUES
('CIP', 'unplannedDowntime'),
('formatChanging', 'unplannedDowntime'),
('packNumberChanging', 'unplannedDowntime'),
('unplannedDowntime', 'unplannedDowntime'),
('break', 'plannedDowntime'),
('lunch', 'plannedDowntime'),
('emergency', 'plannedDowntime'),
('meeting', 'plannedDowntime'),
('noProductionPlanned', 'plannedDowntime'),
('maintenance', 'plannedDowntime'),
('projectImplementation', 'plannedDowntime');

CREATE TABLE [ole_products] (
  [id] int identity(1,1) primary key,
  [product] nvarchar(150) not null,
  [GMID] nvarchar(50) not null,
  [BULK] nvarchar(50) not null,
  [family] nvarchar(150) not null,
  [GIFAP] nvarchar(50) not null,
  [description] nvarchar(150) not null,
  [formulationType] nvarchar(150) not null,
  [size] nvarchar(20) not null,
  [idealRate] float(2) not null,
  [bottlesPerCase] int not null
);

INSERT INTO [ole_products] VALUES
('SUCCESSNATURALY BTLPE1X1GL USA', '87507', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '4', 20.00, 4),
('SUCCESSNATURALY BTLHPE4X1QT EN', '101223', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '1', 40.00, 12),
('CONSERVESC BTLHPE4X4X1QT USA', '124042', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '1', 40.00, 12),
('SUCCESS BTLHPE4X4X1L CAN', '138087', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '1', 40.00, 12),
('GF120NFBAIT BTLHPE4X1GL USA GF1111', '225830', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '4', 20.00, 4),
('GF120FFBAIT BTLHPE4X3.78L CAN GF1111', '275681', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '4', 20.00, 4),
('RADIANTSC BTLHPE4X1GL USA', '295528', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '4', 20.00, 4),
('RADIANTSC BTLHPE4X4X1QT USA', '295529', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '1', 40.00, 12),
('CONSERVESC BTLHPE4X4X1QT PRI', '335871', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Liquid', '1', 40.00, 12),
('GF120NFNATURALYTEFFB BTLFPE4X4L PAK', '336953', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '4', 20.00, 4),
('ENTRUST BTLHPE12X1L CAN', '11039846', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '1', 40.00, 12),
('NATURALUREFLYBAIT BTLHPE2X10L AUS', '11045177', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '10', 10.00, 2),
('INTREPIDEDGE BTLHPE4X1GL USA', '11045655', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '4', 20.00, 4),
('SUCCESSNATURALYTE BTLHPE12X1QT USA', '97018444', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '1', 40.00, 12),
('ENTRUSTSC BTLHPE12X1QT USA', '97018807', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '1', 40.00, 12),
('RADIANTSC BTLHPE12X1QT USA', '97018841', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '1', 40.00, 12),
('RADIANTSC BTLHPE12X1QT PRI', '97019730', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '1', 40.00, 12),
('ENTRUSTSC BTLHPE12X1QT PRI', '97019823', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '1', 40.00, 12),
('MOZKILL120SC BTLHPE4X5L NGA', '99056786', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '5', 17.00, 4),
('ENTRUSTSC BTLHPE4X4X1QT PRI', '99059298', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '1', 40.00, 12),
('RADIANTSC BTLHPE4X4X1QT PRI', '99059340', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '1', 40.00, 12),
('EXALT60SC BTLPET24x0.25L PHL', '11111', 'BT100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '0.25', 100.00, 24),
('EXALT BTLPET24x0.2 5L THA', '11112', 'EX100', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Solid', '0.25', 100.00, 24),
('TENANO360SC BTLPET50x0.1L IDN', '11113', 'BULKTENANO', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Sparkling', '0.1', 115.00, 50),
('ENDURE60SC BTLPET20X0.5L MYS', '11114', 'ENDURE', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Sparkling', '0.5', 80.00, 20),
('TENANO360SC BTLPET24x0.2L IDN', '22222', 'BULKTENANO', 'Herbicide', 'WG', 'Water Dispersible Granule', 'Sparkling', '0.2', 100.00, 24);

CREATE TABLE [ole_rejection_counters] (
  [id] int identity(1,1) primary key,
  [created_at] datetime not null default CURRENT_TIMESTAMP,
  [updated_at] datetime not null default '1900-01-01 00:00:00',
  [po] nvarchar(50) default null,
  [fillerCounter] int not null,
  [caperCounter] int not null,
  [labelerCounter] int not null,
  [weightBoxCounter] int not null,
  [qualityControlCounter] int not null,
  [fillerRejection] int not null,
  [caperRejection] int not null,
  [labelerRejection] int not null,
  [weightBoxRejection] int not null,
  [qualityControlRejection] int not null
);

INSERT INTO [ole_rejection_counters] (created_at, updated_at, po, fillerCounter, caperCounter, labelerCounter, weightBoxCounter, qualityControlCounter, fillerRejection, caperRejection, labelerRejection, weightBoxRejection, qualityControlRejection) VALUES
('2021-11-17 19:13:35', '1900-01-01 00:00:00', 'titi', 30, 1, 3, 4, 0, 20, 5, 15, 3, 0),
('2021-11-17 19:13:56', '1900-01-01 00:00:00', 'lala', 10, 0, 20, 1, 0, 3, 5, 15, 3, 0),
('2021-11-17 19:14:10', '1900-01-01 00:00:00', 'toto', 0, 1, 2, 1, 0, 4, 10, 5, 5, 0),
('2021-11-14 23:11:10', '1900-01-01 00:00:00', 'testPOMedan1', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
('2021-11-14 23:11:10', '1900-01-01 00:00:00', 'testPOMedan3', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
('2021-11-14 23:11:10', '1900-01-01 00:00:00', 'testPOMedan4', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
('2021-11-14 23:11:10', '1900-01-01 00:00:00', 'testPOMedan5', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
('2021-11-20 09:35:02', '2021-11-20 09:35:02', 'testMedanLigne2', 0, 0, 0, 288, 0, 0, 0, 0, 24, 0);
