USE [BaseDatosTiempoRea]
GO
SET IDENTITY_INSERT [dbo].[Sensor] ON 
GO
INSERT [dbo].[Sensor] ([id], [nombre], [tipo], [ubicacion]) VALUES (1, N'Sensor1', N'Sensor prueba', N'OficinaTecnologia')
GO
INSERT [dbo].[Sensor] ([id], [nombre], [tipo], [ubicacion]) VALUES (2, N'Sensor1', N'Sensor 2', N'Contabilidad')
GO
SET IDENTITY_INSERT [dbo].[Sensor] OFF
GO
