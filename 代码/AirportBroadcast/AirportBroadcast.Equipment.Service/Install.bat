%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe "%~dp0\AirportBroadcast.Equipment.Service.exe" 
sc config AirportBroadcast.Equipment.Service start= auto
sc config AirportBroadcast.Equipment.Service binPath= "%~dp0\AirportBroadcast.Equipment.Service.exe Service" 
Net Start AirportBroadcast.Equipment.Service 
