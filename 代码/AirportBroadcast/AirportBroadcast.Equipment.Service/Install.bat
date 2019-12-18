"%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe" "%~dp0\AirportBroadcast.Equipment.Service.exe" INSTALL
Net Start AirportBroadcast.Equipment.Service
sc config AirportBroadcast.Equipment.Service start= auto

