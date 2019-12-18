%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe "%~dp0\RoadEventService.Service.exe" 
sc config RoadEventService.Service start= auto
sc config RoadEventService.Service binPath= "%~dp0\RoadEventService.Service.exe Service" 
Net Start RoadEventService.Service 
